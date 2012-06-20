using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProjectIMBA
{
	public class Camera
	{
		public float Speed { get; set; }
		public float Inertia { get; set; }

		public Matrix Transform { get; private set; }

		public Vector2 Position { get; private set; }

		public Vector2 Velocity { get; private set; }
		public Vector2 Acceleration { get; private set; }

		public Rectangle View { get; private set; }
		public float Zoom { get; set; }

		public Vector2 Target { get; set; }
		private Vector2 oldTarget;

		private bool track;
		ITrackable toTrack;

		public Camera(Rectangle view)
		{
			this.View = view;
			this.Target = Vector2.Zero;
			this.track = false;
			toTrack = null;
		}

		public void MoveRight()
		{
			this.Target += new Vector2(Speed, 0);
		}

		public void MoveLeft()
		{
			this.Target += new Vector2(-Speed, 0);
		}

		public void MoveUp()
		{
			this.Target += new Vector2(0, -Speed);
		}

		public void MoveDown()
		{
			this.Target += new Vector2(0, Speed);
		}

		public void Move(Vector2 offset)
		{
			this.Velocity = Speed * offset;
		}

		public void Track(ITrackable obj)
		{
			this.track = true;
			this.toTrack = obj;
		}

		public void UnTrack()
		{
			this.track = false;
			this.toTrack = null;
		}

		public void Update(double dt)
		{
			if (track)
			{
				Vector2 targetOffset = toTrack.Position - oldTarget;				
				this.Velocity = targetOffset / Inertia;
			}

			//this.Velocity += this.Acceleration * (float)dt;
			
			this.Target += this.Velocity * (float)dt;

			this.Position = this.Target - new Vector2(this.View.Width / 2, this.View.Height / 2);
			this.View = new Rectangle((int)Position.X, (int)Position.Y, this.View.Width, this.View.Height);

			this.oldTarget = Target;

			this.Transform = Matrix.CreateTranslation(new Vector3(-this.Position, 0.0f)) * Matrix.CreateTranslation(new Vector3(-this.Target, 0.0f))
				* Matrix.CreateScale(new Vector3(Zoom, Zoom, 1.0f)) * Matrix.CreateTranslation(new Vector3(this.Target, 0.0f));
		}

		public bool isVisible(Rectangle r)
		{
			return r.Intersects(this.View);
		}
	}
}
