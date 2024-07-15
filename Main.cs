using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wizard_Of_Set_Up
{
	public partial class Main : Form
	{
		public static Main instance;

		public enum GameState
		{
			NO_RENDER = 0,
			CUTSCENE = 1,
			BUILD = 2,
			PLAY = 3,
		}

		private GameState gameState = GameState.NO_RENDER;
		private int storyIndex = 0;
		public Level currLevel = null;
		private List<TableLayoutPanel> storyPanels = new List<TableLayoutPanel>();
		private string[] titles = new string[]
		{
			"Setup Wizard",
			"License Agreement",
			"Choose Installation Location",
			"Ready To Install",
			"Installing",
			"Install Failed"
		};

		private Timer timer = new Timer();

		private Spawnable wizzo = null;
		private (int x, int y) wizzoPosition;
		private bool wizzoRight;
		private WizzoState wizzoState;

		public enum WizzoState
		{
			WALK,
			JUMP_1,
			JUMP_2,
			JUMP_F,
			FALL,
			WIN,
			STUCK
		}

		private int animationCycle = 0;

		public Main()
		{
			instance = this;
			InitializeComponent();
		}

		private void Main_Load(object sender, EventArgs e)
		{
			foreach (Control tableLayoutPanel in Controls)
			{
				if (tableLayoutPanel is TableLayoutPanel)
				{
					storyPanels.Add((TableLayoutPanel)tableLayoutPanel);
				}
			}

			Move += RepositionWizzo;
		}

		private void TryClose(object sender, FormClosingEventArgs e)
		{
			switch (gameState)
			{
				case GameState.NO_RENDER:
					if (storyIndex >= 53)
					{
						storyIndex = 54;
						gameState = GameState.BUILD;
						Text = "Wizard of Setup Level 1";
						Intro5TableLayout.Visible = false;
						currLevel = Level.BuildLevel1();
						BuildLevel();
						WizzoDialogue wizzoDialogue = new WizzoDialogue("In order for you to help me, you should probably know how to help.\r\n" +
							"While I can't conjure forth dialog prompts like OK or Cancel buttons anymore, " +
							"the X button is eternal, so we'll use that to our advantage.\r\n\r\n" +
							"To show you what I mean, in this level click my X button to drop me in. " +
							"From there I will try to make it to the goal.");
						wizzoDialogue.ShowDialog();
						e.Cancel = true;
					}
					break;
				case GameState.CUTSCENE:
					gameState = GameState.BUILD;
					Text = "Wizard of Setup Level " + (storyIndex - 53);
					WizzoDialogue levelIntroDialogue;
					switch (storyIndex)
					{
						case 55:
							currLevel = Level.BuildLevel2();
							levelIntroDialogue = new WizzoDialogue(
								"Now that I have gained some magic powers, I can start creating aditional windows.\r\n" +
								"Before clicking my X button, you can close my objects to spawn them into the world.\r\n" +
								"Try it out with this block!");
							break;
						case 56:
							currLevel = Level.BuildLevel3();
							levelIntroDialogue = new WizzoDialogue("I may have some magic, but that doesn't mean I can survive stepping on spikes!");
							break;
						case 57:
							currLevel = Level.BuildLevel4();
							levelIntroDialogue = new WizzoDialogue("Springs are bouncy and will be useful for gaining height.");
							break;
						case 58:
							currLevel = Level.BuildLevel5();
							levelIntroDialogue = new WizzoDialogue("This last level should give me enough magic power to install the game! We are in the home stretch! Good Luck!");
							break;
						default:
							levelIntroDialogue = new WizzoDialogue("LEVEL DOES NOT EXIST");
							break;
					}
					BuildLevel();
					levelIntroDialogue.ShowDialog();
					e.Cancel = true;
					break;
				case GameState.BUILD:
				case GameState.PLAY:
					gameState = GameState.BUILD;
					switch (storyIndex)
					{
						case 54:
							currLevel = Level.BuildLevel1();
							break;
						case 55:
							currLevel = Level.BuildLevel2();
							break;
						case 56:
							currLevel = Level.BuildLevel3();
							break;
						case 57:
							currLevel = Level.BuildLevel4();
							break;
						case 58:
							currLevel = Level.BuildLevel5();
							break;
					}
					BuildLevel();
					e.Cancel = true;
					break;
			}
		}

		private void BuildLevel()
		{
			Size = new Size(currLevel.tiles.GetLength(0) * 48, (currLevel.tiles.GetLength(1) * 48) + 24);
			DrawTiles();

			while (Spawnable.Spawnables.Count > 0) 
			{
				Spawnable spawnable = Spawnable.Spawnables.ElementAt(0);
				Spawnable.Spawnables.Remove(spawnable);
				spawnable.Close();
			}

			wizzo = new Spawnable(Tile.WIZZO);
			RepositionWizzo(null, EventArgs.Empty);
			wizzoPosition = currLevel.wizzoPosition;
			wizzoPosition.x *= 16;
			wizzoPosition.y *= 16;
			wizzoRight = true;
			wizzoState = WizzoState.WALK;
			wizzo.Move += RepositionWizzo;
			wizzo.Show();
			wizzo.Owner = this;

			foreach (Spawnable spawnable in currLevel.spawnables)
			{
				spawnable.Show();
				spawnable.Owner = this;
			}
		}

		public void DrawTiles()
		{
			Bitmap bitmap = new Bitmap(Size.Width, Size.Height - 24);
			for (int i = 0; i < currLevel.tiles.GetLength(0); ++i)
			{
				for (int j = 0; j < currLevel.tiles.GetLength(1); ++j)
				{
					Bitmap tileImage;
					switch (currLevel.tiles[i, j])
					{
						case Tile.NONE:
							tileImage = Properties.Resources.Sky;
							break;
						case Tile.GROUND:
							tileImage = Properties.Resources.Ground;
							break;
						case Tile.GOAL:
							tileImage = Properties.Resources.Goal;
							break;
						case Tile.SPAWNED:
							tileImage = Properties.Resources.Spawned;
							break;
						case Tile.SPIKE:
							tileImage = Properties.Resources.Spike;
							break;
						case Tile.SPRING:
							tileImage = Properties.Resources.Spring;
							break;
						default:
							tileImage = Properties.Resources.Sky;
							break;
					}
					for (int k = 0; k < 16; ++k)
					{
						for (int l = 0; l < 16; ++l)
						{
							Color pixelColor = tileImage.GetPixel(k, l);
							for (int m = 0; m < 3; ++m)
							{
								for (int n = 0; n < 3; ++n)
								{
									bitmap.SetPixel(
										(i * 16 * 3) + (k * 3) + m,
										(j * 16 * 3) + (l * 3) + n,
										pixelColor);
								}
							}
						}
					}
				}
			}
			ScenePictureBox.Image = bitmap;
		}

		private void UpdateWizzo()
		{
			Bitmap scene = new Bitmap(ScenePictureBox.Image);
			for (int i = 0; i < 16; ++i)
			{
				for (int j = 0; j < 16; ++j)
				{
					Tile underTile = currLevel.tiles[(wizzoPosition.x + i)/16, (wizzoPosition.y + j)/16];
					Bitmap tileImage;
					switch (underTile)
					{
						case Tile.NONE:
							tileImage = Properties.Resources.Sky;
							break;
						case Tile.GROUND:
							tileImage = Properties.Resources.Ground;
							break;
						default: 
							tileImage = Properties.Resources.Sky;
							break;
					}
					Color originalTileColor = tileImage.GetPixel((wizzoPosition.x + i) % 16, (wizzoPosition.y + j) % 16);
					for (int k = 0; k < 3; ++k)
					{
						for (int l = 0; l < 3; ++l)
						{
							scene.SetPixel((wizzoPosition.x * 3) + (i * 3) + k, (wizzoPosition.y * 3) + (j * 3) + l, originalTileColor);
						}
					}
				}
			}

			//Move
			switch (wizzoState)
			{
				case WizzoState.JUMP_1:
				case WizzoState.JUMP_2:
					--wizzoPosition.y;
					break;
				case WizzoState.WALK:
				case WizzoState.JUMP_F:
					wizzoPosition.x += wizzoRight ? 1 : -1;
					break;
				case WizzoState.FALL:
					++wizzoPosition.y; 
					break;
			}

			//NewMove
			if (wizzoPosition.x % 16 == 0 && 
				wizzoPosition.y % 16 == 0)
			{
				(int x, int y) pos = (wizzoPosition.x / 16, wizzoPosition.y / 16);
				switch (wizzoState)
				{
					case WizzoState.JUMP_1:
						if (currLevel.tiles[pos.x + 1, pos.y] == Tile.NONE ||
							currLevel.tiles[pos.x - 1, pos.y] == Tile.NONE)
						{
							wizzoState = WizzoState.JUMP_F;
						} else
						{
							wizzoState = WizzoState.FALL;
						}
						if (currLevel.tiles[pos.x + (wizzoRight ? 1 : -1), pos.y] != Tile.NONE)
						{
							wizzoRight = !wizzoRight;
						}
						break;
					case WizzoState.JUMP_2:
						if (currLevel.tiles[pos.x, pos.y - 1] == Tile.NONE)
						{
							wizzoState = WizzoState.JUMP_1;
						} else
						{
							wizzoState = WizzoState.FALL;
						}
						break;
					case WizzoState.FALL:
					case WizzoState.WALK:
					case WizzoState.JUMP_F:
						if (currLevel.tiles[pos.x, pos.y + 1] == Tile.NONE)
						{
							wizzoState = WizzoState.FALL;
							break;
						}
						else if (currLevel.tiles[pos.x, pos.y + 1] == Tile.SPIKE)
						{
							Close();
							return;
						} else if (currLevel.tiles[pos.x, pos.y + 1] == Tile.SPRING &&
							currLevel.tiles[pos.x, pos.y - 1] == Tile.NONE)
						{
							wizzoState = WizzoState.JUMP_2;
						}
						else if (currLevel.tiles[pos.x, pos.y + 1] == Tile.GOAL)
						{
							wizzoState = WizzoState.WIN;
							gameState = GameState.CUTSCENE;
							if (storyIndex == 54)
							{
								WizzoDialogue wizzoDialogue = new WizzoDialogue(
									"Now that I am at the goal I have gained a little Magic Power.\r\n" +
									"I'll show you how to use it in the next level.\r\n" +
									"After beating a level you can go to the next one by clicking the X button, " +
									"you can also click it at any point in a level to RESTART the level.");
								wizzoDialogue.ShowDialog();
							} else if  (storyIndex == 58)
							{
								WizzoDialogue wizzoDialogue = new WizzoDialogue(
									"YOU DID IT! I have enough magic power now!\r\n" +
									"It is time for you to experience the best DevDev Gamejam Game ever made!\r\n\r\n" +
									"I'm booting it up NOW!");
								wizzoDialogue.ShowDialog();
								MessageBox.Show("Something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
								gameState = GameState.NO_RENDER;
								storyIndex = -1;
								Close();
								return;
							}
							++storyIndex;
							timer.Stop();
						}
						else if (currLevel.tiles[pos.x + 1, pos.y] != Tile.NONE &&
							currLevel.tiles[pos.x - 1, pos.y] != Tile.NONE &&
							currLevel.tiles[pos.x + 1, pos.y - 1] != Tile.NONE &&
							currLevel.tiles[pos.x - 1, pos.y - 1] != Tile.NONE)
						{
							wizzoState = WizzoState.STUCK;
						}
						else if (currLevel.tiles[pos.x + (wizzoRight ? 1 : -1), pos.y] == Tile.NONE)
						{
							wizzoState = WizzoState.WALK;
						}
						else if (currLevel.tiles[pos.x + (wizzoRight ? 1 : -1), pos.y - 1] == Tile.NONE &&
							currLevel.tiles[pos.x, pos.y - 1] == Tile.NONE)
						{
							wizzoState = WizzoState.JUMP_1;
						} else if (currLevel.tiles[pos.x + (wizzoRight ? -1 : 1), pos.y] == Tile.NONE)
						{
							wizzoState = WizzoState.WALK;
							wizzoRight = !wizzoRight;
						} else
						{
							wizzoState = WizzoState.JUMP_1;
							wizzoRight = !wizzoRight;
						}
						break;
				}
			}

			//Redraw
			Bitmap wizzoImage;
			switch (wizzoState)
			{
				case WizzoState.STUCK:
					wizzoImage = Properties.Resources.Wizard_Pose_1;
					break;
				case WizzoState.WIN:
					wizzoImage = Properties.Resources.Wizard_Pose_2;
					break;
				default:
					if (animationCycle / 2 == 0)
					{
						wizzoImage = Properties.Resources.Wizard_Walk_1;
					} else
					{
						wizzoImage = Properties.Resources.Wizard_Walk_2;
					}
					break;
			}
			if (!wizzoRight)
			{
				wizzoImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
			}
			for (int i = 0; i < 16; ++i)
			{
				for (int j = 0; j < 16; ++j)
				{
					Color originalTileColor = wizzoImage.GetPixel((i) % 16, (j) % 16);
					if (originalTileColor.A == 0)
					{
						continue;
					}
					for (int k = 0; k < 3; ++k)
					{
						for (int l = 0; l < 3; ++l)
						{
							scene.SetPixel((wizzoPosition.x * 3) + (i * 3) + k, (wizzoPosition.y * 3) + (j * 3) + l, originalTileColor);
						}
					}
				}
			}

			ScenePictureBox.Image = scene;
		}

		private void RepositionWizzo(object sender, EventArgs e)
		{
			if (wizzo != null && currLevel != null)
			{
				wizzo.Location = new Point(
					Location.X + (currLevel.wizzoPosition.x * 52),
					Location.Y + (currLevel.wizzoPosition.y * 52) - 20);
			}
		}

		public void SpawnWizzo(object sender, EventArgs args)
		{
			if (gameState != GameState.BUILD)
			{
				return;
			}

			while (Spawnable.Spawnables.Count > 0)
			{
				Spawnable spawnable = Spawnable.Spawnables.ElementAt(0);
				Spawnable.Spawnables.Remove(spawnable);
				spawnable.Close();
			}

			wizzoPosition = (currLevel.wizzoPosition.x * 16, currLevel.wizzoPosition.y * 16);

			gameState = GameState.PLAY;
			timer.Interval = 30;
			timer.Start();
		}

		private void OnTick(object sender, EventArgs e)
		{
			switch (storyIndex)
			{
				case 4:
					storyIndex = 5;
					Intro5Label.Text = "Installing magic.exe";
					IntroProgressBar.Value = 5;
					timer.Interval = 1000;
					break;
				case 5:
					storyIndex = 6;
					Intro5Label.Text = "Enchanting your system";
					IntroProgressBar.Value = 25;
					timer.Interval = 1500;
					break;
				case 6:
					storyIndex = 7;
					Intro5Label.Text = "Casting... to int";
					IntroProgressBar.Value = 40;
					timer.Interval = 2000;
					break;
				case 7:
					storyIndex = 8;
					Intro5Label.Text = "Weaving spells into the software";
					IntroProgressBar.Value = 45;
					timer.Interval = 500;
					break;
				case 8:
					storyIndex = 9;
					Intro5Label.Text = "Conjuring from SQL database";
					IntroProgressBar.Value = 75;
					timer.Interval = 2200;
					break;
				case 9:
					storyIndex = 10;
					Intro5Label.Text = "Pondering orbs";
					IntroProgressBar.Value = 90;
					timer.Interval = 200;
					break;
				case 10:
					storyIndex = 11;
					Intro5Label.Text = "Finishing installation";
					IntroProgressBar.Value = 95;
					timer.Interval = 5000;
					break;
				case 11:
					storyIndex = 12;
					Text = "Error";
					Intro5Label.Text = "OH NO!";
					timer.Interval = 5;
					SystemSounds.Beep.Play();
					break;
				case 50:
					storyIndex = 51;
					Intro5Label.Text = "Hold On";
					timer.Interval = 1000;
					break;
				case 51:
					storyIndex = 52;
					Intro5Label.Text = "Hold On";
					timer.Interval = 1000;
					foreach (FakeAlarm alarm in FakeAlarm.alarms)
					{
						alarm.Close();
					}
					break;
				case 52:
					storyIndex = 53;
					Intro5Label.Text = "Looks like there were some... slight... issues.\n" +
						"But I can fix this! I will need your help though, if that's ok. " +
						"Due to those errors I can't offer any more standard prompts, " +
						"so for now go ahead and click the X button.";
					timer.Stop();
					break;

				default:
					if (storyIndex > 10 && storyIndex < 50)
					{
						++storyIndex;
						FakeAlarm alarm = new FakeAlarm();
						alarm.Show();
						IntroProgressBar.Value = IntroProgressBar.Value - 5 < 0 ? 0 : IntroProgressBar.Value - 5;
						timer.Interval = new Random().Next(5, 20);
					}
					break;
			}

			if (gameState == GameState.PLAY)
			{
				animationCycle = (animationCycle + 1) % 4;
				UpdateWizzo();
			}
		}

		public void UpdateStoryPanel()
		{
			if (storyIndex < 0)
			{
				storyIndex = 0;
			}
			Text = titles[storyIndex];
			for (int i = 0; i < storyPanels.Count; ++i)
			{
				storyPanels[i].Visible = i == storyIndex;
			}
		}

		public void NextStoryPanel(object sender, EventArgs args)
		{
			++storyIndex;
			UpdateStoryPanel();
			if (storyIndex == 4)
			{
				timer.Interval = 5000;
				timer.Tick += OnTick;
				timer.Start();
			}
		}

		public void PreviousStoryPanel(object sender, EventArgs args)
		{
			--storyIndex;
			UpdateStoryPanel();
		}

		private void CancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void AcceptLicenseCheckbox_CheckedChanged(object sender, EventArgs e)
		{ 
			Next2Button.Enabled = AcceptLicenseCheckbox.Checked;
		}

		private void LicenseLabel_Click(object sender, EventArgs e)
		{
			MessageBox.Show("IMPORTANT - READ CAREFULLY: This End User License Agreement " +
				"(\"Agreement\") is a legal contract between you (\"Licensee\") and Wah Infinite " +
				"(\"Licensor\") for the software product, which includes computer software and may " +
				"include associated media, printed materials, and online or electronic documentation " +
				"(\"Software\"). By installing, copying, or otherwise using the Software, you agree " +
				"to be bound by the terms of this Agreement. If you do not agree to the terms of " +
				"this Agreement, do not install or use the Software.\r\n\r\n" +
				"Grant of License\r\nWah Infinite hereby grants to Licensee a non-exclusive, " +
				"non-transferable, limited license to install and use the Software on any device " +
				"it works on as specified in the downloaded license, subject to the terms and " +
				"conditions set forth herein.\r\n\r\n" +
				"Restrictions\r\n" +
				"a. Licensee shall not, and shall not permit any third party to:\r\n" +
				"i. Copy, modify, or create derivative works of the Software, unless they also post " +
				"it on Discord so I can see the cool stuff they did with it;\r\n" +
				"ii. Distribute, transfer, sublicense, lease, lend, or rent the Software to any " +
				"third party, unless they pinkie promise to not do anything illegal with it;\r\n" +
				"iii. Reverse engineer, decompile, or disassemble the Software, " +
				"because I already posted the source code on Github;\r\n" +
				"iv. Use the Software for any purpose that is illegal or not authorized by this " +
				"Agreement.\r\n\r\n" +
				"Ownership\r\n" +
				"The Software is licensed, not sold. Licensor retains all right, title, and " +
				"interest in and to the Software, including all intellectual property rights " +
				"therein. Licensee acknowledges that no title to the intellectual property in " +
				"the Software is transferred to Licensee.\r\n\r\n" +
				"Term and Termination\r\n" +
				"This Agreement is effective until terminated. Licensee may terminate this " +
				"Agreement at any time by uninstalling and destroying all copies of the Software. " +
				"Licensor may terminate this Agreement immediately upon notice to Licensee if " +
				"Licensee breaches any term of this Agreement. Upon termination, " +
				"Licensee must uninstall and destroy all copies of the Software.\r\n\r\n" +
				"Limited Warranty\r\n" +
				"Licensor warrants that for a period of ninety (90) days from the date of delivery, " +
				"the Software will perform substantially in accordance with the accompanying " +
				"documentation unless it breaks. The entire liability of Licensor and Licensee's " +
				"exclusive remedy under this warranty will be, at Licensor's option, " +
				"either (a) return of the purchase price paid for the Software, which is $0 since " +
				"the game is free or (b) repair or replacement of the Software that does not meet " +
				"this limited warranty and that is returned to Licensor with a copy of Licensee's " +
				"receipt if I feel like it.\r\n\r\n" +
				"Disclaimer of Warranties\r\n" +
				"EXCEPT FOR THE LIMITED WARRANTY SET FORTH ABOVE, THE SOFTWARE IS PROVIDED " +
				"\"AS IS\" AND LICENSOR DISCLAIMS ALL OTHER WARRANTIES, WHETHER EXPRESS OR " +
				"IMPLIED, INCLUDING, BUT NOT LIMITED TO, IMPLIED WARRANTIES OF MERCHANTABILITY, " +
				"FITNESS FOR A PARTICULAR PURPOSE, TITLE, AND NON-INFRINGEMENT.\r\n\r\n" +
				"Limitation of Liability\r\n" +
				"TO THE MAXIMUM EXTENT PERMITTED BY APPLICABLE LAW, IN NO EVENT SHALL " +
				"LICENSOR BE LIABLE FOR ANY SPECIAL, INCIDENTAL, INDIRECT, OR CONSEQUENTIAL " +
				"DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF " +
				"BUSINESS PROFITS, BUSINESS INTERRUPTION, LOSS OF BUSINESS INFORMATION, " +
				"OR ANY OTHER PECUNIARY LOSS) ARISING OUT OF THE USE OF OR INABILITY TO USE " +
				"THE SOFTWARE, EVEN IF LICENSOR HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH " +
				"DAMAGES.\r\n\r\n" +
				"Indemnification\r\n" +
				"Licensee agrees to indemnify, defend, and hold harmless Licensor, its officers, " +
				"directors, employees, and agents from and against any claims, actions, suits, " +
				"or proceedings, as well as any losses, liabilities, damages, costs, and expenses " +
				"(including reasonable attorneys' fees) arising out of or related to Licensee's " +
				"use of the Software or violation of this Agreement.\r\n\r\n" +
				"Governing Law\r\n" +
				"This Agreement shall be governed by and construed in accordance with the laws " +
				"of The United States of America, without regard to its conflict of laws principles. " +
				"Any legal action or proceeding arising under this Agreement will be brought " +
				"exclusively in the federal courts located in The United States of America, " +
				"and Licensee hereby consents to the personal jurisdiction and venue therein." +
				"\r\n\r\n" +
				"Miscellaneous\r\n" +
				"a. This Agreement constitutes the entire agreement between the parties with " +
				"respect to the use of the Software and supersedes all prior or contemporaneous " +
				"understandings regarding such subject matter.\r\n" +
				"b. If any provision of this Agreement is found to be invalid or unenforceable, " +
				"I probably wont care.\r\n" +
				"c. No failure or delay by Licensor in exercising any right under this Agreement " +
				"will constitute a waiver of that right.\r\n\r\n" +
				"BY INSTALLING, COPYING, OR USING THE SOFTWARE, YOU ACKNOWLEDGE THAT YOU HAVE READ " +
				"THIS AGREEMENT, UNDERSTAND IT, AND AGREE TO BE BOUND BY ITS TERMS AND CONDITIONS, " +
				"unless you don't feel like it.", 
				"End User License Agreement (EULA)",
				MessageBoxButtons.OK);
		}

		private void folderTextBox_TextChanged(object sender, EventArgs e)
		{
			string folderPath = folderTextBox.Text;
			if (string.IsNullOrWhiteSpace(folderPath))
			{
				Next3Button.Enabled = false;
				return;
			}

			char[] invalidPathChars = Path.GetInvalidPathChars();
			foreach (char c in invalidPathChars)
			{
				if (folderPath.Contains(c))
				{
					Next3Button.Enabled = false;
					return;
				}
			}
			try
			{
				Next3Button.Enabled = Directory.Exists(folderPath);
			} catch
			{
				Next3Button.Enabled = false;
				return;
			}
		}

		private void folderButton_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
			{
				folderDialog.Description = "Select a folder";
				folderDialog.ShowNewFolderButton = true;

				DialogResult result = folderDialog.ShowDialog();
				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
				{
					folderTextBox.Text = folderDialog.SelectedPath;
				}
			}
		}
	}
}
