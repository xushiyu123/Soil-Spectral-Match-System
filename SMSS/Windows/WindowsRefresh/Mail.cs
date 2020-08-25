using System;
using System.Windows.Forms;

namespace SMSS.Windows
{
    public partial class Mail : Form
    {
        public Mail()
        {
            InitializeComponent();
            webBrowser1.Url = new Uri("https://outlook.live.com//owa//");
        }
    }
}
