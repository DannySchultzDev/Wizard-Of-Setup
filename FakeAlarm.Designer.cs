namespace Wizard_Of_Set_Up
{
	partial class FakeAlarm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FakeAlarm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.ErrorLabel = new System.Windows.Forms.Label();
			this.ErrorPictureBoxR = new Wizard_Of_Set_Up.HighQualityPictureBox();
			this.OkButton = new System.Windows.Forms.Button();
			this.ErrorPictureBoxL = new Wizard_Of_Set_Up.HighQualityPictureBox();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ErrorPictureBoxR)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorPictureBoxL)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.Controls.Add(this.ErrorPictureBoxL, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.ErrorLabel, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.ErrorPictureBoxR, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.OkButton, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(468, 163);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// ErrorLabel
			// 
			this.ErrorLabel.AutoSize = true;
			this.ErrorLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ErrorLabel.Font = new System.Drawing.Font("Stencil", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ErrorLabel.Location = new System.Drawing.Point(158, 0);
			this.ErrorLabel.Name = "ErrorLabel";
			this.ErrorLabel.Size = new System.Drawing.Size(149, 114);
			this.ErrorLabel.TabIndex = 0;
			this.ErrorLabel.Text = "Error";
			this.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ErrorPictureBoxR
			// 
			this.ErrorPictureBoxR.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ErrorPictureBoxR.Image = global::Wizard_Of_Set_Up.Properties.Resources.Error;
			this.ErrorPictureBoxR.Location = new System.Drawing.Point(313, 3);
			this.ErrorPictureBoxR.Name = "ErrorPictureBoxR";
			this.ErrorPictureBoxR.Size = new System.Drawing.Size(152, 108);
			this.ErrorPictureBoxR.TabIndex = 1;
			this.ErrorPictureBoxR.TabStop = false;
			// 
			// OkButton
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.OkButton, 3);
			this.OkButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OkButton.Location = new System.Drawing.Point(3, 117);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(462, 43);
			this.OkButton.TabIndex = 2;
			this.OkButton.Text = "OK";
			this.OkButton.UseVisualStyleBackColor = true;
			this.OkButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// ErrorPictureBoxL
			// 
			this.ErrorPictureBoxL.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ErrorPictureBoxL.Image = global::Wizard_Of_Set_Up.Properties.Resources.Error;
			this.ErrorPictureBoxL.Location = new System.Drawing.Point(3, 3);
			this.ErrorPictureBoxL.Name = "ErrorPictureBoxL";
			this.ErrorPictureBoxL.Size = new System.Drawing.Size(149, 108);
			this.ErrorPictureBoxL.TabIndex = 3;
			this.ErrorPictureBoxL.TabStop = false;
			// 
			// FakeAlarm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(468, 163);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FakeAlarm";
			this.Text = "Error";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FakeAlarm_FormClosing);
			this.Load += new System.EventHandler(this.FakeAlarm_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ErrorPictureBoxR)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorPictureBoxL)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label ErrorLabel;
		private HighQualityPictureBox ErrorPictureBoxR;
		private HighQualityPictureBox ErrorPictureBoxL;
		private System.Windows.Forms.Button OkButton;
	}
}