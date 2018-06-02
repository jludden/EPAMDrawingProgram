using System;
using System.Collections.Generic;
using System.Text;

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

		//Indexer for easy access
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

		/// <summary>
		/// Draws the Canvas to the console
		/// Writes a full line of hyphens above and below the sheet
		/// And a pipe+whitespace along the sides
		/// The cell contents are padded or trimmed to be exactly 3 characters each
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
			//horizontal hyphens must be at least 3x w
			for (int i = 0; i < width + 4; i++)
			{
				Console.Write("-");
			}
			Console.WriteLine();
		}

		private void WriteLine(int y)
		{
			Console.Write("| ");
			for (int x = 0; x < width; x++) //must write at least 3 chars per iter
			{
				Console.Write(data[x, y] == null ? ' ' : data[x, y]);
			}
			Console.Write(" |");
			Console.WriteLine();
		}
	}
}
