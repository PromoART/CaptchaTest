using System.Drawing;
using System.IO;

namespace Captcha.Core
{
    public struct CaptchaInfo
    {
        public string Text { get; set; }

        public MemoryStream Image { get; set; }
    }
}
