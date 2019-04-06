using ContentApiIntegrationTests;
using ContentClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContentClient
{
    public class ContentClient : IContentClient
    {
        HttpClient httpClient;
        public ContentClient(string contentBaseAddress)
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri(contentBaseAddress);
        }

        private string GetPath(object obj)
        {
            if (obj is Media)
                return "/media";

            if (obj is Person)
                return "/person";

            if (obj is Movie)
                return "/movie";

            throw new ArgumentException("Type not supported");
        }

        public async Task Delete<T>(uint id) where T : IStorable, new()
        {
            var obj = new T();
            var typeName = obj.GetType().ToString();
            var path = GetPath(obj);
            var httpResponse = await this.httpClient.DeleteAsync(path + $"/{id}");

            if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                throw new ArgumentException($"No {typeName} with id {id} found.");
        }

        public async Task<IList<T>> Get<T>() where T : IStorable, new()
        {
            var path = GetPath(new T());
            var httpResponse = await this.httpClient.GetAsync(path);
            return await HttpResponseHelper.ReadBody<List<T>>(httpResponse);
        }

        public async Task<T> Get<T>(uint id) where T : IStorable, new()
        {
            var path = GetPath(new T());
            var httpResponse = await this.httpClient.GetAsync(path + $"/{id}");
            return await HttpResponseHelper.ReadBody<T>(httpResponse);
        }

        public async Task<uint> Insert<T>(T media) where T : IStorable, new()
        {
            var path = GetPath(new T());
            var json = JsonConvert.SerializeObject(media);
            var content = new StringContent(json);
            var httpResponse = await this.httpClient.PostAsync(path, content);
            var postMedia = await HttpResponseHelper.ReadBody<T>(httpResponse);
            return postMedia.Id.Value;
        }
    }
}
