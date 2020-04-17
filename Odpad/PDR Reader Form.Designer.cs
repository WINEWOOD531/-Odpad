namespace Odpad
{
    partial class PDF_Reader_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PDF_Reader_Form));
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.pdfViewer1 = new Spire.PdfViewer.Forms.PdfViewer();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer1.FindTextHighLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(153)))), ((int)(((byte)(193)))), ((int)(((byte)(218)))));
            this.pdfViewer1.FormFillEnabled = false;
            this.pdfViewer1.IgnoreCase = false;
            this.pdfViewer1.IsToolBarVisible = true;
            this.pdfViewer1.Location = new System.Drawing.Point(0, 0);
            this.pdfViewer1.MaximumSize = new System.Drawing.Size(2600, 2000);
            this.pdfViewer1.MinimumSize = new System.Drawing.Size(1300, 700);
            this.pdfViewer1.MultiPagesThreshold = 60;
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.OnRenderPageExceptionEvent = null;
            this.pdfViewer1.Size = new System.Drawing.Size(1344, 700);
            this.pdfViewer1.TabIndex = 2;
            this.pdfViewer1.Text = "pdfViewer1";
            this.pdfViewer1.Threshold = 60;
            this.pdfViewer1.ViewerBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            // 
            // PDF_Reader_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 687);
            this.Controls.Add(this.pdfViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(2550, 2100);
            this.MinimumSize = new System.Drawing.Size(1360, 726);
            this.Name = "PDF_Reader_Form";
            this.Text = "PDF_Reader_Form";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSource1;
        private Spire.PdfViewer.Forms.PdfViewer pdfViewer1;
    }
}