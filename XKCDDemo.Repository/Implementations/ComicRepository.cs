using System;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;
using XKCDDemo.Repository.Interfaces;

namespace XKCDDemo.Repository.Implementations
{
    public class ComicRepository : IComicRepository
    {
        private readonly IXKCDApi _api;

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

        public async Task<int?> GetFirstComicId()
        {
            return await _api.GetFirstComicId();
        }

        public async Task<int?> GetLastComicId()
        {
            try
            {
                var comic = await _api.GetComicOfTheDay();
                return comic.Num;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
