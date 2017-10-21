using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RadisTest
{
    class Program
    {
        static void Main(string[] args)
        {
           var program = new Program();
            Console.WriteLine("Saving random data in cash.");
            program.SaveBigData();

            Console.WriteLine("Reading data from cache");
            program.ReadData();

            Console.ReadLine();
        }

        private void ReadData()
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase();
            var deviceCount = 10000;
            for (int i = 0; i < deviceCount; i++)
            {
                var value = cache.StringGet($"Device_Status:{i}");
                Console.WriteLine($"Valor={value}");
            }
        }

        private void SaveBigData()
        {
            var deviceCount = 10000;
            var rand = new Random();
            var cache = RedisConnectorHelper.Connection.GetDatabase();

            for (int i = 0; i < deviceCount; i++)
            {
                var value = rand.Next(0, 10000);
                cache.StringSet($"Device_Status:{i}", value);
            }
        }
    }

    public class RedisConnectorHelper
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        public static ConnectionMultiplexer Connection => LazyConnection.Value;

        static RedisConnectorHelper()
        {
            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost"));
        }
    }
}
