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
