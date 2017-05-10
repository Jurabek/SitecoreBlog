using Sc.Blog.Abstractions.Facades;
using Sitecore.Data;
using Sitecore.Data.Items;
using System;

namespace Sc.Blog.Core.Facades
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class SitecoreDatabaseFacade : ISitecoreDatabaseFacade
    {
        public Item GetItem(ID id)
        {
            return Sitecore.Context.Database.GetItem(id);
        }

        public Item GetItem(Guid id)
        {
            return Sitecore.Context.Database.GetItem(ID.Parse(id));
        }

        public Item GetItem(string path)
        {
            return Sitecore.Context.Database.GetItem(path);
        }
    }
}
