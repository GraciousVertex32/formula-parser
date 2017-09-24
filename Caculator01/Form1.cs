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
using System.Collections;

namespace Caculator01
{
    public partial class Form1 : Form
    {
        string currentpriority = "";//当前最高计算优先级片段
        public void Priority()//优先级分拣、导出高优先级片段后导入高优先级片段的结果
        {
            int left = 0;//左括号的索引
            int right = 0;//右括号的索引
            
            while(str.Contains(")"))//循环直到式子中没有括号后停止
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '(') { left = i; }//找到右括号前的最后的左括号
                    if (str[i] == ')') { right = i; break; }//找到右括号后循环停止
                }
                str.Remove(left, right - left + 1);//从式子中移除这个括号中的内容
                currentpriority = str.Substring(left + 1, right - left - 1);//最高计算优先级片段为括号内内容
                str.Insert(left,not finished yet);//从移除括号处插入括号应得的结果
            }
        }
        public double formulasolver()
        {
           currentpriority
        }
        //需进行的计算按优先顺序拍好
        public string[] tempstr = new string[10];
        public char chr = new char();
        public char method1 = 'n';
        public char method2 = 'n';
        public int stringindex = 0;
        public double result = new double();
        public string str = "";
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
            str = textBox1.Text;
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
            for(int i=0;i<=9;i++)
            {
                tempstr[i] = "";
            }
            //初始化数组
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
