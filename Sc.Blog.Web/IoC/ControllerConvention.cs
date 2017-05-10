using StructureMap;
using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.TypeRules;
using System.Web.Mvc;
using StructureMap.Graph.Scanning;
using Sitecore.Mvc.Extensions;

namespace Sc.Blog.Web.IoC
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ControllerConvention : IRegistrationConvention
    {
        public void ScanTypes(TypeSet types, Registry registry)
        {
            types.FindTypes(TypeClassification.Concretes | TypeClassification.Closed).Each(type =>
            {
                if (type.CanBeCastTo<Controller>())
                {
                    registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
                }
            });
        }
    }
}