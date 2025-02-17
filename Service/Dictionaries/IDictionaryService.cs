﻿using Entities.Form.Users;
using Entities.Model.Dictionaries;
using Entities.Model.Users;
using Entities.Response.Dictionaries;
using Entities.Response.Users;

public interface IDictionaryService
{
    Task SeedDictionaryData();
    Task<List<string>> GetSimilarWords(string input, int length);
    Task<REnglishPersian> FindEnglish(string input);
    Task<RSuggestWord> SuggestWord(int UserId);
}
