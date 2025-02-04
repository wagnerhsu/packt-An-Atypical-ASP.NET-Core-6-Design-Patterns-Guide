﻿using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MyMinimalApiApp
{
    public class ProgramTestWithoutFixture : IAsyncDisposable
    {
        private readonly WebApplicationFactory<SomeOtherClass> _webApplicationFactory;
        private readonly HttpClient _httpClient;

        public ProgramTestWithoutFixture()
        {
            _webApplicationFactory = new WebApplicationFactory<SomeOtherClass>();
            _httpClient = _webApplicationFactory.CreateClient();
        }

        public ValueTask DisposeAsync()
        {
            return ((IAsyncDisposable)_webApplicationFactory).DisposeAsync();
        }

        public class Get : ProgramTestWithoutFixture
        {
            [Fact]
            public async Task Should_respond_a_status_200_OK()
            {
                // Act
                var result = await _httpClient.GetAsync("/");

                // Assert
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }

            [Fact]
            public async Task Should_respond_hello_world()
            {
                // Act
                var result = await _httpClient.GetAsync("/");

                // Assert
                var contentText = await result.Content.ReadAsStringAsync();
                Assert.Equal("Hello World!", contentText);
            }
        }
    }
}