using Sc.Blog.Abstractions.Facades;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
