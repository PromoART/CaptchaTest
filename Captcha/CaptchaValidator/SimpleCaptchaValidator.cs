using Captcha.Core;

namespace CaptchaValidator
{
    public class SimpleCaptchaValidator : ICaptchaValidator
    {
        public bool Validate(string inputString, string comparisonString)
        {
            return inputString.ToUpper().Equals(comparisonString.ToUpper());
        }
    }
}
