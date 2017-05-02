using Sc.Blog.Abstractions.Providers;
using Sitecore.Data.Items;
using System;
using System.IO;

namespace Sc.Blog.Core.Providers
{
    public class MediaUploadProvider : IMediaUploadProvider
    {
        public MediaItem CreateMedaiItem(Stream stream, string fileName, string sitecorePath)
        {
            Sitecore.Resources.Media.MediaCreatorOptions options = new Sitecore.Resources.Media.MediaCreatorOptions()
            {
                FileBased = false,
                IncludeExtensionInItemName = false,
                OverwriteExisting = false,
                Versioned = false,
                Destination = sitecorePath + "/" + Path.GetFileNameWithoutExtension(fileName),
                Database = Sitecore.Configuration.Factory.GetDatabase("master")
            };
            
            Sitecore.Resources.Media.MediaCreator creator = new Sitecore.Resources.Media.MediaCreator();
            MediaItem mediaItem = creator.CreateFromStream(stream, fileName, options);
            return mediaItem;
        }
    }
}
