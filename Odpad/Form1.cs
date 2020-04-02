using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odpad
{
    public partial class MainNotepadFrm : Form
    {
        public MainNotepadFrm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All Rights reserved by the GRW","Help",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Text = "";
            //MainRichTextBox.Clear();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
