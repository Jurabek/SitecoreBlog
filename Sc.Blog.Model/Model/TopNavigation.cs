using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using Sitecore.Data.Fields;
using System;

namespace Sc.Blog.Model.Model
{
    public class TopNavigation : BaseEntity
    {
        public virtual Guid Id { get; set; }

        public virtual string Title { get; set; }

        [SitecoreField(UrlOptions = SitecoreInfoUrlOptions.AlwaysIncludeServerUrl | SitecoreInfoUrlOptions.LanguageEmbeddingNever)]
        public virtual Link Url { get; set; }
                
        public virtual bool IsRightNavagationItem { get; set; }
    }
}
