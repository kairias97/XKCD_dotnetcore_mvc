using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;
using XKCDDemo.Service.Interfaces;
using XKCDDemo.Web.Controllers;
using Xunit;

namespace XKCDDemo.Test
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

        [Fact]
        public async Task Test_HappyPath_When_Getting_Existing_ComicDetail()
        {
            //Arrange
            int mockId = 1;
            var mockComicService = new Mock<IComicService>();
            mockComicService.Setup(service => service.GetComicDetailById(mockId))
                .ReturnsAsync(GetFoundComicDetailById(mockId));
            var comicController = new ComicController(mockComicService.Object);
            //Act
            var result = await comicController.Detail(mockId);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var displayedComicModel = Assert.IsType<DisplayedComicVM>(viewResult.Model);
            Assert.NotNull(displayedComicModel.Comic);
            Assert.NotNull(displayedComicModel.Navigation);
            Assert.Equal(mockId, displayedComicModel.Navigation.FirstId);
            Assert.Equal(mockId, displayedComicModel.Navigation.LastId);
            Assert.Equal(mockId, displayedComicModel.Comic.Num);
            Assert.Equal("Testing alt", displayedComicModel.Comic.Alt);
            Assert.Equal("Test comic", displayedComicModel.Comic.SafeTitle);
            Assert.Equal("Test comic", displayedComicModel.Comic.Title);
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

        private DisplayedComicVM GetFoundComicDetailById(int id)
        {
            return new DisplayedComicVM
            {
                Comic = new ComicDetailVM 
                { 
                    Alt = $"Testing alt",
                    Img = "http://www.test.com",
                    Num = id,
                    Title = "Test comic",
                    SafeTitle = "Test comic",
                },
                Navigation = new ComicNavigationVM
                {
                    FirstId = id,
                    LastId = id,
                    NextId = null,
                    PreviousId = null
                }
            };
        } 
        #endregion
    }
}
