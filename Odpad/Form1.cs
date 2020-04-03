using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Odpad
{
    public partial class MainNotepadFrm : Form
    {
        public string path;
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

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Undo();
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = true;
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Redo();
            redoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem.Enabled = true;
            
        }

        private void insertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Images |*.bmp;*.jpg;*.png;*.gif;*.jpeg", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromFile(ofd.FileName);
                        Clipboard.SetImage(img);
                        MainRichTextBox.Paste();
                        MainRichTextBox.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error");
                    }
                }
            }


        }


        private void MainRichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MainRichTextBox.Text.Length > 0)
            {
                textColourToolStripMenuItem.Enabled = true;
                fontDialogToolStripMenuItem.Enabled = true;
                undoToolStripMenuItem.Enabled = true;
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                selectAllToolStripMenuItem.Enabled = true;
                boldToolStripMenuItem.Enabled = true;
                italicToolStripMenuItem.Enabled = true;
                underlineToolStripMenuItem.Enabled = true;
                strikeThroughToolStripMenuItem.Enabled = true;
                normalToolStripMenuItem.Enabled = true;
            }
            else
            {
                textColourToolStripMenuItem.Enabled = false;
                fontDialogToolStripMenuItem.Enabled = false;
                boldToolStripMenuItem.Enabled = false;
                italicToolStripMenuItem.Enabled = false;
                underlineToolStripMenuItem.Enabled = false;
                strikeThroughToolStripMenuItem.Enabled = false;
                normalToolStripMenuItem.Enabled = false;
                selectAllToolStripMenuItem.Enabled = false;
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                undoToolStripMenuItem.Enabled = false;
                redoToolStripMenuItem.Enabled = false;
            }
        }

        #region Font
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectAll();
        }

        private void dateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Text += DateTime.Now;
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Bold); //працює
            #region another variant
            if (MainRichTextBox.SelectionFont != null)
            {
                System.Drawing.Font currentFont = MainRichTextBox.SelectionFont;
                System.Drawing.FontStyle newFontStyle;
                if (MainRichTextBox.SelectionFont.Bold == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Bold;
                }

                MainRichTextBox.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                CheckMenuFontCharackterStyle();
            }
            #endregion

        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Italic);//без помітки
            #region another variant
            if (MainRichTextBox.SelectionFont != null)
            {
                System.Drawing.Font currentFont = MainRichTextBox.SelectionFont;
                System.Drawing.FontStyle newFontStyle;
                if (MainRichTextBox.SelectionFont.Italic == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Italic;
                }

                MainRichTextBox.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                CheckMenuFontCharackterStyle();
            }
            #endregion
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Underline);
            #region another variant
            if (MainRichTextBox.SelectionFont != null)
            {
                System.Drawing.Font currentFont = MainRichTextBox.SelectionFont;
                System.Drawing.FontStyle newFontStyle;
                if (MainRichTextBox.SelectionFont.Underline == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Underline;
                }

                MainRichTextBox.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                CheckMenuFontCharackterStyle();
            }
            #endregion

        }

        private void strikeThroughToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MainRichTextBox.SelectionFont = new Font(MainRichTextBox.Font, FontStyle.Strikeout);
            #region another variant
            if (MainRichTextBox.SelectionFont != null)
            {
                System.Drawing.Font currentFont = MainRichTextBox.SelectionFont;
                System.Drawing.FontStyle newFontStyle;
                if (MainRichTextBox.SelectionFont.Strikeout == true)
                {
                    newFontStyle = FontStyle.Regular;
                }
                else
                {
                    newFontStyle = FontStyle.Strikeout;
                }

                MainRichTextBox.SelectionFont = new System.Drawing.Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                CheckMenuFontCharackterStyle();
            }
            #endregion
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new System.Drawing.Font(MainRichTextBox.Font, FontStyle.Regular);
        }

        private void fontDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog()==DialogResult.OK)
            {
                MainRichTextBox.Font = fd.Font;
            }
        }

        private void textColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.ShowDialog();
            MainRichTextBox.SelectionColor = cd.Color;
        }

        #region выравнивание

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
        }
        #endregion

        #endregion


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region
            //using (OpenFileDialog ofd = new OpenFileDialog() {Filter="Text documents| *.txt",ValidateNames=true,Multiselect=false })
            //{
            //    if (ofd.ShowDialog() == DialogResult.OK)
            //    {
            //        try
            //        {
            //            using (StreamReader sr = new StreamReader(ofd.FileName))
            //            {
            //                path = ofd.FileName;
            //                Task<string> text = sr.ReadToEndAsync();
            //                MainRichTextBox.Text = text.Result;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
            #endregion
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "RTF files|*.rtf|Text documents| *.txt|All files|*.*", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //using (StreamReader sr = new StreamReader(ofd.FileName))
                        //{
                        //    path = ofd.FileName;
                        //    Task<string> text = sr.ReadToEndAsync();
                        //    MainRichTextBox.Text = text.Result;
                        //}
                        MainRichTextBox.LoadFile(ofd.FileName, RichTextBoxStreamType.RichText);
                    }
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                    catch (Exception ex)
                    {
                        MainRichTextBox.LoadFile(ofd.FileName, RichTextBoxStreamType.PlainText);
                    }
                    this.Text = ofd.FileName;
                }
            }

        }

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(path))
            {
                using (SaveFileDialog sfd=new SaveFileDialog() { Filter = "RTF files|*.rtf|Text documents| *.txt|All files|*.*", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {//=========================Зберігає без кодування
                        //MainRichTextBox.SaveFile(sfd.FileName);
                        //this.Text = sfd.FileName;
                        //--------------------------------------------------------------
                        try
                        {
                            path = sfd.FileName;
                            using (StreamWriter sw = new StreamWriter(sfd.FileName))
                            {
                                await sw.WriteLineAsync(MainRichTextBox.Text);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        await sw.WriteLineAsync(MainRichTextBox.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "RTF files|*.rtf|Text documents| *.txt|All files|*.*", ValidateNames = true })
            {
               
                    if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            await sw.WriteLineAsync(MainRichTextBox.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
                
                
            }
        }

        #region check style
        private void CheckMenuFontCharackterStyle()
        {
            if (MainRichTextBox.SelectionFont.Bold==true)
            {
                boldToolStripMenuItem.Checked = true;
            }
            else {
                boldToolStripMenuItem.Checked = false;
            }

            if (MainRichTextBox.SelectionFont.Italic == true)
            {
                italicToolStripMenuItem.Checked = true;
            }
            else
            {
                italicToolStripMenuItem.Checked = false;
            }

            if (MainRichTextBox.SelectionFont.Strikeout == true)
            {
                strikeThroughToolStripMenuItem.Checked = true;
            }
            else
            {
                strikeThroughToolStripMenuItem.Checked = false;
            }

            if (MainRichTextBox.SelectionFont.Underline == true)
            {
                underlineToolStripMenuItem.Checked = true;
            }
            else
            {
                underlineToolStripMenuItem.Checked = false;
            }

        }
        #endregion
        #region Search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string[] words = txtSearch.Text.Split(',');
            foreach (string word in words)
            {
                int startIndex = 0;
                while (startIndex<MainRichTextBox.TextLength)
                {
                    int wordStartIndex = MainRichTextBox.Find(word,startIndex,RichTextBoxFinds.None);
                    if (wordStartIndex != -1)
                    {
                        MainRichTextBox.SelectionStart = wordStartIndex;
                        MainRichTextBox.SelectionLength = word.Length;
                        MainRichTextBox.SelectionBackColor = Color.Aqua;
                    }
                    else break;
                    startIndex = wordStartIndex + word.Length;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionStart = 0;
            MainRichTextBox.SelectAll();
            MainRichTextBox.SelectionBackColor = Color.White;
        }
        #endregion


        private void saveInPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF file| *.pdf", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4.Rotate());
                    try
                    {
                        PdfWriter.GetInstance(doc,new FileStream(sfd.FileName,FileMode.Create));
                        doc.Open();
                        doc.Add(new iTextSharp.text.Paragraph(MainRichTextBox.Text));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        doc.Close();
                    }
                }
            }
        }
    }
}
