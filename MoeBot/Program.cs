using System;

namespace MoeClorito
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                new MoeBot().MainAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }        
    }
}
