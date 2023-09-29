using Autofac;
using Autofac.Integration.Mvc;
using DB.DbAccess;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Lession2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        [System.Obsolete]
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterAutofacApi();
        }

        [System.Obsolete]
        private void RegisterAutofacApi()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ProductService>().As<ICommon<Product>>().InstancePerRequest();
            builder.RegisterType<OrderService>().As<ICommon<Order>>().InstancePerRequest();
            builder.RegisterType<OrderDetailService>().As<ICommon<OrderDetail>>();

            builder.RegisterAssemblyTypes(typeof(ProductService).Assembly)
                .Where(p => p.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(typeof(OrderService).Assembly)
                .Where(p => p.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(typeof(OrderDetailService).Assembly)
                .Where(p => p.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerHttpRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}
