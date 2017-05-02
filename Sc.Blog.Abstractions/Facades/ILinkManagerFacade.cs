using Sitecore.Data.Items;

namespace Sc.Blog.Abstractions.Facades
{
    public interface ILinkManagerFacade
    {
        string GetItemUrl(Item item);
    }
}