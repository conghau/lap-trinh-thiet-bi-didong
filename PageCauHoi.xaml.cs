using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
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
using System.Windows.Threading;

namespace AiThongMinhHonLop5
{
    public partial class PageCauHoi : PhoneApplicationPage
    {
        private DispatcherTimer _timerThanhBach = new DispatcherTimer();
        private DispatcherTimer _timerText = new DispatcherTimer();
        private string[] TextTB = new string[11]
    {
      "Bây giờ 2 bạn hãy bàn luận với nhau xem nên chọn môn nào đầu tiên để ghi điểm?.",
      "Với câu hỏi đầu tiên bạn đã ghi được điểm thưởng là 500.000đ. Hãy tiếp tục cùng phối hợp với đồng đội của mình chọn thêm 1 câu hỏi số 2.",
      "Môn nào là môn lựa tiếp theo?.",
      "Bạn đã có 1.500.000đ tiền thưởng. Chúc mừng! Hãy tiếp tục chọn thêm 1 môn hỏi nữa.",
      "Tiếp tục chọn 1 môn tiếp theo!.",
      "Vượt qua câu hỏi này bạn sẽ đến với câu hỏi tiếp theo đạt được điểm thưởng là 4.000.000đ. Xin mời!.",
      "Môn nào là môn lựa tiếp theo?.",
      "Bạn đã có 6.000.000đ tiền thưởng. Cố lên sẽ được 10.000.000đ.",
      "Và bây giờ vượt qua cột mốc này bạn sẽ có tiền thưởng 14.000.000đ.",
      "Thưa quí vị và các bạn! Và bây giờ bạn sẽ có cơ hội vượt qua câu hỏi thứ 10 để đạt tiền thưởng là 22.000.000đ.",
      "Bây giờ còn câu hỏi đặc biệt của chương trình trị giá 50.000.000đ bạn có dám dũng cảm để bước vào hay không?"
    };
        private bool rTB = true;
        private int CountText = 0;
        private VibrateController vc = VibrateController.Default;
        private string RealChude = "";
        private int RealType = -1;
        private bool Quit = false;
        
        private SoundEffect ambienceSoundEF;
        private SoundEffectInstance ambienceInstanceEF;

        public PageCauHoi()
        {
            InitializeComponent();
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
            if (Global.lanchon == 0)
            {
                if (this.CountText == 1)
                {
                    this._timerThanhBach.Start();
                }
                else
                {
                    if (this.CountText != 4)
                        return;
                    this._timerText.Stop();
                    this._timerThanhBach.Stop();
                }
            }
            else if (Global.lanchon == 1)
            {
                if (this.CountText == 1)
                {
                    this._timerThanhBach.Start();
                }
                else
                {
                    if (this.CountText != 9)
                        return;
                    this._timerText.Stop();
                    this._timerThanhBach.Stop();
                }
            }
            else if (Global.lanchon == 2)
            {
                if (this.CountText == 1)
                {
                    this._timerThanhBach.Start();
                }
                else
                {
                    if (this.CountText != 2)
                        return;
                    this._timerText.Stop();
                    this._timerThanhBach.Stop();
                }
            }
            else if (Global.lanchon == 3)
            {
                if (this.CountText == 1)
                {
                    this._timerThanhBach.Start();
                }
                else
                {
                    if (this.CountText != 4)
                        return;
                    this._timerText.Stop();
                    this._timerThanhBach.Stop();
                }
            }
            else if (Global.lanchon == 4)
            {
                if (this.CountText == 1)
                {
                    this._timerThanhBach.Start();
                }
                else
                {
                    if (this.CountText != 2)
                        return;
                    this._timerText.Stop();
                    this._timerThanhBach.Stop();
                }
            }
            else if (Global.lanchon == 5)
            {
                if (this.CountText == 1)
                {
                    this._timerThanhBach.Start();
                }
                else
                {
                    if (this.CountText != 5)
                        return;
                    this._timerText.Stop();
                    this._timerThanhBach.Stop();
                }
            }
            else if (Global.lanchon == 6)
            {
                if (this.CountText == 1)
                {
                    this._timerThanhBach.Start();
                }
                else
                {
                    if (this.CountText != 2)
                        return;
                    this._timerText.Stop();
                    this._timerThanhBach.Stop();
                }
            }
            else if (Global.lanchon == 7)
            {
                if (this.CountText == 1)
                {
                    this._timerThanhBach.Start();
                }
                else
                {
                    if (this.CountText != 3)
                        return;
                    this._timerText.Stop();
                    this._timerThanhBach.Stop();
                }
            }
            else if (Global.lanchon == 8)
            {
                if (this.CountText == 1)
                {
                    this._timerThanhBach.Start();
                }
                else
                {
                    if (this.CountText != 6)
                        return;
                    this._timerText.Stop();
                    this._timerThanhBach.Stop();
                }
            }
            else if (Global.lanchon == 9)
            {
                if (this.CountText == 1)
                {
                    this._timerThanhBach.Start();
                }
                else
                {
                    if (this.CountText != 9)
                        return;
                    this._timerText.Stop();
                    this._timerThanhBach.Stop();
                }
            }
            else
            {
                if (Global.lanchon != 10)
                    return;
                if (this.CountText == 1)
                    this._timerThanhBach.Start();
                else if (this.CountText == 6)
                {
                    this._timerText.Stop();
                    this._timerThanhBach.Stop();
                    this.grdDacbiet.Visibility = Visibility.Visible;
                }
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
            catch (NullReferenceException )
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

        private void Vibrate()
        {
            if (!Global.vibrate)
                return;
            this.vc.Start(TimeSpan.FromMilliseconds(100.0));
        }

        private void NextQuestion()
        {
            {
                Global.RealChudeCauHoi = this.RealChude;
                Global.RealLoaiCauhoi = this.RealType;
                Global.idchude = this.RealType != 0 ? 5 : 4;
                try
                {
                    this.ambienceInstanceEF.Stop();
                }
                catch
                {
                }
                this.NavigationService.GoBack();
                ++Global.lanchon;
            }
           
        }

        private void tblCauhoiA1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCauhoiA1.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            Global.ChonA1 = true;
            Global.CauHoi = 1;
        }

        private void tblCauhoiA1_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCauhoiA1.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
            this.RealChude = Global.CauHoi1;
            this.RealType = Global.TypeCauHoi1;
            this.NextQuestion();
        }

