﻿using ContentApi.Domain;

namespace ContentApi.Database
{
    public interface IContentRepository
    {
        Content Get(string name);
        void Create(Content content);
        void Remove(string name);
    }
}
