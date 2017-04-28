using System;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sc.Blog.Abstractions.Facades
{
    public interface ISitecoreDatabaseFacade
    {
        Item GetItem(Guid id);
        Item GetItem(ID id);
        Item GetItem(string path);
    }
}