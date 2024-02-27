using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Com.Brother.Ptouch.Sdk;
using Com.Brother.Sdk.Lmprinter;
using Com.Brother.Sdk.Lmprinter.Setting;
using Java.Lang;
using System;
using System.Drawing;
using System.Net.NetworkInformation;
using ZXing.QrCode;
using ZXing;
using Android.Graphics;
using System.IO;
using Android.Widget;
using ZXing.Common;
using Java.Util;
using System.Collections.Generic;

namespace TestBluePrintNe
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText txtIP, txtMsg;
        Button btnPrint;

        Spinner spLabelName;
        private Dictionary<string, int> printerLabelName;
        private string[] items;

        private int labelNameIndex = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            txtIP = FindViewById<EditText>(Resource.Id.txtIP);
            txtMsg = FindViewById<EditText>(Resource.Id.txtMsg);
            spLabelName = FindViewById<Spinner>(Resource.Id.spLabelName);

            items = Utils.TestLabelName(ref printerLabelName);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, items);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spLabelName.Adapter = adapter;
            

            txtIP.Text = "192.168.11.36";

            btnPrint = FindViewById<Button>(Resource.Id.btnPrint);

            btnPrint.Click += BtnPrint_Click;
            spLabelName.ItemSelected += SpLabelName_ItemSelected;
        }

        private void SpLabelName_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            labelNameIndex = printerLabelName[(sender as Spinner).SelectedItem.ToString()];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIP.Text) && !string.IsNullOrEmpty(txtMsg.Text))
            {
                PrintImage(txtIP.Text, txtMsg.Text);
            }
        }

        #region GenerateQR
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        Android.Graphics.Bitmap GenerateQR(string msg, int width = 102, int height = 60)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions()
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = height
            };

            BarcodeWriter<Android.Graphics.Bitmap> writer = new BarcodeWriter<Android.Graphics.Bitmap>()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = options,
                Renderer = new BarcodeRender()
            };  
            return writer.Write(msg);
        }

        System.Drawing.Bitmap Convert(Android.Graphics.Bitmap bitmap)
        {
            using var stream = new MemoryStream();
            bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 0, stream);
            stream.Seek(0, SeekOrigin.Begin);
            return new System.Drawing.Bitmap(stream);
        }

        #endregion

        #region Print Not Work
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="msg"></param>
        void PrintImage1(string ip, string msg)
        {
            new System.Threading.Thread(() =>
            {
                Channel channel = Channel.NewWifiChannel(ip);

                Android.Graphics.Bitmap bitmap = GenerateQR(msg);

                PrinterDriverGenerateResult result = PrinterDriverGenerator.OpenChannel(channel);
                if (result.Error.Code != OpenChannelError.ErrorCode.NoError)
                {
                    Console.WriteLine("", "Error - Open Channel: " + result.Error.Code);
                    return;
                }

                string dir = Application.ApplicationContext.GetExternalFilesDir(null).AbsolutePath;
                string path = System.IO.Path.Combine(dir, "douma");

                PrinterDriver printerDriver = result.Driver;
                QLPrintSettings printSettings = new QLPrintSettings(PrinterModel.Td4550dnwb);

                printSettings.SetLabelSize(QLPrintSettings.LabelSize.DTRollW102H51);
                printSettings.AutoCut = true;
                printSettings.NumCopies = 1;
                printSettings.WorkPath = dir;

                try
                {

                    PrintError printError = printerDriver.PrintImage(bitmap, printSettings);
                    if (printError.Code != PrintError.ErrorCode.NoError)
                    {
                        System.Console.WriteLine("", "Error - Print Image: " + printError.Code);
                    }
                    else
                    {
                        System.Console.WriteLine("", "Success - Print Image");
                    }
                }
                finally
                {
                    printerDriver.CloseChannel();
                }

            }).Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="msg"></param>
        void PrintImage3(string ip, string msg)
        {
            new System.Threading.Thread(() =>
            {
                Channel channel = Channel.NewWifiChannel(ip);

                Android.Graphics.Bitmap bitmap = GenerateQR(msg);

                PrinterDriverGenerateResult result = PrinterDriverGenerator.OpenChannel(channel);
                if (result.Error.Code != OpenChannelError.ErrorCode.NoError)
                {
                    Console.WriteLine("", "Error - Open Channel: " + result.Error.Code);
                    return;
                }

                string dir = Application.ApplicationContext.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments).AbsolutePath;
                string path = System.IO.Path.Combine(dir, "TestBrother");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                PrinterDriver printerDriver = result.Driver;
                QLPrintSettings printSettings = new QLPrintSettings(PrinterModel.Td4550dnwb);

                printSettings.SetLabelSize(QLPrintSettings.LabelSize.DieCutW102H51);
                printSettings.AutoCut = true;
                printSettings.NumCopies = 1;
                printSettings.WorkPath = path;

                try
                {
                    using (FileStream stream = new FileStream(System.IO.Path.Combine(path, "test.png"), System.IO.FileMode.Create))
                    {
                        bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, stream);
                    }
                    //Convert(bitmap).Save(System.IO.Path.Combine(path, "test.png"));


                    //PrintError printError = printerDriver.PrintImage(bitmap, printSettings);
                    PrintError printError = printerDriver.PrintImage(System.IO.Path.Combine(path, "test.png"), printSettings);
                    if (printError.Code != PrintError.ErrorCode.NoError)
                    {
                        System.Console.WriteLine("", "Error - Print Image: " + printError.Code);

                        RunOnUiThread(() =>
                        {
                            Toast.MakeText(this, "ERROR - " + printError.Code, ToastLength.Short).Show();
                        });
                    }
                    else
                    {
                        System.Console.WriteLine("", "Success - Print Image");
                    }
                }
                catch (System.Exception ex)
                {
                    RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, "ERROR - " + ex.Message, ToastLength.Short).Show();
                    });
                }
                finally
                {
                    printerDriver.CloseChannel();
                }
            }).Start();
        }
        #endregion

        #region Print Image

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="msg"></param>
        void PrintImage(string ip, string msg)
        {

            Android.Graphics.Bitmap bitmap = GenerateQR(msg, 400, 400);

            CustomPaperInfo paperInfo = CustomPaperInfo.NewCustomDieCutPaper(PrinterInfo.Model.Td4550dnwb, Unit.Mm, 105, 60, 0, 0, 0, 0, 3);

            string dir = Application.ApplicationContext.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments).AbsolutePath;
            string path = System.IO.Path.Combine(dir, "TestBrother");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            // Connect, then print
            new Thread(() =>
            {
                // Specify printer
                Printer printer = new Printer();

                PrinterInfo printerInfo = printer.PrinterInfo;

                printerInfo.IpAddress = ip;
                printerInfo.EsPort = PrinterInfo.Port.Net;
                printerInfo.PrinterModel = PrinterInfo.Model.Td4550dnwb;
                printerInfo.EsPrintMode = PrinterInfo.PrintMode.FitToPage;
                printerInfo.EsOrientation = PrinterInfo.Orientation.Portrait;
                printerInfo.EsPaperSize = PrinterInfo.PaperSize.Custom;

                printerInfo.CustomPaperInfo = paperInfo;
                printerInfo.CustomFeed = 1;

                printerInfo.NumberOfCopies = 1;
                printerInfo.WorkPath = path;
                printerInfo.IsAutoCut = true;
                printerInfo.IsCutAtEnd = false;

                printer.SetPrinterInfo(printerInfo);

                if (printer.StartCommunication())
                {
                    try
                    {

                        using (FileStream stream = new FileStream(System.IO.Path.Combine(path, "test.png"), System.IO.FileMode.Create))
                        {
                            bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, stream);
                        }


                        System.Console.WriteLine("Tag: ", "Connection made.");
                        Com.Brother.Ptouch.Sdk.PrinterStatus result = printer.PrintFile(System.IO.Path.Combine(path, "test.png"));
                        System.Console.WriteLine("Tag: ", "Printing!");
                        if (result.ErrorCode != PrinterInfo.ErrorCode.ErrorNone)
                        {
                            System.Console.WriteLine("TAG", "ERROR - " + result.ErrorCode);
                            RunOnUiThread(() =>
                            {
                                Toast.MakeText(this, "ERROR - " + result.ErrorCode, ToastLength.Short).Show();
                            });
                        }
                    }
                    finally
                    {
                        printer.EndCommunication();
                    }
                }
                else
                {
                    System.Console.WriteLine("Tag: ", "Cannot make a connection.");
                }
            }).Start();
        }
        #endregion
    }

    #region Barcode Render

    /// <summary>
    /// 
    /// </summary>
    class BarcodeRender : Java.Lang.Object, ZXing.Rendering.IBarcodeRenderer<Android.Graphics.Bitmap>
    {
        public Android.Graphics.Bitmap Render(ZXing.Common.BitMatrix matrix, ZXing.BarcodeFormat format, string content)
        {
            int width = matrix.Width;
            int height = matrix.Height;

            Android.Graphics.Bitmap bitmap = Android.Graphics.Bitmap.CreateBitmap(width, height, Android.Graphics.Bitmap.Config.Rgb565);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (matrix[x, y])
                    {
                        bitmap.SetPixel(x, y, Android.Graphics.Color.Black);
                    }
                    else
                    {
                        bitmap.SetPixel(x, y, Android.Graphics.Color.White);
                    }
                }
            }

            return bitmap;
        }

        public Android.Graphics.Bitmap Render(BitMatrix matrix, BarcodeFormat format, string content, EncodingOptions options)
        {
            int width = matrix.Width;
            int height = matrix.Height;

            Android.Graphics.Bitmap bitmap = Android.Graphics.Bitmap.CreateBitmap(width, height, Android.Graphics.Bitmap.Config.Rgb565);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (matrix[x, y])
                    {
                        bitmap.SetPixel(x, y, Android.Graphics.Color.Black);
                    }
                    else
                    {
                        bitmap.SetPixel(x, y, Android.Graphics.Color.White);
                    }
                }
            }

            return bitmap;
        }
    }
    #endregion
}
