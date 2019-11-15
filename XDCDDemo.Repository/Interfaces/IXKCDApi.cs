using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XDCDDemo.DTO.ViewModels;

namespace XDCDDemo.Repository.Interfaces
{
    public interface IXKCDApi
    {
        [Get("info.0.json")]
        Task<ComicDetailVM> GetComicOfTheDay();
    }
}
