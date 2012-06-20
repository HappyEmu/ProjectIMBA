using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProjectIMBA
{
	public class DynamicEntity : WorldEntity
	{
		public Vector2 Velocity
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		public float Mass
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		public Vector2 Acceleration
		{
			get
			{
				throw new System.NotImplementedException();
			}
			set
			{
			}
		}

		public override EntityType Type
		{
			get
			{
				return EntityType.Dynamic;
			}
		}

		/// <summary>
		/// Applies a force on the center of mass of the object
		/// </summary>
		/// <param name="force">The force to be applied on the object</param>
		public void ApplyForce(Vector2 force)
		{
			this.Acceleration = force / this.Mass;
		}

		/// <summary>
		/// Applies a force on the specified contact point of the object
		/// </summary>
		/// <param name="force"></param>
		/// <param name="contactPoint"></param>
		public void ApplyForce(Vector2 force, Vector2 contactPoint)
		{
			// to be calculated...
		}
	}
}
