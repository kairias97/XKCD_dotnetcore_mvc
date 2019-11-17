using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;

namespace XKCDDemo.Repository.Interfaces
{
    public interface IXKCDApi
    {
        //[Get("/info.0.json")]
        Task<ComicDetailVM> GetComicOfTheDay();
        Task<ComicDetailVM> GetComicById(int comicId);
        Task<int?> GetFirstComicId();
    }
}
