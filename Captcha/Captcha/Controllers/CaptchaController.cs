using System;
using System.Web.Mvc;
using Captcha.Core;

namespace Captcha.Controllers
{
    public class CaptchaController : Controller
    {
        private readonly ICaptchaFactory _captchaFactory;
        private readonly ICaptchaValidator _validator;

        public CaptchaController(ICaptchaFactory captchaFactory, ICaptchaValidator validator)
        {
            _captchaFactory = captchaFactory;
            _validator = validator;
        }
        // GET: Captcha
        public ActionResult Index()
        {
            var captcha = _captchaFactory.CreateCaptcha(5);
            ViewBag.Text = captcha.Text;
            ViewBag.Image = Convert.ToBase64String(captcha.Image.ToArray());
            return View();
        }
    }
}