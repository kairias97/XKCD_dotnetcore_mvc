using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;
using XKCDDemo.Repository.Implementations;
using XKCDDemo.Repository.Interfaces;
using Xunit;

namespace XDCDDemo.Test
{
    public class ComicRepositoryTest
    {
        [Fact]
        public async Task Test_ComicId_Not_In_ValidRange()
        {
            //Arrange
            int mockId = 10000;
            var mockComicService = new Mock<IXKCDApi>();
            mockComicService.Setup(api => api.GetFirstComicId())
                .ReturnsAsync(GetValidMockedFirstComicId());
            mockComicService.Setup(api => api.GetComicOfTheDay())
                .ReturnsAsync(GetValidMockedComicOfTheDay());
            var comicRepository = new ComicRepository(mockComicService.Object);

            //Act
            var result = await comicRepository.IsComicInValidRange(mockId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.FirstComicId);
            Assert.Equal(1000, result.LastComicId);
            Assert.False(result.IsValid);
        }

        #region Mock data arrangement
        private int GetValidMockedFirstComicId()
        {
            return 1;
        }

        private ComicDetailVM GetValidMockedComicOfTheDay()
        {
            return new ComicDetailVM
            {
                Alt = $"Testing alt",
                Img = "http://www.test.com",
                Num = 1000,
                Title = "Test comic",
                SafeTitle = "Test comic",
            };
        }

        #endregion
    }
}
