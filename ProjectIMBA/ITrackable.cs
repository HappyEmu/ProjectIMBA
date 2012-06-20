using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProjectIMBA
{
	public interface ITrackable
	{
		Vector2 Position { get; }
		Vector2 Acceleration { get; }
	}
}
