// Type: AiThongMinhHonLop5.PageGameOver
// Assembly: AiThongMinhHonLop5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: E:\HK9\AiThongMinhHonLop5BK_2\AiThongMinhHonLop5.dll

using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework.Audio;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Threading;

namespace AiThongMinhHonLop5
{
    public partial class PageGameOver : PhoneApplicationPage
    {
        private DispatcherTimer _timerText = new DispatcherTimer();
        private bool cc = false;
        private VibrateController vc = VibrateController.Default;
        private bool Quit = false;
        private SoundEffect ambienceSoundEF;
        private SoundEffectInstance ambienceInstanceEF;
        //internal Grid LayoutRoot;
        //internal MediaElement mebg;
        //internal Image image1;
        //internal TextBlock tblText;
        //internal TextBlock tblDiem;
        //internal Image imgGoiDiem;
        //internal Image imgQuayLai;
        //internal TextBlock tblGoiDiem;
        //internal TextBlock tblQuayLai;
        //internal ProgressBar processing;
        //internal Grid grdQuit;
        //internal Image imgDialogQuit;
        //internal TextBlock tblCauHoiQuit;
        //internal Image imgCoQuit;
        //internal Image imgKhongQuit;
        //private bool _contentLoaded;

        public PageGameOver()
        {
            this.InitializeComponent();
            this._timerText.Tick += new EventHandler(this._timerText_Tick);
            this._timerText.Interval = TimeSpan.FromMilliseconds(100.0);
        }

        private void _timerText_Tick(object sender, EventArgs e)
        {
            this.cc = !this.cc;
            if (!this.cc)
            {
                this.tblText.Foreground = (Brush)new SolidColorBrush(Color.FromArgb((byte)254, byte.MaxValue, byte.MaxValue, byte.MaxValue));
                this.tblDiem.Foreground = (Brush)new SolidColorBrush(Color.FromArgb((byte)254, byte.MaxValue, byte.MaxValue, (byte)0));
            }
            else
            {
                this.tblText.Foreground = (Brush)new SolidColorBrush(Color.FromArgb((byte)254, (byte)30, (byte)200, byte.MaxValue));
                this.tblDiem.Foreground = (Brush)new SolidColorBrush(Color.FromArgb((byte)254, (byte)100, (byte)200, (byte)0));
            }
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
            this.LoadSoundInstanceEF(filename, out this.ambienceSoundEF, out this.ambienceInstanceEF);
            this.ambienceInstanceEF.Volume = 1f;
            this.ambienceInstanceEF.IsLooped = true;
            this.ambienceInstanceEF.Play();
        }

        private void PlaySoundEF2(string filename)
        {
            if (!Global.sound)
                return;
            this.LoadSoundInstanceEF(filename, out this.ambienceSoundEF, out this.ambienceInstanceEF);
            this.ambienceInstanceEF.Volume = 1f;
            this.ambienceInstanceEF.IsLooped = false;
            this.ambienceInstanceEF.Play();
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

        private void mebg_MediaEnded(object sender, RoutedEventArgs e)
        {
        }

        private void Vibrate()
        {
            if (!Global.vibrate)
                return;
            this.vc.Start(TimeSpan.FromMilliseconds(100.0));
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
            try
            {
                this._timerText.Stop();
                this.ambienceInstanceEF.Stop();
            }
            catch
            {
            }
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

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Global.Win)
            {
                this.PlaySoundBG("sound/EFGaneOver.mp3");
                this.tblText.Text = "Hu hu! Tôi là " + Global.NamePlayer + ". Tôi hôm nay không thông minh bằng học sinh lớp 5";
            }
            else
            {
                this._timerText.Start();
                this.PlaySoundBG("sound/SoundWin.mp3");
                this.tblText.Text = "Yeah!! Tôi là " + Global.NamePlayer + ". Tôi hôm nay thông minh hơn học sinh lớp 5!";
            }
            this.tblDiem.Text = "Số điểm: " + Global.totalScore.ToString();
        }

        private void tblQuayLai_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgQuayLai.Source = (ImageSource)new BitmapImage(new Uri("/Images/tracnghiem2.png", UriKind.RelativeOrAbsolute));
        }

