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
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            GlobalConfiguration.Configure(WebApiConfig.Register);

            builder.RegisterType<Student>().As<IStudent>();
            builder.RegisterType<Grade>().As<IGrade>();
            builder.RegisterType<Repository>().As<IRepository>();
            builder.RegisterType<PDService>().As<IService>();

            builder.RegisterModule<ServiceDIModule>();
            builder.RegisterModule<RepoDIModule>();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
