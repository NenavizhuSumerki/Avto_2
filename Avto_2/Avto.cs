using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace ConsoleApp4
{
    internal class Avto
    {
        protected float temp0;
        protected float time;
        protected float alltime;
        protected int osh;
        protected int chel;
        protected float km;
        protected float povt;
        protected bool vrem;
        protected float dop;
        protected float way;
        protected float result;
        protected float bak;
        protected float ras;
        protected float mov;
        protected float scor;
        protected float dop_massa;
        protected float max_bak;
        protected int vib;

        protected virtual void info()
        {
            osh = 0;
            Console.WriteLine("Количество бензина в баке: ");
            bak = float.Parse(Console.ReadLine());
            if (bak < 0 || bak > 1300)
            {
                Console.Clear();
                Console.WriteLine("Вы ввели неправильное значение.");
                info();
            }
            else
            {
                max_bak = bak;
            }
            Console.WriteLine("Расход топлива на 100 км: ");
            ras = float.Parse(Console.ReadLine());
            if (ras < 0 || ras > bak)
            {
                Console.Clear();
                Console.WriteLine("Вы ввели неправильное значение.");
                info();
            }
            Console.WriteLine("Сколько проехал до этого: ");
            mov = float.Parse(Console.ReadLine());
            if (mov < 0)
            {
                Console.Clear();
                Console.WriteLine("Вы ввели неправильное значение.");
                info();
            }
        }
        protected void out_info(float allmassa)
        {
            Console.WriteLine("Количество бензина в баке: " + bak);
            Console.WriteLine("Расход бензина за 100 км езды: " + ras);
            Console.WriteLine("Расстояние, которое проехал до этого момента: " + mov);
            Console.WriteLine("Максимальная масса транспорта: " + (allmassa));
            Console.WriteLine("Максимальная скорость автобуса: " + scor);
        }
        protected virtual float F(float massa)
        {
            result = (scor * massa) / temp0;
            return result;
        }
        protected float distance_znach(float km, float allmassa, float massa)
        {
            km -= ((float)Math.Pow(scor, 2) * allmassa) / (2 * F(massa));
            way = (bak - ((((float)Math.Pow(scor, 2) * allmassa) / (2 * F(massa)) * (ras / 100)))) / (ras / 100);
            result = way - km;
            return result;
        }

        protected void move5(float allmassa, float massa)
        {
            do
            {
                Random rnd = new Random();
                km = rnd.Next(10, 1500);
                do
                {
                    povt = 0;
                    Console.WriteLine("Авто должно проехать: " + Math.Round(km));
                    if (distance_znach(km, allmassa, massa) < 0)
                    {
                        vrem = true;
                        time = way / scor + (scor * allmassa / F(massa));
                        way = ((float)Math.Pow(scor, 2) * allmassa) / (2 * F(massa)) + time * scor;
                        alltime += time;
                        mov += way;
                        km -= way;
                        bak = 0;
                        Console.WriteLine($"Пройдено за этот раз {Math.Round(way)}");
                        Console.WriteLine($"Пройдено всего {Math.Round(mov)}");
                        Console.WriteLine($"Осталось топлива {Math.Round(bak)}");
                        Console.WriteLine($"Время всего пути {Math.Floor(alltime)} часов {Math.Round((Math.Round(alltime, 2) - Math.Floor(alltime)) * 60)} минуты");
                        Console.WriteLine($"Время в этом пути {Math.Floor(time)} часов {Math.Round((Math.Round(time, 2) - Math.Floor(time)) * 60)} минуты");
                        Console.WriteLine();
                        do
                        {
                            osh = 0;
                            Console.WriteLine("Топливо закончилось. Хотите заправить? Если да, то введите 1, если нет - 2");
                            vib = Convert.ToInt32(Console.ReadLine());
                            if (vib == 1)
                            {
                                Console.WriteLine("Введите, сколько хотите залить в бак.");
                                dop = float.Parse(Console.ReadLine());
                                if ((dop < max_bak || dop == max_bak) && dop > 0)
                                {
                                    bak += dop;
                                    povt++;
                                }
                                else if (dop == 0)
                                {
                                    Console.WriteLine("Вы ничего не заправляете, а значит авто никуда не едет.");
                                    osh = 2;
                                }
                                else
                                {
                                    Console.WriteLine("Введеное число превышает максимальный объем бака.");
                                    osh = 2;
                                }
                            }
                            else if (vib == 2)
                            {
                                osh++;
                            }
                            else
                            {
                                Console.WriteLine("Вы ввели не то число.");
                                osh = 2;
                            }
                        } while (osh == 2);
                    }
                    else
                    {
                        time = (km - ((float)Math.Pow(scor, 2) * allmassa) / (2 * F(massa))) / scor + (scor * allmassa / F(massa));
                        bak -= km * (ras / 100);
                        alltime += time;
                        mov += km;
                        Console.WriteLine($"Пройдено всего {Math.Round(mov)}");
                        Console.WriteLine($"Осталось топлива {Math.Round(bak)}");
                        Console.WriteLine($"Время всего пути {Math.Floor(alltime)} часов {Math.Round((Math.Round(alltime, 2) - Math.Floor(alltime)) * 60)} минуты");
                        Console.WriteLine($"Время в этом пути {Math.Floor(time)} часов {Math.Round((Math.Round(time, 2) - Math.Floor(time)) * 60)} минуты");
                        Console.WriteLine();
                        time = 0;
                    }
                } while (povt == 1);
            } while (osh == 0);
        }

    }
}