using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizard_Of_Set_Up
{
	public class Level
	{
		public Tile[,] tiles;
		public List<Spawnable> spawnables = new List<Spawnable>();
		public (int x, int y) wizzoPosition = (0, 0);

		public Level(int boundsX, int boundsY)
		{
			tiles = new Tile[boundsX, boundsY];
			for (int i = 0; i < boundsX; ++i)
			{
				for (int j = 0; j < boundsY; ++j)
				{
					tiles[i, j] = Tile.NONE;
				}
			}
			DrawHLine(0, 0, boundsX);
			DrawHLine(0, boundsY - 1, boundsX);
			DrawVLine(0, 0, boundsY);
			DrawVLine(boundsX - 1, 0, boundsY);
		}

		public void DrawTile(int x, int y, Tile tile = Tile.GROUND)
		{
			if (x < 0 || x >= tiles.GetLength(0) || y < 0 || y >= tiles.GetLength(1))
			{
				throw new ArgumentException("tile out of bounds.");
			}
			tiles[x, y] = tile;
		}

		public void DrawHLine(int x, int y, int length, Tile tile = Tile.GROUND)
		{
			if (length < 0)
			{
				x += length;
				length *= -1;
			}
			for (int i = 0; i < length; ++i)
			{
				DrawTile(x + i, y, tile);
			}
		}

		public void DrawVLine(int x, int y, int length, Tile tile = Tile.GROUND)
		{
			if (length < 0)
			{
				y += length;
				length *= -1;
			}
			for (int i = 0; i < length; ++i)
			{
				DrawTile(x, y + i, tile);
			}
		}

		public void DrawRect(int x, int y, int width, int height, Tile tile = Tile.GROUND) 
		{
			if (width < 0)
			{
				x += width;
				width *= -1;
			}
			if (height < 0)
			{
				y += height;
				height *= -1;
			}

			for (int i = 0; i < width; ++i)
			{
				for (int j = 0; j < height; ++j)
				{
					DrawTile(x + i, y + j, tile);
				}
			}
		}

		public static Level BuildLevel1 ()
		{
			Level level1 = new Level(10, 5);
			level1.DrawHLine(0, 4, 10);
			level1.DrawHLine(5, 3, 5);
			level1.DrawTile(8, 3, Tile.GOAL);
			level1.wizzoPosition = (2, 3);
			return level1;
		}

		public static Level BuildLevel2 ()
		{
			Level level2 = new Level(12, 7);
			level2.wizzoPosition = (2, 5);
			level2.DrawHLine(4, 5, 8);
			level2.DrawHLine(5, 4, 7);
			level2.DrawRect(7, 2, 5, 2);
			level2.DrawTile(9, 2, Tile.GOAL);
			level2.spawnables.Add(new Spawnable(Tile.SPAWNED));
			return level2;
		}

		public static Level BuildLevel3 ()
		{
			Level level3 = new Level(13, 10);
			level3.wizzoPosition = (2, 7);
			level3.DrawTile(4, 9, Tile.SPIKE);
			level3.DrawHLine(7, 9, 2, Tile.SPIKE);
			level3.DrawHLine(0, 8, 4);
			level3.DrawHLine(5, 8, 2);
			level3.DrawRect(9, 5, 3, 4);
			level3.DrawTile(10, 5, Tile.GOAL);
			for (int i = 0; i < 3; ++i)
			{
				level3.spawnables.Add(new Spawnable(Tile.SPAWNED));
			}
			return level3;
		}

		public static Level BuildLevel4()
		{
			Level level4 = new Level(12, 10);
			level4.wizzoPosition = (2, 7);
			level4.DrawHLine(1, 8, 10);
			level4.DrawHLine(5, 7, 6);
			level4.DrawHLine(5, 6, 2);
			level4.DrawRect(8, 3, 3, 4);
			level4.DrawTile(4, 8, Tile.SPRING);
			level4.DrawTile(9, 3, Tile.GOAL);
			level4.spawnables.Add(new Spawnable(Tile.SPRING));
			return level4;
		}

		public static Level BuildLevel5()
		{
			Level level5 = new Level(9, 14);
			level5.wizzoPosition = (2, 2);
			level5.DrawHLine(1, 3, 6);
			level5.DrawHLine(4, 4, 3);
			level5.DrawTile(5, 4, Tile.NONE);
			level5.DrawHLine(4, 7, 4);
			level5.DrawRect(4, 8, 2, 3);
			level5.DrawTile(2, 12);
			level5.DrawTile(1, 12, Tile.SPIKE);
			level5.DrawTile(5, 6, Tile.SPRING);
			level5.DrawTile(6, 10, Tile.GOAL);
			level5.spawnables.Add(new Spawnable(Tile.SPAWNED));
			level5.spawnables.Add(new Spawnable(Tile.SPAWNED));
			level5.spawnables.Add(new Spawnable(Tile.SPRING));
			return level5;
		}
	}

	public enum Tile
	{
		NONE,
		GROUND,
		SPAWNED,
		SPRING,
		SPIKE,
		GOAL,
		WIZZO
	}
}
