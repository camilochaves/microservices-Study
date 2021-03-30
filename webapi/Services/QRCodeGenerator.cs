using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SharedTools
{
    public static class QrCodeGenerator
    {
        private static Bitmap GenerateImage(string msg)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(msg, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(50);
            return qrCodeImage;
        }

        private static byte[] ImageToByte(Image img)
        {
            using var stream = new System.IO.MemoryStream();
            img.Save(stream, ImageFormat.Png);
            return stream.ToArray();
        }

        public static byte[] GenerateQRCodeAsByteArray(string msg)
        {
            var image = GenerateImage(msg);
            return ImageToByte(image);
        }
    }
}
