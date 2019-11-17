using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;

namespace XKCDDemo.Service.Interfaces
{
    public interface IComicService
    {
        Task<DisplayedComicVM> GetComicOfTheDay();
        Task<ComicNavigationVM> GetComicNavigationById(int? comicId);
    }
}
