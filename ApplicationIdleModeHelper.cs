// Type: AiThongMinhHonLop5.ApplicationIdleModeHelper
// Assembly: AiThongMinhHonLop5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: E:\HK9\AiThongMinhHonLop5BK_2\AiThongMinhHonLop5.dll

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Threading;
using System.Windows;

namespace AiThongMinhHonLop5
{
    public class ApplicationIdleModeHelper : INotifyPropertyChanged
    {
        private static ApplicationIdleModeHelper current;
        private bool runsUnderLock;
        private bool isRunningUnderLock;
        private bool hasUserAgreedToRunUnderLock;
        private bool isRestartRequired;
        private EventHandler Locked;
        private EventHandler UnLocked;
        private EventHandler RestartRequired;
        private PropertyChangedEventHandler PropertyChanged;

        public static ApplicationIdleModeHelper Current
        {
            get
            {
                if (null == ApplicationIdleModeHelper.current)
                    ApplicationIdleModeHelper.current = new ApplicationIdleModeHelper();
                Debug.Assert(ApplicationIdleModeHelper.current != null);
                return ApplicationIdleModeHelper.current;
            }
        }

        public bool RunsUnderLock
        {
            get
            {
                return this.runsUnderLock;
            }
            set
            {
                if (value == this.runsUnderLock)
                    return;
                this.runsUnderLock = value;
                if (this.runsUnderLock)
                {
                    PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
                    PhoneApplicationFrame applicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
                    Debug.Assert(applicationFrame != null, "This sample assumes RootVisual has been set");
                    if (applicationFrame != null)
                    {
                        applicationFrame.Obscured += new EventHandler<ObscuredEventArgs>(this.rootframe_Obscured);
                        applicationFrame.Unobscured += new EventHandler(this.rootframe_Unobscured);
                    }
                }
                else
                {
                    this.IsRestartRequired = true;
                    EventHandler eventHandler = this.RestartRequired;
                    if (eventHandler != null)
                        eventHandler((object)this, new EventArgs());
                }
                this.SaveSetting("RunsUnderLock", (object)Convert.ToBoolean(this.runsUnderLock ? 1 : 0));
                this.OnPropertyChanged("RunsUnderLock");
            }
        }

        public bool IsRestartRequired
        {
            get
            {
                return this.isRestartRequired;
            }
            private set
            {
                if (value == this.isRestartRequired)
                    return;
                this.isRestartRequired = value;
                this.OnPropertyChanged("IsRestartRequired");
            }
        }

        public bool HasUserAgreedToRunUnderLock
        {
            get
            {
                return this.hasUserAgreedToRunUnderLock;
            }
            set
            {
                if (value == this.hasUserAgreedToRunUnderLock)
                    return;
                this.hasUserAgreedToRunUnderLock = value;
                this.SaveSetting("HasUserAgreedToRunUnderLock", (object)Convert.ToBoolean(this.hasUserAgreedToRunUnderLock ? 1 : 0));
                this.OnPropertyChanged("HasUserAgreedToRunUnderLock");
            }
        }

        public bool IsRunningUnderLock
        {
            get
            {
                return this.isRunningUnderLock;
            }
            private set
            {
                if (value == this.isRunningUnderLock)
                    return;
                this.isRunningUnderLock = value;
                this.OnPropertyChanged("IsRunningUnderLock");
                if (this.isRunningUnderLock)
                {
                    EventHandler eventHandler = this.Locked;
                    if (eventHandler != null)
                        eventHandler((object)this, new EventArgs());
                }
                else
                {
                    EventHandler eventHandler = this.UnLocked;
                    if (eventHandler != null)
                        eventHandler((object)this, new EventArgs());
                }
            }
        }

