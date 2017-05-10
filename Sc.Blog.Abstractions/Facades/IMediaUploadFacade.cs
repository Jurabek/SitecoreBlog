using Sitecore.Data.Items;
using System;
using System.IO;

namespace Sc.Blog.Abstractions.Facades
{
    public interface IMediaUploadFacade
    {
        MediaItem CreateMedaiItem(Stream stream, string fileName, string sitecorePath);
    }
}