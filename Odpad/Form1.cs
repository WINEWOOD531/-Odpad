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
using System.Speech.Synthesis;
using Image = iTextSharp.text.Image;

namespace Odpad
{
    public partial class MainNotepadFrm : Form
    {
        string strImagePath;
        //public bool OriginalText = true;
        private bool isFileAlreadySaved;
        private bool isFileDirty;
        private string currOpenFileName;
        public string path;
        SpeechSynthesizer speech;
        private int indent = 10;
        public MainNotepadFrm()
        {
            InitializeComponent();
            MainRichTextBox.DragDrop += new DragEventHandler(MainRichTextBox_DragDrop);
            MainRichTextBox.AllowDrop = true;
            speech = new SpeechSynthesizer();
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                capsToolStripStatusLabel1.Text = "Caps ON";
            }
            else {
                capsToolStripStatusLabel1.Text = "Caps OFF";
            }
        }

        public int INDENT
        {
            get { return indent; }
            set { indent = value; }
        }

        /// <summary>
        /// Drag&Drop option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainRichTextBox_DragDrop(object sender, DragEventArgs e)
        {
            object filename = e.Data.GetData("FileDrop");
            if (isFileDirty)
            {
                DialogResult dialogResult = MessageBox.Show("Save the file before exiting?", "Save file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    saveToolStripMenuItem.PerformClick();
                    Dispose(true);
                    Application.Exit();
                }
            }
            if (filename != null)
            {
                var list = filename as string[];
                if (list != null && !string.IsNullOrWhiteSpace(list[0]))
                {
                    MainRichTextBox.Clear();
                    MainRichTextBox.LoadFile(list[0], RichTextBoxStreamType.PlainText);
                }
            }


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
/// <summary>
/// Create New File
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isFileDirty)
            {
                DialogResult result = MessageBox.Show("Do you want to save your changes?", "File Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                switch (result)
                {
                    case DialogResult.Yes:
                        SaveFileMenu();
                        ClearScreen();
                        break;
                    case DialogResult.No:
                        //MainRichTextBox.Text = "";
                        //this.Text = "Untitled - Odpad";
                        //MessageToolStripStatusLabel1.Text = "New Document is created";
                        ClearScreen();
                        break;
                }
            }

            else
            {
                MainRichTextBox.Text = "";
                //MainRichTextBox.Clear();
                this.Text = "Untitled - Odpad";
                MessageToolStripStatusLabel1.Text = "New Document is created";
            }
            #region 
            //MainRichTextBox.Text = "";
            ////MainRichTextBox.Clear();
            //this.Text = "Untitled - Odpad";
            //MessageToolStripStatusLabel1.Text = "New Document is created";
            #endregion
        }
        private void ClearScreen()
        {
            MainRichTextBox.Text = "";
            this.Text = "Untitled - Odpad";
            MessageToolStripStatusLabel1.Text = "New Document is created";
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

        /// <summary>
        /// Insert image in document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region
            //using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Images |*.bmp;*.jpg;*.png;*.gif;*.jpeg", ValidateNames = true, Multiselect = false })
            //{
            //    if (ofd.ShowDialog() == DialogResult.OK)
            //    {
            //        try
            //        {
            //            System.Drawing.Image img = System.Drawing.Image.FromFile(ofd.FileName);
            //            Clipboard.SetImage(img);
            //            MainRichTextBox.Paste();
            //            MainRichTextBox.Focus();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Error");
            //        }
            //    }
            //}
            #endregion

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Insert picture";
                dlg.DefaultExt = "jpg";
                dlg.Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif|All files|*.*";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        strImagePath = dlg.FileName;
                        System.Drawing.Image img = System.Drawing.Image.FromFile(strImagePath);
                        Clipboard.SetDataObject(img);
                        DataFormats.Format df;
                        df = DataFormats.GetFormat(DataFormats.Bitmap);
                        if (this.MainRichTextBox.CanPaste(df))
                        {
                            this.MainRichTextBox.Paste(df);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Unable to insert image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            isFileDirty = true;
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

                    isFileAlreadySaved = true;
                    isFileDirty = false;
                    currOpenFileName = ofd.FileName;
                }
            }
            //OriginalText = true;
            MessageToolStripStatusLabel1.Text = "File is Opened";
        }

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region working
            //        if (isFileAlreadySaved)
            //        {
            //            if (Path.GetExtension(currOpenFileName)==".rtf")
            //            {
            //                MainRichTextBox.SaveFile(currOpenFileName, RichTextBoxStreamType.RichText);
            //            }

            //            if (Path.GetExtension(currOpenFileName) == ".txt")
            //            {
            //                MainRichTextBox.SaveFile(currOpenFileName, RichTextBoxStreamType.PlainText);
            //            }

            //            isFileDirty = false;
            //        }
            //else {
            //            if (isFileDirty)
            //            {



            //                #region saving without image
            //                //if (string.IsNullOrEmpty(path))
            //                //{
            //                //    using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "RTF files|*.rtf|Text documents| *.txt|All files|*.*", ValidateNames = true })
            //                //    {
            //                //        if (sfd.ShowDialog() == DialogResult.OK)
            //                //        {//=========================Зберігає без кодування
            //                //            //MainRichTextBox.SaveFile(sfd.FileName);
            //                //            //this.Text = sfd.FileName;
            //                //            //--------------------------------------------------------------
            //                //            try
            //                //            {
            //                //                path = sfd.FileName;
            //                //                using (StreamWriter sw = new StreamWriter(sfd.FileName))
            //                //                {
            //                //                    await sw.WriteLineAsync(MainRichTextBox.Text);
            //                //                }
            //                //            }
            //                //            catch (Exception ex)
            //                //            {
            //                //                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                //            }
            //                //        }
            //                //    }
            //                //}
            //                //else
            //                //{
            //                //    try
            //                //    {
            //                //        using (StreamWriter sw = new StreamWriter(path))
            //                //        {
            //                //            await sw.WriteLineAsync(MainRichTextBox.Text);
            //                //        }
            //                //    }
            //                //    catch (Exception ex)
            //                //    {
            //                //        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                //    }
            //                //}
            //                #endregion
            //                using (SaveFileDialog dlg = new SaveFileDialog())
            //                {
            //                    dlg.Filter = "Rich text format|*.rtf";
            //                    dlg.FilterIndex = 0;
            //                    dlg.OverwritePrompt = true;
            //                    if (dlg.ShowDialog() == DialogResult.OK)
            //                    {
            //                        try
            //                        {
            //                            MainRichTextBox.SaveFile(dlg.FileName, RichTextBoxStreamType.RichText);
            //                        }
            //                        catch (IOException exc)
            //                        {
            //                            MessageBox.Show("Error writing file: \n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                        }
            //                        catch (ArgumentException exc_a)
            //                        {
            //                            MessageBox.Show("Error writing file: \n" + exc_a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                        }
            //                        this.Text = Path.GetFileName(dlg.FileName) + " - Odpad";

            //                        //isFileAlreadySaved = true;
            //                        //isFileDirty = false;
            //                        //currOpenFileName = dlg.FileName;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                MainRichTextBox.Clear();
            //                this.Text = "Untitled - Odpad";
            //                isFileDirty = false;
            //            }  
            //        }
            #endregion
            SaveFileMenu();
        }
        private void SaveFileMenu()
        {
            if (isFileAlreadySaved)
            {
                if (Path.GetExtension(currOpenFileName) == ".rtf")
                {
                    MainRichTextBox.SaveFile(currOpenFileName, RichTextBoxStreamType.RichText);
                }

                if (Path.GetExtension(currOpenFileName) == ".txt")
                {
                    MainRichTextBox.SaveFile(currOpenFileName, RichTextBoxStreamType.PlainText);
                }

                isFileDirty = false;
            }
            else
            {
                if (isFileDirty)
                {



                    #region saving without image
                    //if (string.IsNullOrEmpty(path))
                    //{
                    //    using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "RTF files|*.rtf|Text documents| *.txt|All files|*.*", ValidateNames = true })
                    //    {
                    //        if (sfd.ShowDialog() == DialogResult.OK)
                    //        {//=========================Зберігає без кодування
                    //            //MainRichTextBox.SaveFile(sfd.FileName);
                    //            //this.Text = sfd.FileName;
                    //            //--------------------------------------------------------------
                    //            try
                    //            {
                    //                path = sfd.FileName;
                    //                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    //                {
                    //                    await sw.WriteLineAsync(MainRichTextBox.Text);
                    //                }
                    //            }
                    //            catch (Exception ex)
                    //            {
                    //                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    try
                    //    {
                    //        using (StreamWriter sw = new StreamWriter(path))
                    //        {
                    //            await sw.WriteLineAsync(MainRichTextBox.Text);
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    }
                    //}
                    #endregion
                    using (SaveFileDialog dlg = new SaveFileDialog())
                    {
                        dlg.Filter = "Rich text format|*.rtf";
                        dlg.FilterIndex = 0;
                        dlg.OverwritePrompt = true;
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                MainRichTextBox.SaveFile(dlg.FileName, RichTextBoxStreamType.RichText);
                            }
                            catch (IOException exc)
                            {
                                MessageBox.Show("Error writing file: \n" + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            catch (ArgumentException exc_a)
                            {
                                MessageBox.Show("Error writing file: \n" + exc_a.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            this.Text = Path.GetFileName(dlg.FileName) + " - Odpad";

                            //isFileAlreadySaved = true;
                            //isFileDirty = false;
                            //currOpenFileName = dlg.FileName;
                        }
                    }
                }
                else
                {
                    MainRichTextBox.Clear();
                    this.Text = "Untitled - Odpad";
                    isFileDirty = false;
                }
            }
        }

        /// <summary>
        /// Function Save as 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    this.Text = Path.GetFileName(sfd.FileName) + " - Odpad";

                    isFileAlreadySaved = true;
                    isFileDirty = false;
                    currOpenFileName = sfd.FileName;
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

        /// <summary>
        /// save file in pdf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveInPdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF file| *.pdf", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4.Rotate());
                    iTextSharp.text.Image PNG = iTextSharp.text.Image.GetInstance(strImagePath);
                    try
                    {
                        PdfWriter.GetInstance(doc,new FileStream(sfd.FileName,FileMode.Create));
                        doc.Open();
                        doc.Add(new iTextSharp.text.Paragraph(MainRichTextBox.Text));
                        doc.Add(PNG);
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

        #region Print
        /// <summary>
        /// Print Document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(MainRichTextBox.Text,new System.Drawing.Font("Times New Roman", 14,FontStyle.Bold),Brushes.Black,new PointF(100,100));
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog()==DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        #endregion

        #region Speech text
        /// <summary>
        /// Speech text option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void speakBtn_Click(object sender, EventArgs e)
        {
            if (MainRichTextBox.Text != "")
            {
                speech.SpeakAsync(MainRichTextBox.Text);
            }
        }

        private void pauseBtn_Click(object sender, EventArgs e)
        {
            if (speech.State == SynthesizerState.Speaking)
            {
                speech.Pause();
            }
        }

        private void resumeBtn_Click(object sender, EventArgs e)
        {
            if (speech.State == SynthesizerState.Paused)
            {
                speech.Resume();
            }
        }
        #endregion

        #region Bullets
        /// <summary>
        /// Toggle bullets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bulletsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                MainRichTextBox.SelectionBullet = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            try
            {
                MainRichTextBox.SelectionBullet = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        #endregion

        #region Zoom
        /// <summary>
        /// Zoom text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zoomInbtn_Click(object sender, EventArgs e)
        {
            if (MainRichTextBox.ZoomFactor < 64.0f - 0.20f)
            {
                MainRichTextBox.ZoomFactor += 0.20f;
                //tstxtZoomFactor.Text = String.Format("{0:F0}", MainRichTextBox.ZoomFactor * 100);
            }
        }

        private void zoomOutbtn_Click(object sender, EventArgs e)
        {
            if (MainRichTextBox.ZoomFactor > 0.16f + 0.20f)
            {
                MainRichTextBox.ZoomFactor -= 0.20f;
                //tstxtZoomFactor.Text = String.Format("{0:F0}", MainRichTextBox.ZoomFactor * 100);
            }
        }

        //private void tstxtZoomFactor_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        MainRichTextBox.ZoomFactor = Convert.ToSingle(tstxtZoomFactor.Text) / 100;
        //    }
        //    catch (FormatException)
        //    {
        //        MessageBox.Show("Enter valid number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        tstxtZoomFactor.Focus();
        //        tstxtZoomFactor.SelectAll();
        //    }
        //    catch (OverflowException)
        //    {
        //        MessageBox.Show("Enter valid number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        tstxtZoomFactor.Focus();
        //        tstxtZoomFactor.SelectAll();
        //    }
        //    catch (ArgumentException)
        //    {
        //        MessageBox.Show("Zoom factor should be between 20% and 6400%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        tstxtZoomFactor.Focus();
        //        tstxtZoomFactor.SelectAll();
        //    }
        //}
#endregion
        private void NewFileMenu()
        {
            //if (isFileDirty)
            //{

            //}
        }

        /// <summary>
        /// Caps ON/OFF indicator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                capsToolStripStatusLabel1.Text = "Caps ON";
            }
            else
            {
                capsToolStripStatusLabel1.Text = "Caps OFF";
            }
        }
        #region Text Wrap
        /// <summary>
        /// Text Wrap
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void textWrapToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (textWrapToolStripMenuItem.Checked == true)
        //    {
        //        MainRichTextBox.WordWrap = true;
        //    }
        //    else
        //    {
        //        MainRichTextBox.WordWrap = false;
        //    }
        //}
        #endregion

        /// <summary>
        /// Contex menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Context menu   
        private void normalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new System.Drawing.Font(MainRichTextBox.Font, FontStyle.Regular);
        }

        private void boldToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new System.Drawing.Font(MainRichTextBox.Font, FontStyle.Bold);
        }

        private void italicToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new System.Drawing.Font(MainRichTextBox.Font, FontStyle.Italic);
        }

        private void underlineToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectionFont = new System.Drawing.Font(MainRichTextBox.Font, FontStyle.Underline);
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Undo();
            undoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Enabled = true;
        }

        private void redoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Redo();
            redoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem.Enabled = true;
        }
        


        private void cutToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            MainRichTextBox.Cut();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.Paste();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectedText = "";
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MainRichTextBox.SelectAll();
        }
        #endregion
        
        /// <summary>
        /// OPEN PDF BUTTON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PDF_Reader_Form pdf = new PDF_Reader_Form();
            pdf.ShowDialog();
        }

        /// <summary>
        /// Action when form is load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainNotepadFrm_Load(object sender, EventArgs e)
        {
            isFileAlreadySaved = false;
            isFileDirty = false;
            currOpenFileName="";
    }

        /// <summary>
        /// Show you dilog for saving file before exit Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainNotepadFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            #region
            if (isFileDirty)
            {
                DialogResult result = MessageBox.Show("Do you want to save your changes?", "File Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                switch (result)
                {
                    case DialogResult.Yes:
                        SaveFileMenu();
                        ClearScreen();
                        break;
                    case DialogResult.No:
                        ClearScreen();
                        break;
                }
            }
            else
            {
                Application.Exit();
            }
            #endregion
        }
 
    }

}
