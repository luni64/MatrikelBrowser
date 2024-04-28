using System.Collections.Generic;

namespace Interfaces
{
    public interface IPage
    {
        List<IBookmark> Bookmarks { get; }
        string localFilename { get; }
        string URL { get; }

        string loadImage();
        string ToString();
    }
}