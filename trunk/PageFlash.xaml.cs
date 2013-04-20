// Type: AiThongMinhHonLop5.PageFlash
// Assembly: AiThongMinhHonLop5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: E:\HK9\AiThongMinhHonLop5BK_2\AiThongMinhHonLop5.dll

using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace AiThongMinhHonLop5
{
    public partial class PageFlash : PhoneApplicationPage
    {
        private DispatcherTimer _timerLogo = new DispatcherTimer();
        private int count = 0;
        //internal Grid LayoutRoot;
        //internal Image imgLogo;
        //internal MediaElement mebg;
        //private bool _contentLoaded;

        public PageFlash()
        {
            InitializeComponent();
            this._timerLogo.Tick += new EventHandler(this._timerLogo_Tick);
            this._timerLogo.Interval = TimeSpan.FromMilliseconds(100.0);
        }

        //[DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/PageFlash.xaml", UriKind.Relative));
        //    this.LayoutRoot = (Grid)this.FindName("LayoutRoot");
        //    this.imgLogo = (Image)this.FindName("imgLogo");
        //    this.mebg = (MediaElement)this.FindName("mebg");
        //}

        private void _timerLogo_Tick(object sender, EventArgs e)
        {
            if (this.count < 8)
            {
                ++this.count;
                this.imgLogo.Source = (ImageSource)new BitmapImage(new Uri("/Images/a" + this.count.ToString() + ".png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                this.count = 0;
                this._timerLogo.Stop();
                this.mebg.Stop();
                if (Global.idchude == 0)
                    this.NavigationService.GoBack();
                else if (Global.idchude == 1)
                    this.NavigationService.Navigate(new Uri("/PageLuatChoi.xaml", UriKind.Relative));
                else if (Global.idchude == 2)
                    this.NavigationService.Navigate(new Uri("/PageCacBe.xaml", UriKind.Relative));
                else if (Global.idchude == 3)
                    this.NavigationService.Navigate(new Uri("/PageCauHoi.xaml", UriKind.Relative));
                else if (Global.idchude == 4)
                    this.NavigationService.Navigate(new Uri("/PagePlay.xaml", UriKind.Relative));
                else if (Global.idchude == 5)
                    this.NavigationService.Navigate(new Uri("/PagePlay2.xaml", UriKind.Relative));
                else if (Global.idchude == 10)
                    this.NavigationService.Navigate(new Uri("/PageGameOver.xaml", UriKind.Relative));
                else if (Global.idchude == 11)
                    this.NavigationService.Navigate(new Uri("/PageGioiThieuGameShow.xaml", UriKind.Relative));
                else if (Global.idchude == 20)
                    this.NavigationService.Navigate(new Uri("/PageAbout.xaml", UriKind.Relative));
                else if (Global.idchude == 21)
                    this.NavigationService.Navigate(new Uri("/EnterKey.xaml", UriKind.Relative));
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            e.Cancel = true;
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

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this._timerLogo.Start();
        }
    }
}
