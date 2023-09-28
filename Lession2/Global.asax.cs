using Autofac;
using Autofac.Integration.Mvc;
using DB.DbAccess;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Lession2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterAutofacApi();
        }

        private void RegisterAutofacApi()
        {
            var builder = new ContainerBuilder();

            //builder.RegisterType<ProductService>().As<ICommon<Product>>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<ICommon<Product>>();
            builder.RegisterType<OrderService>().As<ICommon<Order>>();
            builder.RegisterType<Order_DetailService>().As<ICommon<Order_Detail>>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}
