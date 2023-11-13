using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Тестирование
{
    public class Records
    {
        public static void Record(double time, int symbols, User user)
        {
            user.min = symbols / time * 60;
            user.sec = symbols / time;
            Console.WriteLine("Символов в минуту: " + user.min);
            Console.WriteLine("Символов в секунду: " + user.sec);
            List<User> users = new List<User>();
            string path = $"{Environment.CurrentDirectory}\\stats.json";
            if (File.Exists(path))
            {
                string file = File.ReadAllText(path);
                users = JsonConvert.DeserializeObject<List<User>>(file);
                users.Add(user);
            }
            else
            {
                users = [user];
            }
            
            string json = JsonConvert.SerializeObject(users);
            File.WriteAllText(path, json);
            Console.WriteLine("Имя\tСимволов в секунду\tСимволов в минуту");
            foreach (User i in users)
            {
                Console.WriteLine(i.name + "\t" + i.sec + "\t" + i.min);
            }
        }
    }
}
