// Type: AiThongMinhHonLop5.PageLuatChoi
// Assembly: AiThongMinhHonLop5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: E:\HK9\AiThongMinhHonLop5BK_2\AiThongMinhHonLop5.dll

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
using System.Windows.Threading;

namespace AiThongMinhHonLop5
{
    public partial class PageLuatChoi : PhoneApplicationPage
    {
        private DispatcherTimer _timerThanhBach = new DispatcherTimer();
        private DispatcherTimer _timerText = new DispatcherTimer();
        private DispatcherTimer _timerShowLuatChoi = new DispatcherTimer();
        private DispatcherTimer _timerShowMaxTienThuong = new DispatcherTimer();
        private DispatcherTimer _timerShowCacBe = new DispatcherTimer();
        private DispatcherTimer _timerShowTroGiup = new DispatcherTimer();
        private string[] Luat = new string[13]
    {
      "Thưa quí vị, chúng ta đang đến với chương trình Ai thông minh hơn học sinh lớp 5.",
      "Thưa quí vị, luật chơi rất đơn giản...",
      "...chúng ta có 11 câu hỏi theo chương trình giáo khoa  từ lớp 1 đến lớp 5...",
      "...và nếu vượt qua 11 câu hỏi thì sẽ nhận tiền thưởng là 50.000.000đ.",
      "Bạn sẽ có 5 người bạn đồng hành ở đây. Xin được giới thiệu để làm quen với nhau.",
      "Để giúp cho người chơi tự tin, chúng tôi có những quyền trợ giúp...",
      "...như quyền tham khảo là xem đáp án của đồng đội của mình...",
      "...quyền sao chép, mình lấy nguyên đáp án của đồng đội làm đáp án của mình...",
      "...và cái quyền giải cứu may mắn, nếu như bạn đáp sai, đồng đội của bạn đáp đúng...",
      "...bạn sẽ được giải cứu trong cái câu hỏi đó và ở lại chơi tiếp.",
      "Nhưng có một đều tôi xin lưu ý. Nếu bạn không hoàn thành được hết 11 câu hỏi...",
      "...bạn phải nhìn thẳng vào máy quay phim bên kia và nói rằng...",
      "...Tôi hôm nay không thông minh bằng học sinh lớp 5!."
    };
        private bool rTB = true;
        private int CountText = 0;
        private int CountTimeLuatChoi = 0;
        private bool MaxTienThuong = false;
        private int CountCacBe = 0;
        private int CountTroGiup = 0;
        private VibrateController vc = VibrateController.Default;
        private bool Skip = false;
        private bool Quit = false;
        private int[] ar_vt = new int[10]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6,
      7,
      8,
      9
    };
        private string[] ArrMon = new string[26]
    {
      "MATHEMATIC1",
      "MATHEMATIC2",
      "MATHEMATIC3",
      "MATHEMATIC4",
      "MATHEMATIC5",
      "VIETNAMESE1",
      "VIETNAMESE2",
      "VIETNAMESE3",
      "NATURE2",
      "MUSIC1",
      "MUSIC2",
      "MUSIC3",
      "HISTORY4",
      "HISTORY5",
      "NATURE1",
      "NATURE2",
      "NATURE3",
      "MUSIC1",
      "MUSIC2",
      "MUSIC3",
      "GEOGRAPHY4",
      "GEOGRAPHY5",
      "SCIENCE4",
      "SCIENCE5",
      "MORALS4",
      "MORALS5"
    };
        private int cau1 = 0;
        private int cau2 = 0;
        private int cau3 = 0;
        private int cau4 = 0;
        private int cau11 = 0;
        private int cau5 = 0;
        private int cau6 = 0;
        private int cau7 = 0;
        private int cau8 = 0;
        private int cau9 = 0;
        private int cau10 = 0;
        private SoundEffect ambienceSoundEF;
        private SoundEffectInstance ambienceInstanceEF;
        //internal Grid LayoutRoot;
        //internal MediaElement mebg;
        //internal Image imgSkip;
        //internal Image image2;
        //internal Image imgThanhBach;
        //internal Image image1;
        //internal TextBlock tblLuatChoi;
        //internal Grid grdCauHoi;
        //internal Image imgCauhoiA1;
        //internal Image imgCauhoiB1;
        //internal Image imgCauhoiA2;
        //internal Image imgCauhoiB2;
        //internal Image imgCauhoiA3;
        //internal Image imgCauhoiB3;
        //internal Image imgCauhoiA4;
        //internal Image imgCauhoiB4;
        //internal Image imgCauhoiA5;
        //internal Image imgCauhoiB5;
        //internal TextBlock tblCauhoiA1;
        //internal TextBlock tblCauhoiA2;
        //internal TextBlock tblCauhoiA3;
        //internal TextBlock tblCauhoiA4;
        //internal TextBlock tblCauhoiA5;
        //internal TextBlock tblCauhoiB1;
        //internal TextBlock tblCauhoiB2;
        //internal TextBlock tblCauhoiB3;
        //internal TextBlock tblCauhoiB4;
        //internal TextBlock tblCauhoiB5;
        //internal Grid grdTienThuong;
        //internal Image imgTienthuong1;
        //internal Image imgTienthuong6;
        //internal Image imgTienthuong2;
        //internal Image imgTienthuong7;
        //internal Image imgTienthuong3;
        //internal Image imgTienthuong8;
        //internal Image imgTienthuong4;
        //internal Image imgTienthuong9;
        //internal Image imgTienthuong5;
        //internal Image imgTienthuong10;
        //internal Image imgTienthuong11;
        //internal TextBlock tblTienthuong1;
        //internal TextBlock tblTienthuong2;
        //internal TextBlock tblTienthuong3;
        //internal TextBlock tblTienthuong4;
        //internal TextBlock tblTienthuong5;
        //internal TextBlock tblTienthuong6;
        //internal TextBlock tblTienthuong7;
        //internal TextBlock tblTienthuong8;
        //internal TextBlock tblTienthuong9;
        //internal TextBlock tblTienthuong10;
        //internal TextBlock tblTienthuong11;
        //internal Grid grdCacBe;
        //internal Image imgBe1;
        //internal Image imgBe2;
        //internal Image imgBe3;
        //internal Image imgBe4;
        //internal Image imgBe5;
        //internal Image imgThink1;
        //internal TextBlock tblName1;
        //internal Image imgThink2;
        //internal TextBlock tblName2;
        //internal Image imgThink3;
        //internal TextBlock tblName3;
        //internal Image imgThink4;
        //internal TextBlock tblName4;
        //internal Image imgThink5;
        //internal TextBlock tblName5;
        //internal Image imgSaoChep;
        //internal Image imgThamKhao;
        //internal Image imgGiaiCuuMayMan;
        //internal Image imgTieuDeSaoChep;
        //internal Image imgNoiDungSaoChep;
        //internal Image imgTieuDeThamKhao;
        //internal Image imgNoiDungThamKhao;
        //internal Image imgTieuDeGiaiCuuMayMan;
        //internal Image imgNoiDungGiaiCuuMayMan;
        //internal Grid grdSkip;
        //internal Image imgDialog;
        //internal TextBlock tblCauHoi;
        //internal Image imgCo;
        //internal Image imgKhong;
        //internal Grid grdQuit;
        //internal Image imgDialogQuit;
        //internal TextBlock tblCauHoiQuit;
        //internal Image imgCoQuit;
        //internal Image imgKhongQuit;
       // _contentLoaded=true;

