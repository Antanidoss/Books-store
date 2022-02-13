using System.IO;

namespace BooksStore.Common.Helpers
{
    public static class ImageBookHelper
    {
        public static byte[] GetImageData(Stream stream)
        {
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
