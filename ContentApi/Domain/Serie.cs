using System.Collections.Generic;

namespace ContentApi.Domain
{
    public class Serie : Content
    {
        public IList<Episode> Episodes { get; set; }
    }
}
