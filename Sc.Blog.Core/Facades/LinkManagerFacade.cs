using Sc.Blog.Abstractions.Facades;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Sc.Blog.Core.Facades
{
    public class LinkManagerFacade : ILinkManagerFacade
    {
        public string GetItemUrl(Item item)
        {
            return LinkManager.GetItemUrl(item, UrlOptions.DefaultOptions);
        }
    }
}
