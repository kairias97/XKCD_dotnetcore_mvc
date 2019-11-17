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

        public async Task<ComicDetailVM> GetComicOfTheDay()
        {
            try
            {
                var response = await _httpClient.GetAsync("/info.0.json");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var parsedResult = JsonConvert.DeserializeObject<ComicDetailVM>(await response.Content.ReadAsStringAsync());
                    return parsedResult;
                } else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
