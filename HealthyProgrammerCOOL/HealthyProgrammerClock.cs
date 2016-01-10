using System;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;


namespace HealthyProgrammerCOOL
{
    public class HealthyProgrammerClock
    {
        private const double TWENTY_FIVE_MINUTES = 1500;
        private const double TWENTY_MINUTES = 1200;
        #region Fields
        private int _count;

        private string _message;
        private readonly SynchronizationContext _synchronizationContext;

        private readonly Timer _timer = new Timer
        {
            AutoReset = false,
        };
        #endregion


        #region  Constructors & Destructor
        public HealthyProgrammerClock(SynchronizationContext synchronizationContext)
        {
            _synchronizationContext = synchronizationContext;
            _timer.Elapsed += Timer_Elapsed;
        }
        #endregion


        #region Events
        public event MessageEventHandler Elapsed;
        #endregion


        #region Methods
        public void Resume()
        {
            SetNextElapse();
            _timer.Start();
        }

        public void Start()
        {
            ResetElapse();
            _timer.Start();
        }
        #endregion


        #region Event Handlers
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_synchronizationContext != null)
            {
                _synchronizationContext.Send(state => OnElapsed(_message), null);
            }
            else
            {
                OnElapsed(_message);
            }
        }
        #endregion


        #region Implementation
        protected virtual void OnElapsed(string message)
        {
            Elapsed?.Invoke(this, _message);
        }

        private void ResetElapse()
        {
            _count = 0;
            SetNextElapse();
        }

        private void SetNextElapse()
        {
            _count = (_count + 1) % 4;

            switch (_count)
            {
                case 0:
                    _message = "Time to walk!";
                    _timer.Interval = TWENTY_FIVE_MINUTES;
                    break;

                case 1:
                case 3:
                    _message = "Time to change your posture!";
                    _timer.Interval = TWENTY_MINUTES;
                    break;

                case 2:
                    _message = "Time to stand up and relax your eyes!";
                    _timer.Interval = TWENTY_MINUTES;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
        #endregion
    }
}