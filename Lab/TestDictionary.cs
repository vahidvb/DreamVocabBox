﻿using Business.Dictionaries;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Lab
{
    public class TestDictionary : BaseTest<BDictionary>
    {
        private readonly ITestOutputHelper output;
        public TestDictionary(ITestOutputHelper output) : base()
        {
            this.output = output;
        }

        [Fact]
        public async Task NotFoundWord()
        {
            var input = "worddfgdfg";
            var exception = await Assert.ThrowsAsync<Common.AppException>(() => Business.FindEnglish(input));
            Assert.Equal("Common.AppException", exception.GetType().ToString());
        }

        [Fact]
        public async Task FoundWordInWordField()
        {
            var input = "word";
            var res = await Business.FindEnglish(input);

            Assert.Equal(res.Word.ToLower(), input);
        }
        [Fact]
        public async Task FoundWordInFormsField()
        {
            var input = "words";
            var res = await Business.FindEnglish(input);
            Assert.NotNull(res);
        }

    }

}
