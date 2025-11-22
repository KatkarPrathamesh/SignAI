//using MySql.Data.MySqlClient;
//using System.Data;

//namespace SignAi.Services
//{
//    public class DbConnectionFactory
//    {
//        private readonly IConfiguration _config;

//        public DbConnectionFactory(IConfiguration config)
//        {
//            _config = config;
//        }

//        //public IDbConnection CreateConnection()
//        //{
//        //    var connectionString = _config.GetConnectionString("DefaultConnection");
//        //    Console.WriteLine($"🔗 Using connection: {connectionString}");
//        //    return new MySqlConnection(connectionString);
//        //}

//        public IDbConnection CreateConnection()
//        {
//            var connectionString = _config.GetConnectionString("DefaultConnection");
//            Console.WriteLine($"🔗 Connection String: {connectionString}");
//            var conn = new MySqlConnection(connectionString);
//            conn.Open();
//            Console.WriteLine("✅ MySQL connection opened successfully!");
//            return conn;
//        }

//    }
//} 
using MySql.Data.MySqlClient;
using System.Data;

namespace SignAI.Services
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _config;
        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection CreateConnection()
        {
            var conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            conn.Open();
            return conn;
        }

        public async Task<MySqlConnection> CreateConnectionAsync()
        {
            var conn = new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            await conn.OpenAsync();
            return conn;
        }
    }
}

