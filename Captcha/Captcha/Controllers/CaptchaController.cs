using System;
using System.Web.Mvc;
using Captcha.Core;
using Captcha.Models;

namespace Captcha.Controllers
{
    public class CaptchaController : Controller
    {
        private readonly ICaptchaValidator _validator;
        private readonly CaptchaInfo _captcha;

        public CaptchaController(ICaptchaFactory captchaFactory, ICaptchaValidator validator)
        {
            _captcha = captchaFactory.CreateCaptcha(5);
            _validator = validator;
        }
        // GET: Captcha
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Image = Convert.ToBase64String(_captcha.Image.ToArray());

            return View();
        }

        [HttpPost]
        public ActionResult Validate(CaptchaText inputResult)
        {
            var result = _validator.Validate(inputResult.Input, inputResult.ToCompare);

            ViewBag.Result = result.ToString();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _captcha.Image?.Dispose();

            base.Dispose(disposing);
        }
    }
}