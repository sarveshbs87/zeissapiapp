using System;

namespace ZeissAPIApp.Models
{
    public class Products
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int StockAvailable { get; set; }

        public static string GenerateUniqueId()
        {
            Random random = new Random();
            int uniqueNumber = random.Next(100000, 999999);
            return uniqueNumber.ToString();
        }


    }
}
