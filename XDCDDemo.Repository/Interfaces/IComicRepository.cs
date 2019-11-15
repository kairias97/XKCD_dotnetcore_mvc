using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XDCDDemo.DTO.ViewModels;

namespace XDCDDemo.Repository.Interfaces
{
    public interface IComicRepository
    {
        Task<ComicDetailVM> GetComicOfTheDay();
    }
}
