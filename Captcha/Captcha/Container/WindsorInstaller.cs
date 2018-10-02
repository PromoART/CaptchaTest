using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Captcha.Core;
using Captcha.Factory;
using CaptchaValidator;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Captcha.Container
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICaptchaFactory>().ImplementedBy<SimpleCaptchaFactory>().LifeStyle
                .Singleton);
            container.Register(Component.For<ICaptchaValidator>().ImplementedBy<SimpleCaptchaValidator>().LifeStyle
                .Singleton);

            var controllers = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(Controller))
                .ToList();

            foreach (var controller in controllers)
            {
                container.Register(Component.For(controller).LifestylePerWebRequest());
            }
        }
    }
}