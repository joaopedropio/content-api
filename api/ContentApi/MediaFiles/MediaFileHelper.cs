using System;

namespace ContentApi.MediaFiles
{
    public static class MediaFileHelper
    {
        public static string RemoveBasePath(string fullPath, string basePath)
        {
            if (!fullPath.StartsWith(basePath))
                throw new ArgumentException($"Base path does not match with the full path. FullPath:{fullPath}, BasePath: {basePath}");

            return fullPath.Remove(0, basePath.Length);
        }
    }
}
