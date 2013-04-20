// Type: AiThongMinhHonLop5.PageGioiThieuGameShow
// Assembly: AiThongMinhHonLop5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: E:\HK9\AiThongMinhHonLop5BK_2\AiThongMinhHonLop5.dll

using Microsoft.Devices;
using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace AiThongMinhHonLop5
{
    public partial class PageGioiThieuGameShow : PhoneApplicationPage
    {
        private DispatcherTimer _timerThanhBach = new DispatcherTimer();
        private DispatcherTimer _timerText = new DispatcherTimer();
        private bool rTB = true;
        private int CountText = 0;
        private string[] TextTB = new string[6]
    {
      "Thưa quý vị và các bạn, và chúng tôi mời quý vị cùng tham gia chương trình Ai thông minh hơn học sinh lớp năm...",
      "...thông qua địa chỉ như sau. Có ba cách...",
      "Cách thứ nhất là gởi thông tin về email qua địa chỉ: aithongminhonhocsinhlop5@vtv.vn",
      "Cách thứ hai đăng ký qua mạng, đăng ký trên website:\n http://hocsinhlop5.vtv.vn",
      "Và cách thứ ba đó là gọi điện thoại trực tiếp qua số điện thoại vào giờ hành chánh 08.3824.1919",
      "Còn bây giờ thì chúng tôi xin nói lời tạm biệt. Thanh Bạch và các bạn học sinh của mình xin gởi lời chào tạm biệt!."
    };
        private VibrateController vc = VibrateController.Default;
        private bool Quit = false;
        //internal Grid LayoutRoot;
        //internal MediaElement mebg;
        //internal Image imgThanhBach;
        //internal RotateTransform rtf3;
        //internal Image imgText;
        //internal TextBlock tblText;
        //internal TextBlock tblN1;
        //internal TextBlock tblN2;
        //internal Grid grdQuit;
        //internal Image imgDialogQuit;
        //internal TextBlock tblCauHoiQuit;
        //internal Image imgCoQuit;
        //internal Image imgKhongQuit;
        //private bool _contentLoaded;

        public PageGioiThieuGameShow()
        {
            this.InitializeComponent();
            this._timerThanhBach.Tick += new EventHandler(this._timerThanhBach_Tick);
            this._timerThanhBach.Interval = TimeSpan.FromMilliseconds(150.0);
            this._timerText.Tick += new EventHandler(this._timerText_Tick);
            this._timerText.Interval = TimeSpan.FromSeconds(1.0);
        }

        private void _timerThanhBach_Tick(object sender, EventArgs e)
        {
            this.rTB = !this.rTB;
            if (this.rTB)
                this.imgThanhBach.Source = (ImageSource)new BitmapImage(new Uri("/Images/tb2.png", UriKind.RelativeOrAbsolute));
            else
                this.imgThanhBach.Source = (ImageSource)new BitmapImage(new Uri("/Images/tb1.png", UriKind.RelativeOrAbsolute));
        }

        private void _timerText_Tick(object sender, EventArgs e)
        {
            ++this.CountText;
            if (this.CountText == 1)
            {
                this._timerThanhBach.Start();
                this.tblText.Text = this.TextTB[0];
            }
            else if (this.CountText == 5)
                this.tblText.Text = this.TextTB[1];
            else if (this.CountText == 7)
                this.tblText.Text = this.TextTB[2];
            else if (this.CountText == 15)
                this.tblText.Text = this.TextTB[3];
            else if (this.CountText == 21)
                this.tblText.Text = this.TextTB[4];
            else if (this.CountText == 28)
                this.tblText.Text = this.TextTB[5];
            else if (this.CountText == 34)
            {
                this.imgThanhBach.Visibility = Visibility.Collapsed;
                this.tblText.Visibility = Visibility.Collapsed;
                this.imgText.Visibility = Visibility.Collapsed;
            }
            else if (this.CountText == 36)
            {
                this.tblN1.Visibility = Visibility.Visible;
                this.tblN2.Visibility = Visibility.Visible;
                this.tblN1.Text = "Graphics & Sound";
                this.tblN2.Text = "Trần Trung Kiên";
            }
            else if (this.CountText == 40)
            {
                this.tblN1.Text = "Android Developers";
                this.tblN2.Text = "Nguyễn Thị Bảo Trân";
            }
            else if (this.CountText == 45)
            {
                this.tblN1.Text = "Windows Phone Developers";
                this.tblN2.Text = "Trần Trung Kiên";
            }
            else if (this.CountText == 50)
            {
                this.tblN1.Text = "Testers";
                this.tblN2.Text = "Huỳnh Quang Khánh";
            }
            else if (this.CountText == 55)
            {
                this.tblN1.Text = "Info  & Support";
                this.tblN2.Text = "www.ifunsoft.net";
            }
            else if (this.CountText == 60)
            {
                this.tblN1.Text = "Developed & Published";
                this.tblN2.Text = "iFunSoft";
            }
            else
            {
                if (this.CountText != 68)
                    return;
                this._timerText.Stop();
                this._timerThanhBach.Stop();
                Global.idchude = 20;
                this.NavigationService.GoBack();
            }
        }

        private void PlaySoundBG(string filename)
        {
            if (!Global.sound)
                return;
            this.mebg.Stop();
            this.mebg.Volume = 1.0;
            this.mebg.Source = new Uri(filename, UriKind.Relative);
            this.mebg.Play();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            this.Quit = !this.Quit;
            if (this.Quit)
            {
                this.grdQuit.Visibility = Visibility.Visible;
                e.Cancel = true;
            }
            else
            {
                this.grdQuit.Visibility = Visibility.Collapsed;
                e.Cancel = true;
            }
        }

        private void Vibrate()
        {
            if (!Global.vibrate)
                return;
            this.vc.Start(TimeSpan.FromMilliseconds(100.0));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.PlaySoundBG("sound/ketthuc.wma");
            this._timerText.Start();
        }

        private void imgCoQuit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCoQuit.Source = (ImageSource)new BitmapImage(new Uri("/Images/co2.png", UriKind.RelativeOrAbsolute));
        }

        private void imgCoQuit_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCoQuit.Source = (ImageSource)new BitmapImage(new Uri("/Images/co1.png", UriKind.RelativeOrAbsolute));
            Global.idchude = 0;
            this._timerText.Stop();
            this._timerThanhBach.Stop();
            this.NavigationService.GoBack();
        }

        private void imgKhongQuit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgKhongQuit.Source = (ImageSource)new BitmapImage(new Uri("/Images/khong2.png", UriKind.RelativeOrAbsolute));
        }

        private void imgKhongQuit_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgKhongQuit.Source = (ImageSource)new BitmapImage(new Uri("/Images/khong1.png", UriKind.RelativeOrAbsolute));
            this.Quit = !this.Quit;
            this.grdQuit.Visibility = Visibility.Collapsed;
        }

        //[DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/PageGioiThieuGameShow.xaml", UriKind.Relative));
        //    this.LayoutRoot = (Grid)this.FindName("LayoutRoot");
        //    this.mebg = (MediaElement)this.FindName("mebg");
        //    this.imgThanhBach = (Image)this.FindName("imgThanhBach");
        //    this.rtf3 = (RotateTransform)this.FindName("rtf3");
        //    this.imgText = (Image)this.FindName("imgText");
        //    this.tblText = (TextBlock)this.FindName("tblText");
        //    this.tblN1 = (TextBlock)this.FindName("tblN1");
        //    this.tblN2 = (TextBlock)this.FindName("tblN2");
        //    this.grdQuit = (Grid)this.FindName("grdQuit");
        //    this.imgDialogQuit = (Image)this.FindName("imgDialogQuit");
        //    this.tblCauHoiQuit = (TextBlock)this.FindName("tblCauHoiQuit");
        //    this.imgCoQuit = (Image)this.FindName("imgCoQuit");
        //    this.imgKhongQuit = (Image)this.FindName("imgKhongQuit");
        //}
    }
}
