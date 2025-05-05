using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ZXing;
using QRCoder;

namespace IMPALLibrary.Common
{
    public class GenerateQRcode
    {
        public byte[] GenerateInvQRCodeB2C(string QCText)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(QCText, QRCodeGenerator.ECCLevel.L);
            var qRCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qRCode.GetGraphic(1);
            return qrCodeBytes;
        }

        public byte[] GenerateInvQRCode(string QCText)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(QCText, QRCodeGenerator.ECCLevel.L);
            var qRCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qRCode.GetGraphic(1);
            return qrCodeBytes;
        }
    }

    public class Tuple2<T1, T2>
    {
        public T1 Item1 { get; private set; }
        public T2 Item2 { get; private set; }
        internal Tuple2(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }

    public static class Tuple2
    {
        public static Tuple2<T1, T2> New<T1, T2>(T1 item1, T2 item2)
        {
            var tuple = new Tuple2<T1, T2>(item1, item2);
            return tuple;
        }
    }

    public class Tuple1<T1, T2, T3, T4>
    {
        public T1 Item1 { get; private set; }
        public T2 Item2 { get; private set; }
        public T3 Item3 { get; private set; }
        public T4 Item4 { get; private set; }
        internal Tuple1(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }
    }

    public static class Tuple1
    {
        public static Tuple1<T1, T2, T3, T4> New<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            var tuple = new Tuple1<T1, T2, T3, T4>(item1, item2, item3, item4);
            return tuple;
        }
    }
}
