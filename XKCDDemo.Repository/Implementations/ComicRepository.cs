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

        public async Task<ComicDetailVM> GetComicById(int comicId)
        {
            return await _api.GetComicById(comicId);
        }

        public async Task<ComicDetailVM> GetComicOfTheDay()
        {
            return await _api.GetComicOfTheDay();
        }

        public async Task<int?> GetFirstComicId()
        {
            return await _api.GetFirstComicId();
        }

        public async Task<int?> GetLastComicId()
        {
            return (await _api.GetComicOfTheDay())?.Num;
        }

        public async Task<int?> GetNextComicId(int comicId)
        {
            int? nextComicId = null;
            var rangeValidation = await IsComicInValidRange(comicId);
            if (!rangeValidation?.IsValid ?? false) return null;
            //It will fetch and get the reference id of the next available comic dynamically
            for (int i = comicId + 1; i <= rangeValidation?.LastComicId && i >= rangeValidation?.FirstComicId; i++)
            {
                var targetComic = await GetComicById(i);
                if (targetComic != null)
                {
                    nextComicId = targetComic.Num;
                    break;
                }
            }
            return nextComicId;
        }

        public async Task<int?> GetPreviousComicId(int comicId)
        {
            int? previousComicId = null;
            var rangeValidation = await IsComicInValidRange(comicId);
            if (!rangeValidation?.IsValid ?? false) return null;
            //It will fetch and get the reference id of the previous available comic dynamically
            for (int i = comicId - 1; i >= rangeValidation?.FirstComicId && i <= rangeValidation?.LastComicId; i--)
            {
                var targetComic = await GetComicById(i);
                if (targetComic != null)
                {
                    previousComicId = targetComic.Num;
                    break;
                }
            }
            return previousComicId;
        }

        public async Task<RangeValidationResultVM> IsComicInValidRange(int comicId)
        {

            int? lastComicId = await GetLastComicId();
            int? firstComicId = await GetFirstComicId();
            return new RangeValidationResultVM
            {
                FirstComicId = firstComicId,
                LastComicId = lastComicId,
                IsValid = (lastComicId != null && firstComicId != null 
                    && comicId >= firstComicId && comicId <= lastComicId)
            };
        }
    }
}
