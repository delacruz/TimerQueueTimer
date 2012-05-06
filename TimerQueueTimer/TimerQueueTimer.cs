using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Jdlc.Timers
{
    public class QueueTimerEventArgs : EventArgs
    {
    }

    public class TimerQueueTimer : IDisposable
    {
        #region Constants and Fields

        private readonly WaitOrTimerDelegate _callback;
        private readonly IntPtr _timerPtr;
        private bool _isEnabled;

        #endregion

        #region Constructors and Destructors

        public TimerQueueTimer(int intervalInMilliseconds)
        {
            Interval = intervalInMilliseconds;
            _callback = TimerCallback;

            //unsafe
            //{
            //    fixed(TimeCaps* pTimeCaps = &_timeCaps)
            //    {
            //        timeGetDevCaps((IntPtr)pTimeCaps, (uint)sizeof(TimeCaps));
            //    }

            //}

            IntPtr timeCapsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (TimeCaps)));

            try
            {
                timeGetDevCaps(timeCapsPtr, (uint) Marshal.SizeOf(typeof (TimeCaps)));

                var timeCaps = (TimeCaps) Marshal.PtrToStructure(timeCapsPtr, typeof (TimeCaps));

                //timeGetDevCaps((IntPtr)pTimeCaps, (uint)sizeof(TimeCaps));
                Debug.WriteLine(String.Format("QueueTimer Resolution capability MIN: {0}\tMAX:{1}", timeCaps.wPeriodMin,
                                              timeCaps.wPeriodMax));

                CreateTimerQueueTimer(out _timerPtr, IntPtr.Zero, _callback, IntPtr.Zero, 0, (uint) Interval,
                                      (uint) ExecuteFlags.WT_EXECUTEINTIMERTHREAD);
            }
            finally
            {
                Marshal.FreeHGlobal(timeCapsPtr);
            }
        }

        public TimerQueueTimer()
            : this(1000)
        {
        }

        #endregion

        #region Delegates

        public delegate void WaitOrTimerDelegate(IntPtr lpParameter, bool timerOrWaitFired);

        #endregion

        #region Public Events

        public event EventHandler Elapsed;

        #endregion

        #region Enums

        /// <summary>
        /// Timer execution flags from WinNT.h
        /// </summary>
        [Flags]
        private enum ExecuteFlags : uint
        {
            // ReSharper disable InconsistentNaming

            /// <summary>
            /// By default, the callback function is queued to a non-I/O worker thread.
            /// </summary>
            WT_EXECUTEDEFAULT = 0x00000000,

            /// <summary>
            /// The callback function is invoked by the timer thread itself. This flag 
            /// should be used only for short tasks or it could affect other timer operations. 
            /// The callback function is queued as an APC. It should not perform alertable 
            /// wait operations.
            /// </summary>
            WT_EXECUTEINTIMERTHREAD = 0x00000020,

            /// <summary>
            /// The callback function is queued to an I/O worker thread. This flag should 
            /// be used if the function should be executed in a thread that waits in an 
            /// alertable state. 
            /// The callback function is queued as an APC. Be sure to address reentrancy 
            /// issues if the function performs an alertable wait operation.
            /// </summary>
            WT_EXECUTEINIOTHREAD = 0x00000001,

            /// <summary>
            /// The callback function is queued to a thread that never terminates. It 
            /// does not guarantee that the same thread is used each time. This flag 
            /// should be used only for short tasks or it could affect other timer 
            /// operations. currently no worker thread is truly persistent, 
            /// although no worker thread will terminate if there are any pending I/O 
            /// requests.
            /// </summary>
            WT_EXECUTEINPERSISTENTTHREAD = 0x00000080,

            /// <summary>
            /// The callback function can perform a long wait. This flag helps the 
            /// system to decide if it should create a new thread.
            /// </summary>
            WT_EXECUTELONGFUNCTION = 0x00000010,

            /// <summary>
            /// The timer will be set to the signaled state only once. If this flag 
            /// is set, the Period parameter must be zero.
            /// </summary>
            WT_EXECUTEONLYONCE = 0x00000008,

            /// <summary>
            /// Callback functions will use the current access token, whether it 
            /// is a process or impersonation token. If this flag is not specified, 
            /// callback functions execute only with the process token.
            /// Windows XP/2000:  This flag is not supported until Windows XP with 
            /// SP2 and Windows Server 2003.
            /// </summary>
            WT_TRANSFER_IMPERSONATION = 0x00000100

            // ReSharper restore InconsistentNaming
        }

        #endregion

        #region Public Properties

        public bool Enabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled == value)
                {
                    return;
                }

                _isEnabled = value;
            }
        }


        public ISynchronizeInvoke SynchronizingObject { get; set; }

        #endregion

        #region Properties

        private int _interval;

        public int Interval
        {
            get { return _interval; }
            set
            {
                if (_interval != value)
                {
                    _interval = value;

                    ChangeTimerQueueTimer(IntPtr.Zero, _timerPtr, 0, (uint) _interval);
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Start()
        {
            Enabled = true;
        }

        public void Stop()
        {
            Enabled = false;
        }

        #endregion

        #region Methods

        [DllImport("kernel32.dll")]
        private static extern bool ChangeTimerQueueTimer(IntPtr timerQueue, IntPtr timer, uint dueTime, uint period);

        [DllImport("kernel32.dll")]
        private static extern bool CreateTimerQueueTimer(
            out IntPtr phNewTimer,
            IntPtr timerQueue,
            WaitOrTimerDelegate callback,
            IntPtr parameter,
            uint dueTime,
            uint period,
            uint flags);

        [DllImport("kernel32.dll")]
        private static extern bool DeleteTimerQueueTimer(IntPtr timerQueue, IntPtr timer, IntPtr completionEvent);

        [DllImport("winmm.dll")]
        private static extern uint timeGetDevCaps(IntPtr pTimeCaps, uint sizeTimeCaps);

        private void OnElapsed()
        {
            //Debug.WriteLine("Elapsed at " + DateTime.Now.Ticks);
            if (Elapsed != null)
            {
                if (SynchronizingObject != null)
                {
                    SynchronizingObject.Invoke(Elapsed, null);
                }
                else
                    Elapsed(this, new QueueTimerEventArgs());
            }
        }

        private void TimerCallback(IntPtr lpParameter, bool timerOrWaitFired)
        {
            //TODO: lock? http://stackoverflow.com/questions/133270/how-to-illustrate-usage-of-volatile-keyword-in-c-sharp
            if (Enabled) OnElapsed();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Nested type: TimeCaps

        [StructLayout(LayoutKind.Sequential)]
        public struct TimeCaps
        {
            public UInt32 wPeriodMin;

            public UInt32 wPeriodMax;
        };

        #endregion
    }
}