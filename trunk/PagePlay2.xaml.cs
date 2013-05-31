using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Xna.Framework.Audio;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Security;
namespace AiThongMinhHonLop5
{
    public partial class PagePlay2 : PhoneApplicationPage
    {
        private DispatcherTimer _timerTraLoi = new DispatcherTimer();
        private int countTraloi = 0;
        private VibrateController vc = VibrateController.Default;
        private string DapAnGiaiCuu = "";
        private string dapandung = "";
        private bool Quit = false;
        private string DapAnSaoChep = "";
        private bool DaChonTroGiup = false;
        private SoundEffect ambienceSoundEF;
        private SoundEffectInstance ambienceInstanceEF;

        public PagePlay2()
        {
            InitializeComponent();
            this._timerTraLoi.Tick += new EventHandler(this._timerTraLoi_Tick);
            this._timerTraLoi.Interval = TimeSpan.FromMilliseconds(500.0);
        }

       
        private static int RandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }

        private void _timerTraLoi_Tick(object sender, EventArgs e)
        {
            ++this.countTraloi;
            if (this.countTraloi == 1)
            {
                if (Global.CauHoi > 10)
                    this.PlaySoundBG("sound/dapan7.wma");
                else
                    this.PlaySoundBG("sound/dapan" + PagePlay2.RandomNumber(1, 7).ToString() + ".wma");
            }
            if (this.countTraloi == 8)
            {
                try
                {
                    this.ambienceInstanceEF.Stop();
                }
                catch
                {
                }
                this.PlaySoundEF("sound/dapanbg.wav");
            }
            if (this.countTraloi == 12)
            {
                try
                {
                    this.ambienceInstanceEF.Stop();
                }
                catch
                {
                }
                this.PlaySoundBG("sound/chondapan.wav");
                this.grdShowDA.Visibility = Visibility.Visible;
            }
            if (this.countTraloi == 13)
                this.tblDa.Text = this.dapandung;
            if (this.countTraloi == 15)
            {
                if (PagePlay2.Convert(this.txtDA.Text.Trim().ToUpper()) == PagePlay2.Convert(this.dapandung.Trim().ToUpper()))
                {
                    this.PlaySoundBG("sound/votay.mp3");
                    Global.totalScore = this.CoverScore(Global.lanchon);
                    if (Global.lanchon > 10)
                        Global.Win = true;
                }
                else
                    this.PlaySoundBG("sound/soundsai.mp3");
            }
            if (this.countTraloi == 18)
            {
                if (PagePlay2.Convert(this.txtDA.Text.Trim().ToUpper()) == PagePlay2.Convert(this.dapandung.Trim().ToUpper()))
                {
                    this.countTraloi = 0;
                    this._timerTraLoi.Stop();
                    if (Global.CountChonBe == 2 && Global.lanchon < 10)
                        this.TraloidungBack2();
                    else if (Global.CountChonBe == 1 || Global.lanchon == 10)
                        this.TraloidungBack1();
                    else if (Global.lanchon >= 11)
                        this.Traloisai();
                }
                else if (!Global.GiaiCuuMayMan && !this.DaChonTroGiup)
                {
                    this.TraloiSaiGiaiCuuMayMan();
                    Global.GiaiCuuMayMan = true;
                    if (this.DapAnGiaiCuu == this.dapandung)
                        this.PlaySoundBG("sound/votay.mp3");
                    else
                        this.PlaySoundBG("sound/soundsai.mp3");
                }
                else
                {
                    this.countTraloi = 0;
                    this._timerTraLoi.Stop();
                    this.Traloisai();
                }
            }
            if (this.countTraloi != 25)
                return;
            if (this.DapAnGiaiCuu == this.dapandung)
            {
                this.countTraloi = 0;
                this._timerTraLoi.Stop();
                if (Global.CountChonBe == 2)
                    this.TraloidungBack2();
                else
                    this.TraloidungBack1();
            }
            else
            {
                this.countTraloi = 0;
                this._timerTraLoi.Stop();
                this.Traloisai();
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            this.grdShowDA.Visibility = Visibility.Collapsed;
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

        private void Vibrate()
        {
            if (!Global.vibrate)
                return;
            this.vc.Start(TimeSpan.FromMilliseconds(100.0));
        }

        public static string Convert(string text)
        {
            text = text.Replace("A", "A");
            text = text.Replace("a", "a");
            text = text.Replace("Ă", "A");
            text = text.Replace("ă", "a");
            text = text.Replace("Â", "A");
            text = text.Replace("â", "a");
            text = text.Replace("E", "E");
            text = text.Replace("e", "e");
            text = text.Replace("Ê", "E");
            text = text.Replace("ê", "e");
            text = text.Replace("I", "I");
            text = text.Replace("i", "i");
            text = text.Replace("O", "O");
            text = text.Replace("o", "o");
            text = text.Replace("Ô", "O");
            text = text.Replace("ô", "o");
            text = text.Replace("Ơ", "O");
            text = text.Replace("ơ", "o");
            text = text.Replace("U", "U");
            text = text.Replace("u", "u");
            text = text.Replace("Ư", "U");
            text = text.Replace("ư", "u");
            text = text.Replace("Y", "Y");
            text = text.Replace("y", "y");
            text = text.Replace("À", "A");
            text = text.Replace("à", "a");
            text = text.Replace("Ằ", "A");
            text = text.Replace("ằ", "a");
            text = text.Replace("Ầ", "A");
            text = text.Replace("ầ", "a");
            text = text.Replace("È", "E");
            text = text.Replace("è", "e");
            text = text.Replace("Ề", "E");
            text = text.Replace("ề", "e");
            text = text.Replace("Ì", "I");
            text = text.Replace("ì", "i");
            text = text.Replace("Ò", "O");
            text = text.Replace("ò", "o");
            text = text.Replace("Ồ", "O");
            text = text.Replace("ồ", "o");
            text = text.Replace("Ờ", "O");
            text = text.Replace("ờ", "o");
            text = text.Replace("Ù", "U");
            text = text.Replace("ù", "u");
            text = text.Replace("Ừ", "U");
            text = text.Replace("ừ", "u");
            text = text.Replace("Ỳ", "Y");
            text = text.Replace("ỳ", "y");
            text = text.Replace("Á", "A");
            text = text.Replace("á", "a");
            text = text.Replace("Ắ", "A");
            text = text.Replace("ắ", "a");
            text = text.Replace("Ấ", "A");
            text = text.Replace("ấ", "a");
            text = text.Replace("É", "E");
            text = text.Replace("é", "e");
            text = text.Replace("Ế", "E");
            text = text.Replace("ế", "e");
            text = text.Replace("Í", "I");
            text = text.Replace("í", "i");
            text = text.Replace("Ó", "O");
            text = text.Replace("ó", "o");
            text = text.Replace("Ố", "O");
            text = text.Replace("ố", "o");
            text = text.Replace("Ớ", "O");
            text = text.Replace("ớ", "o");
            text = text.Replace("Ú", "U");
            text = text.Replace("ú", "u");
            text = text.Replace("Ứ", "U");
            text = text.Replace("ứ", "u");
            text = text.Replace("Ý", "Y");
            text = text.Replace("ý", "y");
            text = text.Replace("Ả", "A");
            text = text.Replace("ả", "a");
            text = text.Replace("Ẳ", "A");
            text = text.Replace("ẳ", "a");
            text = text.Replace("Ẩ", "A");
            text = text.Replace("ẩ", "a");
            text = text.Replace("Ẻ", "E");
            text = text.Replace("ẻ", "e");
            text = text.Replace("Ể", "E");
            text = text.Replace("ể", "e");
            text = text.Replace("Ỉ", "I");
            text = text.Replace("ỉ", "i");
            text = text.Replace("Ỏ", "O");
            text = text.Replace("ỏ", "o");
            text = text.Replace("Ổ", "O");
            text = text.Replace("ổ", "o");
            text = text.Replace("Ở", "O");
            text = text.Replace("ở", "o");
            text = text.Replace("Ủ", "U");
            text = text.Replace("ủ", "u");
            text = text.Replace("Ử", "U");
            text = text.Replace("ử", "u");
            text = text.Replace("Ỷ", "Y");
            text = text.Replace("ỷ", "y");
            text = text.Replace("Ã", "A");
            text = text.Replace("ã", "a");
            text = text.Replace("Ẵ", "A");
            text = text.Replace("ẵ", "a");
            text = text.Replace("Ẫ", "A");
            text = text.Replace("ẫ", "a");
            text = text.Replace("Ẽ", "E");
            text = text.Replace("ẽ", "e");
            text = text.Replace("Ễ", "E");
            text = text.Replace("ễ", "e");
            text = text.Replace("Ĩ", "I");
            text = text.Replace("ĩ", "i");
            text = text.Replace("Õ", "O");
            text = text.Replace("õ", "o");
            text = text.Replace("Ỗ", "O");
            text = text.Replace("ỗ", "o");
            text = text.Replace("Ỡ", "O");
            text = text.Replace("ỡ", "o");
            text = text.Replace("Ũ", "U");
            text = text.Replace("ũ", "u");
            text = text.Replace("Ữ", "U");
            text = text.Replace("ữ", "u");
            text = text.Replace("Ỹ", "Y");
            text = text.Replace("ỹ", "y");
            text = text.Replace("Ạ", "A");
            text = text.Replace("ạ", "a");
            text = text.Replace("Ặ", "A");
            text = text.Replace("ặ", "a");
            text = text.Replace("Ậ", "A");
            text = text.Replace("ậ", "a");
            text = text.Replace("Ẹ", "E");
            text = text.Replace("ẹ", "e");
            text = text.Replace("Ệ", "E");
            text = text.Replace("ệ", "e");
            text = text.Replace("Ị", "I");
            text = text.Replace("ị", "i");
            text = text.Replace("Ọ", "O");
            text = text.Replace("ọ", "o");
            text = text.Replace("Ộ", "O");
            text = text.Replace("ộ", "o");
            text = text.Replace("Ợ", "O");
            text = text.Replace("ợ", "o");
            text = text.Replace("Ụ", "U");
            text = text.Replace("ụ", "u");
            text = text.Replace("Ự", "U");
            text = text.Replace("ự", "u");
            text = text.Replace("Ỵ", "Y");
            text = text.Replace("ỵ", "y");
            text = text.Replace("Đ", "D");
            text = text.Replace("đ", "d");
            return text;
        }

        private void TraloidungBack1()
        {
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

        private void TraloidungBack2()
        {
            Global.idchude = 2;
            try
            {
                this.ambienceInstanceEF.Stop();
            }
            catch
            {
            }
            this.NavigationService.GoBack();
        }

        private void TraloiSaiGiaiCuuMayMan()
        {
            collapse_cauhoi();// an phan cau hoi
            this.grdShowDA.Visibility = Visibility.Collapsed; // an phan dap an
            this.grdShowGiaiCuu.Visibility = Visibility.Visible;
            if (PagePlay2.RandomNumber(0, 2) == 0)
            {
                this.tblDapAnGiaiCuu.Text = "Xin lỗi cậu!";
                this.tblKetquaGiaiCuu.Text = "Tớ không có câu trả lời!";
            }
            else
            {
               
                this.tblDapAnGiaiCuu.Text = "Đáp án của tớ là ";
                this.tblKetquaGiaiCuu.Text = this.dapandung;
                this.DapAnGiaiCuu = this.dapandung;
            }
            
        }

        private void SaoChep()
        {
        }

        private void Traloisai()
        {
            Global.idchude = 10;
            try
            {
                this.ambienceInstanceEF.Stop();
            }
            catch
            {
            }
            this.NavigationService.GoBack();
        }

        private int CoverScore(int lanchon)
        {
            int num = 0;
            if (lanchon == 1)
                num = 500000;
            else if (lanchon == 2)
                num = 1000000;
            else if (lanchon == 3)
                num = 1500000;
            else if (lanchon == 4)
                num = 2000000;
            else if (lanchon == 5)
                num = 3000000;
            else if (lanchon == 6)
                num = 4000000;
            else if (lanchon == 7)
                num = 6000000;
            else if (lanchon == 8)
                num = 10000000;
            else if (lanchon == 9)
                num = 14000000;
            else if (lanchon == 10)
                num = 22000000;
            else if (lanchon == 11)
                num = 50000000;
            return num;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Global.GiaiCuuSaoChep)
                this.tblSaoChep.Visibility = Visibility.Collapsed;
            if (Global.GiaiCuuThamKhao)
                this.tblThamKhao.Visibility = Visibility.Collapsed;
            //this.imgRoundBe2.Source = (ImageSource)new BitmapImage(new Uri("/Images/roundBe" + Global.ChonBe.ToString() + ".png", UriKind.RelativeOrAbsolute));
            //this.imgRoundBe.Source = (ImageSource)new BitmapImage(new Uri("/Images/roundBe" + Global.ChonBe.ToString() + ".png", UriKind.RelativeOrAbsolute));
            //this.imgRoundBe3.Source = (ImageSource)new BitmapImage(new Uri("/Images/roundBe" + Global.ChonBe.ToString() + ".png", UriKind.RelativeOrAbsolute));
            ++Global.CountChonBe;
            Global.totalScore = this.CoverScore(Global.lanchon - 1);
            this.tblScore.Text = this.CoverScore(Global.lanchon).ToString();
            this.PlaySoundEF("sound/bgPlay.wav");
            if (PagePlay2.RandomNumber(1, 3) == 1)
                this.PlaySoundBG("sound/cauhoinhusau1.wma");
            else
                this.PlaySoundBG("sound/cauhoinhusau2.wma");
            this.LoadXML(Global.RealChudeCauHoi);
        }

        private void LoadXML(string chude)
        {
            IsolatedStorageFile storeForApplication1 = IsolatedStorageFile.GetUserStoreForApplication();
            StreamReader streamReader = new StreamReader((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1" + chude + ".sys", FileMode.Open, storeForApplication1));
            string str = streamReader.ReadLine();
            streamReader.Close();
            storeForApplication1.Dispose();
            string[] strArray = str.Split(new char[1]{'-'});
            int RealID = int.Parse(strArray[0]);
            ++RealID;
            if (RealID > int.Parse(strArray[1]))
                RealID = 1;
            IsolatedStorageFile storeForApplication2 = IsolatedStorageFile.GetUserStoreForApplication();
            storeForApplication2.DeleteFile("AiThongMinhHonLop5\\1" + chude + ".sys");
            StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1" + chude + ".sys", FileMode.Create, storeForApplication2));
            streamWriter.WriteLine(  (RealID).ToString()+ "-" + strArray[1]);
            streamWriter.Close();
            try
            {
                //StreamResourceInfo resourceStream = Application.GetResourceStream(new Uri("/Data/DT2.xml", UriKind.Relative));
                //using (IsolatedStorageFile.GetUserStoreForApplication())
               // {
                //    foreach (XElement xelement in Enumerable.Where<XElement>(XDocument.Load(resourceStream.Stream).Root.Elements((XName)"ProductItem"), (Func<XElement, bool>)(p => (string)p.Element((XName)"SUBJECT") == chude && (string)p.Element((XName)"TYPE") == "1" && (string)p.Element((XName)"ID") == RealID.ToString())))
                XDocument xmlDT1 = XDocument.Load(@"Data\DT2.xml");
                var Cauhoi = from p in xmlDT1.Descendants("ProductItem") where ((string)p.Element("SUBJECT").Value == chude && (string)p.Element("ID").Value == RealID.ToString() && (string)p.Element("TYPE").Value == "1") select p;
                  foreach (var item in Cauhoi)  
                {
                        this.tblCauHoi.Text = item.Element("QUESTION").Value;
                        this.dapandung = item.Element("ANSWER").Value.Trim();
                    }
               // }
            }
            catch
            {
            }
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

        private void Traloicauhoi()
        {
            this.PlaySoundBG("sound/chon.mp3");
            this._timerTraLoi.Start();
        }

        private void tblChotDapAn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.txtDA.Text != "Nhập vào đáp án không dấu" && this.txtDA.Text != "")
            {
                this.tblChotDapAn.IsHitTestVisible = false;
                this.imgChotDapAn.IsHitTestVisible = false;
                this.txtDA.IsReadOnly = true;
                this.Vibrate();
                this.imgChotDapAn.Source = (ImageSource)new BitmapImage(new Uri("/Images/tracnghiem2.png", UriKind.RelativeOrAbsolute));
                this.Traloicauhoi();
            }
            else
            {
                int num = (int)MessageBox.Show("Xin vui lòng nhập vào đáp án!");
            }
        }

        private void tblChotDapAn_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgChotDapAn.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
        }

        private void txtName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!(this.txtDA.Text == "Nhập vào đáp án không dấu"))
                return;
            this.txtDA.Text = "";
        }

