using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using Model.Common.Student;
using Model.Student;
using Model.Grade;
using Model.Common.Grade;
using WebRepository.Common;
using WebRepository;
using Service.Common;
using DService;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //public IContainer Container { get; set; }

        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule(new RepoDIModule());
            builder.RegisterModule(new ServiceDIModule());

            var Container = builder.Build();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}
