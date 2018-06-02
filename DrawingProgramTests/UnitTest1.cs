using System;
using EPAMDrawingProgram;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DrawingProgramTests
{
    [TestClass]
    public class DrawingProgramTests
    {
        [TestMethod]
        public void StandardCase()
        { //copy from instructions
			Canvas c = new Canvas(20, 4);
			c.DrawLine(1, 2, 6, 2);
			Assert.AreEqual('x', c[1, 2]);
			Assert.AreEqual('x', c[4, 2]);
			Assert.AreEqual('x', c[6, 2]);

			c.DrawLine(6, 3, 6, 4);
			Assert.AreEqual('x', c[6, 3]);
			Assert.AreEqual('x', c[6, 4]);

			c.DrawRectangle(14, 1, 18, 3);
			Assert.AreEqual('x', c[14, 1]);
			Assert.AreEqual('x', c[18, 1]);
			Assert.AreEqual('x', c[14, 3]);
			Assert.AreEqual('x', c[18, 3]);

			c.BucketFill(10, 3, 'o');
			Assert.AreEqual('o', c[1, 1]);
			Assert.AreEqual('o', c[10, 3]);
			Assert.AreEqual('o', c[10, 4]);

			//make sure the fill didn't overwrite:
			Assert.AreEqual('x', c[4, 2]);
			Assert.AreEqual('x', c[6, 4]);
			Assert.AreEqual('x', c[18, 3]);
			Assert.AreEqual(null, c[2, 3]);
			Assert.AreEqual(null, c[15, 2]);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestInvalidConstructorParams()
		{
			Canvas c = new Canvas(-20, 0);
		}

		[TestMethod]
		public void TestBounds()
		{
			Canvas c = new Canvas(20, 4);
			c.DrawLine(-1, 2, 2, 2);
			c.DrawLine(1, -2, 2, 2);
			c.DrawLine(-1, -2, 2, 2);

			Assert.AreEqual(null, c[-1, -2]);
			Assert.AreEqual(null, c[-1, 2]);
			Assert.AreEqual(null, c[1, -2]);
			Assert.AreEqual(null, c[2, 22]);

			c.DrawRectangle(-1, -4, 1, 3);
			Assert.AreEqual(null, c[-1, -4]);
			Assert.AreEqual(null, c[1, 3]);

			c.BucketFill(0, -1, 'A');
			Assert.AreEqual(null, c[0, -1]);
		}
	}
}
