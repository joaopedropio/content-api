using System.Collections.Generic;

namespace ContentApi.Domain.Entities
{
    public class Serie : Content
    {
        public IList<Episode> Episodes { get; set; }
    }
}
