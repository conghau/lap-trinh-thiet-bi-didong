using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Devices;
using Microsoft.Phone.Info;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
//using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using AiThongMinhHonLop5.HighScore;

namespace AiThongMinhHonLop5
{
    public partial class page1 : PhoneApplicationPage
    {

        private DispatcherTimer _timerLogo = new DispatcherTimer();
        private DispatcherTimer _timerThanhBach = new DispatcherTimer();
        private bool leftright = false;
        private int TB = 0;
        private bool rTB = true;
        private string[] UserID = new string[1000];
        private VibrateController vc = VibrateController.Default;
        private bool enterName = false;       
        public static string val;
        private SoundEffect ambienceSoundEF;
        private SoundEffectInstance ambienceInstanceEF;

        public page1()
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
            this._timerLogo.Tick += new EventHandler(this._timerLogo_Tick);
            this._timerLogo.Interval = TimeSpan.FromMilliseconds(10.0);
            this._timerThanhBach.Tick += new EventHandler(this._timerThanhBach_Tick);
            this._timerThanhBach.Interval = TimeSpan.FromMilliseconds(30.0);
        }
  
        private void _timerLogo_Tick(object sender, EventArgs e)
        {
            if (!this.leftright)
            {
                Image image = this.imgLogo;
                Thickness margin = this.imgLogo.Margin;
                double left = margin.Left + 1.0;
                margin = this.imgLogo.Margin;
                double top = margin.Top;
                margin = this.imgLogo.Margin;
                double right = margin.Right;
                margin = this.imgLogo.Margin;
                double bottom = margin.Bottom;
                Thickness thickness = new Thickness(left, top, right, bottom);
                image.Margin = thickness;
                margin = this.imgLogo.Margin;
                if (margin.Left < 100.0)
                    return;
                this.leftright = true;
            }
            else
            {
                Image image = this.imgLogo;
                Thickness margin = this.imgLogo.Margin;
                double left = margin.Left - 1.0;
                margin = this.imgLogo.Margin;
                double top = margin.Top;
                margin = this.imgLogo.Margin;
                double right = margin.Right;
                margin = this.imgLogo.Margin;
                double bottom = margin.Bottom;
                Thickness thickness = new Thickness(left, top, right, bottom);
                image.Margin = thickness;
                margin = this.imgLogo.Margin;
                if (margin.Left <= 10.0)
                    this.leftright = false;
            }
        }

