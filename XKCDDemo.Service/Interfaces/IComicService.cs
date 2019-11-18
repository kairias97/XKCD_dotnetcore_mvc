using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;
using XKCDDemo.Util.Attributes;

namespace XKCDDemo.Service.Interfaces
{
    [Scoped(ScopeCoverage = ScopeCoverage.Scoped)]
    public interface IComicService
    {
        Task<DisplayedComicVM> GetComicOfTheDay();
        Task<DisplayedComicVM> GetComicDetailById(int comicId);

        Task<ComicNavigationVM> GetComicNavigationById(int? comicId);
    }
}
