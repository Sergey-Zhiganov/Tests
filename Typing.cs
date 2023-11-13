using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Тестирование
{
    public class Typing
    {
        
        public void Main(int y_max, string[] text, User user)
        {
            int is_type = 1;
            bool is_time = true;
            int symbols = 0;
            int x = 0;
            int y = 2;
            Stopwatch stopwatch = new Stopwatch();
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Прошло: ");
            string current_text = text[y - 2];
            Thread timer = new Thread(_ =>
            {
                stopwatch.Start();
                int time = 0;
                while (time <= 58)
                {
                    time = (int)stopwatch.Elapsed.TotalSeconds;
                    int x = Console.GetCursorPosition().Left;
                    int y = Console.GetCursorPosition().Top;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(8, 0);
                    Console.Write(time);
                    Console.SetCursorPosition(x, y);
                    Thread.Sleep(1000);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(8, 0);
                Console.WriteLine(60);
                is_time = false;
                stopwatch.Stop();
            });
            timer.Start();
            while (is_time == true && is_type == 1)
            {
                Thread.Sleep(100);
                ConsoleKeyInfo key = new ConsoleKeyInfo();
                Console.SetCursorPosition(x, y);
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);
                    if (key.KeyChar == current_text[x] && is_time == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(x, y);
                        Console.Write(current_text[x]);
                        symbols += 1;
                        x++;
                        if (x == current_text.Length)
                        {
                            y++;
                            current_text = text[y - 2];
                            x = 0;
                            if (y - 2 > y_max)
                            {
                                stopwatch.Stop();
                                is_type = 2;
                                break;
                            }
                        }
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        stopwatch.Stop();
                        is_type = 0;
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(current_text[x]);
                    }
                }
            }
            try
            {
                timer.Abort();
                timer.Join(500);
            }
            catch
            {
                
            }
            Console.Clear();
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Gray;
            if (is_time == false)
            {
                Console.WriteLine("Время вышло");
            }
            else if (is_type == 0)
            {
                Console.WriteLine("Выполнение теста прервано. Запись результатов не будет выполнена");
                Program.Stop();
            }
            else
            {
                Console.WriteLine("Вы написали весь текст");
            }
            Records.Record(stopwatch.Elapsed.TotalSeconds - 1, symbols, user);

        }
    }
}
