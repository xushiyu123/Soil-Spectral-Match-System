using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SMSS
{
    public partial class Waiting : Form
    {
        public static bool IsWait = true;
        public Waiting()
        {
            InitializeComponent();
            webBrowser1.Url = new System.Uri(AppDomain.CurrentDomain.BaseDirectory + "waiting.gif");
            timer1.Interval = 10;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (IsWait == false)
                Close();
        }

        private void Waiting_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }

        private void Waiting_Load(object sender, EventArgs e)
        {
            IsWait = true;
            this.TransparencyKey = this.BackColor;
        }
    }
}
