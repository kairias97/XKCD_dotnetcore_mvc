using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;
using XKCDDemo.Util.Attributes;

namespace XKCDDemo.Repository.Interfaces
{
    public interface IXKCDApi
    {
        Task<ComicDetailVM> GetComicOfTheDay();
        Task<ComicDetailVM> GetComicById(int comicId);
        Task<int?> GetFirstComicId();
    }
}
