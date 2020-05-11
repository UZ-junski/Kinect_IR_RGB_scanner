namespace Kinect_01
{
	partial class SJU_Kinect_01
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
			this.btnConnect = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.lblId = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.btnGetNext = new System.Windows.Forms.Button();
			this.btnSaveDistances = new System.Windows.Forms.Button();
			this.btnSaveImage = new System.Windows.Forms.Button();
			this.btnGetColorNext = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(12, 12);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(75, 23);
			this.btnConnect.TabIndex = 0;
			this.btnConnect.Text = "Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(64, 38);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(10, 13);
			this.lblStatus.TabIndex = 1;
			this.lblStatus.Text = "-";
			// 
			// lblId
			// 
			this.lblId.AutoSize = true;
			this.lblId.Location = new System.Drawing.Point(230, 38);
			this.lblId.Name = "lblId";
			this.lblId.Size = new System.Drawing.Size(10, 13);
			this.lblId.TabIndex = 2;
			this.lblId.Text = "-";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 38);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Status: ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(187, 38);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(27, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "ID: -";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox2.Location = new System.Drawing.Point(12, 54);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(782, 542);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox2.TabIndex = 7;
			this.pictureBox2.TabStop = false;
			// 
			// btnGetNext
			// 
			this.btnGetNext.Location = new System.Drawing.Point(93, 12);
			this.btnGetNext.Name = "btnGetNext";
			this.btnGetNext.Size = new System.Drawing.Size(75, 23);
			this.btnGetNext.TabIndex = 8;
			this.btnGetNext.Text = "Get Next";
			this.btnGetNext.UseVisualStyleBackColor = true;
			this.btnGetNext.Click += new System.EventHandler(this.btnGetNext_Click);
			// 
			// btnSaveDistances
			// 
			this.btnSaveDistances.Location = new System.Drawing.Point(570, 12);
			this.btnSaveDistances.Name = "btnSaveDistances";
			this.btnSaveDistances.Size = new System.Drawing.Size(109, 23);
			this.btnSaveDistances.TabIndex = 9;
			this.btnSaveDistances.Text = "Save distances";
			this.btnSaveDistances.UseVisualStyleBackColor = true;
			this.btnSaveDistances.Click += new System.EventHandler(this.btnSaveDistances_Click);
			// 
			// btnSaveImage
			// 
			this.btnSaveImage.Location = new System.Drawing.Point(685, 12);
			this.btnSaveImage.Name = "btnSaveImage";
			this.btnSaveImage.Size = new System.Drawing.Size(109, 23);
			this.btnSaveImage.TabIndex = 10;
			this.btnSaveImage.Text = "Save bitmap";
			this.btnSaveImage.UseVisualStyleBackColor = true;
			this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
			// 
			// btnGetColorNext
			// 
			this.btnGetColorNext.Location = new System.Drawing.Point(174, 12);
			this.btnGetColorNext.Name = "btnGetColorNext";
			this.btnGetColorNext.Size = new System.Drawing.Size(92, 23);
			this.btnGetColorNext.TabIndex = 11;
			this.btnGetColorNext.Text = "Get Color Next";
			this.btnGetColorNext.UseVisualStyleBackColor = true;
			this.btnGetColorNext.Click += new System.EventHandler(this.btnGetColorNext_Click_1);
			// 
			// SJU_Kinect_01
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(806, 608);
			this.Controls.Add(this.btnGetColorNext);
			this.Controls.Add(this.btnSaveImage);
			this.Controls.Add(this.btnSaveDistances);
			this.Controls.Add(this.btnGetNext);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblId);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnConnect);
			this.Name = "SJU_Kinect_01";
			this.Text = "SJU_Kinect_01";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label lblId;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Button btnGetNext;
		private System.Windows.Forms.Button btnSaveDistances;
		private System.Windows.Forms.Button btnSaveImage;
		private System.Windows.Forms.Button btnGetColorNext;
	}
}

