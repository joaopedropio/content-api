using ContentApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContentApi.Helpers
{
    public class GriFsHelper
    {
        public static string CreateFileName(ContentType contentType, FileType fileType, string contentName)
        {
            var contentShortName = new string(contentName.Where(char.IsLetter).ToArray()).ToLower();
            return contentType.ToString() + "_" + fileType.ToString() + "_" + contentShortName;
        }
    }
}
