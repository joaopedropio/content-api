using System.Collections.Generic;

namespace ContentApi.MediaFiles
{
    public interface IContentServerClient
    {
        IList<string> ListFilePaths(string path);
    }
}
