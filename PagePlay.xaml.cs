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

namespace AiThongMinhHonLop5
{
    public partial class PagePlay : PhoneApplicationPage
    {
        private DispatcherTimer _timerTraLoi = new DispatcherTimer();
        private int countTraloi = 0;
        private VibrateController vc = VibrateController.Default;
        private string DapAnGiaiCuu = "";
        private string dapandung = "A";
        private string dapantraloi = "A";
        private string kqA = "";
        private string kqB = "";
        private string kqC = "";
        private string kqD = "";
        private bool Quit = false;
        private string DapAnSaoChep = "";
        private bool DaChonTroGiup = false;
        private SoundEffect ambienceSoundEF;
        private SoundEffectInstance ambienceInstanceEF;
       

        public PagePlay()
        {
            this.InitializeComponent();
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
                    this.PlaySoundBG("sound/dapan" + PagePlay.RandomNumber(1, 7).ToString() + ".wma");
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
                this.PlaySoundEF2("sound/chondapan.wav");
                this.grdShowDA.Visibility = Visibility.Visible;
            }
            if (this.countTraloi == 13)
            {
                this.tblDaABCD.Text = this.dapandung.ToUpper();
                if (this.dapandung == "A")
                {
                    this.tblDAChuongTrinh.Text = this.kqA;
                    this.PlaySoundBG("sound/daA.wma");
                }
                else if (this.dapandung == "B")
                {
                    this.tblDAChuongTrinh.Text = this.kqB;
                    this.PlaySoundBG("sound/daB.wma");
                }
                else if (this.dapandung == "C")
                {
                    this.tblDAChuongTrinh.Text = this.kqC;
                    this.PlaySoundBG("sound/daC.wma");
                }
                else if (this.dapandung == "D")
                {
                    this.tblDAChuongTrinh.Text = this.kqD;
                    this.PlaySoundBG("sound/daD.wma");
                }
            }
            if (this.countTraloi == 15)
            {
                if (this.dapantraloi == this.dapandung)
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
                if (this.dapantraloi == this.dapandung)
                {
                    if (Global.CountChonBe == 2 && Global.lanchon < 10)
                        this.TraloidungBack2();
                    else if (Global.CountChonBe == 1 || Global.lanchon == 10)
                        this.TraloidungBack1();
                    else if (Global.lanchon >= 11)
                        this.Traloisai();
                    this.countTraloi = 0;
                    this._timerTraLoi.Stop();
                }
                else if (!Global.GiaiCuuMayMan && !this.DaChonTroGiup)
                {
                    this.TraloiSaiGiaiCuuMayMan();
                    Global.GiaiCuuMayMan = true;
                    if (this.DapAnGiaiCuu == this.dapandung.ToUpper())
                        this.PlaySoundBG("sound/votay.mp3");
                    else
                        this.PlaySoundBG("sound/soundsai.mp3");
                }
                else
                {
                    this.Traloisai();
                    this.countTraloi = 0;
                    this._timerTraLoi.Stop();
                }
            }
            if (this.countTraloi != 25)
                return;
            if (this.DapAnGiaiCuu == this.dapandung.ToUpper())
            {
                if (Global.CountChonBe == 2)
                    this.TraloidungBack2();
                else
                    this.TraloidungBack1();
                this.countTraloi = 0;
                this._timerTraLoi.Stop();
            }
            else
            {
                this.Traloisai();
                this.countTraloi = 0;
                this._timerTraLoi.Stop();
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
            catch (NullReferenceException )
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
            collapse_cauhoi();
            this.grdShowDA.Visibility = Visibility.Collapsed;
            this.grdShowGiaiCuu.Visibility = Visibility.Visible;
            switch (PagePlay.RandomNumber(0, 10))
            {
                case 0:
                    this.tblDapAnGiaiCuu.Text = "Đáp án của tớ là A";
                    this.tblKetquaGiaiCuu.Text = this.kqA;
                    this.DapAnGiaiCuu = "A";
                    break;
                case 1:
                    this.tblDapAnGiaiCuu.Text = "Đáp án của tớ là B";
                    this.tblKetquaGiaiCuu.Text = this.kqB;
                    this.DapAnGiaiCuu = "B";
                    break;
                case 2:
                    this.tblDapAnGiaiCuu.Text = "Đáp án của tớ là C";
                    this.tblKetquaGiaiCuu.Text = this.kqC;
                    this.DapAnGiaiCuu = "C";
                    break;
                case 3:
                    this.tblDapAnGiaiCuu.Text = "Đáp án của tớ là D";
                    this.tblKetquaGiaiCuu.Text = this.kqD;
                    this.DapAnGiaiCuu = "D";
                    break;
                default:
                    this.DapAnGiaiCuu = this.dapandung.ToUpper();
                    this.tblDapAnGiaiCuu.Text = "Đáp án của tớ là " + this.dapandung.ToUpper();
                    if (this.dapandung.ToUpper() == "A")
                        this.tblKetquaGiaiCuu.Text = this.kqA;
                    else if (this.dapandung.ToUpper() == "B")
                        this.tblKetquaGiaiCuu.Text = this.kqB;
                    else if (this.dapandung.ToUpper() == "C")
                        this.tblKetquaGiaiCuu.Text = this.kqC;
                    else if (this.dapandung.ToUpper() == "D")
                        this.tblKetquaGiaiCuu.Text = this.kqD;
                    break;
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
            if (Global.GiaiCuuSaoChep == true)
                this.tblSaoChep.Visibility = Visibility.Collapsed;
            if (Global.GiaiCuuThamKhao ==true)
                this.tblThamKhao.Visibility = Visibility.Collapsed;
           // this.imgRoundBe.Source = (ImageSource)new BitmapImage(new Uri("/Images/roundBe" + Global.ChonBe.ToString() + ".png", UriKind.RelativeOrAbsolute));
            //this.imgRoundBe2.Source = (ImageSource)new BitmapImage(new Uri("/Images/roundBe" + Global.ChonBe.ToString() + ".png", UriKind.RelativeOrAbsolute));
            //this.imgRoundBe3.Source = (ImageSource)new BitmapImage(new Uri("/Images/roundBe" + Global.ChonBe.ToString() + ".png", UriKind.RelativeOrAbsolute));
            //this.imgRoundBe4.Source = (ImageSource)new BitmapImage(new Uri("/Images/roundBe" + Global.ChonBe.ToString() + ".png", UriKind.RelativeOrAbsolute));
            ++Global.CountChonBe;
            Global.totalScore = this.CoverScore(Global.lanchon - 1);
            this.tblScore.Text = this.CoverScore(Global.lanchon).ToString();
            this.PlaySoundEF("sound/bgPlay.wav");
            if (PagePlay.RandomNumber(1, 3) == 1)
                this.PlaySoundBG("sound/cauhoinhusau1.wma");
            else
                this.PlaySoundBG("sound/cauhoinhusau2.wma");
            this.LoadXML(Global.RealChudeCauHoi);
        }

        private void DisableTbl()
        {
            this.tblA.IsHitTestVisible = false;
            this.tblB.IsHitTestVisible = false;
            this.tblC.IsHitTestVisible = false;
            this.tblD.IsHitTestVisible = false;
            this.imgA.IsHitTestVisible = false;
            this.imgB.IsHitTestVisible = false;
            this.imgC.IsHitTestVisible = false;
            this.imgD.IsHitTestVisible = false;
            this.tblSaoChep.IsHitTestVisible = false;
            this.tblThamKhao.IsHitTestVisible = false;
        }

        private void LoadXML(string chude)
        {
            IsolatedStorageFile storeForApplication1 = IsolatedStorageFile.GetUserStoreForApplication();
            StreamReader streamReader = new StreamReader((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0" + chude + ".sys", FileMode.Open, storeForApplication1));
            string str = streamReader.ReadLine();
            streamReader.Close();
            storeForApplication1.Dispose();
            string[] strArray = str.Split(new char[1]{'-'});
            int RealID = int.Parse(strArray[0]);
            ++RealID;
            if (RealID > int.Parse(strArray[1]))
                RealID = 1;
            IsolatedStorageFile storeForApplication2 = IsolatedStorageFile.GetUserStoreForApplication();
            storeForApplication2.DeleteFile("AiThongMinhHonLop5\\0" + chude + ".sys");
            StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0" + chude + ".sys", FileMode.Create, storeForApplication2));
            streamWriter.WriteLine(Convert.ToString( RealID) +"-" + strArray[1]);
            streamWriter.Close();
           try
            {
                XDocument xmlDT1 = XDocument.Load(@"Data\DT1.xml");
                var Cauhoi = from p in xmlDT1.Descendants("ProductItem") where((string)p.Element("SUBJECT").Value== chude && (string)p.Element("ID").Value==RealID.ToString()&& (string)p.Element("TYPE").Value=="0") select p;
                foreach(var item in Cauhoi)
                    {
                        this.tblCauHoi.Text = item.Element("QUESTION").Value;
                        this.tblA.Text = "A: " + item.Element("A").Value;
                        this.tblB.Text = "B: " + item.Element("B").Value;
                        this.tblC.Text = "C: " + item.Element("C").Value;
                        this.tblD.Text = "D: " + item.Element("D").Value;
                        this.kqA = item.Element("A").Value;
                        this.kqB = item.Element("B").Value;
                        this.kqC = item.Element("C").Value;
                        this.kqD = item.Element("D").Value;
                        this.dapandung = item.Element("ANSWER").Value.Trim();
                    }
                
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

        private void tblA_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DisableTbl();
            this.dapantraloi = "A";
            this.Vibrate();
            this.imgA.Source = (ImageSource)new BitmapImage(new Uri("/Images/tracnghiem2.png", UriKind.RelativeOrAbsolute));
            this.Traloicauhoi();
        }

        private void tblA_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgA.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
        }

        private void tblB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DisableTbl();
            this.dapantraloi = "B";
            this.Vibrate();
            this.imgB.Source = (ImageSource)new BitmapImage(new Uri("/Images/tracnghiem2.png", UriKind.RelativeOrAbsolute));
            this.Traloicauhoi();
        }

        private void tblB_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgB.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
        }

        private void tblC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DisableTbl();
            this.dapantraloi = "C";
            this.Vibrate();
            this.imgC.Source = (ImageSource)new BitmapImage(new Uri("/Images/tracnghiem2.png", UriKind.RelativeOrAbsolute));
            this.Traloicauhoi();
        }

