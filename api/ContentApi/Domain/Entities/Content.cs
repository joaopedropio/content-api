﻿using System;
using System.Collections.Generic;

namespace ContentApi.Domain.Entities
{
    public class Content : IStorable
    {
        public uint? Id { get; set; }

        public string Name { get; set; }

        public Media CoverImage { get; set; }

        public string Country { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Studio { get; set; }
    }
}
