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
using System.Xml.Linq;
using System.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using AiThongMinhHonLop5.HighScore;

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
            luuDiem();
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

       
        private void luuDiem()
        {


                //XmlWriterSettings xmlWriterSetting = new XmlWriterSettings();
                //xmlWriterSetting.Indent = true;

                //// Lấy thông tin IsolateStorage

                //using (IsolatedStorageFile isoStorage =

                //    IsolatedStorageFile.GetUserStoreForApplication())
                //{

                //    // Tạo một FileStream để tạo file hay mở file

                //    using (IsolatedStorageFileStream Stream = new IsolatedStorageFileStream("score.xml", System.IO.FileMode.OpenOrCreate, isoStorage))
                //    {

                //        XmlSerializer serializer = new XmlSerializer(typeof(List<GetListScoreData>));

                //        using (XmlWriter xmlWriter = XmlWriter.Create(Stream, xmlWriterSetting))
                //        {

                //            // Viết dữ liệu theo Serialize

                //            serializer.Serialize(xmlWriter, GenerateScoreData());

                //        }

                //    }
                //}
           List<XmlElement> xmlElements = new List<XmlElement>();
           string id = GetLastId("SC");
           xmlElements.Add(new XmlElement { Name = "id", Value = id });
           xmlElements.Add(new XmlElement { Name = "Ten", Value = Global.NamePlayer });
           xmlElements.Add(new XmlElement { Name = "Diem", Value = Global.totalScore.ToString() });
           HighScore.HighScore.AddItem("Score.xml", "Highscore", "score", xmlElements);

        }
        private string GetLastId(string paramName)
        {
            string lastId = GetParam(paramName);

            if (string.IsNullOrEmpty(lastId))
            {
                AddParam(paramName, "2");
                lastId = "1";
            }
            else
            {
                UpdateParam(paramName, (Convert.ToInt32(lastId) + 1).ToString());
            }

            return lastId;
        }
        public string GetParam(string paramName)
        {
            try
            {
                var myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

                if (myIsolatedStorage.FileExists("Params.xml"))
                {
                    Stream stream = myIsolatedStorage.OpenFile("Params.xml", FileMode.Open, FileAccess.ReadWrite);

                    XDocument xmldoc = XDocument.Load(stream);

                    var templates = from query in xmldoc.Descendants(paramName)
                                    select new Param
                                    {
                                        value = (string)query.Value
                                    };

                    ListBox a = new ListBox();
                    a.ItemsSource = templates;

                    foreach (Param pr in templates)
                    {
                        stream.Close();
                        return pr.value;
                    }

                    stream.Close();
                    return string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }
        public void AddParam(string elementName, string value)
        {
            var myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            if (!myIsolatedStorage.FileExists("Params.xml"))
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("Params.xml", FileMode.Create, myIsolatedStorage))
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;

                    using (XmlWriter writer = XmlWriter.Create(isoStream, settings))
                    {
                        writer.WriteStartElement("params");

                        writer.WriteStartElement(elementName, "");
                        writer.WriteString(value);
                        writer.WriteEndElement();

                        writer.WriteEndDocument();
                        writer.Flush();
                        writer.Close();
                    }
                }
            }
            else
            {
                XDocument loadedData;
                using (Stream stream = myIsolatedStorage.OpenFile("Params.xml", FileMode.Open, FileAccess.ReadWrite))
                {
                    loadedData = XDocument.Load(stream);
                    var fav = new XElement(elementName, value);
                    var root = loadedData.Element("params");
                    root.AddFirst(fav);
                }

                // Save To History.xml File 
                using (IsolatedStorageFileStream myStream = new IsolatedStorageFileStream("Params.xml", FileMode.Create, myIsolatedStorage))
                {
                    loadedData.Save(myStream);
                    myStream.Close();
                }
            }
        }

        public void UpdateParam(string elementName, string newValue)
        {
            var myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            if (myIsolatedStorage.FileExists("Params.xml"))
            {
                XDocument loadedData;
                using (Stream stream = myIsolatedStorage.OpenFile("Params.xml", FileMode.Open, FileAccess.ReadWrite))
                {
                    loadedData = XDocument.Load(stream);

                    var root = loadedData.Element("params");
                    var rows = root.Descendants(elementName);

                    foreach (var row in rows)
                    {
                        row.Value = newValue;
                        break;
                    }
                }

                // Save To History.xml File
                using (IsolatedStorageFileStream myStream = new IsolatedStorageFileStream("Params.xml", FileMode.Create, myIsolatedStorage))
                {
                    loadedData.Save(myStream);
                    myStream.Close();
                }
            }
        }

    }
 
}
