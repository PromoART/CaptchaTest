using System;
using Castle.Windsor;


namespace Captcha.Container
{
    public class DependencyRegistrator : IDisposable
    {

        public DependencyRegistrator()
        {
            Container = new WindsorContainer();
            Container.Install(new WindsorInstaller());

        }
        public IWindsorContainer Container { get; }

        public void Dispose()
        {
            Container?.Dispose();
        }
    }
}