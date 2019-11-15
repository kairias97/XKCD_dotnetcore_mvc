using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;

namespace XKCDDemo.Repository.Interfaces
{
    public interface IComicRepository
    {
        Task<ComicDetailVM> GetComicOfTheDay();
    }
}
