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
       // private string imei = "";
       // private bool reg = false;
       // private string Keycode = "";
        private string marketTest = "";
        private VibrateController vc = VibrateController.Default;
        private bool enterName = false;
        private bool About = false;
        //internal Grid LayoutRoot;
        //internal Grid grdMain;
        //internal Image imgLogo;
        //internal PlaneProjection Target;
        //internal Image imgPlayNow;
        //internal Image imgHighScore;
        //internal Image imgInfo;
        //internal Image imgVibrate;
        //internal Image imgSound;
        //internal Image imgThanhBach;
        //internal RotateTransform rtf3;
        //internal Grid grdTenNguoiChoi;
        //internal Image image1;
        //internal Image imgDongY;
        //internal Image image3;
        //internal TextBox txtName;
        //internal Grid grdAbout;
        //internal Image imgDialogAbout;
        //internal Image imgDongAbout;
        //internal Image imgXemThemAbout;
        //internal TextBlock tblDongAbout;
        //internal TextBlock tblXemThemAbout;
        // private bool _contentLoaded;
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
            //page1.GetDeviceUniqueID();
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

        ////public static byte[] GetDeviceUniqueID()
        ////{
        ////    byte[] inArray = (byte[])null;
        ////    object propertyValue;
        ////    if (DeviceExtendedProperties.TryGetValue("DeviceUniqueId", out propertyValue))
        ////        inArray = (byte[])propertyValue;
        ////    page1.val = Convert.ToBase64String(inArray);
        ////    return inArray;
        ////}

        //private void ReadFileIMEI()
        //{
        //    StreamReader streamReader = new StreamReader((Stream)new IsolatedStorageFileStream("AiThongMinhHonLop5\\bin.sys", FileMode.Open, IsolatedStorageFile.GetUserStoreForApplication()));
        //    this.Keycode = streamReader.ReadLine();
        //    streamReader.Close();
        //}

        //private void GetVirtualIMEI()
        //{
        //    int index1 = 0;
        //    foreach (char ch in page1.val)
        //    {
        //        this.UserID[index1] = Convert.ToInt32(ch).ToString();
        //        ++index1;
        //    }
        //    string str = "";
        //    for (int index2 = 0; index2 < this.UserID.Length; ++index2)
        //        str = str + this.UserID[index2];
        //    this.imei = str.Substring(0, 17);
        //    Global.imei = this.imei;
        //}

        //private void checkMasterNormalKey(long sobaomat)
        //{
        //    string str1 = (long.Parse(this.imei) + sobaomat).ToString();
        //    string str2 = str1.Substring(str1.Length - 10, 10);
        //    string[] strArray = new string[10];
        //    for (int startIndex = 0; startIndex < 10; ++startIndex)
        //        strArray[startIndex] = str2.Substring(startIndex, 1);
        //    if (!(strArray[9] + strArray[6] + strArray[3] + strArray[2] + strArray[5] + strArray[8] == this.Keycode))
        //        return;
        //    this.reg = true;
        //    //Global.reg = true;
        //}
        public class GetListData
        {
            //string id;
            //string Ten;
            //string Diem;            
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
                // storeForApplication.CreateDirectory("AiThongMinhHonLop5");
                //StreamWriter streamWriter = new StreamWriter((Stream)new IsolatedStorageFileStream("score.xml", FileMode.Create, storeForApplication));
                //streamWriter.WriteLine("");
                //streamWriter.Close();
                //XmlWriterSettings xmlWriterSetting = new XmlWriterSettings();
                //xmlWriterSetting.Indent = true;

                //  // Lấy thông tin IsolateStorage

                //  using (IsolatedStorageFile isoStorage =

                //      IsolatedStorageFile.GetUserStoreForApplication())
                //  {

                //      // Tạo một FileStream để tạo file hay mở file

                //      using (IsolatedStorageFileStream Stream = new IsolatedStorageFileStream("score.xml", System.IO.FileMode.OpenOrCreate, isoStorage))
                //      {

                //          XmlSerializer serializer = new XmlSerializer(typeof(List<GetListData>));

                //          using (XmlWriter xmlWriter = XmlWriter.Create(Stream, xmlWriterSetting))
                //          {

                //              // Viết dữ liệu theo Serialize

                //              serializer.Serialize(xmlWriter, GenerateScoreData());

                //          }

                //      }
                //  }
                List<XmlElement> xmlElements = new List<XmlElement>();
                xmlElements.Add(new XmlElement { Name = "id", Value = "1" });
                xmlElements.Add(new XmlElement { Name = "Ten", Value = "Super" });
                xmlElements.Add(new XmlElement { Name = "Diem", Value = "500000" });
                HighScore.HighScore.AddItem("Score.xml","Highscore", "score", xmlElements);
                
                xmlElements = new List<XmlElement>();
                xmlElements.Add(new XmlElement { Name = "id", Value = "2" });
                xmlElements.Add(new XmlElement { Name = "Ten", Value = "Nik" });
                xmlElements.Add(new XmlElement { Name = "Diem", Value = "50000000000" });
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

        public void CheckZuneNotifyUser()
        {
            if (MediaPlayer.State != MediaState.Playing || MediaPlayer.GameHasControl)
                return;
            MessageBox.Show(string.Format("Zune hiện đang phát \nAi thông minh hơn học sinh lớp 5 cần dừng Zune để phát âm thanh. \nNhấn Ok để tiếp tục trò chơi và vô hiệu hóa âm thanh!", new object[0]));
            Global.sound = false;
            try
            {
                this.ambienceInstanceEF.Stop();
            }
            catch
            {
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.grdMain.Visibility = Visibility.Visible;
            this.grdTenNguoiChoi.Visibility = Visibility.Collapsed;
            this.enterName = false;
            this.imgPlayNow.Source = (ImageSource)new BitmapImage(new Uri("/Images/choingay1.png", UriKind.RelativeOrAbsolute));
            this.imgHighScore.Source = (ImageSource)new BitmapImage(new Uri("/Images/diemcaonhat1.png", UriKind.RelativeOrAbsolute));
            this.imgDongY.Source = (ImageSource)new BitmapImage(new Uri("/Images/dongy1.png", UriKind.RelativeOrAbsolute));
            bool flag = !ApplicationIdleModeHelper.Current.RunsUnderLock;
            if (flag && !ApplicationIdleModeHelper.Current.HasUserAgreedToRunUnderLock)
                ApplicationIdleModeHelper.Current.HasUserAgreedToRunUnderLock = true;
            ApplicationIdleModeHelper.Current.RunsUnderLock = flag;
            this.CheckZuneNotifyUser();
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
            //this.GetVirtualIMEI();
            //this.ReadFileIMEI();
            //this.checkMasterNormalKey(31201216260708L);
            //if (!this.reg)
            //    this.checkMasterNormalKey(120320120400L);
            //if (Global.reg)
            //    return;
            //this.GetMarketOnline();
            //this.GetSynTaxSMS();
        }

        //private void GetMarketOnline()
        //{
        //    WebClient webClient = new WebClient();
        //    webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(this.webClientGetMarketOnline_OpenReadCompleted);
        //    webClient.OpenReadAsync(new Uri("http://ifunsoft.net/api/aithongminhhonlop5/checkmkbk.php", UriKind.Absolute));
        //}

        //private void webClientGetMarketOnline_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        //{
        //    try
        //    {
        //        this.marketTest = new StreamReader(e.Result).ReadToEnd().Trim();
        //        if (!(this.marketTest == "0"))
        //            return;
        //        Global.reg = true;
        //    }
        //    catch
        //    {
        //    }
        //}

        //private void GetSynTaxSMS()
        //{
        //    WebClient webClient = new WebClient();
        //    webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(this.webClientGetSynTaxSMS_OpenReadCompleted);
        //    webClient.OpenReadAsync(new Uri("http://ifunsoft.net/baokim/windowsphone/hocsinhlop5.php", UriKind.Absolute));
        //}

        //private void webClientGetSynTaxSMS_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        //{
        //    try
        //    {
        //        string[] strArray = new StreamReader(e.Result).ReadToEnd().Trim().Split(new char[1]
        //{
        //  '~'
        //});
        //        Global.syntaxsms = strArray[0];
        //        Global.portsms = strArray[1];
        //    }
        //    catch
        //    {
        //    }
        //}

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
           // this.grdAbout.Visibility = Visibility.Visible;
            this.About = true;
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
                Global.idchude = 1;
                this.NavigationService.Navigate(new Uri("/PageFlash.xaml", UriKind.Relative));
            }
            else
            {
                int num = (int)MessageBox.Show("Vui lòng nhập vào tên của bạn!");
                this.imgDongY.Source = (ImageSource)new BitmapImage(new Uri("/Images/dongy1.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void tblDongAbout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.About = false;
          //  this.grdAbout.Visibility = Visibility.Collapsed;
        }

        private void tblXemThemAbout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.About = false;
           // this.grdAbout.Visibility = Visibility.Collapsed;
            try
            {
                this.ambienceInstanceEF.Stop();
            }
            catch
            {
            }
            this.NavigationService.Navigate(new Uri("/PageAbout.xaml", UriKind.Relative));
        }


    }
}

    
