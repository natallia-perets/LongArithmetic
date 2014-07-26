using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongArithmetic
{
    class Program
    {
        static void Main(string[] args)

    
        {
            VeryBigInteger bi = new VeryBigInteger("1056456346");
            VeryBigInteger bi2 = new VeryBigInteger("-203463463463463");
     
            VeryBigInteger res = bi * bi2;
            Console.WriteLine(res);
            Console.ReadLine();
        }
    }
}
