using System;

namespace ContentApi.Domain.Entities
{
    public class Person
    {
        public uint? Id { get; set; }
        public string Name { get; set; }
        public int Age => Convert.ToInt32(DateTime.Now.Subtract(Birthday).TotalDays / 365);
        public DateTime Birthday { get; set; }
        public string Nationality { get; set; }
    }
}