        private void _timerThanhBach_Tick(object sender, EventArgs e)
        {
            if (this.TB < -5)
                this.rTB = true;
            if (this.TB > 5)
                this.rTB = false;
            if (this.rTB)
            {
                ++this.TB;
                this.rtf3.Angle = (double)this.TB;
                this.imgThanhBach.Source = (ImageSource)new BitmapImage(new Uri("/Images/tb2.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                --this.TB;
                this.rtf3.Angle = (double)this.TB;
                this.imgThanhBach.Source = (ImageSource)new BitmapImage(new Uri("/Images/tb1.png", UriKind.RelativeOrAbsolute));
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
            try
            {
                this.ambienceInstanceEF.Stop();
            }
            catch
            {
            }
            this.LoadSoundInstanceEF(filename, out this.ambienceSoundEF, out this.ambienceInstanceEF);
            this.ambienceInstanceEF.Volume = 1;
            this.ambienceInstanceEF.IsLooped = true;
            this.ambienceInstanceEF.Play();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (this.enterName)
            {
                this.grdMain.Visibility = Visibility.Visible;
                this.grdTenNguoiChoi.Visibility = Visibility.Collapsed;
                this.enterName = false;
                this.imgPlayNow.Source = (ImageSource)new BitmapImage(new Uri("/Images/choingay1.png", UriKind.RelativeOrAbsolute));
                this.imgHighScore.Source = (ImageSource)new BitmapImage(new Uri("/Images/diemcaonhat1.png", UriKind.RelativeOrAbsolute));
                this.imgDongY.Source = (ImageSource)new BitmapImage(new Uri("/Images/dongy1.png", UriKind.RelativeOrAbsolute));
                e.Cancel = true;
            }
            //else if (this.About)
            //{
            //    this.About = false;
            //    this.grdAbout.Visibility = Visibility.Collapsed;
            //    e.Cancel = true;
            //}
            else if (MessageBox.Show("Bạn có chắc chắn muốn thoát trò chơi?", "Thoát trò chơi", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                }
                catch
                {
                }
            }
        }

 
        public class GetListData
        {          
            public string id { get; set; }

            public string Ten { get; set; }

            public string Diem { get; set; }
        }

        private List<GetListData> GenerateScoreData()
        {

            List<GetListData> data = new List<GetListData>();

            data.Add(new GetListData { id = "1", Ten = "Brown", Diem = "50000000" });

            return data;

        }

        private void CheckFile()
        {
            IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\bin.sys"))
            {
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\bin.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine("000000");
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("score.xml"))
            {
              
                List<XmlElement> xmlElements = new List<XmlElement>();
                xmlElements.Add(new XmlElement { Name = "id", Value = "1" });
                xmlElements.Add(new XmlElement { Name = "Ten", Value = "Super" });
                xmlElements.Add(new XmlElement { Name = "Diem", Value = "0" });
                HighScore.HighScore.AddItem("Score.xml","Highscore", "score", xmlElements);
                
                xmlElements = new List<XmlElement>();
                xmlElements.Add(new XmlElement { Name = "id", Value = "2" });
                xmlElements.Add(new XmlElement { Name = "Ten", Value = "Zik" });
                xmlElements.Add(new XmlElement { Name = "Diem", Value = "0" });
                HighScore.HighScore.AddItem("Score.xml", "Highscore", "score", xmlElements);
                //AddParam("CVS");
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0MATHEMATIC1.sys"))
            {
                int max = 1;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0MATHEMATIC1.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0MATHEMATIC2.sys"))
            {
                int max = 1;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0MATHEMATIC2.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0MATHEMATIC3.sys"))
            {
                int max = 3;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0MATHEMATIC3.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0MATHEMATIC4.sys"))
            {
                int max = 3;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0MATHEMATIC4.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0MATHEMATIC5.sys"))
            {
                int max = 3;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0MATHEMATIC5.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0VIETNAMESE1.sys"))
            {
                int max = 4;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0VIETNAMESE1.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0VIETNAMESE2.sys"))
            {
                int max = 3;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0VIETNAMESE2.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0VIETNAMESE3.sys"))
            {
                int max = 4;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0VIETNAMESE3.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0HISTORY4.sys"))
            {
                int max = 10;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0HISTORY4.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0HISTORY5.sys"))
            {
                int max = 10;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0HISTORY5.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0NATURE1.sys"))
            {
                int max = 9;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0NATURE1.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0NATURE2.sys"))
            {
                int max = 8;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0NATURE2.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0NATURE3.sys"))
            {
                int max = 9;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0NATURE3.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0MUSIC1.sys"))
            {
                int max = 7;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0MUSIC1.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0MUSIC2.sys"))
            {
                int max = 9;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0MUSIC2.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0MUSIC3.sys"))
            {
                int max = 7;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0MUSIC3.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0GEOGRAPHY4.sys"))
            {
                int max = 10;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0GEOGRAPHY4.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0GEOGRAPHY5.sys"))
            {
                int max = 10;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0GEOGRAPHY5.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0SCIENCE4.sys"))
            {
                int max = 7;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0SCIENCE4.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0SCIENCE5.sys"))
            {
                int max = 10;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0SCIENCE5.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0MORALS4.sys"))
            {
                int max = 10;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0MORALS4.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\0MORALS5.sys"))
            {
                int max = 10;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\0MORALS5.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\1MATHEMATIC1.sys"))
            {
                int max = 8;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1MATHEMATIC1.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\1MATHEMATIC2.sys"))
            {
                int max = 9;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1MATHEMATIC2.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\1MATHEMATIC3.sys"))
            {
                int max = 7;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1MATHEMATIC3.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\1MATHEMATIC4.sys"))
            {
                int max = 7;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1MATHEMATIC4.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\1MATHEMATIC5.sys"))
            {
                int max = 6;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1MATHEMATIC5.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\1VIETNAMESE1.sys"))
            {
                int max = 6;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1VIETNAMESE1.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\1VIETNAMESE2.sys"))
            {
                int max = 6;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1VIETNAMESE2.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\1VIETNAMESE3.sys"))
            {
                int max = 1;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1VIETNAMESE3.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\1NATURE2.sys"))
            {
                int max = 1;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1NATURE2.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\1MUSIC1.sys"))
            {
                int max = 3;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1MUSIC1.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (!storeForApplication.FileExists("AiThongMinhHonLop5\\1MUSIC2.sys"))
            {
                int max = 1;
                storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1MUSIC2.sys", FileMode.Create, storeForApplication));
                streamWriter.WriteLine(page1.RandomNumber(0, max).ToString() + "-" + max.ToString());
                streamWriter.Close();
            }
            if (storeForApplication.FileExists("AiThongMinhHonLop5\\1MUSIC3.sys"))
                return;
            int max1 = 3;
            storeForApplication.CreateDirectory("AiThongMinhHonLop5");
            StreamWriter streamWriter1 = new StreamWriter((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\1MUSIC3.sys", FileMode.Create, storeForApplication));
            streamWriter1.WriteLine(page1.RandomNumber(0, max1).ToString() + "-" + max1.ToString());
            streamWriter1.Close();
        }

        private static int RandomNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.grdMain.Visibility = Visibility.Visible;
            this.grdTenNguoiChoi.Visibility = Visibility.Collapsed;
            this.enterName = false;
            this.imgPlayNow.Source = (ImageSource)new BitmapImage(new Uri("/Images/choingay1.png", UriKind.RelativeOrAbsolute));
            this.imgHighScore.Source = (ImageSource)new BitmapImage(new Uri("/Images/diemcaonhat1.png", UriKind.RelativeOrAbsolute));
            this.imgDongY.Source = (ImageSource)new BitmapImage(new Uri("/Images/dongy1.png", UriKind.RelativeOrAbsolute));          
            this.PlaySoundEF("sound/soundbg.wav");
            this._timerLogo.Start();
            this._timerThanhBach.Start();
            Global.lanchon = 0;
            Global.totalScore = 0;
            Global.CauHoi = 0;
            Global.ChonA1 = false;
            Global.ChonA2 = false;
            Global.ChonA3 = false;
            Global.ChonA4 = false;
            Global.ChonA5 = false;
            Global.ChonB1 = false;
            Global.ChonB2 = false;
            Global.ChonB3 = false;
            Global.ChonB4 = false;
            Global.ChonB5 = false;
            Global.ChonBe = 0;
            Global.ChonBe1 = false;
            Global.ChonBe2 = false;
            Global.ChonBe3 = false;
            Global.ChonBe4 = false;
            Global.ChonBe5 = false;
            Global.CountChonBe = 0;
            Global.GiaiCuuMayMan = false;
            Global.GiaiCuuThamKhao = false;
            Global.GiaiCuuSaoChep = false;
            Global.Win = false;
            this.CheckFile();
           
        }
     
        private void Vibrate()
        {
            if (!Global.vibrate)
                return;
            this.vc.Start(TimeSpan.FromMilliseconds(100.0));
        }

        private void imgPlayNow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgPlayNow.Source = (ImageSource)new BitmapImage(new Uri("/Images/choingay2.png", UriKind.RelativeOrAbsolute));
        }

        private void imgPlayNow_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgPlayNow.Source = (ImageSource)new BitmapImage(new Uri("/Images/choingay3.png", UriKind.RelativeOrAbsolute));
            this.enterName = true;
            this.grdMain.Visibility = Visibility.Collapsed;
            this.grdTenNguoiChoi.Visibility = Visibility.Visible;
            this.txtName.Focus();
        }

        private void imgHighScore_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgHighScore.Source = (ImageSource)new BitmapImage(new Uri("/Images/diemcaonhat2.png", UriKind.RelativeOrAbsolute));
        }

        private void imgHighScore_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imgHighScore.Source = (ImageSource)new BitmapImage(new Uri("/Images/diemcaonhat3.png", UriKind.RelativeOrAbsolute));
            this.NavigationService.Navigate(new Uri("/HighScores.xaml", UriKind.Relative));
        }

        private void imgSound_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            Global.sound = !Global.sound;
            if (Global.sound)
            {
                this.PlaySoundEF("sound/soundbg.wav");
                this.imgSound.Source = (ImageSource)new BitmapImage(new Uri("/Images/soundon.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                try
                {
                    this.ambienceInstanceEF.Stop();
                }
                catch
                {
                }
                this.imgSound.Source = (ImageSource)new BitmapImage(new Uri("/Images/soundoff.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void imgVibrate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            Global.vibrate = !Global.vibrate;
            if (Global.vibrate)
                this.imgVibrate.Source = (ImageSource)new BitmapImage(new Uri("/Images/vibrateon.png", UriKind.RelativeOrAbsolute));
            else
                this.imgVibrate.Source = (ImageSource)new BitmapImage(new Uri("/Images/vibrateoff.png", UriKind.RelativeOrAbsolute));
        }

        private void imgInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.NavigationService.Navigate(new Uri("/PageAbout.xaml",UriKind.Relative));
        }

        private void imgDongY_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.imgDongY.Source = (ImageSource)new BitmapImage(new Uri("/Images/dongy2.png", UriKind.RelativeOrAbsolute));
        }

        private void imgDongY_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.txtName.Text.Trim() != "")
            {
                Global.NamePlayer = this.txtName.Text.Trim();
                try
                {
                    this.ambienceInstanceEF.Stop();
                }
                catch
                {
                }
                this.imgDongY.Source = (ImageSource)new BitmapImage(new Uri("Images/dongy3.png", UriKind.RelativeOrAbsolute));
                //dùng để gọi màn hình bên pageflash
                Global.idchude = 1;
                this.NavigationService.Navigate(new Uri("/PageFlash.xaml", UriKind.Relative));
            }
            else
            {
                int num = (int)MessageBox.Show("Vui lòng nhập vào tên của bạn!");
                this.imgDongY.Source = (ImageSource)new BitmapImage(new Uri("/Images/dongy1.png", UriKind.RelativeOrAbsolute));
            }
        }
      

    }
}

    
