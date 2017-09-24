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
        //Data DataInstance = new Data();

        //实例化都放在这里了

        CalculatorModule cm = new CalculatorModule();
        IOControl ioc = new IOControl();

        //public string[] tempstr = new string[10]; // 为什么是10啊
        //public char method1 = 'n'; //
        //public char method2 = 'n'; //
        //public int stringindex = 0;
        //public double result = new double();

        #region 由Visual Studio自动生成
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            //预先使用一次SaveTo这样可以保存你需要的内容到Data库中
            //该方法暂时损坏
            //SaveTo();

            //Debug Usage
            //label1.Text = textBox1.Text;
            Data.textBoxText = textBox1.Text;
            Data.label1Text = label1.Text;
            Data.label2Text = label2.Text;
            //label3.Text = Data.SaveStringTo(Data.textBoxText, textBox1.Text);

            //Data.label2Text = StringOut(label2.Text);

            //label2.Text = Data.textBoxText;

            //运行之前的代码
            cm.Calculate(); //CalculatorModuled 的 Calculator 方法，之前写在这里的代码都在这个方法里面。实例化已经在上文进行

            label1.Text = Data.label1Text;
            label2.Text = Data.label2Text;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ioc.FileIODO(); //IOControl 的 FileIODO 方法
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //这个是从哪里冒出来的？
        }

        /*
        //这边SaveTo用于维护和控制数据存储
        //neko还不知道form怎么操作数据
        public void SaveTo()
        {
            //String Control 字符串控制
            Data.SaveStringTo(Data.textBoxText, textBox1.Text);
            Data.SaveStringTo(Data.label1Text, label1.Text); //将之前的label1的文本保存到Data.label1Text中方便其他方法操作
            Data.SaveStringTo(Data.label2Text, label2.Text); //将之前的label2的文本保存到Data.label2Text中方便其他方法操作
                                                             //如果你有其他的数据需要保存，可以使用这个来操作

            //Others - Todo
            //Options 以下都是可选方法 格式均为 Method(target, source)

            //Data.SaveDoubleTo()
            //Data.SaveArrayListTo()
            //Data.SaveInt32To()
        }
        */
    }

    #region CalculatorModule 计算器的模组，比如计算控制，基础方法控制
    class CalculatorModule
    {
        public string[] tempstr = TempData.tempstr;

        public char method1 = 'n'; 
        public char method2 = 'n';

        public double Basicfunction()
        {
            double temp1 = double.Parse(tempstr[0]);
            double temp2 = double.Parse(tempstr[1]); //如果用户值键入一个字符，这里NullPointerException，需要做一个处理w
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
                            tempstr[index] = tempstr[index] + str[i];
                            breakpoint = i + 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                    index++;
                    //提取第一组数字

                    Data.label2Text = tempstr[index - 1];

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
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            Data.label1Text = Basicfunction().ToString();
            
            //调用计算函数，输出
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
        public static string[] tempstr = new string[10];
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

            if(source == target)
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
