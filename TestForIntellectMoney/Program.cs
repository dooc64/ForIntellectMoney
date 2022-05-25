using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForIntellectMoney
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите доллары");
            string dol = Console.ReadLine();
            Console.WriteLine("Введите центы, если их нету введите просто 0");
            string cent = "," + Console.ReadLine();
            double sum = double.Parse(dol + cent);
            Converter converter = new Converter();

            Console.WriteLine(converter.Start(sum));

            Console.ReadLine();
        }
    }
}
