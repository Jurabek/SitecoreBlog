using Sitecore.Data;
using System;
using System.Web.Mvc;

namespace Sc.Blog.Abstractions.Providers
{
    public interface IRouteProvider
    {
        ActionResult RedirectToItem(ID itemId, Func<string, object, RedirectToRouteResult> redirectToRoute);
    }
}