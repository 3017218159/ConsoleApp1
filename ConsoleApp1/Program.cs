using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using System.Net;
using System.Drawing.Imaging;
using System.Drawing;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //设置命令行参数的限制条件（此处设为只允许输入英文，输入错误会提示并退出）
            if(args.Length == 0)//命令行参数不可为空
            {
                Console.WriteLine("You cannot input no words");
            }
            //判断命令行是否为文件目录
            else if (args[0][0] == '-')
            {
                if (args[0].Length <= 1)
                {
                    Console.WriteLine("You must input a command");
                }
                else if (args[0][1] != 'f')
                {
                    Console.WriteLine("Please input a correct command");
                }
                else if (args[0].Length <= 2)
                {
                    Console.WriteLine("Please input a file name");
                }
                //读取.txt文件并将每行文字生成一个二维码，保存为.png
                else
                {
                    string fileName = args[0].Substring(2);//去掉前两位即"-f"
                    //若文件存在并不为空，生成二维码，若文件不存在，则创建文件(文件编码方式必须为Unicode)
                    if (File.Exists(fileName))
                    {
                        string[] str = File.ReadAllLines(fileName);//每行读一次
                        if (str.Length == 0)
                        {
                            Console.WriteLine("文件为空");
                        }
                        CreateQrCode_txt(fileName, str);
                    }
                    else
                    {
                        File.Create(fileName);
                        Console.WriteLine("文件不存在，已自动生成");
                    }
                }
            }
            else if(args.Length > 20)//命令行参数长度不可超过二十
            {
                Console.WriteLine("You cannot input more than 20 words");
            }
            else
            {
                int num = 0;
                //用两层循环检查每一个字符是否为英文
                for(int i = 0; i < args.Length; i++)
                {
                    for(int j = 0; j < args[i].Length; j++)
                    {
                        //只允许命令行参数为英文
                        if (!(((int)args[i][j] >= 65 && (int)args[i][j] <= 90) || 
                            ((int)args[i][j] >= 97 && (int)args[i][j] <= 122)))
                        {
                            Console.WriteLine("You can only input in English");
                            num++;
                            break;
                        }
                    }
                    if(num == 1)//保证不冗余检测
                    {
                        break;
                    }
                }
                if(num == 0)//确保以上条件都满足
                {
                    //将命令行参数整合到一个字符串中
                    string text = args[0];
                    for(int i = 1; i < args.Length; i++)
                    {
                        text = text + " " + args[i];
                    }

                    //定义QrCode
                    QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);//QrCode纠错等级
                    QrCode qrCode = qrEncoder.Encode(text);//生成QrCode
                    Console.BackgroundColor = ConsoleColor.White;//白底黑方块符合一般QrCode的风格

                    //绘制输出QrCode
                    //两层循环将QrCode矩阵转化为黑白方块
                    for (int j = 0; j < qrCode.Matrix.Width; j++)
                    {
                        for (int i = 0; i < qrCode.Matrix.Width; i++)
                        {
                            if(qrCode.Matrix[i, j])//QRCode矩阵中，1代表黑格，0代表白格
                            {
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write('█');
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write('█');
                            }
                        }
                        Console.WriteLine();
                    }
                    //最后恢复颜色
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// 将字符串数组转化为二维码
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="str"></param>
        public static void CreateQrCode_txt(string fileName, string[] str)
        {
            for(int i=0; i<str.Length; i++)
            {
                CreateQrCode_strToPng(fileName, str[i], i+1);
            }
        }

        /// <summary>
        /// 将字符串转化为二维码图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="str"></param>
        /// <param name="image_num"></param>
        public static void CreateQrCode_strToPng(string fileName, string str, int image_num)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);//QrCode纠错等级
            QrCode qrCode = qrEncoder.Encode(str);//生成QrCode
            GraphicsRenderer graphicsRenderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            //将二维码保存为png，保存至当前目录下，并输出提示消息
            MemoryStream ms = new MemoryStream();
            graphicsRenderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            Image img = Image.FromStream(ms);
            string num = "00";
            num = num + image_num;
            img.Save(num.Substring(num.Length-3, 3) + ".png");
            Console.WriteLine("已经生成图片"+ num.Substring(num.Length - 3, 3));
        }
    }
}