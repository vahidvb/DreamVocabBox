using Common.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Json;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Common.CacheManager
{
    public static class CacheManager
    {
        private static CacheType cacheType;
        private static string redisPassword;
        private static int cacheDurationInMinutes;
        private static readonly object CacheLock = new();
        private static IDatabase redisCache;
        private static readonly IMemoryCache MemoryCache = new MemoryCache(new MemoryCacheOptions());

        public static void Initialize(CacheType type, IConfiguration configuration)
        {
            cacheType = type;
            redisPassword = configuration["CacheSettings:RedisPassword"];
            cacheDurationInMinutes = int.Parse(configuration["CacheSettings:CacheDurationInMinutes"]);
            var redisServer = configuration["CacheSettings:RedisServer"];

            if (type == CacheType.RedisCache)
            {
                var connection = new RedisConnectorHelper(redisPassword, redisServer).Connection;
                redisCache = connection.GetDatabase();
                var server = connection.GetServer(redisServer);
                server.FlushDatabase();
            }
        }

        public static void Add<T>(string key, T data)
        {
            if (data == null || string.IsNullOrWhiteSpace(key)) return;

            var cacheKey = key.Trim().ToLower();
            lock (CacheLock)
            {
                switch (cacheType)
                {
                    case CacheType.MemoryCache:
                        MemoryCache.Set(cacheKey, data, TimeSpan.FromMinutes(cacheDurationInMinutes));
                        break;
                    case CacheType.RedisCache:
                        var serializedData = JsonSerializer.Serialize(data);
                        var compressedData = serializedData.Compress();
                        var base64Data = Convert.ToBase64String(compressedData);
                        redisCache.StringSet(cacheKey, base64Data, TimeSpan.FromMinutes(cacheDurationInMinutes));
                        break;
                }
            }
        }
        public static T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return default;

            var cacheKey = key.Trim().ToLower();
            return cacheType switch
            {
                CacheType.MemoryCache => MemoryCache.TryGetValue(cacheKey, out T value) ? value : default,
                CacheType.RedisCache => redisCache.KeyExists(cacheKey)
                    ? (Convert.FromBase64String(redisCache.StringGet(cacheKey))).Decompress<T>()
                    : default,
                _ => default
            };
        }
        public static void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return;

            var cacheKey = key.Trim().ToLower();
            lock (CacheLock)
            {
                switch (cacheType)
                {
                    case CacheType.MemoryCache:
                        MemoryCache.Remove(cacheKey);
                        break;
                    case CacheType.RedisCache:
                        redisCache.KeyDelete(cacheKey);
                        break;
                }
            }
        }

        public static void Update<T>(T data, string key)
        {
            if (data == null || string.IsNullOrWhiteSpace(key)) return;

            var cacheKey = key.Trim().ToLower();
            lock (CacheLock)
            {
                Remove(cacheKey);
                Add(cacheKey, data);
            }
        }

        public static void UpdateExpireTime<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return;

            var cacheKey = key.Trim().ToLower();
            lock (CacheLock)
            {
                var existingData = Get<T>(cacheKey);
                if (existingData != null)
                {
                    Remove(cacheKey);
                    Add(cacheKey, existingData);
                }
            }
        }
    }

}
