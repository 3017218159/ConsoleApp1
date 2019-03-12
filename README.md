# 实验1 Experiment01
该项目大致可以分为两个步骤：  
    1.设置命令行参数的限制条件  
    2.根据命令行参数生成二维码并绘制输出  
## 步骤一
设置命令行参数的限制条件：  
   1.不能为空  
   2.长度不能超过20  
   3.只允许输入英文  
具体代码如下：  
![image](https://github.com/3017218159/ConsoleApp1/blob/master/2.png)  
## 步骤二
命令行参数满足自定义条件之后，开始着手生成二维码  
首先安装QRCode.NET程序包，在命名空间引用Gma.QrCodeNet.Encoding  
接着使用QrEncoder类实例化一个二维码  
用qrEncoder.Encode(string)方法将命令行参数传入二维码中，生成qrCode  
（由于该方法只能传入string,故将string[] args放入一个字符串中）  
最后根据生成的二维码矩阵绘制二维码图像（0为白格，1为黑格）  
具体代码如下：  
![image](https://github.com/3017218159/ConsoleApp1/blob/master/3.png)  
在命令行输入：I like C sharp，最终运行结果如下：  
![image](https://github.com/3017218159/ConsoleApp1/blob/master/1.png)  
用手机扫描二维码，得到正确信息，实验成功：  
![image](https://github.com/3017218159/ConsoleApp1/blob/master/4.png)
