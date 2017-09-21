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

namespace Caculator01
{
    public partial class Form1 : Form
    {
        public string[] tempstr = new string[10];
        public char method1 = 'n';
        public char method2 = 'n';
        public int stringindex = 0;
        public double result = new double();
        public Form1()
        {
            InitializeComponent();
        }

        public double Basicfunction()
        {
            double temp1 = double.Parse(tempstr[0]);
            double temp2 = double.Parse(tempstr[1]);
            switch (method1)
            {
                case '+':
                    return temp1 + temp2;
                    break;
                case '-':
                    return temp1 - temp2;
                    break;
                case '*':
                    return temp1 * temp2;
                    break;
                case '/':
                    return temp1 / temp2;
                    break;
                case '^':
                    return Math.Pow(temp1,temp2);
                    break;
                default:
                    Console.WriteLine("Must be Seriously Wrong");
                    return 55555555555;
                    break;
            }
        }
        //计算部分


        private void button4_Click(object sender, EventArgs e)
        {
            stringindex = 0;
            string str = textBox1.Text;
            int breakpoint=0;
            while(breakpoint<str.Length)
            {
                for (int i = breakpoint; i < str.Length; i++)
                {
                    if (str[i] <= '9' && str[i] >= '0')
                    {
                        tempstr[stringindex] = tempstr[stringindex]+ str[i];
                        breakpoint = i+1;
                    }
                    else
                    {
                        break;
                    }
                }
                stringindex++;
                //提取第一组数字
                label2.Text = tempstr[stringindex-1];
               
                if (breakpoint < str.Length)
                {
                    char chr = new char();
                    chr = str[breakpoint];
                    switch (chr)
                    {
                        case '+':
                            method1 = '+';
                            break;
                        case '-':
                            method1 = '-';
                            break;
                        case '*':
                            method1 = '*';
                            break;
                        case '/':
                            method1 = '/';
                            break;
                        case '^':
                            method1 = '^';
                            break;
                        default:
                            Console.WriteLine("Invalid Expression");
                            break;
                    }
                }
                breakpoint++;
            }

            label1.Text = Basicfunction().ToString();
            //调用计算函数，输出
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(result);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        //保存结果
    }
}
