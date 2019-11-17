using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;

namespace XKCDDemo.Repository.Interfaces
{
    public interface IXKCDApi
    {
        //[Get("/info.0.json")]
        Task<ComicDetailVM> GetComicOfTheDay();
    }
}
