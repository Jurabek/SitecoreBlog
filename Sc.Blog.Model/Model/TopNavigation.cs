using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data.Fields;
using System;

namespace Sc.Blog.Model.Model
{
    public class TopNavigation : BaseEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        [SitecoreField(UrlOptions = SitecoreInfoUrlOptions.AlwaysIncludeServerUrl | SitecoreInfoUrlOptions.LanguageEmbeddingNever)]
        public Link Url { get; set; }
                
        public bool IsRightNavagationItem { get; set; }
    }
}
