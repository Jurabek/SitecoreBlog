using System;
using System.IO;

namespace Sc.Blog.Abstractions.Providers
{
    public interface IMediaUploadProvider
    {
        Guid CreateMedaiItem(Stream stream, string fileName, string sitecorePath, string mediaItemName);
    }
}