using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ContentApiTests.Helpers
{
    public static class FileHelper {
        public static Stream GetInputFile(string filename)
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            string path = "ContentApiTests.InputFiles";

            return thisAssembly.GetManifestResourceStream(path + "." + filename);
        }

        public static string ImageToBase64(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);

                var bytes = memoryStream.GetBuffer();

                return System.Convert.ToBase64String(bytes);
            }
        }
    }
}
