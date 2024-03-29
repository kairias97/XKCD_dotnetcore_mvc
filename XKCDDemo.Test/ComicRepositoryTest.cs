﻿using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XKCDDemo.DTO.ViewModels;
using XKCDDemo.Repository.Implementations;
using XKCDDemo.Repository.Interfaces;
using Xunit;

namespace XKCDDemo.Test
{
    public class ComicRepositoryTest
    {
        [Fact]
        public async Task Test_ComicId_Not_In_ValidRange()
        {
            //Arrange
            int mockId = 10000;
            var mockComicApi = new Mock<IXKCDApi>();
            mockComicApi.Setup(api => api.GetFirstComicId())
                .ReturnsAsync(GetValidMockedFirstComicId());
            mockComicApi.Setup(api => api.GetComicOfTheDay())
                .ReturnsAsync(GetValidMockedComicOfTheDay());
            var comicRepository = new ComicRepository(mockComicApi.Object);

            //Act
            var result = await comicRepository.IsComicInValidRange(mockId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.FirstComicId);
            Assert.Equal(1000, result.LastComicId);
            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task Test_ComicId_In_ValidRange()
        {
            //Arrange
            int mockId = 100;
            var mockComicApi = new Mock<IXKCDApi>();
            mockComicApi.Setup(api => api.GetFirstComicId())
                .ReturnsAsync(GetValidMockedFirstComicId());
            mockComicApi.Setup(api => api.GetComicOfTheDay())
                .ReturnsAsync(GetValidMockedComicOfTheDay());
            var comicRepository = new ComicRepository(mockComicApi.Object);

            //Act
            var result = await comicRepository.IsComicInValidRange(mockId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.FirstComicId);
            Assert.Equal(1000, result.LastComicId);
            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Test_Next_Valid_Comic_When_Current_Is_Not_Last()
        {
            //Arrange
            int mockId = 403;
            int nextValidId = 406;
            var mockComicApi = new Mock<IXKCDApi>();
            mockComicApi.Setup(api => api.GetFirstComicId())
                .ReturnsAsync(GetValidMockedFirstComicId());
            mockComicApi.Setup(api => api.GetComicOfTheDay())
                .ReturnsAsync(GetValidMockedComicOfTheDay());
            mockComicApi.Setup(api => api.GetComicById(404))
                .ReturnsAsync(GetInvalidComic());
            mockComicApi.Setup(api => api.GetComicById(405))
                .ReturnsAsync(GetInvalidComic());
            mockComicApi.Setup(api => api.GetComicById(nextValidId))
                .ReturnsAsync(GetMockedComicById(nextValidId));

            var comicRepository = new ComicRepository(mockComicApi.Object);

            //Act
            var result = await comicRepository.GetNextComicId(mockId);
            var nextComic = await comicRepository.GetComicById(nextValidId);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(nextValidId, result);
            Assert.NotNull(nextComic);
            Assert.Equal(nextComic.Num, result);
            Assert.Equal($"Testing {result.Value}", nextComic.Alt);
            Assert.Equal($"Test comic {result.Value}", nextComic.SafeTitle);
            Assert.Equal($"Test comic {result.Value}", nextComic.Title);
            mockComicApi.Verify(mock => mock.GetComicById(mockId + 1), Times.Once());
            mockComicApi.Verify(mock => mock.GetComicById(mockId + 2), Times.Once());
            //Because of the fetch during the act
            mockComicApi.Verify(mock => mock.GetComicById(nextValidId), Times.Exactly(2));
            mockComicApi.Verify(mock => mock.GetComicById(mockId + 4), Times.Never());

        }

        [Fact]
        public async Task Test_Previous_Valid_Comic_When_Current_Is_Not_First()
        {
            //Arrange
            int mockId = 406;
            int previousValidId = 403;
            var mockComicApi = new Mock<IXKCDApi>();
            mockComicApi.Setup(api => api.GetFirstComicId())
                .ReturnsAsync(GetValidMockedFirstComicId());
            mockComicApi.Setup(api => api.GetComicOfTheDay())
                .ReturnsAsync(GetValidMockedComicOfTheDay());
            mockComicApi.Setup(api => api.GetComicById(404))
                .ReturnsAsync(GetInvalidComic());
            mockComicApi.Setup(api => api.GetComicById(405))
                .ReturnsAsync(GetInvalidComic());
            mockComicApi.Setup(api => api.GetComicById(previousValidId))
                .ReturnsAsync(GetMockedComicById(previousValidId));

            var comicRepository = new ComicRepository(mockComicApi.Object);

            //Act
            var result = await comicRepository.GetPreviousComicId(mockId);
            var previousComic = await comicRepository.GetComicById(previousValidId);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(previousValidId, result);
            Assert.NotNull(previousComic);
            Assert.Equal(previousComic.Num, result);
            Assert.Equal($"Testing {result.Value}", previousComic.Alt);
            Assert.Equal($"Test comic {result.Value}", previousComic.SafeTitle);
            Assert.Equal($"Test comic {result.Value}", previousComic.Title);

            mockComicApi.Verify(mock => mock.GetComicById(mockId - 1), Times.Once());
            mockComicApi.Verify(mock => mock.GetComicById(mockId - 2), Times.Once());
            //Because of the fetch during the act
            mockComicApi.Verify(mock => mock.GetComicById(previousValidId), Times.Exactly(2));
            mockComicApi.Verify(mock => mock.GetComicById(mockId - 4), Times.Never());

        }

        [Fact]
        public async Task Test_No_Previous_Comic_When_Current_Is_First()
        {
            //Arrange
            int mockId = 1;
            int? previousValidId = null;
            var mockComicApi = new Mock<IXKCDApi>();
            mockComicApi.Setup(api => api.GetFirstComicId())
                .ReturnsAsync(GetValidMockedFirstComicId());
            mockComicApi.Setup(api => api.GetComicOfTheDay())
                .ReturnsAsync(GetValidMockedComicOfTheDay());
            mockComicApi.Setup(api => api.GetComicById(previousValidId ?? default))
                .ReturnsAsync(GetInvalidComic());

            var comicRepository = new ComicRepository(mockComicApi.Object);

            //Act
            var result = await comicRepository.GetPreviousComicId(mockId);
            var previousComic = await comicRepository.GetComicById(previousValidId ?? default);
            var firstComicId = await comicRepository.GetFirstComicId();

            //Assert
            Assert.Null(result);
            Assert.Equal(previousValidId, result);
            Assert.Null(previousComic);
            Assert.Equal(firstComicId, mockId);

        }

        [Fact]
        public async Task Test_No_Next_Comic_When_Current_Is_Last()
        {
            //Arrange
            int mockId = 1000;
            int? nextValidId = null;
            var mockComicApi = new Mock<IXKCDApi>();
            mockComicApi.Setup(api => api.GetFirstComicId())
                .ReturnsAsync(GetValidMockedFirstComicId());
            mockComicApi.Setup(api => api.GetComicOfTheDay())
                .ReturnsAsync(GetValidMockedComicOfTheDay());
            mockComicApi.Setup(api => api.GetComicById(nextValidId ?? default))
                .ReturnsAsync(GetInvalidComic());

            var comicRepository = new ComicRepository(mockComicApi.Object);

            //Act
            var result = await comicRepository.GetNextComicId(mockId);
            var nextComic = await comicRepository.GetComicById(nextValidId ?? default);
            var lastComicId = await comicRepository.GetLastComicId();
            //Assert
            Assert.Null(result);
            Assert.Equal(nextValidId, result);
            Assert.Null(nextComic);
            Assert.Equal(lastComicId, mockId);
        }

        [Fact]
        public async Task Test_No_Previous_Comic_When_Current_Is_Invalid()
        {
            //Arrange
            int mockId = 10000;
            int? previousValidId = null;
            var mockComicApi = new Mock<IXKCDApi>();
            mockComicApi.Setup(api => api.GetFirstComicId())
                .ReturnsAsync(GetValidMockedFirstComicId());
            mockComicApi.Setup(api => api.GetComicOfTheDay())
                .ReturnsAsync(GetValidMockedComicOfTheDay());
            mockComicApi.Setup(api => api.GetComicById(mockId))
                .ReturnsAsync(GetInvalidComic());

            var comicRepository = new ComicRepository(mockComicApi.Object);

            //Act
            var result = await comicRepository.GetPreviousComicId(mockId);
            //Assert
            Assert.Null(result);
            Assert.Equal(previousValidId, result);
            mockComicApi.Verify(mock => mock.GetComicById(mockId - 1), Times.Never());
        }

        [Fact]
        public async Task Test_No_Next_Comic_When_Current_Is_Invalid()
        {
            //Arrange
            int mockId = 10000;
            int? nextValidId = null;
            var mockComicApi = new Mock<IXKCDApi>();
            mockComicApi.Setup(api => api.GetFirstComicId())
                .ReturnsAsync(GetValidMockedFirstComicId());
            mockComicApi.Setup(api => api.GetComicOfTheDay())
                .ReturnsAsync(GetValidMockedComicOfTheDay());
            mockComicApi.Setup(api => api.GetComicById(mockId))
                .ReturnsAsync(GetInvalidComic());

            var comicRepository = new ComicRepository(mockComicApi.Object);

            //Act
            var result = await comicRepository.GetPreviousComicId(mockId);

            //Assert
            Assert.Null(result);
            Assert.Equal(nextValidId, result);
            mockComicApi.Verify(mock => mock.GetComicById(mockId + 1), Times.Never());

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

        private ComicDetailVM GetMockedComicById(int id)
        {
            return new ComicDetailVM
            {
                Alt = $"Testing {id}",
                Img = "http://www.test.com",
                Num = id,
                Title = $"Test comic {id}",
                SafeTitle = $"Test comic {id}",
            };
        }

        private ComicDetailVM GetInvalidComic()
        {
            return null;
        }

        #endregion
    }
}
