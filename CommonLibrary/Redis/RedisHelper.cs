using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Redis
{
    public static class RedisHelper
    {
        private static readonly Lazy<ConnectionMultiplexer> _connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost:6379"));

        public static IDatabase GetDatabase()
        {

            return _connection.Value.GetDatabase();
        }

    }
}
