using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Jdlc.Timers;

namespace QTimerSandbox
{

    public partial class Form1 : Form
    {
        private short _callbackCount;
        private readonly TimerQueueTimer _timer = new TimerQueueTimer();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _timer.Elapsed += _timer_Elapsed;
            _timer.SynchronizingObject = lblCounter;
        }

        void _timer_Elapsed(object sender, EventArgs e)
        {
            lblCounter.Text = _callbackCount++.ToString();
        }


        private void btnDisposeTimer_Click(object sender, EventArgs e)
        {
            _timer.Dispose();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _timer.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _timer.Stop();
        }

        private void btnModifyPeriod_Click(object sender, EventArgs e)
        {
            _timer.Interval = int.Parse(txtPeriod.Text);
        }


    }
}
