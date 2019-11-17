using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;
using XKCDDemo.Repository.Interfaces;

namespace XKCDDemo.Repository.Implementations
{

    public class XKCDApiHelper : IXKCDApi
    {
        private readonly HttpClient _httpClient;

        public XKCDApiHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://xkcd.com/");
        }

        public async Task<ComicDetailVM> GetComicById(int comicId)
        {
            try
            {
                string endpoint = string.Format("/{0}/info.0.json", comicId);
                var response = await _httpClient.GetAsync(endpoint);
                return await ProcessComicRawResponse(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ComicDetailVM> GetComicOfTheDay()
        {
            try
            {
                string endpoint = "/info.0.json";
                var response = await _httpClient.GetAsync(endpoint);
                return await ProcessComicRawResponse(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Eventually if the api chances and it adds an endpoint to get the initial value id, the only change needed would be here so we keep the repository agnostic of this knowledge
        public Task<int?> GetFirstComicId()
        {
            //This is a simulation since by domain knowledge we know the first comic id will be 1
            return Task.Run(()  => (int?)1);
        }

        #region Private methods
        private async Task<ComicDetailVM> ProcessComicRawResponse(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var parsedResult = JsonConvert.DeserializeObject<ComicDetailVM>(await response.Content.ReadAsStringAsync());
                return parsedResult;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
