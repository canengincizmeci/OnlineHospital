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
        public static async Task SetAsync(string key, string value, TimeSpan expiry)
        {
            var db = GetDatabase();
            await db.StringSetAsync(key, value, expiry);
        }

        public static async Task<string?> GetAsync(string key)
        {
            var db = GetDatabase();
            return await db.StringGetAsync(key);
        }
        public static async Task SetDateTimeAsync(string key, DateTime value, TimeSpan expiry)
        {
            var db = GetDatabase();
            string dateTimeString = value.ToString("o");
            await db.StringSetAsync(key, dateTimeString, expiry);
        }

        public static async Task<DateTime?> GetDateTimeAsync(string key)
        {
            var db = GetDatabase();
            string? dateTimeString = await db.StringGetAsync(key);

            if (dateTimeString != null && DateTime.TryParse(dateTimeString, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime result))
            {
                return result;
            }
            return null;
        }

    }
}
