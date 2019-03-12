using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.QrCodeNet.Encoding;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //首先设置命令行参数的限制条件（此处设为只允许输入英文，输入错误会提示并退出）
            if(args.Length == 0)//命令行参数不可为空
            {
                Console.WriteLine("You cannot input no words");
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
    }
}
