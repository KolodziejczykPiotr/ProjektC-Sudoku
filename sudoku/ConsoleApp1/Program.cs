using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Pier(4);
            Console.WriteLine(Pier(13));
            Console.ReadLine();
        }


        public static bool Pier(int n)
        {
            if (n <= 1)
                throw new Exception();
            int i = 2;
            while (n % i > 0&&i*i<=n)
            {
                i++;
            }
            if (n % i == 0) return false;

            return true;
            
        }
    }
}
