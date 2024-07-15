using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wizard_Of_Set_Up
{
	public partial class HighQualityPictureBox : PictureBox
	{
		public HighQualityPictureBox()
		{
			InitializeComponent();
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			// Call the base class method
			base.OnPaint(pe);

			// Check if there is an image to draw
			if (this.Image == null)
				return;

			// Set the interpolation mode to high quality
			pe.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;

			// Calculate the aspect ratio
			float aspectRatio = (float)this.Image.Width / this.Image.Height;
			int newWidth, newHeight;
			int offsetX = 0, offsetY = 0;

			// Calculate the new size maintaining the aspect ratio
			if (this.Width / aspectRatio > this.Height)
			{
				newHeight = this.Height;
				newWidth = (int)(newHeight * aspectRatio);
				offsetX = (this.Width - newWidth) / 2;
			}
			else
			{
				newWidth = this.Width;
				newHeight = (int)(newWidth / aspectRatio);
				offsetY = (this.Height - newHeight) / 2;
			}

			pe.Graphics.Clear(Main.DefaultBackColor);

			// Draw the image with high quality
			pe.Graphics.DrawImage(this.Image, offsetX, offsetY, newWidth, newHeight);
		}
	}
}
