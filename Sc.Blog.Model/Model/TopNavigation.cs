using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;
using System;

namespace Sc.Blog.Model.Model
{
    public class TopNavigation
    {
        public virtual Guid Id { get; set; }

        public virtual string Title { get; set; }

        [SitecoreField(UrlOptions = SitecoreInfoUrlOptions.AlwaysIncludeServerUrl | SitecoreInfoUrlOptions.LanguageEmbeddingNever)]
        public virtual Link Url { get; set; }
    }
}
