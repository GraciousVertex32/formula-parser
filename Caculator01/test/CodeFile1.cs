using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace test
{
    class Program
    {
        static void Main(string[] arges)
        {
            string str;
            string number = "";
            str = Console.ReadLine();
            for (int i = 0; i <= str.Length; i++)
            {
                if (str[i] <= 9 && str[i] >= 0)
                {
                    number = "123";
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine(number);
        }
    }
}