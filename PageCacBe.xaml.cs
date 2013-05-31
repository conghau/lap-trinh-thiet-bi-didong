using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework.Audio;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace AiThongMinhHonLop5
{
    public partial class PageCacBe : PhoneApplicationPage
    {
        private VibrateController vc = VibrateController.Default;
        private int ChonBe = 0;
        private bool Quit = false;
        private SoundEffect ambienceSoundEF;
        private SoundEffectInstance ambienceInstanceEF;
       

        public PageCacBe()
        {
            this.InitializeComponent();
        }

        private void LoadSoundEF(string SoundFilePath, out SoundEffect Sound)
        {
            Sound = (SoundEffect)null;
            try
            {
                StreamResourceInfo resourceStream = Application.GetResourceStream(new Uri(SoundFilePath, UriKind.Relative));
                Sound = SoundEffect.FromStream(resourceStream.Stream);
            }
            catch (NullReferenceException)
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
            catch (NullReferenceException)
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

        private void setDefaultDongDoi()
        {
            this.imgBe1.Source = (ImageSource)new BitmapImage(new Uri("/Images/be1b.png", UriKind.RelativeOrAbsolute));
            this.imgBe2.Source = (ImageSource)new BitmapImage(new Uri("/Images/be2b.png", UriKind.RelativeOrAbsolute));
            this.imgBe3.Source = (ImageSource)new BitmapImage(new Uri("/Images/be3b.png", UriKind.RelativeOrAbsolute));
            this.imgBe4.Source = (ImageSource)new BitmapImage(new Uri("/Images/be4b.png", UriKind.RelativeOrAbsolute));
            this.imgBe5.Source = (ImageSource)new BitmapImage(new Uri("/Images/be5b.png", UriKind.RelativeOrAbsolute));
        }

        private void Vibrate()
        {
            if (!Global.vibrate)
                return;
            this.vc.Start(TimeSpan.FromMilliseconds(100.0));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.PlaySoundEF("sound/chonbg.wav");
            if (Global.lanchon == 0)
                this.PlaySoundBG("sound/chonbedautien.mp3");
            else
                this.PlaySoundBG("sound/chonbetieptheo.mp3");
            if (!Global.ChonBe1)
            {
                this.tblNameBe1.Visibility = Visibility.Visible;
                this.imgBe1.Visibility = Visibility.Visible;
            }
            if (!Global.ChonBe2)
            {
                this.tblNameBe2.Visibility = Visibility.Visible;
                this.imgBe2.Visibility = Visibility.Visible;
            }
            if (!Global.ChonBe3)
            {
                this.tblNameBe3.Visibility = Visibility.Visible;
                this.imgBe3.Visibility = Visibility.Visible;
            }
            if (!Global.ChonBe4)
            {
                this.tblNameBe4.Visibility = Visibility.Visible;
                this.imgBe4.Visibility = Visibility.Visible;
            }
            if (Global.ChonBe5)
                return;
            this.tblNameBe5.Visibility = Visibility.Visible;
            this.imgBe5.Visibility = Visibility.Visible;
        }

        private void imgBe1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.imgChon.Visibility = Visibility.Visible;
            this.tblSothich.Visibility = Visibility.Visible;
            this.tblMonhocyeuthich.Visibility = Visibility.Visible;
            this.ChonBe = 1;
            this.Vibrate();
            this.setDefaultDongDoi();
            this.imgRoundBe.Source = (ImageSource)new BitmapImage(new Uri("/Images/roundBe1.png", UriKind.RelativeOrAbsolute));
            this.imgBe1.Source = (ImageSource)new BitmapImage(new Uri("/Images/be1b.png", UriKind.RelativeOrAbsolute));
            this.tblTenDongDoi.Text = "HẠNH";
            this.tblSothich.Text = "Thích Khám phá và thám hiểm";
            this.tblMonhocyeuthich.Text = "Khoa học & Toán học";
        }

        private void imgBe2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.imgChon.Visibility = Visibility.Visible;
            this.tblSothich.Visibility = Visibility.Visible;
            this.tblMonhocyeuthich.Visibility = Visibility.Visible;
            this.ChonBe = 2;
            this.Vibrate();
            this.setDefaultDongDoi();
            this.imgRoundBe.Source = (ImageSource)new BitmapImage(new Uri("/Images/roundBe2.png", UriKind.RelativeOrAbsolute));
            this.imgBe2.Source = (ImageSource)new BitmapImage(new Uri("/Images/be2b.png", UriKind.RelativeOrAbsolute));
            this.tblTenDongDoi.Text = "NAM";
            this.tblSothich.Text = "Đá bóng và đọc truyện tranh";
            this.tblMonhocyeuthich.Text = "Khoa học & Âm nhạc";
        }

        private void imgBe3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.imgChon.Visibility = Visibility.Visible;
            this.tblSothich.Visibility = Visibility.Visible;
            this.tblMonhocyeuthich.Visibility = Visibility.Visible;
            this.ChonBe = 3;
            this.Vibrate();
            this.setDefaultDongDoi();
            this.imgRoundBe.Source = (ImageSource)new BitmapImage(new Uri("/Images/roundBe3.png", UriKind.RelativeOrAbsolute));
            this.imgBe3.Source = (ImageSource)new BitmapImage(new Uri("/Images/be3b.png", UriKind.RelativeOrAbsolute));
            this.tblTenDongDoi.Text = "ĐỨC";
            this.tblSothich.Text = "Đọc truyện và chơi game";
            this.tblMonhocyeuthich.Text = "Khoa học và Địa lý";
        }

        private void imgBe4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.imgChon.Visibility = Visibility.Visible;
            this.tblSothich.Visibility = Visibility.Visible;
            this.tblMonhocyeuthich.Visibility = Visibility.Visible;
            this.ChonBe = 4;
            this.Vibrate();
            this.setDefaultDongDoi();
            this.imgRoundBe.Source = (ImageSource)new BitmapImage(new Uri("/Images/roundBe4.png", UriKind.RelativeOrAbsolute));
            this.imgBe4.Source = (ImageSource)new BitmapImage(new Uri("/Images/be4b.png", UriKind.RelativeOrAbsolute));
            this.tblTenDongDoi.Text = "HƯƠNG";
            this.tblSothich.Text = "Hát và đọc truyện";
            this.tblMonhocyeuthich.Text = "Tự nhiên & Âm nhạc";
        }

        private void imgBe5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.imgChon.Visibility = Visibility.Visible;
            this.tblSothich.Visibility = Visibility.Visible;
            this.tblMonhocyeuthich.Visibility = Visibility.Visible;
            this.ChonBe = 5;
            this.Vibrate();
            this.setDefaultDongDoi();
            this.imgRoundBe.Source = (ImageSource)new BitmapImage(new Uri("/Images/roundBe5.png", UriKind.RelativeOrAbsolute));
            this.imgBe5.Source = (ImageSource)new BitmapImage(new Uri("/Images/be5b.png", UriKind.RelativeOrAbsolute));
            this.tblTenDongDoi.Text = "BẢO";
            this.tblSothich.Text = "Bơi và đọc sách lịch sử";
            this.tblMonhocyeuthich.Text = "Tiếng Việt & Lịch sử";
        }

        private void imgChon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            if (this.ChonBe == 1)
                Global.ChonBe1 = true;
            else if (this.ChonBe == 2)
                Global.ChonBe2 = true;
            else if (this.ChonBe == 3)
                Global.ChonBe3 = true;
            else if (this.ChonBe == 4)
                Global.ChonBe4 = true;
            else if (this.ChonBe == 5)
                Global.ChonBe5 = true;
            Global.ChonBe = this.ChonBe;
            Global.CountChonBe = 0;
            Global.idchude = 3;
            try
            {
                this.ambienceInstanceEF.Stop();
            }
            catch
            {
            }
            this.NavigationService.GoBack();
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

           }
}
