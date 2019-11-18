using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;
using XKCDDemo.Util.Attributes;

namespace XKCDDemo.Repository.Interfaces
{

    [Scoped(ScopeCoverage = ScopeCoverage.Scoped)]
    public interface IComicRepository
    {
        Task<ComicDetailVM> GetComicOfTheDay();
        Task<int?> GetLastComicId();
        Task<int?> GetFirstComicId();
        Task<int?> GetPreviousComicId(int comicId);
        Task<int?> GetNextComicId(int comicId);
        Task<ComicDetailVM> GetComicById(int comicId);
        Task<RangeValidationResultVM> IsComicInValidRange(int comicId);
    }
}
