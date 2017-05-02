using StructureMap.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using StructureMap.Graph.Scanning;
using Sitecore.Mvc.Extensions;
using StructureMap.Pipeline;

namespace Sc.Blog.Web.IoC
{
    public class CoreAssemblyConvention : IRegistrationConvention
    {        
        public void ScanTypes(TypeSet types, Registry registry)
        {
            var abstractionAssembly = AppDomain.CurrentDomain.GetAssemblies().
                 SingleOrDefault(assembly => assembly.GetName().Name == "Sc.Blog.Abstractions");

             var abstractAssemblyInterfaces = abstractionAssembly.GetTypes().Where(t => t.IsInterface);

            types.FindTypes(TypeClassification.Concretes | TypeClassification.Closed).Each(type =>
            {
                var implementedInterface = type.GetInterfaces().Where(t => abstractAssemblyInterfaces.Select(i => i.Name).Contains(t.Name));
                if (implementedInterface.Any())
                {
                    registry.For(type.GetInterfaces().FirstOrDefault())
                        .LifecycleIs(new UniquePerRequestLifecycle())
                        .Use(type);
                }
                else
                {
                    registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
                }
            });
        }
    }
}