using System.IO;
using System.Threading;
using Windows.UI.Notifications;
using CB.Windows.UI;


namespace HealthyProgrammerCOOL
{
    public partial class MainWindow
    {
        #region Fields
        private const string TOAST_TITLE = "Healthy Programmer COOL";
        private HealthyProgrammerClock _clock;
        private Toast _toast;
        #endregion


        #region  Constructors & Destructor
        public MainWindow()
        {
            InitializeComponent();
            InitializeMore();
        }
        #endregion


        #region Event Handlers
        private void Clock_Elapsed(object sender, string message)
        {
            _toast.Lines = new[] { TOAST_TITLE, message };
            _toast.Show();
        }

        private void Toast_Activated(ToastNotification sender, object args)
        {
            _clock.Resume();
        }

        private void Toast_Dismissed(ToastNotification sender, ToastDismissedEventArgs args)
        {
            _clock.Resume();
        }
        #endregion


        #region Implementation
        private void InitializeMore()
        {
            _clock = new HealthyProgrammerClock(SynchronizationContext.Current);
            _clock.Elapsed += Clock_Elapsed;
            _clock.Start();
            _toast = new Toast
            {
                ImageSource = "file:///" + Path.GetFullPath("health2.png")
            };
            _toast.Activated += Toast_Activated;
            _toast.Dismissed += Toast_Dismissed;
        }
        #endregion
    }

    public delegate void MessageEventHandler(object sender, string message);
}