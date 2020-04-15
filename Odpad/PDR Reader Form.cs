using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.PdfViewer.Forms;
using System.IO;

namespace Odpad
{
    public partial class PDF_Reader_Form : Form
    {
        public PDF_Reader_Form()
        {
            InitializeComponent();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "PDF documents| *.pdf", ValidateNames = true, Multiselect = false })
        //    {
        //        if (ofd.ShowDialog() == DialogResult.OK)
        //        {
        //            try
        //            {
        //                if (File.Exists(ofd.FileName))
        //                {
        //                    this.pdfViewer1.LoadFromFile(ofd.FileName);
        //                }
        //                //webBrowser1.Url = new Uri(ofd.FileName);
        //                //using (StreamReader sr = new StreamReader(ofd.FileName))
        //                //{
        //                //    path = ofd.FileName;
        //                //    Task<string> text = sr.ReadToEndAsync();
        //                //    MainRichTextBox.Text = text.Result;
        //                //}
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //}
    }
}
