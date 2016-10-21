namespace qrCodeTest
{
    partial class Form1
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
            this.picQy = new System.Windows.Forms.PictureBox();
            this.generate = new System.Windows.Forms.Button();
            this.content = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picQy)).BeginInit();
            this.SuspendLayout();
            // 
            // picQy
            // 
            this.picQy.Location = new System.Drawing.Point(61, 142);
            this.picQy.Name = "picQy";
            this.picQy.Size = new System.Drawing.Size(423, 259);
            this.picQy.TabIndex = 0;
            this.picQy.TabStop = false;
            // 
            // generate
            // 
            this.generate.Location = new System.Drawing.Point(353, 93);
            this.generate.Name = "generate";
            this.generate.Size = new System.Drawing.Size(75, 23);
            this.generate.TabIndex = 1;
            this.generate.Text = "生成";
            this.generate.UseVisualStyleBackColor = true;
            this.generate.Click += new System.EventHandler(this.generate_Click);
            // 
            // content
            // 
            this.content.Location = new System.Drawing.Point(61, 29);
            this.content.Multiline = true;
            this.content.Name = "content";
            this.content.Size = new System.Drawing.Size(285, 87);
            this.content.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 480);
            this.Controls.Add(this.content);
            this.Controls.Add(this.generate);
            this.Controls.Add(this.picQy);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picQy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picQy;
        private System.Windows.Forms.Button generate;
        private System.Windows.Forms.TextBox content;
    }
}

