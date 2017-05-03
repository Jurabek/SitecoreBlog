using Sc.Blog.Abstractions.Facades;
using Sc.Blog.Abstractions.Providers;
using Sitecore.Data;
using System;
using System.Web.Mvc;

namespace Sc.Blog.Core.Providers
{
    public class RouteProvider : IRouteProvider
    {
        private ISitecoreSettingsFacade _sitecoreSettingsFacade;
        private ISitecoreDatabaseFacade _sitecoreDatabaseFacade;
        private ILinkManagerFacade _linkManagerFacade;

        public RouteProvider(ISitecoreSettingsFacade sitecoreSettingsFacade,
            ISitecoreDatabaseFacade sitecoreDatabaseFacade,
            ILinkManagerFacade linkManagerFacade)
        {
            _sitecoreSettingsFacade = sitecoreSettingsFacade;
            _sitecoreDatabaseFacade = sitecoreDatabaseFacade;
            _linkManagerFacade = linkManagerFacade;
        }

        public ActionResult RedirectToItem(string itemPath, Func<string, object, RedirectToRouteResult> redirectToRoute)
        {
            var item = _sitecoreDatabaseFacade.GetItem(itemPath);
            var itemUrl = _linkManagerFacade.GetItemUrl(item);
            return redirectToRoute.Invoke(_sitecoreSettingsFacade.SitecoreRouteName, new { pathInfo = itemUrl.TrimStart(new char[] { '/' }) });
        }
    }
}
