using Captcha.Core;

namespace CaptchaValidator
{
    public class SimpleCaptchaValidator : ICaptchaValidator
    {
        public bool Validate(string inputString)
        {
            return true;
        }
    }
}
