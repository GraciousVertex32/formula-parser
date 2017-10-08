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
using System.Diagnostics;

namespace Caculator01
{
    public partial class Form1 : Form
    {

        List<double> Numberlist = new List<double>(15);
        List<string> numberlist = new List<string>(15);
        List<char> methodlist = new List<char>(15);
        public char chr = new char();//储存当前运算符
        public int stringindex = 0;//式子的索引
        public int methodindex = 0;//计算符的索引
        public double result = new double();//保存结果
        public string str = "";//储存，操作输入的字符串
        public string currentpriority = "";//当前最高计算优先级片段

        public Form1()
        {
            InitializeComponent();
        }

        public void Priority()//优先级分拣、导出高优先级片段后导入高优先级片段的结果
        {
            int left = 0;//左括号的索引
            int right = 0;//右括号的索引
            while (str.Contains(")"))//循环直到式子中没有括号后停止
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '(') { left = i; }//找到右括号前的最后的左括号
                    if (str[i] == ')') { right = i; break; }//找到右括号后循环停止
                }
                currentpriority = str.Substring(left + 1, right - left - 1);//最高计算优先级片段为括号内内容
                str=str.Remove(left, right - left + 1);//从式子中移除这个括号中的内容       
                str=str.Insert(left, Formulasolver().ToString());//从移除括号处插入括号应得的结果
            }
        }
        //处理括号

        public double Formulasolver()
        {
            DataSorter();
            double exponent = 0;
            double multiply = 0;
            double divide = 0;
            double add = 0;
            double subtract = 0;
            for (int i = 0; i < methodlist.Count; i++)//遍历所有乘方
            {
                if(i < methodlist.Count && methodlist[i]=='^')//乘方方法
                {
                    exponent = Math.Pow(Numberlist[i], Numberlist[i + 1]);//计算乘方符号前后数字的乘方
                    Numberlist[i] = exponent;//将结果代入double数组，计算符号前对应数字的位置
                    for(int following =i+1;following<Numberlist.Count-1;following++)
                    {
                        Numberlist[following] = Numberlist[following + 1];//后方数据进一位，替换计算符号后一位
                        methodlist[following-1] = methodlist[following ];
                    }
                    Numberlist.RemoveAt(Numberlist.Count-1);//删除最后一位多余数字
                    methodlist.RemoveAt(methodlist.Count-1);//删除最后一位多余运算符
                }
            }
            for (int i = 0; i < methodlist.Count; i++)//遍历所有乘除法
            {
                if (i < methodlist.Count && methodlist[i] == '*')//乘法方法
                {
                    multiply = Numberlist[i] * Numberlist[i + 1];
                    Numberlist[i] = multiply;
                    for (int following = i + 1; following < Numberlist.Count - 1; following++)
                    {
                        Numberlist[following] = Numberlist[following + 1];
                        methodlist[following - 1] = methodlist[following];
                    }
                    Numberlist.RemoveAt(Numberlist.Count-1);
                    methodlist.RemoveAt(methodlist.Count-1);
                }
                if (i<methodlist.Count && methodlist[i] == '/')//除法方法
                {
                    divide= Numberlist[i] / Numberlist[i + 1];
                    Numberlist[i] = divide;
                    for (int following = i + 1; following < Numberlist.Count - 1; following++)
                    {
                        Numberlist[following] = Numberlist[following + 1];
                        methodlist[following - 1] = methodlist[following];
                    }
                    Numberlist.RemoveAt(Numberlist.Count-1);
                    methodlist.RemoveAt(methodlist.Count-1);
                }
            }
            for (int i = 0; i < methodlist.Count; i++)//遍历所有加减法
            {
                if (i < methodlist.Count && methodlist[i] == '+')//加法方法
                {
                    add= Numberlist[i] + Numberlist[i + 1];
                    Numberlist[i] = add;
                    for (int following = i + 1; following < Numberlist.Count - 1; following++)
                    {
                        Numberlist[following] = Numberlist[following + 1];
                        methodlist[following - 1] = methodlist[following];
                    }
                    Numberlist.RemoveAt(Numberlist.Count-1);
                    methodlist.RemoveAt(methodlist.Count-1);
                }
                if (i < methodlist.Count && methodlist[i] == '-')//减法方法
                {
                    subtract= Numberlist[i] - Numberlist[i + 1];
                    Numberlist[i] = subtract;
                    for (int following = i + 1; following < Numberlist.Count - 1; following++)
                    {
                        Numberlist[following] = Numberlist[following + 1];
                        methodlist[following - 1] = methodlist[following];
                    }
                    Numberlist.RemoveAt(Numberlist.Count-1);
                    methodlist.RemoveAt(methodlist.Count-1);
                }
            }//理论上Numberlist只有第一位有数字了
            return Numberlist[0];
        }

        public void DataSorter()//储存数据与符号在两个数组中
        {
            stringindex = 0;
            methodindex = 0;
            int breakpoint = 0;
            int numberamount = 0;
            for (int i = 0; i < currentpriority.Length; i++)//确定当前片段数字的量
            {
                if(
                    currentpriority[i] == '+' ||
                    currentpriority[i] == '-' ||
                    currentpriority[i] == '*' ||
                    currentpriority[i] == '/' ||
                    currentpriority[i] == '^'
                    )
                {
                    numberamount++;
                }
            }
            numberlist.Clear();
            Numberlist.Clear();
            methodlist.Clear();
            for (int i=0;i< numberamount+1;i++)//数字list初始化
            {
                numberlist.Add("");
                Numberlist.Add(0);
            }
            for (int i = 0; i <numberamount; i++)//运算符list初始化
            {
                methodlist.Add('n');
            }
            while (breakpoint < currentpriority.Length)//确保输入字符串读取完毕
            {
                for (int i = breakpoint; i < currentpriority.Length; i++)//从断点处继续读取
                {
                    if (currentpriority[i] <= '9' && currentpriority[i] >= '0')//寻找数字
                    {
                        numberlist[stringindex] = numberlist[stringindex] + currentpriority[i];//将找到的数字存入数组中位置
                        breakpoint = i + 1;//下一位始终默认为断点
                    }
                    else
                    {
                        break;//遇到非数字退出寻找数字循环
                    }                 
                }
                stringindex++;//储存数字的数组index+1,为下一组数字做准备
                label2.Text = numberlist[stringindex - 1];//debug用显示
                if (breakpoint < currentpriority.Length)//若断点未走到字符串尽头，寻找运算符
                {
                    char chr = new char();
                    chr = currentpriority[breakpoint];//断点breakpoint位置为运算符应在位置
                    switch (chr)//将此处运算符存入运算符数组
                    {
                        case '+':
                            methodlist[methodindex] = '+';
                            break;
                            case '-':
                                methodlist[methodindex] = '-';
                                break;
                            case '*':
                                methodlist[methodindex] = '*';
                                break;
                            case '/':
                                methodlist[methodindex] = '/';
                                break;
                            case '^':
                                methodlist[methodindex] = '^';
                                break;
                            default:
                                MessageBox.Show("Invalid Input");
                                break;
                    }
                    methodindex++;//储存运算符的数组index+1,为下一个运算符做准备
                }
                breakpoint++;//记录运算符后断点位置+1
                
            }
            for(int i=0;i<numberlist.Count;i++)//将string数组转换成double数组
            {
                Numberlist[i] = double.Parse(numberlist[i]);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            str = "("+textBox1.Text+")";
            Priority();
            label1.Text = str;
            result = double.Parse(str);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase+"/save.txt";
            FileStream fs = new FileStream(path,FileMode.Create); //这里有个异常需要处理，使用try和catch解决文件检查问题
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(result);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //这个是从哪里冒出来的？
        }
       
        #region IOControl 文件控制，比如文件传输流，文件写入和读取
        class IOControl
        {
            public void FileIODO()
            {
                string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                

                FileStream fs = new FileStream(path, FileMode.Create); //这里有个异常需要处理，使用try和catch解决文件检查问题
                StreamWriter sw = new StreamWriter(fs);
                sw.Write("");
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            //保存结果
        }
        #endregion

       
    }
}
