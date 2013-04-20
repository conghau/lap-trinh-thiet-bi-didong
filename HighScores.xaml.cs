// Type: AiThongMinhHonLop5.HighScore
// Assembly: AiThongMinhHonLop5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: E:\HK9\LTDD\HocSinhLop5BK\AiThongMinhHonLop5\AiThongMinhHonLop5\AiThongMinhHonLop5\Bin\Debug\AiThongMinhHonLop5.dll

using Microsoft.Devices;
using Microsoft.Phone.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace AiThongMinhHonLop5
{
     public partial class HighScores : PhoneApplicationPage
    {
        private DispatcherTimer _timerLogo = new DispatcherTimer();
        private bool leftright = false;
        private bool Rotating = false;
        private Storyboard Rotation = new Storyboard();
        private VibrateController vc = VibrateController.Default;
        //internal Grid LayoutRoot;
        //internal Image imgLogo;
        //internal PlaneProjection Target;
        //internal Rectangle rectangle1;
        //internal TextBlock tblThamKhao;
        //internal ListBox lbxScore;
        //internal Image imgUpdate;
        //private bool _contentLoaded;

        public HighScores()
        {
            InitializeComponent();
            this._timerLogo.Tick += new EventHandler(this._timerLogo_Tick);
            this._timerLogo.Interval = TimeSpan.FromMilliseconds(10.0);
        }

        private void _timerLogo_Tick(object sender, EventArgs e)
        {
            if (!this.leftright)
            {
                this.imgLogo.Margin = new Thickness(this.imgLogo.Margin.Left + 1.0, this.imgLogo.Margin.Top, this.imgLogo.Margin.Right, this.imgLogo.Margin.Bottom);
                if (this.imgLogo.Margin.Left < 370.0)
                    return;
                this.leftright = true;
            }
            else
            {
                this.imgLogo.Margin = new Thickness(this.imgLogo.Margin.Left - 1.0, this.imgLogo.Margin.Top, this.imgLogo.Margin.Right, this.imgLogo.Margin.Bottom);
                if (this.imgLogo.Margin.Left <= 100.0)
                    this.leftright = false;
            }
        }

        private void LoadScore()
        {
        }

        private void GetScores()
        {
            try
            {
                XDocument xml = XDocument.Load(@"Data\Score.xml");
                //for(int i=0;i<6;i++)

                var temp = (from p in xml.Descendants("score") orderby p.Element("Diem").Value descending select new GetListData { id = p.Element("id").Value.ToString(), Ten = p.Element("Ten").Value.ToString(), Diem = p.Element("Diem").Value }).Take(3);//where ((string)p.Element("SUBJECT").Value == chude && (string)p.Element("ID").Value == RealID.ToString() && (string)p.Element("TYPE").Value == "1") select p;
                this.lbxScore.ItemsSource = temp;
            }
            catch { }


        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.GetScores();
            this.Rotate((object)PlaneProjection.RotationYProperty);
        }

        private void Rotate(object Parameter)
        {
            if (this.Rotating)
            {
                this.Rotation.Stop();
                this.Rotating = false;
            }
            else
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation();
                doubleAnimation.From = new double?(0.0);
                doubleAnimation.To = new double?(360.0);
                doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(10.0));
                doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                Storyboard.SetTarget((Timeline)doubleAnimation, (DependencyObject)this.Target);
                Storyboard.SetTargetProperty((Timeline)doubleAnimation, new PropertyPath(Parameter));
                this.Rotation.Children.Clear();
                this.Rotation.Children.Add((Timeline)doubleAnimation);
                this.Rotation.Begin();
                this.Rotating = true;
            }
        }

        private void Vibrate()
        {
            if (!Global.vibrate)
                return;
            this.vc.Start(TimeSpan.FromMilliseconds(100.0));
        }

        private void imgUpdate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Vibrate();
            this.GetScores();
        }

        [DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("HighScore.xaml", UriKind.Relative));
        //    this.LayoutRoot = (Grid)this.FindName("LayoutRoot");
        //    this.imgLogo = (Image)this.FindName("imgLogo");
        //    this.Target = (PlaneProjection)this.FindName("Target");
        //    this.rectangle1 = (Rectangle)this.FindName("rectangle1");
        //    this.tblThamKhao = (TextBlock)this.FindName("tblThamKhao");
        //    this.lbxScore = (ListBox)this.FindName("lbxScore");
        //    this.imgUpdate = (Image)this.FindName("imgUpdate");
        //}

        public class GetListData
        {
            public string id { get; set; }

            public string Ten { get; set; }

            public string Diem { get; set; }
        }
    }
}
