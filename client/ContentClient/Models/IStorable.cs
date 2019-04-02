using System;
using System.Collections.Generic;
using System.Text;

namespace ContentClient.Models
{
    public interface IStorable
    {
        uint? Id { get; set; }
        string Name { get; set; }
    }
}
