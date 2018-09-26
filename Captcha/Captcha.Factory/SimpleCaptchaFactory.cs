using Captcha.Core;

namespace Captcha.Factory
{
    public class SimpleCaptchaFactory : ICaptchaFactory
    {
        public CaptchaInfo CreateCaptcha(int charCount)
        {
            return new CaptchaInfo();
        }
    }
}
