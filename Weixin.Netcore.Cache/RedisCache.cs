using StackExchange.Redis;
using System;
using Weixin.Netcore.Cache.Exceptions;

namespace Weixin.Netcore.Cache
{
    /// <summary>
    /// Redis缓存
    /// </summary>
    public class RedisCache : ICache
    {
        #region .ctor
        private static IDatabase _database;
        private static ConnectionMultiplexer _connection;

        public RedisCache(string serverHostr, int serverPort, string password = null)
        {
            _connection = ConnectionMultiplexer.Connect($"{serverHostr}:{serverPort}{(string.IsNullOrEmpty(password) ? string.Empty : string.Concat(",password=", password))}");
            _database = Connection.GetDatabase();
        }
        #endregion

        public static ConnectionMultiplexer Connection
        {
            get
            {
                if (_connection == null)
                {
                    throw new RedisConnectionNullException("Redis连接为null");
                }
                if (!_connection.IsConnected)
                {
                    throw new RedisNotConnectException("Redis未连接");
                }

                return _connection;
            }
        }

        public string Get(string key)
        {
            return _database.StringGet(key);
        }

        public void Set(string key, string value)
        {
            _database.StringSet(key, value);
        }

        public void Set(string key, string value, TimeSpan ts)
        {
            _database.StringSet(key, value, ts);
        }
    }
}
