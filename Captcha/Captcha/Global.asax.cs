using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Captcha.Container;


namespace Captcha
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly DependencyRegistrator _registrator;

        public MvcApplication()
        {
            _registrator = new DependencyRegistrator();
           
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
            DependencyResolver.SetResolver(new WindsorDependencyResolver(_registrator.Container));
        }

        public override void Dispose()
        {
            _registrator.Dispose();

            base.Dispose();
        }
    }
}
