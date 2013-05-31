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
