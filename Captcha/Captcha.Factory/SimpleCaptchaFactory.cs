using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Captcha.Core;

namespace Captcha.Factory
{
    public class SimpleCaptchaFactory : ICaptchaFactory
    {
        public CaptchaInfo CreateCaptcha(int charCount)
        {
            var range = new { Min = 4, Max = 10 };
            var count = charCount < range.Max && charCount >= range.Min ? charCount : range.Min;

            var text = GenerateCaptchaText(count);
            var imageStream = GenerateCaptchaImageStream(text);
            return new CaptchaInfo { Text = text, Image = imageStream };
        }

        private static string GenerateCaptchaText(int charCount)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var text = new StringBuilder();
            var random = new Random();

            while (charCount > 0)
            {
                text.Append(chars[random.Next(0, chars.Length)]);
                charCount--;
            }

            return text.ToString();
        }

        private static MemoryStream GenerateCaptchaImageStream(string text)
        {
            SizeF textSize;
            var font = new Font(FontFamily.GenericMonospace, 20, FontStyle.Regular);
            var memoryStream = new MemoryStream();

            using (var image = new Bitmap(1, 1))
            {
                using (var drawing = Graphics.FromImage(image))
                {
                    textSize = drawing.MeasureString(text, font);
                }
            }

            using (var image = new Bitmap((int)textSize.Width, (int)textSize.Height))
            {
                using (var drawing = Graphics.FromImage(image))
                {
                    drawing.Clear(Color.Azure);

                    using (var brush = new SolidBrush(Color.Black))
                    {
                        drawing.DrawString(text, font, brush, 0, 0);
                    }

                    drawing.Save();
                }

                image.Save(memoryStream, ImageFormat.Png);
            }

            return memoryStream;
        }
    }
}
