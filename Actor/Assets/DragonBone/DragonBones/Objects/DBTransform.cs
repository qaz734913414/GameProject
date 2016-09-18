// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
namespace DragonBones
{
	public class DBTransform
	{
		/**
		 * Position on the x axis.
		 */
		public float X;
		/**
		 * Position on the y axis.
		 */
		public float Y;
		/**
		 * Skew on the x axis.
		 */
		public float SkewX;
		/**
		 * skew on the y axis.
		 */
		public float SkewY;
		/**
		 * Scale on the x axis.
		 */
		public float ScaleX;
		/**
		 * Scale on the y axis.
		 */
		public float ScaleY;
		/**
		 * The rotation of that DBTransform instance.
		 */
		public float Rotation
		{
			get { return SkewX; }
			set { SkewX = SkewY = value; }
		}
	
		/**
		 * Creat a new DBTransform instance.
		 */
		public  DBTransform()
		{
			X = 0;
			Y = 0;
			SkewX = 0;
			SkewY = 0;
			ScaleX = 1;
			ScaleY = 1;
		}
		/**
		 * Copy all properties from this DBTransform instance to the passed DBTransform instance.
		 * @param	node
		 */
		public void Copy(DBTransform transform)
		{
			X = transform.X;
			Y = transform.Y;
			SkewX = transform.SkewX;
			SkewY = transform.SkewY;
			ScaleX = transform.ScaleX;
			ScaleY = transform.ScaleY;
		}

		/**
		 * Get a string representing all DBTransform property values.
		 * @return String All property values in a formatted string.
		 */

		public string toString()
		{
			string str = "X:" + X + " Y:" + Y + " SkewX:" + SkewX + " SkewY:" + SkewY + " ScaleX:" + ScaleX + " ScaleY:" + ScaleY;
			return str;
		}
	}
}

