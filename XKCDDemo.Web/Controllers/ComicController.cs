using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XKCDDemo.Service.Interfaces;

namespace XKCDDemo.Web.Controllers
{
    [Route("comic")]
    public class ComicController : Controller
    {
        private readonly IComicService _comicService;

        public ComicController(IComicService comicService)
        {
            _comicService = comicService;
        }

        [Route("{id}")]
        public async Task<IActionResult> Detail([FromRoute]int id)
        {
            var detail = await _comicService.GetComicDetailById(id);
            return View(detail);
        }
    }
}