using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;
using XKCDDemo.Service.Interfaces;
using XKCDDemo.Web.Controllers;
using Xunit;

namespace XDCDDemo.Test
{
    public class ComicControllerTest
    {
        [Fact]
        public async Task Test_Redirect_When_ComicDetail_Not_Found()
        {
            //Arrange
            int mockId = 1;
            var mockComicService = new Mock<IComicService>();
            mockComicService.Setup(service => service.GetComicDetailById(mockId))
                .ReturnsAsync(GetNotFoundComicResult());
            var comicController = new ComicController(mockComicService.Object);
            //Act
            var result = await comicController.Detail(mockId);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Home", redirectResult.ControllerName);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        #region Mock Arrangement
        private DisplayedComicVM GetNotFoundComicResult()
        {
            return new DisplayedComicVM
            {
                Comic = null,
                Navigation = new ComicNavigationVM
                {
                    FirstId = null,
                    LastId = null,
                    NextId = null,
                    PreviousId = null
                }
            };
        }
        #endregion
    }
}
