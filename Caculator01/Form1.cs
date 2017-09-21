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
        public static string tempstr1 = null;
        public static string tempstr2 = null;
        public char method1 = 'n';
        public char method2 = 'n';
        
        public double result = new double();
        public Form1()
        {
            InitializeComponent();
        }

        public double Basicfunction()
        {
            double temp1 = double.Parse(tempstr1);
            double temp2 = double.Parse(tempstr2);
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

        private void button4_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            int breakpoint=0;
            for(int i= 0; i<str.Length; i++)
            {
                if(str[i]<='9' && str[i]>='0')
                {
                    tempstr1 = tempstr1 + str[i];
                }
                else
                {
                    breakpoint = i;
                    break;
                }
            }
            label2.Text = tempstr1;
            //提取第一组数字
            switch(str[breakpoint])
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
            //提取第一个运算符
            for (int i = breakpoint+1; i < str.Length; i++)
            {
                if (str[i] <= '9' && str[i] >= '0')
                {
                    tempstr2 = tempstr2 + str[i];
                }
                else
                {
                    breakpoint = i;
                    break;
                }
            }
            label3.Text = tempstr2;
            //提取第二组数字
            label1.Text = Basicfunction().ToString();
        }
    }
}
