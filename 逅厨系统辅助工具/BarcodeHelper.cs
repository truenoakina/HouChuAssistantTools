using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ZXing;
using ZXing.QrCode;

namespace 逅厨系统辅助工具
{
    class BarcodeHelper
    {
        public static Bitmap GenerateBarcode(string barcodeText, BarcodeFormat format)
        {
            var writer = new BarcodeWriter
            {
                Format = format,
                Options = new QrCodeEncodingOptions
                {
                    Height = 60,
                    Width = 160,
                    Margin = 5, // 设置边距大小
                    PureBarcode = false
                }
            };
            return writer.Write(barcodeText);
        }



    }
}
