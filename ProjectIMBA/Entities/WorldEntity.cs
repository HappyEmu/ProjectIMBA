using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProjectIMBA
{
	public abstract class WorldEntity
	{
		public Polygon Shape
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		public Vector2 Position
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		public Vector2 RotationOrigin
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		public float Rotation
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		public float Scale
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		public abstract EntityType Type
		{
			get;
		}

		public Polygon Polygon
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}
	}

	public enum EntityType
	{
		Dynamic,
		Static
	};
}