        private void tblCauhoiA2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCauhoiA2.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            Global.ChonA2 = true;
            Global.CauHoi = 2;
        }

        private void tblCauhoiA2_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCauhoiA2.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
            this.RealChude = Global.CauHoi2;
            this.RealType = Global.TypeCauHoi2;
            this.NextQuestion();
        }

        private void tblCauhoiA3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCauhoiA3.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            Global.ChonA3 = true;
            Global.CauHoi = 3;
        }

        private void tblCauhoiA3_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCauhoiA3.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
            this.RealChude = Global.CauHoi3;
            this.RealType = Global.TypeCauHoi3;
            this.NextQuestion();
        }

        private void tblCauhoiA4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCauhoiA4.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            Global.ChonA4 = true;
            Global.CauHoi = 4;
        }

        private void tblCauhoiA4_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCauhoiA4.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
            this.RealChude = Global.CauHoi4;
            this.RealType = Global.TypeCauHoi4;
            this.NextQuestion();
        }

        private void tblCauhoiA5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCauhoiA5.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            Global.ChonA5 = true;
            Global.CauHoi = 5;
        }

        private void tblCauhoiA5_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCauhoiA5.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
            this.RealChude = Global.CauHoi5;
            this.RealType = Global.TypeCauHoi5;
            this.NextQuestion();
        }

        private void tblCauhoiB1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCauhoiB1.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            Global.ChonB1 = true;
            Global.CauHoi = 6;
        }

        private void tblCauhoiB1_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCauhoiB1.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
            this.RealChude = Global.CauHoi6;
            this.RealType = Global.TypeCauHoi6;
            this.NextQuestion();
        }

        private void tblCauhoiB2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCauhoiB2.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            Global.ChonB2 = true;
            Global.CauHoi = 7;
        }

        private void tblCauhoiB2_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCauhoiB2.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
            this.RealChude = Global.CauHoi7;
            this.RealType = Global.TypeCauHoi7;
            this.NextQuestion();
        }

        private void tblCauhoiB3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCauhoiB3.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            Global.ChonB3 = true;
            Global.CauHoi = 8;
        }

        private void tblCauhoiB3_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCauhoiB3.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
            this.RealChude = Global.CauHoi8;
            this.RealType = Global.TypeCauHoi8;
            this.NextQuestion();
        }

        private void tblCauhoiB4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCauhoiB4.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            Global.ChonB4 = true;
            Global.CauHoi = 9;
        }

        private void tblCauhoiB4_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCauhoiB4.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
            this.RealChude = Global.CauHoi9;
            this.RealType = Global.TypeCauHoi9;
            this.NextQuestion();
        }

        private void tblCauhoiB5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCauhoiB5.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            Global.ChonB5 = true;
            Global.CauHoi = 10;
        }

        private void tblCauhoiB5_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCauhoiB5.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
            this.RealChude = Global.CauHoi10;
            this.RealType = Global.TypeCauHoi10;
            this.NextQuestion();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            int num = Global.lanchon + 1;
            this.PlaySoundEF("sound/chonbg.wav");
            this.PlaySoundBG("sound/chonmon" + num.ToString() + ".wma");
            this.tblLuatChoi.Text = this.TextTB[Global.lanchon];
            this._timerText.Start();
            if (Global.ChonA1)
            {
                this.imgCauhoiA1.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
                this.tblCauhoiA1.IsHitTestVisible = false;
            }
            if (Global.ChonA2)
            {
                this.imgCauhoiA2.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
                this.tblCauhoiA2.IsHitTestVisible = false;
            }
            if (Global.ChonA3)
            {
                this.imgCauhoiA3.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
                this.tblCauhoiA3.IsHitTestVisible = false;
            }
            if (Global.ChonA4)
            {
                this.imgCauhoiA4.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
                this.tblCauhoiA4.IsHitTestVisible = false;
            }
            if (Global.ChonA5)
            {
                this.imgCauhoiA5.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
                this.tblCauhoiA5.IsHitTestVisible = false;
            }
            if (Global.ChonB1)
            {
                this.imgCauhoiB1.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
                this.tblCauhoiB1.IsHitTestVisible = false;
            }
            if (Global.ChonB2)
            {
                this.imgCauhoiB2.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
                this.tblCauhoiB2.IsHitTestVisible = false;
            }
            if (Global.ChonB3)
            {
                this.imgCauhoiB3.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
                this.tblCauhoiB3.IsHitTestVisible = false;
            }
            if (Global.ChonB4)
            {
                this.imgCauhoiB4.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
                this.tblCauhoiB4.IsHitTestVisible = false;
            }
            if (Global.ChonB5)
            {
                this.imgCauhoiB5.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiXam.png", UriKind.RelativeOrAbsolute));
                this.tblCauhoiB5.IsHitTestVisible = false;
            }
            this.LoadCauHoi();
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

        private void LoadCauHoi()
        {
            this.tblCauhoiA1.Text = this.CauHoiTiengViet(Global.CauHoi1);
            this.tblCauhoiA2.Text = this.CauHoiTiengViet(Global.CauHoi2);
            this.tblCauhoiA3.Text = this.CauHoiTiengViet(Global.CauHoi3);
            this.tblCauhoiA4.Text = this.CauHoiTiengViet(Global.CauHoi4);
            this.tblCauhoiA5.Text = this.CauHoiTiengViet(Global.CauHoi5);
            this.tblCauhoiB1.Text = this.CauHoiTiengViet(Global.CauHoi6);
            this.tblCauhoiB2.Text = this.CauHoiTiengViet(Global.CauHoi7);
            this.tblCauhoiB3.Text = this.CauHoiTiengViet(Global.CauHoi8);
            this.tblCauhoiB4.Text = this.CauHoiTiengViet(Global.CauHoi9);
            this.tblCauhoiB5.Text = this.CauHoiTiengViet(Global.CauHoi10);
        }

        private string CauHoiTiengViet(string a)
        {
            string str = "";
            if (a == "MATHEMATIC1")
                str = "Toán lớp 1";
            else if (a == "MATHEMATIC2")
                str = "Toán lớp 2";
            else if (a == "MATHEMATIC3")
                str = "Toán lớp 3";
            else if (a == "MATHEMATIC4")
                str = "Toán lớp 4";
            else if (a == "MATHEMATIC5")
                str = "Toán lớp 5";
            else if (a == "VIETNAMESE1")
                str = "Tiếng Việt lớp 1";
            else if (a == "VIETNAMESE2")
                str = "Tiếng Việt lớp 2";
            else if (a == "VIETNAMESE3")
                str = "Tiếng Việt lớp 3";
            else if (a == "NATURE1")
                str = "Tự nhiên lớp 1";
            else if (a == "NATURE2")
                str = "Tự nhiên lớp 2";
            else if (a == "NATURE3")
                str = "Tự nhiên lớp 3";
            else if (a == "MUSIC1")
                str = "Âm nhạc lớp 1";
            else if (a == "MUSIC2")
                str = "Âm nhạc lớp 2";
            else if (a == "MUSIC3")
                str = "Âm nhạc lớp 3";
            else if (a == "HISTORY4")
                str = "Lịch Sử lớp 4";
            else if (a == "HISTORY5")
                str = "Lịch Sử lớp 5";
            else if (a == "MUSIC1")
                str = "Âm nhạc lớp 1";
            else if (a == "MUSIC2")
                str = "Âm nhạc lớp 2";
            else if (a == "MUSIC3")
                str = "Âm nhạc lớp 3";
            else if (a == "GEOGRAPHY4")
                str = "Địa lý lớp 4";
            else if (a == "GEOGRAPHY5")
                str = "Địa lý lớp 5";
            else if (a == "SCIENCE4")
                str = "Khoa học lớp 4";
            else if (a == "SCIENCE5")
                str = "Khoa học lớp 5";
            else if (a == "MORALS4")
                str = "Đạo đức lớp 4";
            else if (a == "MORALS5")
                str = "Đạo đức lớp 5";
            return str;
        }

        private void imgKhongDacbiet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.Quit = true;
            this.grdQuit.Visibility = Visibility.Visible;
        }

        private void imgCoDacbiet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            Global.CauHoi = 11;
            this.RealChude = Global.CauHoi11;
            this.RealType = 1;
            this.NextQuestion();
        }
    }
}
