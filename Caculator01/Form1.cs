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

        List<double> Numberlist = new List<double>();
        List<string> numberlist = new List<string>();
        List<char> methodlist = new List<char>();
        public char chr = new char();
        public int stringindex = 0;
        public int methodindex = 0;
        public double result = new double();
        public string str = "";
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
                str.Remove(left, right - left + 1);//从式子中移除这个括号中的内容
                currentpriority = str.Substring(left + 1, right - left - 1);//最高计算优先级片段为括号内内容
                str.Insert(left, Formulasolver().ToString());//从移除括号处插入括号应得的结果
            }
        }
        //处理括号

        public double Formulasolver()
        {
            double exponent = 0;
            for (int i = 0; i < methodlist.Count; i++)
            {
                if(methodlist[i]=='^')//乘方方法
                {
                    exponent = Math.Pow(Numberlist[i], Numberlist[i + 1]);//计算乘方符号前后数字的乘方
                    Numberlist[i] = exponent;//将结果代入double数组，计算符号前对应数字的位置
                    for(int following =i+1;following<Numberlist.Count-1;following++)
                    {
                        Numberlist[following] = Numberlist[following + 1];
                    }//后方数据进一位，替换计算符号后一位
                    Numberlist.RemoveAt(Numberlist.Count);//删除最后一位数字
                }
            }
        }

        public void DataSorter()//储存数据与符号在两个数组中
        {
            stringindex = 0;
            methodindex = 0;
            str = currentpriority;
            int breakpoint = 0;
            while (breakpoint < str.Length)//确保输入字符串读取完毕
            {
                for (int i = breakpoint; i < str.Length; i++)//从断点处继续读取
                {
                    if (str[i] <= '9' && str[i] >= '0')//寻找数字
                    {
                        numberlist[stringindex] = numberlist[stringindex] + str[i];//将找到的数字存入数组中位置
                        breakpoint = i + 1;//下一位始终默认为断点
                    }
                    else
                    {
                        break;//遇到非数字退出寻找数字循环
                    }
                    stringindex++;//储存数字的数组index+1,为下一组数字做准备
                    label2.Text = numberlist[stringindex - 1];//debug用显示
                    if (breakpoint < str.Length)//若断点未走到字符串尽头，寻找运算符
                    {
                        char chr = new char();
                        chr = str[breakpoint];//断点breakpoint位置为运算符应在位置
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
            }
            for(int i=0;i<numberlist.Count;i++)//将string数组转换成double数组
            {
                Numberlist[i] = double.Parse(numberlist[i]);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //这个是从哪里冒出来的？
        }
        #region CalculatorModule 计算器的模组，比如计算控制，基础方法控制
        class CalculatorModule
        {

            public string[] numberlist = TempData.numberlist;


            public double Basicfunction()
            {
                double temp1 = double.Parse(numberlist[0]);
                double temp2 = double.Parse(numberlist[1]); //如果用户值键入一个字符，这里NullPointerException，需要做一个处理w
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
                        return Math.Pow(temp1, temp2);
                        break;
                    default:
                        Console.WriteLine("Must be Seriously Wrong");
                        return 55555555555;
                        break;
                }
            }
            //计算部分

            public void Calculate()
            {

                while (breakpoint < str.Length)
                    int index = Data.stringindex; //优化了数据控制，这里 int stringindex 换成了 int index 等效
                                                  //string str = textBox1.Text;
                string str = Data.textBoxText; //同上优化了数据控制，所有的东西都可以在Data里面进行设定
                int breakpoint = 0;
                try
                {
                    while (breakpoint < str.Length)
                    {
                        for (int i = breakpoint; i < str.Length; i++)
                        {
                            if (str[i] <= '9' && str[i] >= '0')
                            {
                                numberlist[index] = numberlist[index] + str[i];
                                breakpoint = i + 1;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    stringindex++;
                    //提取第一组数字
                    label2.Text = numberlist[stringindex - 1];

                    if (breakpoint < str.Length)
                    {

                        chr = str[breakpoint];
                        switch (chr)
                    index++;
                        //提取第一组数字
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
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                Data.label1Text = Basicfunction().ToString();

                //调用计算函数，输出
                for (int i = 0; i <= 9; i++)
                {
                    numberlist[i] = "";
                }
                //初始化数组
            }
        }
        #endregion

        #region IOControl 文件控制，比如文件传输流，文件写入和读取
        class IOControl
        {
            public void FileIODO()
            {
                string path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                double result = Data.result;

                FileStream fs = new FileStream(path, FileMode.Create); //这里有个异常需要处理，使用try和catch解决文件检查问题
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(result);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            //保存结果
        }
        #endregion

        #region DataStorage 数据的存储，包括缓存数据和主要数据（此部分尚未测试）
        static class TempData
        {
            public static string[] numberlist = new string[10];
        }

        class Data
        {
            public static string textBoxText = "textBox.Text";
            //public static string textBoxText = null;
            public static string label1Text = "label1.Text";
            public static string label2Text = "label2.Text";

            public static int stringindex = 0;

            public static double result = 0;

            #region 本区域下的所有方法需要优化，但是基本的东西都是可以使用的w 如果有其他建议可以写上comments
            public static string SaveStringTo(string target, string source)
            {
                //save the source data to the target, accept string only

                target = source;

                if (source == target)
                {
                    return target;
                    //return true;
                }
                else
                {
                    return target;
                    //return false;
                }
            }

            public static Boolean SaveDoubleTo(double target, double source)
            {
                //save the source data to the target, accept double only
                target = source;

                if (source == target)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static Boolean SaveArrayListTo(ArrayList target, ArrayList source)
            {
                //save the source data to the target, accept ArrayList only
                target = source;

                if (source == target)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static Boolean SaveInt32To(int target, int source)
            {
                //save the source data to the target, accept Int only
                target = source;

                if (source == target)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            #endregion
        }
        #endregion
    }
}
