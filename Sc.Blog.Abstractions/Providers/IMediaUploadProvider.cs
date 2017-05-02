using Sitecore.Data.Items;
using System;
using System.IO;

namespace Sc.Blog.Abstractions.Providers
{
    public interface IMediaUploadProvider
    {
        MediaItem CreateMedaiItem(Stream stream, string fileName, string sitecorePath);
    }
}