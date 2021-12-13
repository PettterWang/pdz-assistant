using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Process.NET;
using Process.NET.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pdz助手
{
    public partial class Form1 : Form
    {
        public int ssReaderPid = -1;
        public IntPtr ssReaderHandle = IntPtr.Zero;

        public string 当前用户名 = string.Empty;

        public bool 是否最大化 = false;

        public bool png目录是否存在 = false;
        public bool pdf目录是否存在 = false;

        public string 缓冲器路径 = string.Empty;

        public int 持续间隔 = 0;
        public int 上一次bmp数量 = 0;

        public bool 是否正在一键转换 = false;
        public bool 是否处于阅读模式 = false;

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(int hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [SuppressUnmanagedCodeSecurity]
        internal static class SafeNativeMethods
        {
            [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
            public static extern int StrCmpLogicalW(string psz1, string psz2);
        }
        public sealed class 对文件按名称自然序排序 : IComparer<FileInfo>
        {
            public int Compare(FileInfo a, FileInfo b)
            {
                return SafeNativeMethods.StrCmpLogicalW(a.Name, b.Name);
            }
        }

        public Form1()
        {
            //
            InitializeComponent();

            //
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void 自动获取ssReaderPid()
        {
            //
            var processes = System.Diagnostics.Process.GetProcessesByName("ssReader");
            //
            if (processes.Length == 0)
            {
                //
                label_ssReader状态.Text = "关";
                label_ssReader状态.ForeColor = Color.Red;
                //
                ssReaderPid = 0;
                ssReaderHandle = IntPtr.Zero;
                //
                groupBox_主面板.Enabled = false;
            }
            else
            {
                foreach (var p in processes)
                {
                    //
                    label_ssReader状态.Text = "开";
                    label_ssReader状态.ForeColor = Color.Green;
                    //
                    ssReaderPid = p.Id;
                    ssReaderHandle = p.MainWindowHandle;
                    //
                    if (!是否正在一键转换)
                    {
                        groupBox_主面板.Enabled = true;
                    }
                }
            }
        }

        public void 自动获取ssReader窗口状态()
        {
            //是否存在
            if (ssReaderHandle == IntPtr.Zero)
            {
                //
                radioButton_不存在.Checked = true;
                //
                radioButton_前台.Checked = false;
                radioButton_后台.Checked = false;
                //
                return;
            }

            //前台、后台
            if (GetForegroundWindow() == ssReaderHandle)
            {
                radioButton_前台.Checked = true;
            }
            else
            {
                radioButton_后台.Checked = true;
            }

            //最大化、最小化、常规
            int WS_MAXIMIZE = 0x01000000;
            int WS_MINIMIZE = 0x20000000;
            int WS_VISIBLE = 0x10000000;
            int GWL_STYLE = -16;
            int thisStyle = GetWindowLong(ssReaderHandle, GWL_STYLE);

            if ((thisStyle & WS_MAXIMIZE) == WS_MAXIMIZE)
            {
                //
                radioButton_最大化.Checked = true;
                //
                是否最大化 = true;
                //
                return;
            }

            if ((thisStyle & WS_MINIMIZE) == WS_MINIMIZE)
            {
                //
                radioButton_最小化.Checked = true;
                //
                是否最大化 = false;
                //
                button_一键转换.Enabled = false;
                //
                label_气泡.Text = "[*]pdz助手运行中,请使ssReader窗口最大化.(若受[气泡提示]干扰，移动鼠标到别处即可)";
                label_气泡.ForeColor = Color.Red;
                气泡控制器.Enabled = true;
                //
                return;
            }
            if ((thisStyle & WS_VISIBLE) == WS_VISIBLE)
            {
                //
                radioButton_常规.Checked = true;
                //
                是否最大化 = false;
                //
                button_一键转换.Enabled = false;
                //
                label_气泡.Text = "[*]pdz助手运行中,请使ssReader窗口最大化.(若受[气泡提示]干扰，移动鼠标到别处即可)";
                label_气泡.ForeColor = Color.Red;
                气泡控制器.Enabled = true;
                //
                return;
            }
        }

        public void 自动切换到双页模式()
        {
            //x轴系数
            double x轴系数 = (double)170 / 1920;
            //y轴系数
            double y轴系数 = (double)75 / 1080;

            //[双叶]按钮位置
            int x_双叶 = (int)(Screen.PrimaryScreen.Bounds.Width * x轴系数);
            int y_双叶 = (int)(Screen.PrimaryScreen.Bounds.Height * y轴系数);

            //点击[双叶]按钮
            int WM_LBUTTONDOWN = 0x0201;
            int WM_LBUTTONUP = 0x0202;
            SendMessage((int)ssReaderHandle, WM_LBUTTONDOWN, 0x00000001, (IntPtr)((y_双叶 << 16) | (x_双叶 & 0xffff)));
            SendMessage((int)ssReaderHandle, WM_LBUTTONUP, 0x00000000, (IntPtr)((y_双叶 << 16) | (x_双叶 & 0xffff)));
        }

        public void 自动切换到连续模式()
        {
            //x轴系数
            double x轴系数 = (double)140 / 1920;
            //y轴系数
            double y轴系数 = (double)75 / 1080;

            //[连续]按钮位置
            int x_连续 = (int)(Screen.PrimaryScreen.Bounds.Width * x轴系数);
            int y_连续 = (int)(Screen.PrimaryScreen.Bounds.Height * y轴系数);

            //点击[连续]按钮
            int WM_LBUTTONDOWN = 0x0201;
            int WM_LBUTTONUP = 0x0202;
            SendMessage((int)ssReaderHandle, WM_LBUTTONDOWN, 0x00000001, (IntPtr)((y_连续 << 16) | (x_连续 & 0xffff)));
            SendMessage((int)ssReaderHandle, WM_LBUTTONUP, 0x00000000, (IntPtr)((y_连续 << 16) | (x_连续 & 0xffff)));
        }

        public void 自动批量BitmapToPng()
        {
            //序号
            int Order = 1;
            //批处理
            DirectoryInfo info = new DirectoryInfo(缓冲器路径);
            FileInfo[] files = info.GetFiles("*.bmp", SearchOption.AllDirectories).OrderBy(n => Regex.Replace(n.Name,@"\d+",p => p.Value.PadLeft(5,'0'))).ToArray();//按名称排序读取bmp
            foreach (FileInfo file in files)
            {
                //获取此bmp路径
                string thisFile =file.FullName;
                //读取此bmp到内存
                Bitmap thisbmp = new Bitmap(thisFile);
                //设置新png路径
                string newName = textBox_png存储目录.Text + "\\" + Order.ToString() + ".png";
                //转换此bmp到png
                thisbmp.Save(newName, ImageFormat.Png);
                //序号自增
                Order++;
            }
            //
            //MessageBox.Show("png转换完成.");
            进度与提示("png转换完成，正在合成pdf...", 60, Color.Black);
        }

        public void 自动批量PngInPdf()
        {
            //
            string png存储目录 = textBox_png存储目录.Text;
            string pdf存储目录 = Path.GetDirectoryName(textBox_pdf存储目录.Text);

            //
            递归遍历png目录(png存储目录,png存储目录,pdf存储目录);

            //
            进度与提示("pdf生成成功.", 100 ,Color.Green);

            //清理png
            if (checkBox_不存储png.Checked == true)
            {
                清空目录下的文件(textBox_png存储目录.Text);
            }
        }

        private void 递归遍历png目录(String png存储目录, String png当前所在目录, String pdf存储目录)
        {
            //判断png当前所在目录是否含有子目录，有则递归遍历,为每一个子目录中的文件合成一个pdf
            if (Directory.GetDirectories(png当前所在目录).Length > 0)
            {
                //
                String[] 子目录 = Directory.GetDirectories(png当前所在目录);
                //
                foreach (String i in 子目录)
                {
                    递归遍历png目录(png存储目录, i, pdf存储目录);
                }
            }
            else
            {
                对单个目录PngInPdf(png当前所在目录, pdf存储目录 + png当前所在目录.Substring(png存储目录.Length));
            }
        }

        private bool 对单个目录PngInPdf(String path, String Destination)
        {
            //
            DirectoryInfo folder = new DirectoryInfo(path);
            //
            FileInfo[] images = folder.GetFiles("*.png");

            //
            if (images.Length > 0)
            {
                //对png进行排序
                Array.Sort<FileInfo>(images, new 对文件按名称自然序排序());
                
                //
                PdfDocument doc = new PdfDocument();

                //设置pdf布局为单页布局
                doc.PageLayout = PdfPageLayout.SinglePage;

                //
                foreach (FileInfo f in images)
                {
                    //
                    doc.Pages.Add(new PdfPage());

                    //
                    XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[doc.Pages.Count - 1]);

                    //
                    Image testimage = Image.FromFile(f.Directory.ToString() + Path.DirectorySeparatorChar + f.Name);

                    //
                    if (Array.IndexOf(testimage.PropertyIdList, 274) > -1)
                    {
                        //
                        var orientation = (int)testimage.GetPropertyItem(274).Value[0];
                        //
                        switch (orientation)
                        {
                            case 1:
                                // No rotation required.
                                break;
                            case 2:
                                testimage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                break;
                            case 3:
                                testimage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            case 4:
                                testimage.RotateFlip(RotateFlipType.Rotate180FlipX);
                                break;
                            case 5:
                                testimage.RotateFlip(RotateFlipType.Rotate90FlipX);
                                break;
                            case 6:
                                testimage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            case 7:
                                testimage.RotateFlip(RotateFlipType.Rotate270FlipX);
                                break;
                            case 8:
                                testimage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                        }
                        //This EXIF data is now invalid and should be removed.
                        testimage.RemovePropertyItem(274);
                    }

                    //
                    XImage img = XImage.FromGdiPlusImage(testimage);

                    //
                    doc.Pages[doc.Pages.Count - 1].Width = img.PointWidth;
                    doc.Pages[doc.Pages.Count - 1].Height = img.PointHeight;

                    //
                    xgr.DrawImage(img, 0, 0);

                    //
                    xgr.Dispose();
                    testimage.Dispose();
                    img.Dispose();
                }

                //
                string foldername = Path.GetFileNameWithoutExtension(textBox_pdf存储目录.Text);

                //
                if (!Directory.Exists(Destination))
                {
                    Directory.CreateDirectory(Destination);
                }

                //
                doc.Save(Destination + Path.DirectorySeparatorChar + foldername + ".pdf");
                doc.Close();
                doc.Dispose();

                //
                return true;
            }
            else
            {
                return false;
            }
        }

        public void 进度与提示(string 提示内容,int 进度值,Color 颜色)
        {
            //
            label_气泡.Text = "[*]" + 提示内容;
            label_气泡.ForeColor = 颜色;
            //
            progressBar_进度条.Value = 进度值;
            //
            label_进度.Text = 进度值.ToString() + "%";
        }

        public void 自动读取页总数()
        {
            //
            var process = System.Diagnostics.Process.GetProcessesByName("ssReader").FirstOrDefault();
            if (process != null)
            {
                try
                {
                    //
                    ProcessSharp _process = new ProcessSharp(process, MemoryType.Remote);
                    //
                    var myModule = _process.ModuleFactory["ssReader.exe"].BaseAddress;

                    //读取tab
                    var address_tab = _process.Memory.GetAddress(myModule + 0x00598C54, new[] { 0x0, 0x130, 0x3C, 0x8C, 0x84, 0x44, 0x598 });
                    var value_tab = _process.Memory.Read<int>(address_tab);

                    if (value_tab != 0)
                    {
                        //
                        是否处于阅读模式 = true;

                        //读取前言页
                        var address1 = _process.Memory.GetAddress(myModule + 0x00598C54, new[] { 0x0, 0x164, 0x654, 0x390 });
                        var value1 = _process.Memory.Read<int>(address1);
                        //读取目录页
                        var address2 = _process.Memory.GetAddress(myModule + 0x00598C54, new[] { 0x0, 0x164, 0x654, 0x394 });
                        var value2 = _process.Memory.Read<int>(address2);
                        //读取正文页
                        var address3 = _process.Memory.GetAddress(myModule + 0x00598C54, new[] { 0x0, 0x164, 0x654, 0x398 });
                        var value3 = _process.Memory.Read<int>(address3);

                        //
                        int 页总数 = value1 + value2 + value3;
                        //
                        textBox_总页数.Text = 页总数.ToString();
                    }
                    else
                    {
                        //
                        是否处于阅读模式 = false;
                        //
                        进度与提示("ssReader未处于阅读模式", 0, Color.Red);
                        气泡控制器.Enabled = true;
                    }

                    ////读取阅读比例
                    //var address4 = _process.Memory.GetAddress(myModule + 0x00598C54, new[] { 0x0, 0x164, 0x658, 0x30 });
                    //var value4 = _process.Memory.Read<float>(address4);

                    ////
                    //float 阅读比例 = value4;
                    ////
                    //阅读比例 = float.Parse(阅读比例.ToString("0.00"));
                    ////
                    //if (阅读比例 > 0.05f && 阅读比例 < 1.00f)
                    //{
                    //    //
                    //    是否处于阅读模式 = true;
                    //}
                    //else
                    //{
                    //    //
                    //    是否处于阅读模式 = false;
                    //    //
                    //    进度与提示("ssReader未处于阅读模式", 0, Color.Red);
                    //    气泡控制器.Enabled = true;
                    //}

                    //_process.Memory.Write(address, value * 1000); //写内存示例
                }
                catch (Exception e){
                    Console.WriteLine(e);
                }
            }
        }

        private void 进程扫描器_Tick(object sender, EventArgs e)
        {
            //
            自动获取ssReaderPid();
            //
            自动获取ssReader窗口状态();
            //
            自动读取页总数();
            //
            if (是否最大化 == true && png目录是否存在 == true && pdf目录是否存在 == true && textBox_总页数.Text!=string.Empty && textBox_总页数.Text != "0" && 是否处于阅读模式 == true)
            {
                button_一键转换.Enabled = true;
            }
            else
            {
                button_一键转换.Enabled = false;
            }
            //
            if (Directory.Exists(缓冲器路径))
            {
                button_查看缓存器.Enabled = true;
            }
            else
            {
                button_查看缓存器.Enabled = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        public void 自动设置目录()
        {
            //设置png存储目录
            string png存储目录 = @"C:\Users\" + 当前用户名 + @"\Desktop\" + "png存储目录";
            //创建文件夹
            Directory.CreateDirectory(png存储目录);
            //
            textBox_png存储目录.Text = png存储目录;

            //设置pdf存储目录
            string pdf存储目录 = @"C:\Users\" + 当前用户名 + @"\Desktop\1.pdf";
            //
            textBox_pdf存储目录.Text = pdf存储目录;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //
            当前用户名 = Environment.UserName;
            //
            label_当前系统用户名.Text = 当前用户名;
            缓冲器路径 = @"C:\Users\" + 当前用户名 + @"\AppData\Local\Temp\buffer";

            //
            自动设置目录();

            //
            textBox_png存储目录_TextChanged(sender, e);
            textBox_pdf存储目录_TextChanged(sender, e);
        }

        private void button_查看缓存器_Click(object sender, EventArgs e)
        {
            //
            string addr = 缓冲器路径;
            //
            if (Directory.Exists(addr))
            {
                System.Diagnostics.Process.Start(addr);
            }
            else
            {
                MessageBox.Show("缓冲器未创建，请先在ssReader中打开一本书进入阅读模式。");   
            }
        }

        private void 气泡控制器_Tick(object sender, EventArgs e)
        {
            //
            label_气泡.Text = "[*]";
            label_气泡.ForeColor = Color.Black;
            //
            气泡控制器.Enabled = false;
        }

        private void button_一键转换_Click(object sender, EventArgs e)
        {
            //切换阅读模式
            //自动切换到连续模式();
            //自动切换到双页模式();

            //
            if (textBox_png存储目录.Text == Path.GetDirectoryName(textBox_pdf存储目录.Text))
            {
                //
                MessageBox.Show("png存储目录不能与pdf存储目录相同");
                //
                return;
            }
            
            //
            是否正在一键转换 = true;

            //
            最大化控制器.Enabled = true;
            groupBox_主面板.Enabled = false;

            //
            进度与提示("现在锁定ssReader最大化，文档正在转换中...", 5, Color.Black);

            //清空png存储目录
            清空目录下的文件(textBox_png存储目录.Text);

            //清空缓冲器
            清空目录下的文件(缓冲器路径);

            ////释放bmp
            //if (ssReaderHandle != IntPtr.Zero)
            //{
            //    //
            //    释放检测器.Enabled = true;
            //}

            //释放bmp
            if (ssReaderHandle != IntPtr.Zero)
            {
                //
                new Thread(() =>
                {
                    //
                    Thread.CurrentThread.IsBackground = false;

                    //按下Home
                    Key a = new Key(Messaging.VKeys.KEY_HOME);
                    a.PressBackground(ssReaderHandle);

                    //
                    bool 强行合成 = false;

                    //
                    while (true)
                    {
                        //按下PgDown
                        a = new Key(Messaging.VKeys.KEY_NEXT);
                        a.PressBackground(ssReaderHandle);

                        //
                        Thread.Sleep(1);

                        //统计bmp数量
                        int BmpCount = Directory.GetFiles(缓冲器路径, "*.bmp", SearchOption.AllDirectories).Length;
                        if (BmpCount >= Int32.Parse(textBox_总页数.Text) || 强行合成 == true)
                        {
                            //
                            进度与提示("bmp完成释放，正在转换为png...", 20, Color.Black);
                            
                            //
                            自动批量BitmapToPng();

                            //
                            自动批量PngInPdf();

                            //
                            break;
                        }

                        //释放bmp时间控制
                        if (上一次bmp数量 == BmpCount)
                        {
                            持续间隔 += 1;
                        }
                        else
                        {
                            持续间隔 = 0;
                        }

                        //超时检测
                        if (持续间隔 >= 1000)
                        {
                            //
                            进度与提示("bmp释放失败，请检查 ①是否进入[阅读模式] ②是否有显示[此页无效]的损坏页", 0, Color.Red);

                            //
                            int 缺少页 = Int32.Parse(textBox_总页数.Text) - BmpCount;
                            DialogResult dialogResult = MessageBox.Show("此pdz共有" + textBox_总页数.Text + "页,"
                                                                        +"现共释放了" + BmpCount.ToString() + "页,"
                                                                        +"缺少" + 缺少页.ToString() + "页,"
                                                                        +"是否继续合成pdf?", "提示:", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                强行合成 = true;
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                break;
                            }
                        }

                        //
                        上一次bmp数量 = BmpCount;
                    }

                    //
                    持续间隔 = 0;
                    上一次bmp数量 = 0;

                    //
                    是否正在一键转换 = false;
                    最大化控制器.Enabled = false;
                    groupBox_主面板.Enabled = true;

                }).Start();
            }
        }

        private void textBox_png存储目录_TextChanged(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBox_png存储目录.Text))
            {
                //
                png目录是否存在 = false;
                //
                进度与提示("设置的[png存储目录]不存在,请重新设置.", 0, Color.Red);
            }
            else
            {
                //
                进度与提示("", 0, Color.Black);
                //
                png目录是否存在 = true;
            }
        }

        private void checkBox_不存储png_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_不存储png.Checked == true)
            {
                //禁用设置
                textBox_png存储目录.Enabled = false;
                //指定临时目录路径
                string 临时目录 = Path.GetTempPath() + @"png临时存储目录";
                //创建文件夹
                Directory.CreateDirectory(临时目录);
                //更改设置
                textBox_png存储目录.Text = 临时目录;
            }
            else
            {
                textBox_png存储目录.Enabled = true;
            }
        }

        private void 最大化控制器_Tick(object sender, EventArgs e)
        {
            //确保最大化
            if (!是否最大化 && ssReaderHandle != IntPtr.Zero)
            {
                //int SW_SHOWNORMAL = 1;
                //int SW_SHOWMINIMIZED = 2;
                //int SW_SHOWMAXIMIZED = 3;
                ShowWindowAsync(ssReaderHandle, 3);
            }
        }

        public void 清空目录下的文件(string 目录)
        {
            //
            DirectoryInfo di = new DirectoryInfo(目录);
            //
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        private void textBox_pdf存储目录_TextChanged(object sender, EventArgs e)
        {
            //
            if (textBox_pdf存储目录.Text == string.Empty || textBox_pdf存储目录.Text == null)
            {
                //
                pdf目录是否存在 = false;
                //
                进度与提示("设置的[pdf存储目录]不存在,请重新设置.", 0, Color.Red);
                //
                return;
            }

            //检查目录
            string pdf存储目录 = Path.GetDirectoryName(textBox_pdf存储目录.Text);
            //
            if (!Directory.Exists(pdf存储目录))
            {
                //
                pdf目录是否存在 = false;
                //
                进度与提示("设置的[pdf存储目录]不存在,请重新设置.", 0, Color.Red);
                //
                return;
            }
            else
            {
                //
                pdf目录是否存在 = true;
                //
                进度与提示("", 0, Color.Black);
            }

            //检查文件格式
            if (Path.GetExtension(textBox_pdf存储目录.Text) != ".pdf")
            {
                //
                pdf目录是否存在 = false;
                //
                进度与提示("设置的[pdf文件名]不正确,请重新设置.", 0, Color.Red);
                //
                return;
            }
            else
            {
                //
                pdf目录是否存在 = true;
                //
                进度与提示("", 0, Color.Black);
            }
        }

        private void textBox_总页数_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&(e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }

    public static class Extensions
    {
        public static IntPtr GetAddress(this IMemory memory, IntPtr intPtr, int[] chain)
        {
            var result = memory.Read<IntPtr>(intPtr);
            int lastIndex = chain.Length - 1;
            for (int i = 0; i < lastIndex; i++)
                result = memory.Read<IntPtr>(result + chain[i]);
            return result + chain[lastIndex];
        }
    }
}
