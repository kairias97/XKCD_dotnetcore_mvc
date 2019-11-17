using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;
using XKCDDemo.Repository.Interfaces;
using XKCDDemo.Service.Interfaces;

namespace XKCDDemo.Service.Implementations
{
    public class ComicService : IComicService
    {
        private readonly IComicRepository _comicRepository;

        public ComicService(IComicRepository comicRepository)
        {
            _comicRepository = comicRepository;
        }

        public async Task<ComicNavigationVM> GetComicNavigationById(int? comicId)
        {
            if (comicId == null) return new ComicNavigationVM { NextId = null, PreviousId = null };
            return new ComicNavigationVM
            {
                PreviousId = await _comicRepository.GetPreviousComicId(comicId.Value),
                NextId = await _comicRepository.GetNextComicId(comicId.Value)
            };
        }

        public async Task<DisplayedComicVM> GetComicOfTheDay()
        {
            var comicOfTheDay = await _comicRepository.GetComicOfTheDay();
            return new DisplayedComicVM
            {
                Comic = comicOfTheDay
            };
        }
    }
}
