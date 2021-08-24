using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using WebRepository.Common;

namespace WebRepository
{
    public class RepoDIModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Repository>().As<IRepository>();
        }
    }
}
