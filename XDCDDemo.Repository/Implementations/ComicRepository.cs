using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XDCDDemo.DTO.ViewModels;
using XDCDDemo.Repository.Interfaces;

namespace XDCDDemo.Repository.Implementations
{
    public class ComicRepository : IComicRepository
    {
        private readonly IXKCDApi _api;

        [ActivatorUtilitiesConstructor]
        public ComicRepository()
        {
            _api = RestService.For<IXKCDApi>("https://xkcd.com/");
        }
        //For future testing purposes
        public ComicRepository(IXKCDApi api)
        {
            _api = api;
        }
        public async Task<ComicDetailVM> GetComicOfTheDay()
        {
            try
            {
                return await _api.GetComicOfTheDay();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
