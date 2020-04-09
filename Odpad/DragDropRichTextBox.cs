using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odpad
{
    class DragDropRichTextBox: RichTextBox
    {
        //    public DragDropRichTextBox()
        //    {
        //        this.AllowDrop = true;
        //        this.DragDrop += new DragEventHandler(MainRichTextBox_DragDrop);
        //    }

        //    private void MainRichTextBox_DragDrop(object sender, DragEventArgs e)
        //    {
        //        string[] fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];
        //        if (fileNames != null)
        //        {
        //            foreach (string name in fileNames)
        //            {
        //                try
        //                {
        //                    this.AppendText(File.ReadAllText(name));
        //                        }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.Message);
        //                }
        //            }

        //        }
        //    }





    }
}