        private void tblQuayLai_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgQuayLai.Source = (ImageSource)new BitmapImage(new Uri("/Images/tracnghiem1.png", UriKind.RelativeOrAbsolute));
            Global.idchude = 0;
            try
            {
                this._timerText.Stop();
                this.ambienceInstanceEF.Stop();
            }
            catch
            {
            }
            this.NavigationService.GoBack();
        }

        //private void tblGoiDiem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    this.Vibrate();
        //    this.imgGoiDiem.Source = (ImageSource)new BitmapImage(new Uri("/Images/tracnghiem2.png", UriKind.RelativeOrAbsolute));
        //}

        //private void tblGoiDiem_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    this.imgGoiDiem.Source = (ImageSource)new BitmapImage(new Uri("/Images/tracnghiem1.png", UriKind.RelativeOrAbsolute));
        //    this.Goidiem();
        //}

        private void DisableControl()
        {
            this.tblQuayLai.IsHitTestVisible = false;
           // this.tblGoiDiem.IsHitTestVisible = false;
        }

        private void EnableControl()
        {
            this.tblQuayLai.IsHitTestVisible = true;
         //   this.tblGoiDiem.IsHitTestVisible = true;
        }

        private void Goidiem()
        {
            this.DisableControl();
            this.processing.Visibility = Visibility.Visible;
            WebClient webClient = new WebClient();
            webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(this.webClient_OpenReadCompleted);
            webClient.OpenReadAsync(new Uri("http://ifunsoft.net/api/aithongminhhonlop5/sendscore.php?user=" + Global.NamePlayer + "&score=" + Global.totalScore.ToString() + "&d=" + DateTime.Now.Second.ToString(), UriKind.Absolute));
        }

        private void webClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            this.processing.Visibility = Visibility.Collapsed;
            try
            {
                string str = new StreamReader(e.Result).ReadToEnd();
                if (str != "")
                {
                    IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
                    storeForApplication.DeleteFile("AiThongMinhHonLop5\\score.sys");
                    StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\score.sys", FileMode.Create, storeForApplication));
                    streamWriter.WriteLine(str.Trim());
                    streamWriter.Close();
                    Global.idchude = 11;
                    try
                    {
                        this._timerText.Stop();
                        this.ambienceInstanceEF.Stop();
                    }
                    catch
                    {
                    }
                    this.NavigationService.GoBack();
                }
                else
                {
                    int num = (int)MessageBox.Show("Lỗi không lấy được dữ liệu từ máy chủ!");
                    this.EnableControl();
                }
            }
            catch
            {
                int num = (int)MessageBox.Show("Lỗi kết nối với Server. Có thể hệ thống đang bảo trì hoặc lỗi kết nối mạng của bạn.");
                this.EnableControl();
            }
        }

        //[DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/PageGameOver.xaml", UriKind.Relative));
        //    this.LayoutRoot = (Grid)this.FindName("LayoutRoot");
        //    this.mebg = (MediaElement)this.FindName("mebg");
        //    this.image1 = (Image)this.FindName("image1");
        //    this.tblText = (TextBlock)this.FindName("tblText");
        //    this.tblDiem = (TextBlock)this.FindName("tblDiem");
        //    this.imgGoiDiem = (Image)this.FindName("imgGoiDiem");
        //    this.imgQuayLai = (Image)this.FindName("imgQuayLai");
        //    this.tblGoiDiem = (TextBlock)this.FindName("tblGoiDiem");
        //    this.tblQuayLai = (TextBlock)this.FindName("tblQuayLai");
        //    this.processing = (ProgressBar)this.FindName("processing");
        //    this.grdQuit = (Grid)this.FindName("grdQuit");
        //    this.imgDialogQuit = (Image)this.FindName("imgDialogQuit");
        //    this.tblCauHoiQuit = (TextBlock)this.FindName("tblCauHoiQuit");
        //    this.imgCoQuit = (Image)this.FindName("imgCoQuit");
        //    this.imgKhongQuit = (Image)this.FindName("imgKhongQuit");
        //}
    }
}
