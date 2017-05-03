using Sitecore.Data;

namespace Sc.Blog.Common
{
    public static class Constants
    {
        public struct Templates
        {
            public struct Article
            {
                public static readonly ID ID = new ID("{E533EFBD-106E-4632-B501-DE7326298FDD}");
                public const string TemplateId = "{E533EFBD-106E-4632-B501-DE7326298FDD}";
            }

            public struct Comment
            {
                public static readonly ID ID = new ID("{4891CB56-DC24-40D0-A5EB-D81B548154A6}");
                public const string TemplateId = "{4891CB56-DC24-40D0-A5EB-D81B548154A6}";
            }
        }

        public struct Folders
        {
            public struct MediaLibrary
            {
                public struct Images
                {
                    public const string Blog = "/sitecore/media library/images/blog";
                }
            }

            public struct Content
            {
                public const string Home = "/sitecore/content/home";

                public struct Global
                {
                    public const string Articles = "/sitecore/content/global/articles";
                    public const string Comments = "sitecore/content/global/comments/";
                    public const string NavigationItems = "/sitecore/content/global/navigationitems";
                }
            }
        }
    }
}
