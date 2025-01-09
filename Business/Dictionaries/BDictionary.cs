using Common;
using Common.Api;
using Common.Extensions;
using Data;
using Entities.Model.Dictionaries;
using Entities.Response.Dictionaries;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Business.Dictionaries
{
    public class BDictionary : BaseBusiness, IDictionaryService
    {
        public BDictionary(DreamVocabBoxContext DataBase, IConfiguration configuration) : base(DataBase, configuration)
        {
        }
        public async Task<RSuggestWord> SuggestWord(int UserId)
        {
            var dictionaryWord = await (from d in DataBase.DictionaryEnglishToEnglishs
                                        join v in DataBase.Vocabularies
                                        on d.Word.ToLower() equals v.Word.ToLower() into vocabGroup
                                        from vg in vocabGroup.DefaultIfEmpty()
                                        where vg == null || vg.UserId != UserId
                                        orderby Guid.NewGuid()
                                        select d)
                                        .FirstOrDefaultAsync();
            
            if (dictionaryWord == null)
                throw new AppException(ApiResultStatusCode.NotFound);

            return new RSuggestWord
            {
                Word = dictionaryWord.Word.ToUppercaseFirst(),
                Definition = dictionaryWord.Definition
            };
        }
        public async Task<REnglishPersian> FindEnglish(string input)
        {
            var en = await DataBase.DictionaryEnglishToEnglishs.FirstOrDefaultAsync(x => x.Word.ToLower().Equals(input.ToLower()));
            var fa = await DataBase.DictionaryEnglishToPersians.FirstOrDefaultAsync(x => x.Word.ToLower().Equals(input.ToLower()));
            if (en == null && fa == null)
                throw new AppException(ApiResultStatusCode.NotFound);
            return new REnglishPersian()
            {
                Word = input,
                DefinitionEn = en?.Definition,
                DefinitionFa = fa?.Definition,
                Forms = en?.Forms,
            };
        }
        public async Task<List<DictionaryEnglishToEnglish>> SearchEnglishToEnglish(string input, int length)
        {
            return await DataBase.DictionaryEnglishToEnglishs.Where(x => x.Word.ToLower().StartsWith(input)).Take(length).ToListAsync();
        }
        public async Task SeedDictionaryData()
        {
            if (!DataBase.DictionaryEnglishToPersians.Any() || !DataBase.DictionaryEnglishToEnglishs.Any() || !DataBase.DictionaryPersianToEnglishs.Any() || !DataBase.IdiomsEnglishToPersians.Any())
            {
                var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
                string sqliteFilePath = Path.Combine($"{directory.FullName}\\", "AdditionalItems", "Dictionary.db");
                if (!File.Exists(sqliteFilePath))
                    throw new FileNotFoundException("SQLite file not found", sqliteFilePath);

                using (var sqliteConnection = new SqliteConnection($"Data Source={sqliteFilePath}"))
                {
                    SQLitePCL.Batteries.Init();
                    sqliteConnection.Open();

                    var reader = await ReadDataAsync("SELECT * FROM DictionaryEnglishToPersians", sqliteConnection);

                    var dictionaryEnglishToPersians = new List<DictionaryEnglishToPersian>();
                    while (await reader.ReadAsync())
                    {
                        dictionaryEnglishToPersians.Add(new DictionaryEnglishToPersian
                        {
                            Word = reader.GetString(0),
                            Definition = reader.GetString(1)
                        });
                    }
                    await DataBase.DictionaryEnglishToPersians.AddRangeAsync(dictionaryEnglishToPersians);

                    reader = await ReadDataAsync("SELECT * FROM DictionaryPersianToEnglishs", sqliteConnection);
                    var dictionaryPersianToEnglishes = new List<DictionaryPersianToEnglish>();
                    while (await reader.ReadAsync())
                    {
                        dictionaryPersianToEnglishes.Add(new DictionaryPersianToEnglish
                        {
                            Word = reader.GetString(0),
                            Definition = reader.GetString(1)
                        });
                    }
                    await DataBase.DictionaryPersianToEnglishs.AddRangeAsync(dictionaryPersianToEnglishes);

                    reader = await ReadDataAsync("SELECT * FROM DictionaryEnglishToEnglishs", sqliteConnection);
                    var dictionaryEnglishToEnglish = new List<DictionaryEnglishToEnglish>();
                    while (await reader.ReadAsync())
                    {
                        dictionaryEnglishToEnglish.Add(new DictionaryEnglishToEnglish
                        {
                            Word = reader.GetString(0),
                            Definition = reader.GetString(1),
                            Forms = !reader.IsDBNull(2) ? reader.GetString(2) : null
                        });
                    }
                    await DataBase.DictionaryEnglishToEnglishs.AddRangeAsync(dictionaryEnglishToEnglish);

                    reader = await ReadDataAsync("SELECT * FROM IdiomsEnglishToPersians", sqliteConnection);
                    var idiomsEnglishToPersian = new List<IdiomsEnglishToPersian>();
                    while (await reader.ReadAsync())
                    {
                        idiomsEnglishToPersian.Add(new IdiomsEnglishToPersian
                        {
                            Phrase = reader.GetString(0),
                            Definition = reader.GetString(1),
                            Base = reader.GetString(2)
                        });
                    }
                    await DataBase.IdiomsEnglishToPersians.AddRangeAsync(idiomsEnglishToPersian);

                }

                DataBase.SaveChanges();
            }
        }
        private async Task<SqliteDataReader> ReadDataAsync(string query, SqliteConnection sqliteConnection)
        {
            var command = new SqliteCommand(query, sqliteConnection);
            return await command.ExecuteReaderAsync();
        }
    }
}
