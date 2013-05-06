// Type: AiThongMinhHonLop5.PageAbout
// Assembly: AiThongMinhHonLop5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: E:\HK9\AiThongMinhHonLop5BK_2\AiThongMinhHonLop5.dll

using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AiThongMinhHonLop5
{
    public partial class PageAbout : PhoneApplicationPage
    {
        private DispatcherTimer _tmr = new DispatcherTimer();
        private VibrateController vc = VibrateController.Default;
        //internal Grid LayoutRoot;
        //internal Image imgLogo;
        //internal MediaElement mebg;
        //internal System.Windows.Shapes.Rectangle rectangle1;
        //internal Image imgAb1;
        //internal PlaneProjection TargetAbout1;
        //internal Image imgAb2;
        //internal PlaneProjection TargetAbout2;
        //internal Image imgAb3;
        //internal PlaneProjection TargetAbout3;
        //internal Image imgVisit;
        //internal Image imgMoreApp;
        //internal Image imgRateApp;
        //private bool _contentLoaded;
        private SoundEffect ambienceSoundEF;
        private SoundEffectInstance ambienceInstanceEF;

        public PageAbout()
        {
            InitializeComponent();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(50.0);
            dispatcherTimer.Tick += (EventHandler)delegate
            {
                try
                {
                    FrameworkDispatcher.Update();
                }
                catch
                {
                }
            };
            dispatcherTimer.Start();
           // this._tmr.Tick += new EventHandler(this._tmr_Tick);
            this._tmr.Interval = TimeSpan.FromMilliseconds(10.0);
            this._tmr.Start();
        }

        //[DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/PageAbout.xaml", UriKind.Relative));
        //    this.LayoutRoot = (Grid)this.FindName("LayoutRoot");
        //    this.imgLogo = (Image)this.FindName("imgLogo");
        //    this.mebg = (MediaElement)this.FindName("mebg");
        //    this.rectangle1 = (System.Windows.Shapes.Rectangle)this.FindName("rectangle1");
        //    this.imgAb1 = (Image)this.FindName("imgAb1");
        //    this.TargetAbout1 = (PlaneProjection)this.FindName("TargetAbout1");
        //    this.imgAb2 = (Image)this.FindName("imgAb2");
        //    this.TargetAbout2 = (PlaneProjection)this.FindName("TargetAbout2");
        //    this.imgAb3 = (Image)this.FindName("imgAb3");
        //    this.TargetAbout3 = (PlaneProjection)this.FindName("TargetAbout3");
        //    this.imgVisit = (Image)this.FindName("imgVisit");
        //    this.imgMoreApp = (Image)this.FindName("imgMoreApp");
        //    this.imgRateApp = (Image)this.FindName("imgRateApp");
        //}

        //private void _tmr_Tick(object sender, EventArgs e)
        //{
        //    Image image1 = this.imgAb1;
        //    Thickness margin1 = this.imgAb1.Margin;
        //    double left1 = margin1.Left;
        //    margin1 = this.imgAb1.Margin;
        //    double top1 = margin1.Top - 2.0;
        //    margin1 = this.imgAb1.Margin;
        //    double right1 = margin1.Right;
        //    margin1 = this.imgAb1.Margin;
        //    double bottom1 = margin1.Bottom;
        //    Thickness thickness1 = new Thickness(left1, top1, right1, bottom1);
        //    image1.Margin = thickness1;
        //    Image image2 = this.imgAb2;
        //    Thickness margin2 = this.imgAb2.Margin;
        //    double left2 = margin2.Left;
        //    margin2 = this.imgAb2.Margin;
        //    double top2 = margin2.Top - 2.0;
        //    margin2 = this.imgAb2.Margin;
        //    double right2 = margin2.Right;
        //    margin2 = this.imgAb2.Margin;
        //    double bottom2 = margin2.Bottom;
        //    Thickness thickness2 = new Thickness(left2, top2, right2, bottom2);
        //    image2.Margin = thickness2;
        //    Image image3 = this.imgAb3;
        //    Thickness margin3 = this.imgAb3.Margin;
        //    double left3 = margin3.Left;
        //    margin3 = this.imgAb3.Margin;
        //    double top3 = margin3.Top - 2.0;
        //    margin3 = this.imgAb3.Margin;
        //    double right3 = margin3.Right;
        //    margin3 = this.imgAb3.Margin;
        //    double bottom3 = margin3.Bottom;
        //    Thickness thickness3 = new Thickness(left3, top3, right3, bottom3);
        //    image3.Margin = thickness3;
        //    if (this.imgAb1.Margin.Top > -5000.0)
        //        return;
        //    Image image4 = this.imgAb1;
        //    Thickness margin4 = this.imgAb1.Margin;
        //    double left4 = margin4.Left;
        //    double top4 = 440.0;
        //    margin4 = this.imgAb1.Margin;
        //    double right4 = margin4.Right;
        //    margin4 = this.imgAb1.Margin;
        //    double bottom4 = margin4.Bottom;
        //    Thickness thickness4 = new Thickness(left4, top4, right4, bottom4);
        //    image4.Margin = thickness4;
        //    Image image5 = this.imgAb2;
        //    margin4 = this.imgAb2.Margin;
        //    double left5 = margin4.Left;
        //    double top5 = 2250.0;
        //    margin4 = this.imgAb2.Margin;
        //    double right5 = margin4.Right;
        //    margin4 = this.imgAb2.Margin;
        //    double bottom5 = margin4.Bottom;
        //    Thickness thickness5 = new Thickness(left5, top5, right5, bottom5);
        //    image5.Margin = thickness5;
        //    Image image6 = this.imgAb3;
        //    margin4 = this.imgAb3.Margin;
        //    double left6 = margin4.Left;
        //    double top6 = 3780.0;
        //    margin4 = this.imgAb3.Margin;
        //    double right6 = margin4.Right;
        //    margin4 = this.imgAb3.Margin;
        //    double bottom6 = margin4.Bottom;
        //    Thickness thickness6 = new Thickness(left6, top6, right6, bottom6);
        //    image6.Margin = thickness6;
        //}

        private void LoadSoundEF(string SoundFilePath, out SoundEffect Sound)
        {
            Sound = (SoundEffect)null;
            try
            {
                StreamResourceInfo resourceStream = Application.GetResourceStream(new Uri(SoundFilePath, UriKind.Relative));
                Sound = SoundEffect.FromStream(resourceStream.Stream);
            }
            catch (NullReferenceException ex)
            {
            }
        }

        private void LoadSoundInstanceEF(string SoundFilePath, out SoundEffect Sound, out SoundEffectInstance SoundInstance)
        {
            Sound = (SoundEffect)null;
            SoundInstance = (SoundEffectInstance)null;
            try
            {
                this.LoadSoundEF(SoundFilePath, out Sound);
                SoundInstance = Sound.CreateInstance();
            }
            catch (NullReferenceException ex)
            {
            }
        }

        private void PlaySoundEF(string filename)
        {
            if (!Global.sound)
                return;
            try
            {
                this.ambienceInstanceEF.Stop();
            }
            catch
            {
            }
            this.LoadSoundInstanceEF(filename, out this.ambienceSoundEF, out this.ambienceInstanceEF);
            this.ambienceInstanceEF.Volume = 1f;
            this.ambienceInstanceEF.IsLooped = false;
            this.ambienceInstanceEF.Play();
        }

        private void PlaySoundBG(string filename)
        {
            if (!Global.sound)
                return;
            this.mebg.Volume = 1.0;
            this.mebg.Stop();
            this.mebg.Source = new Uri(filename, UriKind.Relative);
            this.mebg.Play();
        }

        private void mebg_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.PlaySoundBG("sound/soundbgabout.mp3");
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.PlaySoundBG("sound/soundbgabout.mp3");
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            Global.idchude = 0;
            try
            {
                this.ambienceInstanceEF.Stop();
            }
            catch
            {
            }
            this.NavigationService.GoBack();
        }

        private void Vibrate()
        {
            if (!Global.vibrate)
                return;
            this.vc.Start(TimeSpan.FromMilliseconds(100.0));
        }



        private void imgBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
