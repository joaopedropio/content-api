using ContentClient.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContentClient
{
    public interface IContentClient
    {
        Task<IList<T>> Get<T>() where T : IStorable, new();
        Task<IList<T>> GetByName<T>(string name) where T : IStorable, new();
        Task<T> Get<T>(uint id) where T : IStorable, new();
        Task Delete<T>(uint id) where T : IStorable, new();
        Task<uint> Insert<T>(T media) where T : IStorable, new();
    }
}
