using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Context;
using UrlShortener.Data.Entities;
using UrlShortener.Domain.Abstraction;
using Xunit;
using Moq.Language.Flow;
using UrlShortener.WebApplication.Controllers;

namespace UrlShortener.Domain.Tests
{
    public class ShortUrlApiControllerTests
    {
        private readonly Mock<ILogger<ShortUrlApiController>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IShortUrlService> _shortUrlServiceMock;
        private readonly ShortUrlApiController _sut;

        public ShortUrlApiControllerTests()
        {
            _loggerMock = new Mock<ILogger<ShortUrlApiController>>();
            _mapperMock = new Mock<IMapper>();
            _shortUrlServiceMock = new Mock<IShortUrlService>();
            _sut = new ShortUrlApiController(
                _loggerMock.Object,
                _mapperMock.Object,
                _shortUrlServiceMock.Object);
        }

        [Fact]
        public async Task GenerateShortUrl_WithInvalidUrl_ReturnsBadRequest()
        {
            // Arrange
            var request = new ShortUrlRequest { Url = "invalid url" };

            // Act
            var result = _sut.GenerateShortUrl(request).Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GenerateShortUrl_WithValidUrl_ReturnsShortUrlResponse()
        {
            // Arrange
            var request = new ShortUrlRequest { Url = "https://example.com" };
            var shortUrl = new ShortUrl { Alias = "abc", Url = "https://example.com" };
            var expectedResponse = new ShortUrlResponse { Alias = "abc", Url = "https://example.com" };
            _shortUrlServiceMock.Setup(x => x.GenerateShortUrl(request.Url)).ReturnsAsync(shortUrl);
            _mapperMock.Setup(x => x.Map<ShortUrlResponse>(shortUrl)).Returns(expectedResponse);

            // Act
            var result = await _sut.GenerateShortUrl(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task Redirection_WithNonExistentAlias_ReturnsNotFound()
        {
            // Arrange
            var alias = "non-existent-alias";
            _shortUrlServiceMock.Setup(x => x.GetUrlByAlias(alias));

            // Act
            var result = await _sut.Redirection(alias);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Redirection_WithExistentAlias_ReturnsRedirectToUrl()
        {
            // Arrange
            var alias = "existent-alias";
            var url = "https://example.com";
            _shortUrlServiceMock.Setup(x => x.GetUrlByAlias(alias)).ReturnsAsync(url);

            // Act
            var result = await _sut.Redirection(alias);

            // Assert
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal(url, redirectResult.Url);
        }
    }
}