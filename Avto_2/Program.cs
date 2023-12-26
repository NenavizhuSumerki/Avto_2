using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Programm
    {
        static void Main()
        {
            int osh;
            Gruz gryz = new Gruz();
            Bus avtobus = new Bus();
            osh = 0;
            Console.WriteLine("Введите цифру соответсвующего транспорта: 1 - грузовик, 2 - автобус. ");
            int var = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            if (var == 1)
            {
                gryz = new Gruz();
                gryz.vivod_info();
                Main();
            }
            else if (var == 2)
            {
                avtobus = new Bus();
                avtobus.vivod_info();
                Main();
            }

            else if (var < 0 || var > 3)
            {
                Console.WriteLine("Вы ввели не то число.");
                Main();
            }
        }
    }
}
