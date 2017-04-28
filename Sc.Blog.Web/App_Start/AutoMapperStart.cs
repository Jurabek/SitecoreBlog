using Sc.Blog.Core.Mappers;
using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sc.Blog.Web.App_Start
{
    public class AutoMapperStart
    {
        public void Process(PipelineArgs args)
        {
            AutoMapperConfiguration.Configure();
        }
    }
}