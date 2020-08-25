using System.Windows.Forms;

namespace SMSS.Windows
{
    public partial class ShowPDF : Form
    {
        public ShowPDF(string filename)
        {
            InitializeComponent();
            axAcroPDF1.LoadFile(filename);
        }
    }
}
