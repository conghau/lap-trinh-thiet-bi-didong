using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace AiThongMinhHonLop5
{
        public class Global
        {
            public static int lanchon { get; set; }

            //public static bool reg { get; set; }

            public static bool trial { get; set; }

            public static bool sound { get; set; }

            public static bool vibrate { get; set; }

            // dùng để gọi các màn hình theo id
            public static int idchude { get; set; }

            public static int totalScore { get; set; }

            public static string imei { get; set; }

            public static string Notification { get; set; }

            public static bool ChonA1 { get; set; }

            public static bool ChonA2 { get; set; }

            public static bool ChonA3 { get; set; }

            public static bool ChonA4 { get; set; }

            public static bool ChonA5 { get; set; }

            public static bool ChonB1 { get; set; }

            public static bool ChonB2 { get; set; }

            public static bool ChonB3 { get; set; }

            public static bool ChonB4 { get; set; }

            public static bool ChonB5 { get; set; }

            public static int CauHoi { get; set; }

            public static string CauHoi1 { get; set; }

            public static string CauHoi2 { get; set; }

            public static string CauHoi3 { get; set; }

            public static string CauHoi4 { get; set; }

            public static string CauHoi5 { get; set; }

            public static string CauHoi6 { get; set; }

            public static string CauHoi7 { get; set; }

            public static string CauHoi8 { get; set; }

            public static string CauHoi9 { get; set; }

            public static string CauHoi10 { get; set; }

            public static string CauHoi11 { get; set; }

            public static int TypeCauHoi1 { get; set; }

            public static int TypeCauHoi2 { get; set; }

            public static int TypeCauHoi3 { get; set; }

            public static int TypeCauHoi4 { get; set; }

            public static int TypeCauHoi5 { get; set; }

            public static int TypeCauHoi6 { get; set; }

            public static int TypeCauHoi7 { get; set; }

            public static int TypeCauHoi8 { get; set; }

            public static int TypeCauHoi9 { get; set; }

            public static int TypeCauHoi10 { get; set; }

            public static string RealChudeCauHoi { get; set; }

            public static int RealLoaiCauhoi { get; set; }

            public static int ChonBe { get; set; }

            public static bool ChonBe1 { get; set; }

            public static bool ChonBe2 { get; set; }

            public static bool ChonBe3 { get; set; }

            public static bool ChonBe4 { get; set; }

            public static bool ChonBe5 { get; set; }

            public static int CountChonBe { get; set; }

            public static bool GiaiCuuMayMan { get; set; }

            public static bool GiaiCuuThamKhao { get; set; }

            public static bool GiaiCuuSaoChep { get; set; }

            public static string NamePlayer { get; set; }

            public static bool Win { get; set; }

          //  public static string syntaxsms { get; set; }

           // public static string portsms { get; set; }

            static Global()
            {
                Global.sound = true;
                Global.vibrate = true;
                Global.idchude = 1; //số id của màn hình , xem ở pageFlash
                Global.totalScore = 0;
                Global.Notification = "";
                Global.lanchon = 0;
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
                Global.CauHoi = 0;
                Global.CauHoi1 = "";
                Global.CauHoi2 = "";
                Global.CauHoi3 = "";
                Global.CauHoi4 = "";
                Global.CauHoi5 = "";
                Global.CauHoi6 = "";
                Global.CauHoi7 = "";
                Global.CauHoi8 = "";
                Global.CauHoi9 = "";
                Global.CauHoi10 = "";
                Global.CauHoi11 = "";
                Global.TypeCauHoi1 = 0;
                Global.TypeCauHoi2 = 0;
                Global.TypeCauHoi3 = 0;
                Global.TypeCauHoi4 = 0;
                Global.TypeCauHoi5 = 0;
                Global.TypeCauHoi6 = 0;
                Global.TypeCauHoi7 = 0;
                Global.TypeCauHoi8 = 0;
                Global.TypeCauHoi9 = 0;
                Global.TypeCauHoi10 = 0;
                Global.RealChudeCauHoi = "";
                Global.RealLoaiCauhoi = 0;
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
                Global.NamePlayer = "";
                Global.Win = false;
            }
        }

        public class ActionTimer : DispatcherTimer
        {
            public Action Action { get; set; }
        }

        public static class UIToolkit
        {
            public static void setTimeout(int milliseconds, Action action)
            {
                ActionTimer actionTimer1 = new ActionTimer();
                actionTimer1.Interval = new TimeSpan(0, 0, 0, 0, milliseconds);
                actionTimer1.Action = action;
                ActionTimer actionTimer2 = actionTimer1;
                actionTimer2.Tick += new EventHandler(UIToolkit.timer_Tick);
                actionTimer2.Start();
            }

            private static void timer_Tick(object sender, EventArgs e)
            {
                ActionTimer actionTimer = sender as ActionTimer;
                actionTimer.Stop();
                actionTimer.Action();
                actionTimer.Tick -= new EventHandler(UIToolkit.timer_Tick);
            }
        }

        public class GetListScoreData
        {
            string _id;
            string _Ten;
            string _Diem;            
            public string id { get{return _id;} set{ _id=value;} }

            public string Ten { get{return _Ten;} set{_Ten= value;} }

            public string Diem { get{return _Diem;} set{_Diem = value;} }
        }
        public class Param
        {
            private string _name = string.Empty;
            public string value
            {
                get { return _name; }
                set
                {
                    _name = value;
                    
                }
            }
        }

       
}
