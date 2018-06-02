using System;

namespace EPAMDrawingProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

			Canvas c = new Canvas(20, 6);
			c.Render();

			Console.ReadLine();
        }
    }
}