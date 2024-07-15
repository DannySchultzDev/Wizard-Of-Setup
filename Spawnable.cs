using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wizard_Of_Set_Up
{
	public partial class Spawnable : Form
	{
		public static List<Spawnable> Spawnables = new List<Spawnable>();

		public bool isResizable;
		public bool isDataGridView;
		public Tile tile;


		public Spawnable(Tile tile, bool isResizable = false, bool isDatagridView = false)
		{
			this.tile = tile;

			this.isResizable = isResizable;
			this.isDataGridView = isDatagridView;
			InitializeComponent();
		}

		private void Spawnable_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.FormOwnerClosing)
			{
				e.Cancel = true;
				return;
			}
			if (Spawnables.Contains(this))
			{
				if (e.CloseReason == CloseReason.UserClosing)
				{
					if (tile == Tile.WIZZO)
					{
						Spawnables.Remove(this);
						Main.instance.SpawnWizzo(sender, e);
					} else
					{
						if (!isResizable && !isDataGridView)
						{
							try
							{
								(int x, int y) targetPos = ((Location.X - Main.instance.Location.X + 20) / 48, (Location.Y - Main.instance.Location.Y + 24) / 48);
								if (Main.instance.currLevel.tiles[targetPos.x, targetPos.y] != Tile.NONE ||
									targetPos == Main.instance.currLevel.wizzoPosition)
								{
									throw new Exception();
								}
								Main.instance.currLevel.DrawTile(targetPos.x, targetPos.y, tile);
								Spawnables.Remove(this);
								Main.instance.DrawTiles();
							}
							catch
							{
								WizzoDialogue error = new WizzoDialogue("I couldn't spawn the object there.");
								error.ShowDialog();
								e.Cancel = true;
							}
						}

					}
				} else
				{
					Spawnables.Remove(this);
				}
			}
		}

		private void Spawnable_Load(object sender, EventArgs e)
		{
			Spawnables.Add(this);

			switch (tile)
			{
				case Tile.WIZZO:
					SpawnableImage.Image = Properties.Resources.Wizard_Walk_1;
					Icon = Properties.Resources.Wizzo;
					break;
				case Tile.SPAWNED:
					SpawnableImage.Image = Properties.Resources.Spawned;
					break;
				case Tile.SPRING:
					SpawnableImage.Image = Properties.Resources.SpringNB;
					break;
			}
		}
	}
}