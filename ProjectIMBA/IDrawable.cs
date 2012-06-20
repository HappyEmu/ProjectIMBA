using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectIMBA
{
	public interface IDrawable
	{
		Vector2 Position
		{
			get;
			set;
		}

		float Scale
		{
			get;
			set;
		}

		float Rotation
		{
			get;
			set;
		}

		Vector2 RotationOrigin
		{
			get;
			set;
		}

		Texture2D Texture
		{
			get;
			set;
		}

		int Layer
		{
			get;
			set;
		}
	
		void Draw();

		void LoadContent();
	}
}
