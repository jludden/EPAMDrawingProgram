using System;

namespace EPAMDrawingProgram
{
	class DrawingProgramConsole
    {
		Canvas canvas;

		public DrawingProgramConsole()
		{
			Console.WriteLine("Welcome to the Drawing Program");
			Console.WriteLine("Type ? or help for a list of commands");
			ReadNext();
		}

		void ReadNext()
		{
			Console.Write("enter command: ");
			String input = Console.ReadLine();
			if (string.IsNullOrEmpty(input)) quit();
			else switch (Char.ToLower(input[0]))
				{
					case 'c':
						create(input);
						break;
					case 'l':
						drawLine(input);
						break;
					case 'r':
						drawRectangle(input);
						break;
					case 'b':
						bucketFill(input);
						break;
					case 'q':
						quit();
						break;
					case '?':
					case 'h':
						showHelp();
						break;
					default:
						showUnknownCommandError();
						break;
				}
		}

		void create(String input)
		{
			if (parseInts(input, 2, out int[] vals))
			{
				canvas = new Canvas(vals[0], vals[1]);
				canvas.Render();
			}
			ReadNext();
		}

		void drawLine(String input)
		{
			if (canvas == null)
			{
				Console.WriteLine("You must create a canvas first");
			}
			else if (parseInts(input, 4, out int[] vals))
			{
				canvas.DrawLine(vals[0], vals[1], vals[2], vals[3]);
				canvas.Render();
			}
			ReadNext();
		}

		void drawRectangle(String input)
		{
			if (canvas == null)
			{
				Console.WriteLine("You must create a canvas first");
			}
			else if (parseInts(input, 4, out int[] vals))
			{
				canvas.DrawRectangle(vals[0], vals[1], vals[2], vals[3]);
				canvas.Render();
			}
			ReadNext();
		}

		void bucketFill(String input)
		{
			if (canvas == null)
			{
				Console.WriteLine("You must create a canvas first");
			}
			else 
			{
				try
				{
					String[] inputs = input.Split(' ');
					int x = int.Parse(inputs[1]);
					int y = int.Parse(inputs[2]);
					char c = char.Parse(inputs[3]);

					canvas.BucketFill(x, y, c);
					canvas.Render();
				}
				catch
				{
					showInputFormatError();
				}
			}
			ReadNext();
		}

		// helper function for parsing integers from an input string
		bool parseInts(String line, int count, out int[] vals)
		{
			int i = 0;
			bool error = false;
			String[] inputs = line.Split(' '); //skip the first (non-int) value
			vals = new int[inputs.Length - 1];

			try
			{
				while (i < inputs.Length - 1)
				{
					vals[i] = int.Parse(inputs[++i]);
				}
			}
			catch
			{
				error = true;
			}

			if (i != count || error)
			{
				showInputFormatError();
				return false;
			}
			return true;
		}

		void quit()
		{
			Console.WriteLine("Thanks for using the Drawing Program. Goodbye");
		}

		void showInputFormatError()
		{
			Console.WriteLine("Command formatting error. Type help or ? to get a list of supported commands");
		}

		void showUnknownCommandError()
		{
			Console.WriteLine("Unknown command. Type help or ? to get a list of supported commands");
			ReadNext();
		}

		void showHelp()
		{
			Console.WriteLine("Drawing Program Help:");
			Console.WriteLine("C w h - Create a new canvas with w columns and h rows");
			Console.WriteLine("L x1 y1 x2 y2 - Create a new line from (x1,y1) to (x2,y2)");
			Console.WriteLine("R x1 y1 x2 y2 - create a new rectangle, whose upper left corner is (x1,y1) and lower right corner is (x2, y2)");
			Console.WriteLine("B x y c - fill the entire area connected to (x,y) with char c");
			Console.WriteLine("Q - quit the program");
			ReadNext();
		}
	}
}
