using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Utillities.Cachecontrol
{
    public static class RedisCache
    {
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        static RedisCache()
        {
            ConfigurationOptions configuration = new ConfigurationOptions();
            configuration.EndPoints.Add("127.0.0.1:6379");
            configuration.DefaultDatabase = Convert.ToInt32("1");
            RedisCache.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(configuration);
            });
        }
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
