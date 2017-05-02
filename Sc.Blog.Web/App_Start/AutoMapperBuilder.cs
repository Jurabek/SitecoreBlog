using Sc.Blog.Core.Mappers;
using Sitecore.Mvc.Controllers;
using Sitecore.Pipelines;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Sc.Blog.Web.App_Start
{
    public class AutoMapperBuilder
    {
        public void Process(PipelineArgs args)
        {
            AutoMapperConfiguration.Configure();
        }
    }
}