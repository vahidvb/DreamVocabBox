using StackExchange.Redis;
namespace Common.CacheManager
{

    public class RedisConnectorHelper
    {
        private static readonly object LockObject = new();
        private static ConnectionMultiplexer _connection;
        private readonly string _redisPassword;
        private readonly string _redisServer;

        public RedisConnectorHelper(string redisPassword, string redisServer)
        {
            _redisPassword = redisPassword;
            _redisServer = redisServer;
        }

        public ConnectionMultiplexer Connection
        {
            get
            {
                if (_connection == null || !_connection.IsConnected)
                {
                    lock (LockObject)
                    {
                        if (_connection == null || !_connection.IsConnected)
                        {
                            var configurationOptions = new ConfigurationOptions
                            {
                                EndPoints = { _redisServer },
                                Password = _redisPassword,
                                AbortOnConnectFail = false
                            };

                            _connection = ConnectionMultiplexer.Connect(configurationOptions);
                        }
                    }
                }

                return _connection;
            }
        }
    }

}