        private void txtDA_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(this.txtDA.Text.Trim() == ""))
                return;
            this.txtDA.Text = "Nhập vào đáp án không dấu";
        }

        private void tblThamKhao_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            collapse_cauhoi();
            this.tblThamKhao.Source = (ImageSource)new BitmapImage(new Uri("/Images/thamkhao1.png", UriKind.RelativeOrAbsolute));

            this.DaChonTroGiup = true;
            Global.GiaiCuuThamKhao = true;
            this.tblThamKhao.Visibility = Visibility.Collapsed;
            this.tblSaoChep.IsHitTestVisible = false;
            this.PlaySoundBG("sound/quyenthamkhao.wma");
            this.grdShowThamKhao.Visibility = Visibility.Visible;
            if (PagePlay2.RandomNumber(0, 2) == 0)
            {
                
                this.tblDongThamKhao2.Text = "";
                this.tblDapAnThamKhao.Text = "Xin lỗi cậu! Hiện tại tớ chưa suy nghĩ ra đáp án.";
            }
            else
            {
                this.tblDongThamKhao2.Text = this.dapandung;
                this.tblDapAnThamKhao.Text = "Đáp án của tớ là phương án";
            }
        }

        private void tblSaoChep_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            collapse_cauhoi();
            this.tblSaoChep.Source = (ImageSource)new BitmapImage(new Uri("/Images/saochep1.png", UriKind.RelativeOrAbsolute));

            this.DaChonTroGiup = true;
            this.Vibrate();
            Global.GiaiCuuSaoChep = true;
            this.tblSaoChep.Visibility = Visibility.Collapsed;
            this.tblThamKhao.IsHitTestVisible = false;
            this.PlaySoundBG("sound/soundclick.mp3");
            this.grdShowSaoChep.Visibility = Visibility.Visible;
            switch (PagePlay2.RandomNumber(0, 6))
            {
                case 0:
                    this.tblDapAnSaoChep1.Text = "Tớ rất tự tin với đáp án này! Cậu hãy sử dụng đáp án của tớ nhé!";
                    this.DapAnSaoChep = this.dapandung;
                    break;
                case 1:
                    this.tblDapAnSaoChep1.Text = "Câu này dễ mà! Cậu hãy suy nghĩ một tí sẽ được thôi!";
                    this.DapAnSaoChep = this.dapandung;
                    break;
                case 2:
                    this.tblDapAnSaoChep1.Text = "Hi hi! Câu hỏi dễ quá! Hãy sao chép đáp án của tớ nhé!";
                    this.DapAnSaoChep = this.dapandung;
                    break;
                case 3:
                    this.tblDapAnSaoChep1.Text = "Tớ không tự tin lắm! Cậu nên suy nghĩ thật kỹ nhé!";
                    if (PagePlay2.RandomNumber(0, 2) == 0)
                    {
                        this.DapAnSaoChep = "Không có đáp án";
                        break;
                    }
                    else
                    {
                        this.DapAnSaoChep = this.dapandung;
                        break;
                    }
                case 4:
                    this.tblDapAnSaoChep1.Text = "Tớ chưa từng nghe câu này bao giờ! Cậu nên suy nghĩ thật kỹ nhé!";
                    if (PagePlay2.RandomNumber(0, 3) == 0)
                    {
                        this.DapAnSaoChep = this.dapandung;
                        break;
                    }
                    else
                    {
                        this.DapAnSaoChep = "Là gì thế nhỉ?";
                        break;
                    }
                case 5:
                    this.tblDapAnSaoChep1.Text = "Hì hì! Đáp án của tớ chắc ăn luôn!";
                    this.DapAnSaoChep = this.dapandung;
                    break;
            }
        }

        private void imgCoSaoChep_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.grdShowSaoChep.Visibility = Visibility.Collapsed;
            this.txtDA.Text = this.DapAnSaoChep;
            this.tblChotDapAn.IsHitTestVisible = false;
            this.imgChotDapAn.IsHitTestVisible = false;
            this.txtDA.IsReadOnly = true;
            this.Vibrate();
            this.imgChotDapAn.Source = (ImageSource)new BitmapImage(new Uri("/Images/tracnghiem2.png", UriKind.RelativeOrAbsolute));
            this.Traloicauhoi();
            visibility_cauhoi();
        }

        private void imgKhongSaoChep_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.grdShowSaoChep.Visibility = Visibility.Collapsed;
            visibility_cauhoi();
        }

        private void tblDongThamKhao_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.grdShowThamKhao.Visibility = Visibility.Collapsed;
            this.visibility_cauhoi();
        }
       
        public void collapse_cauhoi()
        {
            this.tblCauHoi.Visibility = Visibility.Collapsed;
            this.txtDA.Visibility = Visibility.Collapsed;
            this.image3.Visibility = Visibility.Collapsed;
            this.tblChotDapAn.Visibility = Visibility.Collapsed;
            this.imgChotDapAn.Visibility = Visibility.Collapsed;
        }
        
        public void visibility_cauhoi()
        {
            this.tblCauHoi.Visibility = Visibility.Visible;
            this.txtDA.Visibility = Visibility.Visible;
            this.image3.Visibility = Visibility.Visible;
            this.tblChotDapAn.Visibility = Visibility.Visible;
            this.imgChotDapAn.Visibility = Visibility.Visible;
        }
    }
}
