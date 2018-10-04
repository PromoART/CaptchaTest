namespace Captcha.Core
{
    /// <summary>
    /// Intarface of captcha validator
    /// </summary>
    public interface ICaptchaValidator
    {
        /// <summary>
        /// Validate input string is an a captcha string
        /// </summary>
        /// <param name="inputString">input string</param>
        /// <param name="comparisonString"></param>
        /// <returns>Input string matches captcha string(true-match, false-don't match) </returns>
        bool Validate(string inputString, string comparisonString);
    }
}
