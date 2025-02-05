using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Netzwerklabrinth_V_WPF
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void gameStartbutton_Click(object sender, EventArgs e)
        {
            if (ManuelradioButton.Checked)
            {
                ManuelForm Manu = new ManuelForm();
                Manu.ShowDialog();
                this.Close();
            }
            else if (AutomaticradioButton.Checked)
            {
                AutomaticForm Auto = new AutomaticForm();
                Auto.ShowDialog();
                this.Close();
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtInfos_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblLabyrinth_Click(object sender, EventArgs e)
        {

        }
    }
}










