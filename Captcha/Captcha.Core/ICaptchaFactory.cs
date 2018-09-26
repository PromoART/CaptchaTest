namespace Captcha.Core
{
    public interface ICaptchaFactory
    {
        CaptchaInfo CreateCaptcha(int charCount);
    }
}
