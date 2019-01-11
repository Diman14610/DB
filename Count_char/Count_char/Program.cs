using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Count_char
{

    class Program
    {
        //static List<string[]> vs = new List<string[]>();
        //static List<string> pat = new List<string>();
        static void Main(string[] args)
        {
            string[] dir = Directory.GetFiles(Environment.CurrentDirectory + "\\count");
            //    foreach (string s in dir)
            //    {
            //        vs.Add(File.ReadAllLines(s));
            //        pat.Add(Path.GetFileName(s));
            //    }
            //    int reb = 0;
            //    foreach (string[] ss in vs)
            //    {
            //        //Regex.IsMatch(s, @"\w*дата\w*")
            //        StreamWriter fs = new StreamWriter(pat[reb] + ".txt");
            //        foreach (string ssss in ss)//WindowsFormsHost
            //        {
            //            if (ssss.Contains("ComboBox") | ssss.Contains("TextBox") | ssss.Contains("WindowsFormsHost") |
            //                ssss.Contains("TextBlock") | ssss.Contains("Button") | ssss.Contains("DatePicker"))
            //            fs.WriteLine(Regex.Replace(ssss, @"^(\w*x:Name=" + "\"" + @"\w*" + "\")", ""));//x:Name="textbox_representive_lname"
            //        }
            //        fs.Dispose();
            //        reb++;
            //    }
            int i = 0;
            int rr = 1;
            File.WriteAllLines(Environment.CurrentDirectory + "\\name.txt", dir.Select(d => Path.GetFileName(d)));
            string sst = null;
            string sttt1 = null;
            foreach (string s in dir)
            {
                sst += Path.GetFileName(s) + " ";
                sttt1 += Path.GetFileName(s) + ", ";
            }
            File.WriteAllText(Environment.CurrentDirectory + "\\с пробелами.txt", sst);
            File.WriteAllText(Environment.CurrentDirectory + "\\с запятыми.txt", sttt1);
            foreach (string s in dir)
            {
                i += File.ReadAllText(s).Count();
                Console.WriteLine("Количество символов " + File.ReadAllText(s).Count() + "   Имя " + Path.GetFileName(s) + " Индекс " + rr);
                rr++;
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine(i + "  Количество символов");
            Console.ReadKey();


        }
    }
}
