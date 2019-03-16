若含有"-f"，遍将# 实验1 Experiment01
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
## 新增需求
### 项目新增传入文档信息并批量生成二维码图片的功能
首先引用新的命名空间：  
  using System.IO;  
  using System.Drawing.Imaging;  
  using System.Drawing;  
第一个为文件输入输出流所需，后两个为生成图片所需  
在之前的基础上，在命令行参数条件判断处新增一条判断：  
判断参数是否为文件目录，即是否含有"-f"  
若含有"-f"，遍将"-f"删掉，其余读为文件地址  
接着读取文件内容，每行读一次，存进字符串数组中  
再将每个字符串转化成二维码图片  
具体代码如下：  
![image](https://github.com/3017218159/ConsoleApp1/blob/master/5.png)  
其中CreateQrCode_txt()方法和CreateQrCode_strToPng()方法将字符串数组转化成图片  
具体代码如下：  
![image](https://github.com/3017218159/ConsoleApp1/blob/master/6.png)  
![image](https://github.com/3017218159/ConsoleApp1/blob/master/7.png)  
在命令行参数输入：-fdata/qrcode.txt  
运行程序：  
![image](https://github.com/3017218159/ConsoleApp1/blob/master/8.png)  
生成的二维码图片保存在了Debug目录下，分别是：  
![image](https://github.com/3017218159/ConsoleApp1/blob/master/0.png)  
![image](https://github.com/3017218159/ConsoleApp1/blob/master/1%20(2).png)  
扫描结果同于文件内容，目标实现
