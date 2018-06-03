using System;

namespace EPAMDrawingProgram
{
	public class Canvas
    {
		char?[,] data;
		int width, height;

		public Canvas(int w, int h)
		{
			if (w < 1 || h < 1) throw new ArgumentOutOfRangeException();

			width = w;
			height = h;

			data = new char?[w + 1, h + 1];
		}

		//Indexer for easy testing
		public int? this[int x, int y]
		{
			get
			{
				if (!CheckBounds(ref x, ref y)) return null;
				return data[x, y];
			}
		}

		//checks that the cell is legal for the canvas
		//also decrements x & y to fit in a zero-index array
		private bool CheckBounds(ref int x, ref int y)
		{
			x--; y--;
			return (x >= 0 && y >= 0 && x < width && y < height) ? true : false;
		}

		public void DrawLine(int x1, int y1, int x2, int y2)
		{
			if (!CheckBounds(ref x1, ref y1) || !CheckBounds(ref x2, ref y2)) return;

			LineHelper(x1, y1, x2, y2);
		}

		//helper function to create a horizontal or vertical line
		private void LineHelper(int x1, int y1, int x2, int y2)
		{
			int minX = Math.Min(x1, x2);
			int maxX = Math.Max(x1, x2);
			int minY = Math.Min(y1, y2);
			int maxY = Math.Max(y1, y2);
			if (minX != maxX && minY != maxY) return; //only support horizontal or vertical lines

			for (int x = minX; x <= maxX; x++)
			{
				data[x, minY] = 'x';
			}
			for (int y = minY; y <= maxY; y++)
			{
				data[minX, y] = 'x';
			}
		}

		public void DrawRectangle(int x1, int y1, int x2, int y2)
		{
			if (!CheckBounds(ref x1, ref y1) || !CheckBounds(ref x2, ref y2)) return;

			LineHelper(x1, y1, x2, y1);
			LineHelper(x2, y1, x2, y2);
			LineHelper(x1, y2, x2, y2);
			LineHelper(x1, y1, x1, y2);
		}

		public void BucketFill(int x, int y, char c)
		{
			if (!CheckBounds(ref x, ref y)) return;

			data[x, y] = null; //reset the selected pixel
			BucketFillHelper(x, y, c);
		}

		//helper function to recursively fill in a continuous area
		private void BucketFillHelper(int x, int y, char c)
		{
			if (!(x >= 0 && y >= 0 && x < width && y < height)) return;
			if (data[x, y] == 'x' || data[x,y] == c) return;

			data[x, y] = c;
			BucketFillHelper(x - 1, y, c);
			BucketFillHelper(x + 1, y, c);
			BucketFillHelper(x, y - 1, c);
			BucketFillHelper(x, y + 1, c);
		}

		/// <summary>
		/// Draws the Canvas to the console
		/// Writes a full line of hyphens above and below the sheet
		/// And a pipe along the sides
		/// </summary>
		public void Render()
		{
			WriteFrame();
			for (int i = 0; i < height; i++)
			{
				WriteLine(i);
			}
			WriteFrame();
		}

		private void WriteFrame()
		{
			for (int i = 0; i < width + 2; i++)
			{
				Console.Write("-");
			}
			Console.WriteLine();
		}

		private void WriteLine(int y)
		{
			Console.Write("|");
			for (int x = 0; x < width; x++) 
			{
				Console.Write(data[x, y] ?? ' ');
			}
			Console.Write("|");
			Console.WriteLine();
		}
	}
}