        public PageLuatChoi()
        {
            this.InitializeComponent();
            this._timerThanhBach.Tick += new EventHandler(this._timerThanhBach_Tick);
            this._timerThanhBach.Interval = TimeSpan.FromMilliseconds(150.0);
            this._timerText.Tick += new EventHandler(this._timerText_Tick);
            this._timerText.Interval = TimeSpan.FromSeconds(1.0);
            this._timerShowLuatChoi.Tick += new EventHandler(this._timerShowLuatChoi_Tick);
            this._timerShowLuatChoi.Interval = TimeSpan.FromMilliseconds(300.0);
            this._timerShowMaxTienThuong.Tick += new EventHandler(this._timerShowMaxTienThuong_Tick);
            this._timerShowMaxTienThuong.Interval = TimeSpan.FromMilliseconds(100.0);
            this._timerShowCacBe.Tick += new EventHandler(this._timerShowCacBe_Tick);
            this._timerShowCacBe.Interval = TimeSpan.FromMilliseconds(800.0);
            this._timerShowTroGiup.Tick += new EventHandler(this._timerShowTroGiup_Tick);
            this._timerShowTroGiup.Interval = TimeSpan.FromMilliseconds(500.0);
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
                this.tblLuatChoi.Text = this.Luat[0];
            }
            else if (this.CountText == 4)
                this._timerThanhBach.Stop();
            else if (this.CountText == 5)
                this.tblLuatChoi.Text = this.Luat[1];
            else if (this.CountText == 6)
                this._timerThanhBach.Start();
            else if (this.CountText == 7)
                this.tblLuatChoi.Text = this.Luat[2];
            else if (this.CountText == 11)
            {
                this.tblLuatChoi.Text = this.Luat[3];
            }
            else
            {
                if (this.CountText != 15)
                    return;
                this._timerText.Stop();
                this._timerThanhBach.Stop();
            }
        }

        private void _timerShowLuatChoi_Tick(object sender, EventArgs e)
        {
            ++this.CountTimeLuatChoi;
            if (this.CountTimeLuatChoi == 20)
            {
                this.grdCauHoi.Visibility = Visibility.Visible;
                //this
            }
            else if (this.CountTimeLuatChoi == 25)
            {
                this.imgCauhoiA1.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiB1.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 26)
            {
                this.imgCauhoiA1.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiB1.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiA2.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiB2.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 27)
            {
                this.imgCauhoiA2.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiB2.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiA3.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiB3.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 28)
            {
                this.imgCauhoiA3.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiB3.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiA4.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiB4.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 29)
            {
                this.imgCauhoiA4.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiB4.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiA5.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiB5.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 30)
            {
                this.imgCauhoiA5.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgCauhoiB5.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 33)
            {
                this.grdCauHoi.Visibility = Visibility.Collapsed;
                this.grdTienThuong.Visibility = Visibility.Visible;
            }
            else if (this.CountTimeLuatChoi == 34)
                this.imgTienthuong1.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            else if (this.CountTimeLuatChoi == 35)
            {
                this.imgTienthuong1.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgTienthuong2.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 36)
            {
                this.imgTienthuong2.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgTienthuong3.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 37)
            {
                this.imgTienthuong3.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgTienthuong4.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 38)
            {
                this.imgTienthuong4.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgTienthuong5.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 39)
            {
                this.imgTienthuong5.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgTienthuong6.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 40)
            {
                this.imgTienthuong6.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgTienthuong7.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 41)
            {
                this.imgTienthuong7.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgTienthuong8.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 42)
            {
                this.imgTienthuong8.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgTienthuong9.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 43)
            {
                this.imgTienthuong9.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgTienthuong10.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.CountTimeLuatChoi == 44)
            {
                this.imgTienthuong10.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
                this.imgTienthuong11.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
                this._timerShowMaxTienThuong.Start();
            }
            else if (this.CountTimeLuatChoi == 50)
                this.PlaySoundBG("sound/gioithieucacbe.mp3");
            else if (this.CountTimeLuatChoi == 52)
            {
                this.grdTienThuong.Visibility = Visibility.Collapsed;
                this.tblLuatChoi.Text = this.Luat[4];
            }
            else
            {
                if (this.CountTimeLuatChoi != 53)
                    return;
                this._timerShowLuatChoi.Stop();
                this.CountTimeLuatChoi = 0;
                this._timerShowMaxTienThuong.Stop();
                this._timerShowCacBe.Start();
                this.grdCacBe.Visibility = Visibility.Visible;
            }
        }

        private void _timerShowMaxTienThuong_Tick(object sender, EventArgs e)
        {
            this.MaxTienThuong = !this.MaxTienThuong;
            if (this.MaxTienThuong)
                this.imgTienthuong11.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame2.png", UriKind.RelativeOrAbsolute));
            else
                this.imgTienthuong11.Source = (ImageSource)new BitmapImage(new Uri("/Images/frame1.png", UriKind.RelativeOrAbsolute));
        }

        private void _timerShowCacBe_Tick(object sender, EventArgs e)
        {
            ++this.CountCacBe;
            if (this.CountCacBe == 1)
            {
                this.imgBe1.Source = (ImageSource)new BitmapImage(new Uri("/Images/be1.png", UriKind.RelativeOrAbsolute));
                this.imgThink1.Visibility = Visibility.Visible;
                this.tblName1.Visibility = Visibility.Visible;
            }
            else if (this.CountCacBe == 2)
            {
                this._timerThanhBach.Start();
                this.imgBe1.Source = (ImageSource)new BitmapImage(new Uri("/Images/be1b.png", UriKind.RelativeOrAbsolute));
                this.imgThink1.Visibility = Visibility.Collapsed;
                this.tblName1.Visibility = Visibility.Collapsed;
                this.imgBe2.Source = (ImageSource)new BitmapImage(new Uri("/Images/be2.png", UriKind.RelativeOrAbsolute));
                this.imgThink2.Visibility = Visibility.Visible;
                this.tblName2.Visibility = Visibility.Visible;
            }
            else if (this.CountCacBe == 3)
            {
                this.imgBe2.Source = (ImageSource)new BitmapImage(new Uri("/Images/be2b.png", UriKind.RelativeOrAbsolute));
                this.imgThink2.Visibility = Visibility.Collapsed;
                this.tblName2.Visibility = Visibility.Collapsed;
                this.imgBe3.Source = (ImageSource)new BitmapImage(new Uri("/Images/be3.png", UriKind.RelativeOrAbsolute));
                this.imgThink3.Visibility = Visibility.Visible;
                this.tblName3.Visibility = Visibility.Visible;
            }
            else if (this.CountCacBe == 4)
            {
                this.imgBe3.Source = (ImageSource)new BitmapImage(new Uri("/Images/be3b.png", UriKind.RelativeOrAbsolute));
                this.imgThink3.Visibility = Visibility.Collapsed;
                this.tblName3.Visibility = Visibility.Collapsed;
                this.imgBe4.Source = (ImageSource)new BitmapImage(new Uri("/Images/be4.png", UriKind.RelativeOrAbsolute));
                this.imgThink4.Visibility = Visibility.Visible;
                this.tblName4.Visibility = Visibility.Visible;
            }
            else if (this.CountCacBe == 5)
            {
                this.imgBe4.Source = (ImageSource)new BitmapImage(new Uri("/Images/be4b.png", UriKind.RelativeOrAbsolute));
                this.imgThink4.Visibility = Visibility.Collapsed;
                this.tblName4.Visibility = Visibility.Collapsed;
                this.imgBe5.Source = (ImageSource)new BitmapImage(new Uri("/Images/be5.png", UriKind.RelativeOrAbsolute));
                this.imgThink5.Visibility = Visibility.Visible;
                this.tblName5.Visibility = Visibility.Visible;
            }
            else if (this.CountCacBe == 6)
            {
                this.imgBe5.Source = (ImageSource)new BitmapImage(new Uri("/Images/be5b.png", UriKind.RelativeOrAbsolute));
                this.imgThink5.Visibility = Visibility.Collapsed;
                this.tblName5.Visibility = Visibility.Collapsed;
                this._timerThanhBach.Stop();
            }
            else
            {
                if (this.CountCacBe != 7)
                    return;
                this.grdCacBe.Visibility = Visibility.Collapsed;
                this._timerShowCacBe.Stop();
                this.PlaySoundBG("sound/cacquyentrogiup.mp3");
                this._timerShowTroGiup.Start();
            }
        }

        private void _timerShowTroGiup_Tick(object sender, EventArgs e)
        {
            ++this.CountTroGiup;
            if (this.CountTroGiup == 1)
            {
                this._timerThanhBach.Start();
                this.tblLuatChoi.Text = this.Luat[5];
                this.imgThamKhao.Visibility = Visibility.Visible;
            }
            else if (this.CountTroGiup == 2)
                this.imgSaoChep.Visibility = Visibility.Visible;
            else if (this.CountTroGiup == 3)
                this.imgGiaiCuuMayMan.Visibility = Visibility.Visible;
            else if (this.CountTroGiup == 4)
            {
                this.tblLuatChoi.Text = this.Luat[6];
                this.imgThamKhao.Source = (ImageSource)new BitmapImage(new Uri("/Images/thamkhao2.png", UriKind.RelativeOrAbsolute));
                this.imgTieuDeThamKhao.Visibility = Visibility.Visible;
                this.imgNoiDungThamKhao.Visibility = Visibility.Visible;
            }
            else if (this.CountTroGiup == 12)
            {
                this.tblLuatChoi.Text = this.Luat[7];
                this.imgThamKhao.Source = (ImageSource)new BitmapImage(new Uri("/Images/thamkhao1.png", UriKind.RelativeOrAbsolute));
                this.imgTieuDeThamKhao.Visibility = Visibility.Collapsed;
                this.imgNoiDungThamKhao.Visibility = Visibility.Collapsed;
                this.imgSaoChep.Source = (ImageSource)new BitmapImage(new Uri("/Images/saochep2.png", UriKind.RelativeOrAbsolute));
                this.imgTieuDeSaoChep.Visibility = Visibility.Visible;
                this.imgNoiDungSaoChep.Visibility = Visibility.Visible;
            }
            else if (this.CountTroGiup == 18)
            {
                this.tblLuatChoi.Text = this.Luat[8];
                this.imgSaoChep.Source = (ImageSource)new BitmapImage(new Uri("/Images/saochep1.png", UriKind.RelativeOrAbsolute));
                this.imgTieuDeSaoChep.Visibility = Visibility.Collapsed;
                this.imgNoiDungSaoChep.Visibility = Visibility.Collapsed;
                this.imgGiaiCuuMayMan.Source = (ImageSource)new BitmapImage(new Uri("/Images/giaicuumayman2.png", UriKind.RelativeOrAbsolute));
                this.imgTieuDeGiaiCuuMayMan.Visibility = Visibility.Visible;
                this.imgNoiDungGiaiCuuMayMan.Visibility = Visibility.Visible;
            }
            else if (this.CountTroGiup == 25)
                this.tblLuatChoi.Text = this.Luat[9];
            else if (this.CountTroGiup == 32)
                this.tblLuatChoi.Text = this.Luat[10];
            else if (this.CountTroGiup == 40)
                this.tblLuatChoi.Text = this.Luat[11];
            else if (this.CountTroGiup == 45)
            {
                this.tblLuatChoi.Text = this.Luat[12];
            }
            else
            {
                if (this.CountTroGiup != 50)
                    return;
                this.CountTroGiup = 0;
                this._timerShowTroGiup.Stop();
                this._timerThanhBach.Stop();
                Global.idchude = 2;
                this.NavigationService.GoBack();
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
            if (this.Skip)
            {
                this.Skip = false;
                this.grdSkip.Visibility = Visibility.Collapsed;
                e.Cancel = true;
            }
            else
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
        }

        private void Vibrate()
        {
            if (!Global.vibrate)
                return;
            this.vc.Start(TimeSpan.FromMilliseconds(100.0));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.PlaySoundBG("sound/luatchoi.mp3");
            this._timerText.Start();
            this._timerShowLuatChoi.Start();
            this.LoadCauHoi();
        }

        private void imgSkip_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.Skip = true;
            this.grdSkip.Visibility = Visibility.Visible;
        }

        private void imgCo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCo.Source = (ImageSource)new BitmapImage(new Uri("/Images/co2.png", UriKind.RelativeOrAbsolute));
        }

        private void imgCo_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCo.Source = (ImageSource)new BitmapImage(new Uri("/Images/co1.png", UriKind.RelativeOrAbsolute));
            this.CountTroGiup = 0;
            this._timerShowTroGiup.Stop();
            this._timerThanhBach.Stop();
            this._timerText.Stop();
            this._timerShowLuatChoi.Stop();
            this._timerShowMaxTienThuong.Stop();
            this._timerShowCacBe.Stop();
            Global.idchude = 2;
            this.NavigationService.GoBack();
        }

        private void imgKhong_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgKhong.Source = (ImageSource)new BitmapImage(new Uri("/Images/khong2.png", UriKind.RelativeOrAbsolute));
        }

        private void imgKhong_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgKhong.Source = (ImageSource)new BitmapImage(new Uri("/Images/khong1.png", UriKind.RelativeOrAbsolute));
            this.grdSkip.Visibility = Visibility.Collapsed;
            this.Skip = false;
        }

        private void imgCoQuit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgCoQuit.Source = (ImageSource)new BitmapImage(new Uri("/Images/co2.png", UriKind.RelativeOrAbsolute));
        }

        private void imgCoQuit_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgCoQuit.Source = (ImageSource)new BitmapImage(new Uri("/Images/co1.png", UriKind.RelativeOrAbsolute));
            this.CountTroGiup = 0;
            this._timerShowTroGiup.Stop();
            this._timerThanhBach.Stop();
            this._timerText.Stop();
            this._timerShowLuatChoi.Stop();
            this._timerShowMaxTienThuong.Stop();
            this._timerShowCacBe.Stop();
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

        private static int RandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }

        private void ngaunhien_vt()
        {
            int[] numArray = new int[this.ar_vt.Length];
            for (int index = 0; index < numArray.Length; ++index)
                numArray[index] = index;
            int length = numArray.Length;
            for (int index1 = 0; index1 < this.ar_vt.Length; ++index1)
            {
                int num = 0;
                int index2 = PageLuatChoi.RandomNumber(0, length);
                this.ar_vt[index1] = numArray[index2];
                for (int index3 = 0; index3 < length; ++index3)
                {
                    if (numArray[index3] == this.ar_vt[index1])
                        num = index3;
                }
                --length;
                for (int index3 = num; index3 < length; ++index3)
                    numArray[index3] = numArray[index3 + 1];
            }
        }

        private void LoadCau1()
        {
            this.cau1 = PageLuatChoi.RandomNumber(0, 12);
        }

        private void LoadCau2()
        {
            this.cau2 = PageLuatChoi.RandomNumber(0, 12);
            if (this.cau2 != this.cau1)
                return;
            this.LoadCau2();
        }

        private void LoadCau3()
        {
            this.cau3 = PageLuatChoi.RandomNumber(0, 12);
            if (this.cau3 != this.cau1 && this.cau3 != this.cau2)
                return;
            this.LoadCau3();
        }

        private void LoadCau4()
        {
            this.cau4 = PageLuatChoi.RandomNumber(0, 12);
            if (this.cau4 != this.cau1 && this.cau4 != this.cau2 && this.cau4 != this.cau3)
                return;
            this.LoadCau4();
        }

        private void LoadCau11()
        {
            this.cau11 = PageLuatChoi.RandomNumber(0, 12);
            if (this.cau11 != this.cau1 && this.cau11 != this.cau2 && this.cau11 != this.cau3 && this.cau11 != this.cau4)
                return;
            this.LoadCau11();
        }

        private void LoadCau5()
        {
            this.cau5 = PageLuatChoi.RandomNumber(0, 26);
            if (this.cau5 != this.cau1 && this.cau5 != this.cau2 && (this.cau5 != this.cau3 && this.cau5 != this.cau4) && this.cau5 != this.cau11)
                return;
            this.LoadCau5();
        }

        private void LoadCau6()
        {
            this.cau6 = PageLuatChoi.RandomNumber(0, 26);
            if (this.cau6 != this.cau1 && this.cau6 != this.cau2 && (this.cau6 != this.cau3 && this.cau6 != this.cau4) && this.cau6 != this.cau5 && this.cau6 != this.cau11)
                return;
            this.LoadCau6();
        }

        private void LoadCau7()
        {
            this.cau7 = PageLuatChoi.RandomNumber(0, 26);
            if (this.cau7 != this.cau1 && this.cau7 != this.cau2 && (this.cau7 != this.cau3 && this.cau7 != this.cau4) && (this.cau7 != this.cau5 && this.cau7 != this.cau6) && this.cau7 != this.cau11)
                return;
            this.LoadCau7();
        }

        private void LoadCau8()
        {
            this.cau8 = PageLuatChoi.RandomNumber(0, 26);
            if (this.cau8 != this.cau1 && this.cau8 != this.cau2 && (this.cau8 != this.cau3 && this.cau8 != this.cau4) && (this.cau8 != this.cau5 && this.cau8 != this.cau6 && this.cau8 != this.cau7) && this.cau8 != this.cau11)
                return;
            this.LoadCau8();
        }

        private void LoadCau9()
        {
            this.cau9 = PageLuatChoi.RandomNumber(0, 26);
            if (this.cau9 != this.cau1 && this.cau9 != this.cau2 && (this.cau9 != this.cau3 && this.cau9 != this.cau4) && (this.cau9 != this.cau5 && this.cau9 != this.cau6 && (this.cau9 != this.cau7 && this.cau9 != this.cau8)) && this.cau9 != this.cau11)
                return;
            this.LoadCau9();
        }

        private void LoadCau10()
        {
            this.cau10 = PageLuatChoi.RandomNumber(0, 26);
            if (this.cau10 != this.cau1 && this.cau10 != this.cau2 && (this.cau10 != this.cau3 && this.cau10 != this.cau4) && (this.cau10 != this.cau5 && this.cau10 != this.cau6 && (this.cau10 != this.cau7 && this.cau10 != this.cau8)) && this.cau10 != this.cau9 && this.cau10 != this.cau11)
                return;
            this.LoadCau10();
        }

        private void LoadCauHoi()
        {
            this.LoadCau1();
            this.LoadCau2();
            this.LoadCau3();
            this.LoadCau4();
            this.LoadCau11();
            this.LoadCau5();
            this.LoadCau6();
            this.LoadCau7();
            this.LoadCau8();
            this.LoadCau9();
            this.LoadCau10();
            Global.CauHoi11 = this.ArrMon[this.cau11];
            string[] strArray = new string[10];
            int[] numArray = new int[10]
      {
        1,
        1,
        1,
        1,
        0,
        0,
        0,
        0,
        0,
        0
      };
            strArray[0] = this.ArrMon[this.cau1];
            strArray[1] = this.ArrMon[this.cau2];
            strArray[2] = this.ArrMon[this.cau3];
            strArray[3] = this.ArrMon[this.cau4];
            strArray[4] = this.ArrMon[this.cau5];
            strArray[5] = this.ArrMon[this.cau6];
            strArray[6] = this.ArrMon[this.cau7];
            strArray[7] = this.ArrMon[this.cau8];
            strArray[8] = this.ArrMon[this.cau9];
            strArray[9] = this.ArrMon[this.cau10];
            this.ngaunhien_vt();
            Global.CauHoi1 = strArray[this.ar_vt[0]];
            Global.CauHoi2 = strArray[this.ar_vt[1]];
            Global.CauHoi3 = strArray[this.ar_vt[2]];
            Global.CauHoi4 = strArray[this.ar_vt[3]];
            Global.CauHoi5 = strArray[this.ar_vt[4]];
            Global.CauHoi6 = strArray[this.ar_vt[5]];
            Global.CauHoi7 = strArray[this.ar_vt[6]];
            Global.CauHoi8 = strArray[this.ar_vt[7]];
            Global.CauHoi9 = strArray[this.ar_vt[8]];
            Global.CauHoi10 = strArray[this.ar_vt[9]];
            Global.TypeCauHoi1 = numArray[this.ar_vt[0]];
            Global.TypeCauHoi2 = numArray[this.ar_vt[1]];
            Global.TypeCauHoi3 = numArray[this.ar_vt[2]];
            Global.TypeCauHoi4 = numArray[this.ar_vt[3]];
            Global.TypeCauHoi5 = numArray[this.ar_vt[4]];
            Global.TypeCauHoi6 = numArray[this.ar_vt[5]];
            Global.TypeCauHoi7 = numArray[this.ar_vt[6]];
            Global.TypeCauHoi8 = numArray[this.ar_vt[7]];
            Global.TypeCauHoi9 = numArray[this.ar_vt[8]];
            Global.TypeCauHoi10 = numArray[this.ar_vt[9]];
        }

        //[DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/PageLuatChoi.xaml", UriKind.Relative));
        //    this.LayoutRoot = (Grid)this.FindName("LayoutRoot");
        //    this.mebg = (MediaElement)this.FindName("mebg");
        //    this.imgSkip = (Image)this.FindName("imgSkip");
        //    this.image2 = (Image)this.FindName("image2");
        //    this.imgThanhBach = (Image)this.FindName("imgThanhBach");
        //    this.image1 = (Image)this.FindName("image1");
        //    this.tblLuatChoi = (TextBlock)this.FindName("tblLuatChoi");
        //    this.grdCauHoi = (Grid)this.FindName("grdCauHoi");
        //    this.imgCauhoiA1 = (Image)this.FindName("imgCauhoiA1");
        //    this.imgCauhoiB1 = (Image)this.FindName("imgCauhoiB1");
        //    this.imgCauhoiA2 = (Image)this.FindName("imgCauhoiA2");
        //    this.imgCauhoiB2 = (Image)this.FindName("imgCauhoiB2");
        //    this.imgCauhoiA3 = (Image)this.FindName("imgCauhoiA3");
        //    this.imgCauhoiB3 = (Image)this.FindName("imgCauhoiB3");
        //    this.imgCauhoiA4 = (Image)this.FindName("imgCauhoiA4");
        //    this.imgCauhoiB4 = (Image)this.FindName("imgCauhoiB4");
        //    this.imgCauhoiA5 = (Image)this.FindName("imgCauhoiA5");
        //    this.imgCauhoiB5 = (Image)this.FindName("imgCauhoiB5");
        //    this.tblCauhoiA1 = (TextBlock)this.FindName("tblCauhoiA1");
        //    this.tblCauhoiA2 = (TextBlock)this.FindName("tblCauhoiA2");
        //    this.tblCauhoiA3 = (TextBlock)this.FindName("tblCauhoiA3");
        //    this.tblCauhoiA4 = (TextBlock)this.FindName("tblCauhoiA4");
        //    this.tblCauhoiA5 = (TextBlock)this.FindName("tblCauhoiA5");
        //    this.tblCauhoiB1 = (TextBlock)this.FindName("tblCauhoiB1");
        //    this.tblCauhoiB2 = (TextBlock)this.FindName("tblCauhoiB2");
        //    this.tblCauhoiB3 = (TextBlock)this.FindName("tblCauhoiB3");
        //    this.tblCauhoiB4 = (TextBlock)this.FindName("tblCauhoiB4");
        //    this.tblCauhoiB5 = (TextBlock)this.FindName("tblCauhoiB5");
        //    this.grdTienThuong = (Grid)this.FindName("grdTienThuong");
        //    this.imgTienthuong1 = (Image)this.FindName("imgTienthuong1");
        //    this.imgTienthuong6 = (Image)this.FindName("imgTienthuong6");
        //    this.imgTienthuong2 = (Image)this.FindName("imgTienthuong2");
        //    this.imgTienthuong7 = (Image)this.FindName("imgTienthuong7");
        //    this.imgTienthuong3 = (Image)this.FindName("imgTienthuong3");
        //    this.imgTienthuong8 = (Image)this.FindName("imgTienthuong8");
        //    this.imgTienthuong4 = (Image)this.FindName("imgTienthuong4");
        //    this.imgTienthuong9 = (Image)this.FindName("imgTienthuong9");
        //    this.imgTienthuong5 = (Image)this.FindName("imgTienthuong5");
        //    this.imgTienthuong10 = (Image)this.FindName("imgTienthuong10");
        //    this.imgTienthuong11 = (Image)this.FindName("imgTienthuong11");
        //    this.tblTienthuong1 = (TextBlock)this.FindName("tblTienthuong1");
        //    this.tblTienthuong2 = (TextBlock)this.FindName("tblTienthuong2");
        //    this.tblTienthuong3 = (TextBlock)this.FindName("tblTienthuong3");
        //    this.tblTienthuong4 = (TextBlock)this.FindName("tblTienthuong4");
        //    this.tblTienthuong5 = (TextBlock)this.FindName("tblTienthuong5");
        //    this.tblTienthuong6 = (TextBlock)this.FindName("tblTienthuong6");
        //    this.tblTienthuong7 = (TextBlock)this.FindName("tblTienthuong7");
        //    this.tblTienthuong8 = (TextBlock)this.FindName("tblTienthuong8");
        //    this.tblTienthuong9 = (TextBlock)this.FindName("tblTienthuong9");
        //    this.tblTienthuong10 = (TextBlock)this.FindName("tblTienthuong10");
        //    this.tblTienthuong11 = (TextBlock)this.FindName("tblTienthuong11");
        //    this.grdCacBe = (Grid)this.FindName("grdCacBe");
        //    this.imgBe1 = (Image)this.FindName("imgBe1");
        //    this.imgBe2 = (Image)this.FindName("imgBe2");
        //    this.imgBe3 = (Image)this.FindName("imgBe3");
        //    this.imgBe4 = (Image)this.FindName("imgBe4");
        //    this.imgBe5 = (Image)this.FindName("imgBe5");
        //    this.imgThink1 = (Image)this.FindName("imgThink1");
        //    this.tblName1 = (TextBlock)this.FindName("tblName1");
        //    this.imgThink2 = (Image)this.FindName("imgThink2");
        //    this.tblName2 = (TextBlock)this.FindName("tblName2");
        //    this.imgThink3 = (Image)this.FindName("imgThink3");
        //    this.tblName3 = (TextBlock)this.FindName("tblName3");
        //    this.imgThink4 = (Image)this.FindName("imgThink4");
        //    this.tblName4 = (TextBlock)this.FindName("tblName4");
        //    this.imgThink5 = (Image)this.FindName("imgThink5");
        //    this.tblName5 = (TextBlock)this.FindName("tblName5");
        //    this.imgSaoChep = (Image)this.FindName("imgSaoChep");
        //    this.imgThamKhao = (Image)this.FindName("imgThamKhao");
        //    this.imgGiaiCuuMayMan = (Image)this.FindName("imgGiaiCuuMayMan");
        //    this.imgTieuDeSaoChep = (Image)this.FindName("imgTieuDeSaoChep");
        //    this.imgNoiDungSaoChep = (Image)this.FindName("imgNoiDungSaoChep");
        //    this.imgTieuDeThamKhao = (Image)this.FindName("imgTieuDeThamKhao");
        //    this.imgNoiDungThamKhao = (Image)this.FindName("imgNoiDungThamKhao");
        //    this.imgTieuDeGiaiCuuMayMan = (Image)this.FindName("imgTieuDeGiaiCuuMayMan");
        //    this.imgNoiDungGiaiCuuMayMan = (Image)this.FindName("imgNoiDungGiaiCuuMayMan");
        //    this.grdSkip = (Grid)this.FindName("grdSkip");
        //    this.imgDialog = (Image)this.FindName("imgDialog");
        //    this.tblCauHoi = (TextBlock)this.FindName("tblCauHoi");
        //    this.imgCo = (Image)this.FindName("imgCo");
        //    this.imgKhong = (Image)this.FindName("imgKhong");
        //    this.grdQuit = (Grid)this.FindName("grdQuit");
        //    this.imgDialogQuit = (Image)this.FindName("imgDialogQuit");
        //    this.tblCauHoiQuit = (TextBlock)this.FindName("tblCauHoiQuit");
        //    this.imgCoQuit = (Image)this.FindName("imgCoQuit");
        //    this.imgKhongQuit = (Image)this.FindName("imgKhongQuit");
        //}
    }
}