        //public event EventHandler Locked
        //{
        //    add
        //    {
        //        EventHandler eventHandler = this.Locked;
        //        EventHandler comparand;
        //        do
        //        {
        //            comparand = eventHandler;
        //            eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.Locked, comparand + value, comparand);
        //        }
        //        while (eventHandler != comparand);
        //    }
        //    remove
        //    {
        //        EventHandler eventHandler = this.Locked;
        //        EventHandler comparand;
        //        do
        //        {
        //            comparand = eventHandler;
        //            eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.Locked, comparand - value, comparand);
        //        }
        //        while (eventHandler != comparand);
        //    }
        //}

        //public event EventHandler UnLocked
        //{
        //    add
        //    {
        //        EventHandler eventHandler = this.UnLocked;
        //        EventHandler comparand;
        //        do
        //        {
        //            comparand = eventHandler;
        //            eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.UnLocked, comparand + value, comparand);
        //        }
        //        while (eventHandler != comparand);
        //    }
        //    remove
        //    {
        //        EventHandler eventHandler = this.UnLocked;
        //        EventHandler comparand;
        //        do
        //        {
        //            comparand = eventHandler;
        //            eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.UnLocked, comparand - value, comparand);
        //        }
        //        while (eventHandler != comparand);
        //    }
        //}

        //public event EventHandler RestartRequired
        //{
        //    add
        //    {
        //        EventHandler eventHandler = this.RestartRequired;
        //        EventHandler comparand;
        //        do
        //        {
        //            comparand = eventHandler;
        //            eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.RestartRequired, comparand + value, comparand);
        //        }
        //        while (eventHandler != comparand);
        //    }
        //    remove
        //    {
        //        EventHandler eventHandler = this.RestartRequired;
        //        EventHandler comparand;
        //        do
        //        {
        //            comparand = eventHandler;
        //            eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.RestartRequired, comparand - value, comparand);
        //        }
        //        while (eventHandler != comparand);
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged
        //{
        //    add
        //    {
        //        PropertyChangedEventHandler changedEventHandler = this.PropertyChanged;
        //        PropertyChangedEventHandler comparand;
        //        do
        //        {
        //            comparand = changedEventHandler;
        //            changedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, comparand + value, comparand);
        //        }
        //        while (changedEventHandler != comparand);
        //    }
        //    remove
        //    {
        //        PropertyChangedEventHandler changedEventHandler = this.PropertyChanged;
        //        PropertyChangedEventHandler comparand;
        //        do
        //        {
        //            comparand = changedEventHandler;
        //            changedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, comparand - value, comparand);
        //        }
        //        while (changedEventHandler != comparand);
        //    }
        //}

        private ApplicationIdleModeHelper()
        {
        }

        public void Initialize()
        {
            Debug.WriteLine("initialized " + (object)PhoneApplicationService.Current.StartupMode);
            bool flag1 = false;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue<bool>("RunsUnderLock", out flag1))
            {
                if (flag1 != (PhoneApplicationService.Current.ApplicationIdleDetectionMode == IdleDetectionMode.Disabled))
                {
                    this.RunsUnderLock = flag1;
                    Debug.WriteLine("Did not match");
                }
                else
                {
                    Debug.WriteLine("Matched");
                    this.runsUnderLock = flag1;
                }
            }
            bool flag2 = false;
            if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue<bool>("HasUserAgreedToRunUnderLock", out flag2))
                return;
            this.HasUserAgreedToRunUnderLock = flag2;
        }

        public void SaveSetting(string key, object value)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
                IsolatedStorageSettings.ApplicationSettings[key] = value;
            else
                IsolatedStorageSettings.ApplicationSettings.Add(key, value);
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void rootframe_Unobscured(object sender, EventArgs e)
        {
            this.IsRunningUnderLock = false;
        }

        private void rootframe_Obscured(object sender, ObscuredEventArgs e)
        {
            if (!e.IsLocked)
                return;
            this.IsRunningUnderLock = e.IsLocked;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler changedEventHandler = this.PropertyChanged;
            if (changedEventHandler == null)
                return;
            changedEventHandler((object)this, new PropertyChangedEventArgs(propertyName));
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }
    }
}
