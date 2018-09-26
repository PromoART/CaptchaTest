using System.Drawing;

namespace Captcha.Core
{
    public struct CaptchaInfo
    {
        public string Text { get; set; }

        public Bitmap Image { get; set; }
    }
}
