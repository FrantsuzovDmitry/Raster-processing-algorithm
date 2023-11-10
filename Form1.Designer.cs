namespace WindowsFormsApp1
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
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.selectImageButton = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.transformButton = new System.Windows.Forms.Button();
			this.applyFilterButton = new System.Windows.Forms.Button();
			this.filtersBox = new System.Windows.Forms.ComboBox();
			this.primaryAlgorythmButtom = new System.Windows.Forms.RadioButton();
			this.bilinearAlgorythmButton = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.pictureBox2.Location = new System.Drawing.Point(527, 12);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(605, 484);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 1;
			this.pictureBox2.TabStop = false;
			this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox2_MouseClick);
			// 
			// selectImageButton
			// 
			this.selectImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.selectImageButton.Location = new System.Drawing.Point(27, 26);
			this.selectImageButton.Name = "selectImageButton";
			this.selectImageButton.Size = new System.Drawing.Size(249, 44);
			this.selectImageButton.TabIndex = 2;
			this.selectImageButton.Text = "Select image";
			this.selectImageButton.UseVisualStyleBackColor = true;
			this.selectImageButton.Click += new System.EventHandler(this.SelectImageButton_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.pictureBox1.Location = new System.Drawing.Point(27, 146);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(451, 321);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseClick);
			// 
			// transformButton
			// 
			this.transformButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.transformButton.Location = new System.Drawing.Point(27, 91);
			this.transformButton.Name = "transformButton";
			this.transformButton.Size = new System.Drawing.Size(249, 32);
			this.transformButton.TabIndex = 2;
			this.transformButton.Text = "Make transformation";
			this.transformButton.UseVisualStyleBackColor = true;
			this.transformButton.Click += new System.EventHandler(this.TransformButton_Click);
			// 
			// applyFilterButton
			// 
			this.applyFilterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.applyFilterButton.Location = new System.Drawing.Point(282, 91);
			this.applyFilterButton.Name = "applyFilterButton";
			this.applyFilterButton.Size = new System.Drawing.Size(196, 32);
			this.applyFilterButton.TabIndex = 2;
			this.applyFilterButton.Text = "Apply filter";
			this.applyFilterButton.UseVisualStyleBackColor = true;
			this.applyFilterButton.Click += new System.EventHandler(this.ApplyFilterButton_Click);
			// 
			// filtersBox
			// 
			this.filtersBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.filtersBox.FormattingEnabled = true;
			this.filtersBox.Location = new System.Drawing.Point(282, 46);
			this.filtersBox.Name = "filtersBox";
			this.filtersBox.Size = new System.Drawing.Size(196, 24);
			this.filtersBox.TabIndex = 3;
			// 
			// primaryAlgorythmButtom
			// 
			this.primaryAlgorythmButtom.AutoSize = true;
			this.primaryAlgorythmButtom.Location = new System.Drawing.Point(282, 26);
			this.primaryAlgorythmButtom.Name = "primaryAlgorythmButtom";
			this.primaryAlgorythmButtom.Size = new System.Drawing.Size(59, 17);
			this.primaryAlgorythmButtom.TabIndex = 5;
			this.primaryAlgorythmButtom.TabStop = true;
			this.primaryAlgorythmButtom.Text = "Primary";
			this.primaryAlgorythmButtom.UseVisualStyleBackColor = true;
			this.primaryAlgorythmButtom.CheckedChanged += new System.EventHandler(this.PrimaryAlgorythmButtom_CheckedChanged);
			// 
			// bilinearAlgorythmButton
			// 
			this.bilinearAlgorythmButton.AutoSize = true;
			this.bilinearAlgorythmButton.Location = new System.Drawing.Point(419, 26);
			this.bilinearAlgorythmButton.Name = "bilinearAlgorythmButton";
			this.bilinearAlgorythmButton.Size = new System.Drawing.Size(59, 17);
			this.bilinearAlgorythmButton.TabIndex = 5;
			this.bilinearAlgorythmButton.TabStop = true;
			this.bilinearAlgorythmButton.Text = "Bileniar";
			this.bilinearAlgorythmButton.UseVisualStyleBackColor = true;
			this.bilinearAlgorythmButton.CheckedChanged += new System.EventHandler(this.BilinearAlgorythmButton_CheckedChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1301, 508);
			this.Controls.Add(this.bilinearAlgorythmButton);
			this.Controls.Add(this.primaryAlgorythmButtom);
			this.Controls.Add(this.filtersBox);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.transformButton);
			this.Controls.Add(this.applyFilterButton);
			this.Controls.Add(this.selectImageButton);
			this.Controls.Add(this.pictureBox2);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Button selectImageButton;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button transformButton;
		private System.Windows.Forms.Button applyFilterButton;
		private System.Windows.Forms.ComboBox filtersBox;
		private System.Windows.Forms.RadioButton primaryAlgorythmButtom;
		private System.Windows.Forms.RadioButton bilinearAlgorythmButton;
	}
}

