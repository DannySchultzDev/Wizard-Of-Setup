namespace Wizard_Of_Set_Up
{
	partial class Spawnable
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
			this.SpawnableImage = new Wizard_Of_Set_Up.HighQualityPictureBox();
			((System.ComponentModel.ISupportInitialize)(this.SpawnableImage)).BeginInit();
			this.SuspendLayout();
			// 
			// SpawnableImage
			// 
			this.SpawnableImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SpawnableImage.Location = new System.Drawing.Point(0, 0);
			this.SpawnableImage.Name = "SpawnableImage";
			this.SpawnableImage.Size = new System.Drawing.Size(32, 39);
			this.SpawnableImage.TabIndex = 0;
			this.SpawnableImage.TabStop = false;
			// 
			// Spawnable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(32, 39);
			this.Controls.Add(this.SpawnableImage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Spawnable";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Spawnable_FormClosing);
			this.Load += new System.EventHandler(this.Spawnable_Load);
			((System.ComponentModel.ISupportInitialize)(this.SpawnableImage)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private HighQualityPictureBox SpawnableImage;
	}
}