        private void tblC_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgC.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
        }

        private void tblD_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DisableTbl();
            this.dapantraloi = "D";
            this.Vibrate();
            this.imgD.Source = (ImageSource)new BitmapImage(new Uri("/Images/tracnghiem2.png", UriKind.RelativeOrAbsolute));
            this.Traloicauhoi();
        }

        private void tblD_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgD.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
        }

        private void tblThamKhao_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            collapse_cauhoi();
            this.tblThamKhao.Source = (ImageSource)new BitmapImage(new Uri("/Images/thamkhao1.png", UriKind.RelativeOrAbsolute));
            this.DaChonTroGiup = true;
            this.Vibrate();
            Global.GiaiCuuThamKhao = true;
            this.tblThamKhao.Visibility = Visibility.Collapsed;
            this.tblSaoChep.IsHitTestVisible = false;
            this.PlaySoundBG("sound/quyenthamkhao.wma");
            this.grdShowThamKhao.Visibility = Visibility.Visible;
            switch (PagePlay.RandomNumber(0, 6))
            {
                case 0:
                    switch (PagePlay.RandomNumber(0, 8))
                    {
                        case 0:
                            this.tblDongThamKhao2.Text = "A";
                            break;
                        case 1:
                            this.tblDongThamKhao2.Text = "B";
                            break;
                        case 2:
                            this.tblDongThamKhao2.Text = "C";
                            break;
                        case 3:
                            this.tblDongThamKhao2.Text = "D";
                            break;
                        default:
                            this.tblDongThamKhao2.Text = this.dapandung;
                            break;
                    }
                    this.tblDapAnThamKhao.Text = "Đáp án của tớ là phương án";
                    break;
                case 1:
                    switch (PagePlay.RandomNumber(0, 10))
                    {
                        case 0:
                            this.tblDongThamKhao2.Text = "A";
                            break;
                        case 1:
                            this.tblDongThamKhao2.Text = "B";
                            break;
                        case 2:
                            this.tblDongThamKhao2.Text = "C";
                            break;
                        case 3:
                            this.tblDongThamKhao2.Text = "D";
                            break;
                        default:
                            this.tblDongThamKhao2.Text = this.dapandung;
                            break;
                    }
                    this.tblDapAnThamKhao.Text = "Câu trả lời của tớ là";
                    break;
                case 2:
                    switch (PagePlay.RandomNumber(0, 12))
                    {
                        case 0:
                            this.tblDongThamKhao2.Text = "A";
                            break;
                        case 1:
                            this.tblDongThamKhao2.Text = "B";
                            break;
                        case 2:
                            this.tblDongThamKhao2.Text = "C";
                            break;
                        case 3:
                            this.tblDongThamKhao2.Text = "D";
                            break;
                        default:
                            this.tblDongThamKhao2.Text = this.dapandung;
                            break;
                    }
                    this.tblDapAnThamKhao.Text = "Theo tớ, câu trả lời chính xác là";
                    break;
                case 3:
                    switch (PagePlay.RandomNumber(0, 5))
                    {
                        case 0:
                            this.tblDongThamKhao2.Text = "A";
                            break;
                        case 1:
                            this.tblDongThamKhao2.Text = "B";
                            break;
                        case 2:
                            this.tblDongThamKhao2.Text = "C";
                            break;
                        case 3:
                            this.tblDongThamKhao2.Text = "D";
                            break;
                        default:
                            this.tblDongThamKhao2.Text = this.dapandung;
                            break;
                    }
                    this.tblDapAnThamKhao.Text = "Tớ đoán là phương án";
                    break;
                case 4:
                    switch (PagePlay.RandomNumber(0, 4))
                    {
                        case 0:
                            this.tblDongThamKhao2.Text = "A";
                            break;
                        case 1:
                            this.tblDongThamKhao2.Text = "B";
                            break;
                        case 2:
                            this.tblDongThamKhao2.Text = "C";
                            break;
                        case 3:
                            this.tblDongThamKhao2.Text = "D";
                            break;
                        default:
                            this.tblDongThamKhao2.Text = this.dapandung;
                            break;
                    }
                    this.tblDapAnThamKhao.Text = "Tớ đoán đáp án chính xác là";
                    break;
                case 5:                   
                    this.tblDongThamKhao2.Text = this.dapandung;
                    this.tblDapAnThamKhao.Text = "Chắc chắn đáp án chính xác sẽ là";
                    break;
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
            this.PlaySoundBG("sound/quyensaochep.wma");
            this.grdShowSaoChep.Visibility = Visibility.Visible;
            switch (PagePlay.RandomNumber(0, 6))
            {
                case 0:
                    this.tblDapAnSaoChep1.Text = "Tớ rất tự tin với đáp án này! Cậu hãy sử dụng đáp án của tớ nhé!";
                    switch (PagePlay.RandomNumber(0, 12))
                    {
                        case 0:
                            this.DapAnSaoChep = "A";
                            return;
                        case 1:
                            this.DapAnSaoChep = "B";
                            return;
                        case 2:
                            this.DapAnSaoChep = "C";
                            return;
                        case 3:
                            this.DapAnSaoChep = "D";
                            return;
                        default:
                            this.DapAnSaoChep = this.dapandung;
                            return;
                    }
                case 1:
                    this.tblDapAnSaoChep1.Text = "Câu này dễ mà! Cậu hãy suy nghĩ một tí sẽ được thôi!";
                    switch (PagePlay.RandomNumber(0, 10))
                    {
                        case 0:
                            this.DapAnSaoChep = "A";
                            return;
                        case 1:
                            this.DapAnSaoChep = "B";
                            return;
                        case 2:
                            this.DapAnSaoChep = "C";
                            return;
                        case 3:
                            this.DapAnSaoChep = "D";
                            return;
                        default:
                            this.DapAnSaoChep = this.dapandung;
                            return;
                    }
                case 2:
                    this.tblDapAnSaoChep1.Text = "Hi hi! Câu hỏi dễ quá! Hãy sao chép đáp án của tớ nhé!";
                    switch (PagePlay.RandomNumber(0, 12))
                    {
                        case 0:
                            this.DapAnSaoChep = "A";
                            return;
                        case 1:
                            this.DapAnSaoChep = "B";
                            return;
                        case 2:
                            this.DapAnSaoChep = "C";
                            return;
                        case 3:
                            this.DapAnSaoChep = "D";
                            return;
                        default:
                            this.DapAnSaoChep = this.dapandung;
                            return;
                    }
                case 3:
                    this.tblDapAnSaoChep1.Text = "Tớ không tự tin lắm! Cậu nên suy nghĩ thật kỹ nhé!";
                    switch (PagePlay.RandomNumber(0, 6))
                    {
                        case 0:
                            this.DapAnSaoChep = "A";
                            return;
                        case 1:
                            this.DapAnSaoChep = "B";
                            return;
                        case 2:
                            this.DapAnSaoChep = "C";
                            return;
                        case 3:
                            this.DapAnSaoChep = "D";
                            return;
                        default:
                            this.DapAnSaoChep = this.dapandung;
                            return;
                    }
                case 4:
                    this.tblDapAnSaoChep1.Text = "Tớ chưa từng nghe câu này bao giờ! Cậu nên suy nghĩ thật kỹ nhé!";
                    switch (PagePlay.RandomNumber(0, 4))
                    {
                        case 0:
                            this.DapAnSaoChep = "A";
                            return;
                        case 1:
                            this.DapAnSaoChep = "B";
                            return;
                        case 2:
                            this.DapAnSaoChep = "C";
                            return;
                        case 3:
                            this.DapAnSaoChep = "D";
                            return;
                        default:
                            this.DapAnSaoChep = this.dapandung;
                            return;
                    }
                case 5:
                    this.tblDapAnSaoChep1.Text = "Hì hì! Đáp án của tớ chắc ăn luôn!";
                    switch (PagePlay.RandomNumber(0, 20))
                    {
                        case 0:
                            this.DapAnSaoChep = "A";
                            break;
                        case 1:
                            this.DapAnSaoChep = "B";
                            break;
                        case 2:
                            this.DapAnSaoChep = "C";
                            break;
                        case 3:
                            this.DapAnSaoChep = "D";
                            break;
                        default:
                            this.DapAnSaoChep = this.dapandung;
                            break;
                    }
                    break;
            }
        }

        private void imgCoSaoChep_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            visibitily_cauhoi();
            this.grdShowSaoChep.Visibility = Visibility.Collapsed;
            this.dapantraloi = this.DapAnSaoChep;
            if (this.DapAnSaoChep == "A")
                this.imgA.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            else if (this.DapAnSaoChep == "B")
                this.imgB.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            else if (this.DapAnSaoChep == "C")
                this.imgC.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            else if (this.DapAnSaoChep == "D")
                this.imgD.Source = (ImageSource)new BitmapImage(new Uri("/Images/bgCauhoiHong.png", UriKind.RelativeOrAbsolute));
            this.DisableTbl();
            this.Traloicauhoi();
        }

        private void imgKhongSaoChep_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.grdShowSaoChep.Visibility = Visibility.Collapsed;
            visibitily_cauhoi();
        }

        private void tblDongThamKhao_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.grdShowThamKhao.Visibility = Visibility.Collapsed;
            visibitily_cauhoi();
        }

        public void collapse_cauhoi()
        {
            this.tblA.Visibility = Visibility.Collapsed;
            this.tblB.Visibility = Visibility.Collapsed;
            this.tblC.Visibility = Visibility.Collapsed;
            this.tblD.Visibility = Visibility.Collapsed;
            this.tblCauHoi.Visibility = Visibility.Collapsed;
            this.imgA.Visibility = Visibility.Collapsed;
            this.imgB.Visibility = Visibility.Collapsed;
            this.imgC.Visibility = Visibility.Collapsed;
            this.imgD.Visibility = Visibility.Collapsed;
        }
        public void visibitily_cauhoi()
        {
            this.tblA.Visibility = Visibility.Visible;
            this.tblB.Visibility = Visibility.Visible;
            this.tblC.Visibility = Visibility.Visible;
            this.tblD.Visibility = Visibility.Visible;
            this.tblCauHoi.Visibility = Visibility.Visible;
            this.imgA.Visibility = Visibility.Visible;
            this.imgB.Visibility = Visibility.Visible;
            this.imgC.Visibility = Visibility.Visible;
            this.imgD.Visibility = Visibility.Visible;
        }
    }
}
