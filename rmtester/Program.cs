

using RMDatabase;

namespace rmtester
{
    
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            DB.sqLiteFile = "test.rmtree";

            using (var db = new DB())
            {
                foreach(var  address in db.AddressTables)
                {
                    Console.WriteLine(address.Name);
                }                    
            }            
        }
    }
}
