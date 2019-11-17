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

        public async Task<int?> GetNextComicId(int comicId)
        {
            int? lastComicId = await GetLastComicId();
            if (lastComicId == null) return null;
            //The next comic id will be one id higher than the current comic id;
            return (comicId + 1 > lastComicId) ? (int?)null : comicId + 1;
        }

        public async Task<int?> GetPreviousComicId(int comicId)
        {
            int? firstComicId = await GetFirstComicId();
            if (firstComicId == null) return null;
            //The previous comic id will be one id lower than the current comic id;
            return (comicId - 1 < firstComicId) ? (int?) null : comicId - 1; 
        }
    }
}
