using Glass.Mapper.Sc;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Configuration;
using Sitecore.DependencyInjection;
using StructureMap;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Sc.Blog.Web.IoC
{
    [ExcludeFromCodeCoverage]
    public class StructureMapServiceProviderBuilder : BaseServiceProviderBuilder
    {
        protected override IServiceProvider BuildServiceProvider(IServiceCollection serviceCollection)
        {
            var coreAssembly = AppDomain.CurrentDomain.GetAssemblies().
                 SingleOrDefault(assembly => assembly.GetName().Name == "Sc.Blog.Core");

            var container = new Container(x =>
            {
                x.Populate(serviceCollection);

                x.Scan(scan => 
                {
                    scan.Assembly(coreAssembly);
                    scan.With(new CoreAssemblyConvention());
                });

                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });

                x.For<ISitecoreContext>().Use(c => new SitecoreContext(Factory.GetDatabase("master")));
            });          

            return container.GetInstance<IServiceProvider>();
        }
    }
}