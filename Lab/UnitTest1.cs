using Business;
using Business.Dictionaries;
using Common.Api;
using Common.Extensions;
using Data;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace Lab
{
    public class BusinessTests : IClassFixture<TestFixture>
    {

        private readonly BDictionary _bDictionary;
        private readonly ITestOutputHelper _output;

        public BusinessTests(TestFixture fixture, ITestOutputHelper testOutputHelper)
        {
            _bDictionary = new BDictionary(fixture.DataBase, fixture.Configuration);
            _output = testOutputHelper;
        }

        [Fact]
        public async Task NotFoundWord()
        {
            var input = "worddfgdfg";
            var exception = await Assert.ThrowsAsync<Common.AppException>(() => _bDictionary.FindEnglish(input));
            Assert.Equal("Common.AppException", exception.GetType().ToString());
        }

        [Fact]
        public async Task FoundWordInWordField()
        {
            var input = "word";
            var res = await _bDictionary.FindEnglish(input);
            
            Assert.Equal(res.Word.ToLower(), input);
        }
        [Fact]
        public async Task FoundWordInFormsField()
        {
            var input = "words";
            var res = await _bDictionary.FindEnglish(input);
            Assert.NotNull(res);
        }

    }

}
