using Sitecore.Data;

namespace Sc.Blog.Common
{
    public class Constants
    {
        public struct Templates
        {
            public struct Home
            {
                public static readonly ID ID = new ID("{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}");
                public const string TemplateId = "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}";
            }

            public struct Article
            {
                public static readonly ID ID = new ID("{E533EFBD-106E-4632-B501-DE7326298FDD}");
                public const string TemplateId = "{E533EFBD-106E-4632-B501-DE7326298FDD}";
            }
        }
    }
}
