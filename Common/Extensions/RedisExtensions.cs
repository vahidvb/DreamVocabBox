using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class RedisExtensions
    {
        public static TEntity My_FirstOrDefault<TEntity>(this RedisClient redis, Enum_Redis redisKeyPrefix, Func<string, bool> predicate)
            where TEntity : class
        {
            TEntity result = null;

            var entity = GetStringValues(redis, redisKeyPrefix)
                        .FirstOrDefault(predicate);

            if (entity != null)
                result = JsonConvert.DeserializeObject<TEntity>(entity.ToString());

            return result;
        }

        public static IEnumerable<TEntity> My_Where<TEntity>(this RedisClient redis, Enum_Redis redisKeyPrefix, Func<string, bool> predicate)
            where TEntity : class
        {
            IEnumerable<TEntity> result = new TEntity[] { };

            var entities = GetStringValues(redis, redisKeyPrefix)
                        .Where(predicate);

            if (entities.Count() != 0)
                result = entities
                         .Select(s => JsonConvert.DeserializeObject<TEntity>(s.ToString()));

            return result;
        }

        public static bool My_Any(this RedisClient redis, Enum_Redis redisKeyPrefix, Func<string, bool> predicate)
        {
            var result = GetStringValues(redis, redisKeyPrefix)
                        .Any(predicate);

            return result;
        }

        public static List<string> GetStringValues(RedisClient redis, Enum_Redis redisKeyPrefix)
        {
            var strings = redis.Scan(0, AppConst.scanCount, $"{redisKeyPrefix}*")
                         .AsStrings();

            var values = redis.GetValues(strings);

            return values;
        }

        public static string My_CreateKey<TId>(this RedisClient redis, TId id, params string[] keys)
        {
            string result = null;

            foreach (var key in keys)
                result += $"{key}:";

            return $"{result}{id}";
        }
    }
}
