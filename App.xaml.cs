// Type: AiThongMinhHonLop5.App
// Assembly: AiThongMinhHonLop5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: E:\HK9\LTDD\HocSinhLop5BK\AiThongMinhHonLop5BK_2\AiThongMinhHonLop5.dll

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace AiThongMinhHonLop5
{
    public partial class App : Application
    {
        private bool phoneApplicationInitialized = false;
       //private bool _contentLoaded;

        public PhoneApplicationFrame RootFrame { get; private set; }

        public App()
        {
            this.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.Application_UnhandledException);
            this.InitializeComponent();
            this.InitializePhoneApplication();
            if (!Debugger.IsAttached)
                return;
            Application.Current.Host.Settings.EnableFrameRateCounter = true;
            PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
        }

        

        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (!Debugger.IsAttached)
                return;
            Debugger.Break();
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (!Debugger.IsAttached)
                return;
            Debugger.Break();
        }

        private void InitializePhoneApplication()
        {
            if (this.phoneApplicationInitialized)
                return;
            this.RootFrame = new PhoneApplicationFrame();
            this.RootFrame.Navigated += new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
            this.RootFrame.NavigationFailed += new NavigationFailedEventHandler(this.RootFrame_NavigationFailed);
            this.phoneApplicationInitialized = true;
        }

        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            if (this.RootVisual != this.RootFrame)
                this.RootVisual = (UIElement)this.RootFrame;
            this.RootFrame.Navigated -= new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
        }
    }
}
