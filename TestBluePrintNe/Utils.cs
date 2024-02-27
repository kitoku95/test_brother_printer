using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Brother.Ptouch.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestBluePrintNe
{
    public  class Utils
    {
        public static string[] TestLabelName(ref Dictionary<string, int> printerLabelName)
        {
            printerLabelName = new Dictionary<string, int>
            {
                { "PT.FleW21h45", LabelInfo.PT.FleW21h45.Ordinal() },
                { "PT.Unsupport", LabelInfo.PT.Unsupport.Ordinal() },
                { "PT.Hs3W11", LabelInfo.PT.Hs3W11.Ordinal() },
                { "PT.Hs3W21", LabelInfo.PT.Hs3W21.Ordinal() },
                { "PT.Hs3W31", LabelInfo.PT.Hs3W31.Ordinal() },
                { "PT.Hs3W5", LabelInfo.PT.Hs3W5.Ordinal() },
                { "PT.Hs3W9", LabelInfo.PT.Hs3W9.Ordinal() },
                { "PT.HsW12", LabelInfo.PT.HsW12.Ordinal() },
                { "PT.HsW18", LabelInfo.PT.HsW18.Ordinal() },
                { "PT.HsW24", LabelInfo.PT.HsW24.Ordinal() },
                { "PT.HsW6", LabelInfo.PT.HsW6.Ordinal() },
                { "PT.HsW9", LabelInfo.PT.HsW9.Ordinal() },
                { "PT.R25", LabelInfo.PT.R25.Ordinal() },
                { "PT.R30", LabelInfo.PT.R30.Ordinal() },
                { "PT.R35", LabelInfo.PT.R35.Ordinal() },
                { "PT.R40", LabelInfo.PT.R40.Ordinal() },
                { "PT.R50", LabelInfo.PT.R50.Ordinal() },
                { "PT.R60", LabelInfo.PT.R60.Ordinal() },
                { "PT.W12", LabelInfo.PT.W12.Ordinal() },
                { "PT.W18", LabelInfo.PT.W18.Ordinal() },
                { "PT.W24", LabelInfo.PT.W24.Ordinal() },
                { "PT.W35", LabelInfo.PT.W35.Ordinal() },
                { "PT.W36", LabelInfo.PT.W36.Ordinal() },
                { "PT.W6", LabelInfo.PT.W6.Ordinal() },
                { "PT.W9", LabelInfo.PT.W9.Ordinal() },
                { "PT3.Unsupport", LabelInfo.PT3.Unsupport.Ordinal() },
                { "PT3.W6", LabelInfo.PT3.W6.Ordinal() },
                { "PT3.W9", LabelInfo.PT3.W9.Ordinal() },
                { "PT3.W12", LabelInfo.PT3.W12.Ordinal() },
                { "PT3.W35", LabelInfo.PT3.W35.Ordinal() },
                { "QL1100.Unsupport", LabelInfo.QL1100.Unsupport.Ordinal() },
                { "QL1100.W102", LabelInfo.QL1100.W102.Ordinal() },
                { "QL1100.W102h152", LabelInfo.QL1100.W102h152.Ordinal() },
                { "QL1100.W102h51", LabelInfo.QL1100.W102h51.Ordinal() },
                { "QL1100.W103", LabelInfo.QL1100.W103.Ordinal() },
                { "QL1100.W103h164", LabelInfo.QL1100.W103h164.Ordinal() },
                { "QL1100.W12", LabelInfo.QL1100.W12.Ordinal() },
                { "QL1100.W12dia", LabelInfo.QL1100.W12dia.Ordinal() },
                { "QL1100.W17h54", LabelInfo.QL1100.W17h54.Ordinal() },
                { "QL1100.W17h87", LabelInfo.QL1100.W17h87.Ordinal() },
                { "QL1100.W23h23", LabelInfo.QL1100.W23h23.Ordinal() },
                { "QL1100.W24dia", LabelInfo.QL1100.W24dia.Ordinal() },
                { "QL1100.W29", LabelInfo.QL1100.W29.Ordinal() },
                { "QL1100.W29h42", LabelInfo.QL1100.W29h42.Ordinal() },
                { "QL1100.W29h90", LabelInfo.QL1100.W29h90.Ordinal() },
                { "QL1100.W38", LabelInfo.QL1100.W38.Ordinal() },
                { "QL1100.W38h90", LabelInfo.QL1100.W38h90.Ordinal() },
                { "QL1100.W50", LabelInfo.QL1100.W50.Ordinal() },
                { "QL1100.W52h29", LabelInfo.QL1100.W52h29.Ordinal() },
                { "QL1100.W54", LabelInfo.QL1100.W54.Ordinal() },
                { "QL1100.W58dia", LabelInfo.QL1100.W58dia.Ordinal() },
                { "QL1100.W60h86", LabelInfo.QL1100.W60h86.Ordinal() },
                { "QL1100.W62", LabelInfo.QL1100.W62.Ordinal() },
                { "QL1100.W62h29", LabelInfo.QL1100.W62h29.Ordinal() },
                { "QL1100.W62h100", LabelInfo.QL1100.W62h100.Ordinal() },
                { "QL1115.Unsupport", LabelInfo.QL1115.Unsupport.Ordinal() },
                { "QL1115.DtW102", LabelInfo.QL1115.DtW102.Ordinal() },
                { "QL1115.DtW102h152", LabelInfo.QL1115.DtW102h152.Ordinal() },
                { "QL1115.DtW102h51", LabelInfo.QL1115.DtW102h51.Ordinal() },
                { "QL1115.DtW90", LabelInfo.QL1115.DtW90.Ordinal() },
                { "QL1115.W102", LabelInfo.QL1115.W102.Ordinal() },
                { "QL1115.W102h152", LabelInfo.QL1115.W102h152.Ordinal() },
                { "QL1115.W102h51", LabelInfo.QL1115.W102h51.Ordinal() },
                { "QL1115.W103", LabelInfo.QL1115.W103.Ordinal() },
                { "QL1115.W103164", LabelInfo.QL1115.W103h164.Ordinal() },
                { "QL1115.W12", LabelInfo.QL1115.W12.Ordinal() },
                { "QL1115.W12dia", LabelInfo.QL1115.W12dia.Ordinal() },
                { "QL1115.W17h54", LabelInfo.QL1115.W17h54.Ordinal() },
                { "QL1115.W17h87", LabelInfo.QL1115.W17h87.Ordinal() },
                { "QL1115.W23h23", LabelInfo.QL1115.W23h23.Ordinal() },
                { "QL1115.W24dia", LabelInfo.QL1115.W24dia.Ordinal() },
                { "QL1115.W29", LabelInfo.QL1115.W29.Ordinal() },
                { "QL1115.W29h42", LabelInfo.QL1115.W29h42.Ordinal() },
                { "QL1115.W29h90", LabelInfo.QL1115.W29h90.Ordinal() },
                { "QL1115.W38", LabelInfo.QL1115.W38.Ordinal() },
                { "QL1115.W38h90", LabelInfo.QL1115.W38h90.Ordinal() },
                { "QL1115.W39h48", LabelInfo.QL1115.W39h48.Ordinal() },
                { "QL1115.W50", LabelInfo.QL1115.W50.Ordinal() },
                { "QL1115.W52h29", LabelInfo.QL1115.W52h29.Ordinal() },
                { "QL1115.W54", LabelInfo.QL1115.W54.Ordinal() },
                { "QL1115.W58dia", LabelInfo.QL1115.W58dia.Ordinal() },
                { "QL1115.W60h86", LabelInfo.QL1115.W60h86.Ordinal() },
                { "QL1115.W62", LabelInfo.QL1115.W62.Ordinal() },
                { "QL1115.W62h100", LabelInfo.QL1115.W62h100.Ordinal() },
                { "QL1115.W62h29", LabelInfo.QL1115.W62h29.Ordinal() },
                { "QL700.Unsupport", LabelInfo.QL700.Unsupport.Ordinal() },
                { "QL700.W12", LabelInfo.QL700.W12.Ordinal() },
                { "QL700.W12dia", LabelInfo.QL700.W12dia.Ordinal() },
                { "QL700.W17h54", LabelInfo.QL700.W17h54.Ordinal() },
                { "QL700.W17h87", LabelInfo.QL700.W17h87.Ordinal() },
                { "QL700.W23h23", LabelInfo.QL700.W23h23.Ordinal() },
                { "QL700.W24dia", LabelInfo.QL700.W24dia.Ordinal() },
                { "QL700.W29", LabelInfo.QL700.W29.Ordinal() },
                { "QL700.W29h42", LabelInfo.QL700.W29h42.Ordinal() },
                { "QL700.W29h90", LabelInfo.QL700.W29h90.Ordinal() },
                { "QL700.W38", LabelInfo.QL700.W38.Ordinal() },
                { "QL700.W38h90", LabelInfo.QL700.W38h90.Ordinal() },
                { "QL700.W39h48", LabelInfo.QL700.W39h48.Ordinal() },
                { "QL700.W50", LabelInfo.QL700.W50.Ordinal() },
                { "QL700.W52h29", LabelInfo.QL700.W52h29.Ordinal() },
                { "QL700.W54", LabelInfo.QL700.W54.Ordinal() },
                { "QL700.W54h29", LabelInfo.QL700.W54h29.Ordinal() },
                { "QL700.W58dia", LabelInfo.QL700.W58dia.Ordinal() },
                { "QL700.W60h86", LabelInfo.QL700.W60h86.Ordinal() },
                { "QL700.W62", LabelInfo.QL700.W62.Ordinal() },
                { "QL700.W62h29", LabelInfo.QL700.W62h29.Ordinal() },
                { "QL700.W62h60", LabelInfo.QL700.W62h60.Ordinal() },
                { "QL700.W62h75", LabelInfo.QL700.W62h75.Ordinal() },
                { "QL700.W62h100", LabelInfo.QL700.W62h100.Ordinal() },
                { "QL700.W62rb", LabelInfo.QL700.W62rb.Ordinal() }
            };
            string[] items = new string[printerLabelName.Count];

            int idx = 0;
            foreach (var key in printerLabelName.Keys)
            {
                items[idx] = key;
                idx++;
            }
            return items;
        }
    }
}
