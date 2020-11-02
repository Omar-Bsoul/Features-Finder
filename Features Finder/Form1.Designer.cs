namespace Features_Finder
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
            this.textBox_video = new System.Windows.Forms.TextBox();
            this.textBox_concept = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_frames = new System.Windows.Forms.TextBox();
            this.radioButton_1fps = new System.Windows.Forms.RadioButton();
            this.radioButton_4fps = new System.Windows.Forms.RadioButton();
            this.processBtn = new System.Windows.Forms.Button();
            this.searchResultGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.searchResultGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_video
            // 
            this.textBox_video.Enabled = false;
            this.textBox_video.Location = new System.Drawing.Point(150, 79);
            this.textBox_video.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_video.Name = "textBox_video";
            this.textBox_video.Size = new System.Drawing.Size(549, 27);
            this.textBox_video.TabIndex = 1;
            // 
            // textBox_concept
            // 
            this.textBox_concept.Enabled = false;
            this.textBox_concept.Location = new System.Drawing.Point(150, 41);
            this.textBox_concept.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_concept.Name = "textBox_concept";
            this.textBox_concept.Size = new System.Drawing.Size(549, 27);
            this.textBox_concept.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Selected Videos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 45);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Selected Concept";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 120);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Frame Count";
            // 
            // textBox_frames
            // 
            this.textBox_frames.Enabled = false;
            this.textBox_frames.Location = new System.Drawing.Point(150, 116);
            this.textBox_frames.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_frames.Name = "textBox_frames";
            this.textBox_frames.Size = new System.Drawing.Size(549, 27);
            this.textBox_frames.TabIndex = 9;
            // 
            // radioButton_1fps
            // 
            this.radioButton_1fps.AutoSize = true;
            this.radioButton_1fps.Location = new System.Drawing.Point(20, 195);
            this.radioButton_1fps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton_1fps.Name = "radioButton_1fps";
            this.radioButton_1fps.Size = new System.Drawing.Size(65, 24);
            this.radioButton_1fps.TabIndex = 20;
            this.radioButton_1fps.TabStop = true;
            this.radioButton_1fps.Text = "1 FPS";
            this.radioButton_1fps.UseVisualStyleBackColor = true;
            // 
            // radioButton_4fps
            // 
            this.radioButton_4fps.AutoSize = true;
            this.radioButton_4fps.Location = new System.Drawing.Point(20, 229);
            this.radioButton_4fps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton_4fps.Name = "radioButton_4fps";
            this.radioButton_4fps.Size = new System.Drawing.Size(65, 24);
            this.radioButton_4fps.TabIndex = 21;
            this.radioButton_4fps.TabStop = true;
            this.radioButton_4fps.Text = "4 FPS";
            this.radioButton_4fps.UseVisualStyleBackColor = true;
            // 
            // processBtn
            // 
            this.processBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.processBtn.Location = new System.Drawing.Point(624, 195);
            this.processBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.processBtn.Name = "processBtn";
            this.processBtn.Size = new System.Drawing.Size(75, 58);
            this.processBtn.TabIndex = 22;
            this.processBtn.Text = "Process";
            this.processBtn.UseVisualStyleBackColor = true;
            this.processBtn.Click += new System.EventHandler(this.ProcessBtn_Click);
            // 
            // searchResultGridView
            // 
            this.searchResultGridView.AllowUserToAddRows = false;
            this.searchResultGridView.AllowUserToDeleteRows = false;
            this.searchResultGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchResultGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.searchResultGridView.Location = new System.Drawing.Point(706, 0);
            this.searchResultGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.searchResultGridView.Name = "searchResultGridView";
            this.searchResultGridView.ReadOnly = true;
            this.searchResultGridView.RowHeadersWidth = 51;
            this.searchResultGridView.Size = new System.Drawing.Size(660, 599);
            this.searchResultGridView.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 599);
            this.Controls.Add(this.searchResultGridView);
            this.Controls.Add(this.processBtn);
            this.Controls.Add(this.radioButton_4fps);
            this.Controls.Add(this.radioButton_1fps);
            this.Controls.Add(this.textBox_frames);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_concept);
            this.Controls.Add(this.textBox_video);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.searchResultGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_video;
        private System.Windows.Forms.TextBox textBox_concept;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_frames;
        private System.Windows.Forms.RadioButton radioButton_1fps;
        private System.Windows.Forms.RadioButton radioButton_4fps;
        private System.Windows.Forms.Button processBtn;
        private System.Windows.Forms.DataGridView searchResultGridView;
    }
}

