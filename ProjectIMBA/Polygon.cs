using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectIMBA
{
	public class Polygon
	{
		#region Fields
		private readonly List<Vector2> edges;

		private short[] indices;
		private VertexPositionColor[] vertices;

		private bool triangulated;
		private Polygon.Orientation orientation;
		#endregion		

		public List<Vector2> Edges { get { return this.edges; } }
		public Vector2 CenterOfMass { get; private set; }
		public float Area { get; private set; }
		public Color Color { get; set; }		

		public Polygon(Polygon.Orientation orientation = Polygon.Orientation.Clockwise)
		{
			this.edges = new List<Vector2>(20);
			this.triangulated = false;
		}

		public void AddEdge(float x, float y)
		{
			this.edges.Add(new Vector2(x, y));
		}

		public void AddEdge(Vector2 edge)
		{
			this.edges.Add(edge);
		}

		public void Transform(Matrix transform)
		{
			Vector2 v;
			for (int i = 0; i < this.edges.Count; i++)
			{
				v = this.edges[i];
				Vector2.Transform(ref v, ref transform, out v);
				this.edges[i] = v;
			}
		}

		public void Transform(float rotation, float scale = 1.0f)
		{
			Matrix transform = Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(new Vector3(scale, scale, 1.0f));
			this.Transform(transform);
		}

		/// <summary>
		/// Rotates the polygon around a specified point or around the polygon's center of mass
		/// </summary>
		/// <param name="angle">The angle to be rotated by</param>
		/// <param name="origin">The origin to be rotated around. If empty, rotation will be around the polygon's center of mass</param>
		public void Rotate(float angle, Vector2? origin = null)
		{
			origin = origin ?? this.CenterOfMass;

			Matrix transform = Matrix.CreateTranslation(new Vector3(-origin.Value, 0.0f)) *
				Matrix.CreateRotationZ(angle) *
				Matrix.CreateTranslation(new Vector3(origin.Value, 0.0f));

			this.Transform(transform);
		}

		public void Draw(GraphicsDevice device)
		{
			if (!this.triangulated)
				return;

			for (int i = 0; i < this.edges.Count; i++)
			{
				this.vertices[i].Color = this.Color;
				this.vertices[i].Position = new Vector3(this.edges[i], 0.0f);
			}

			device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, this.vertices, 0, this.vertices.Length, this.indices, 0, vertices.Length - 2);
		}

		/// <summary>
		/// Triangulate polygon for drawing
		/// </summary>
		public void Triangulate()
		{
			this.vertices = new VertexPositionColor[this.edges.Count];

			int[] idc = Triangulator.Triangulate(edges.ToArray());
			this.indices = new short[idc.Length];

			for (int i = 0; i < idc.Length; i++)
			{
				this.indices[i] = (short)idc[i];
			}

			this.triangulated = true;
		}

		/// <summary>
		/// Finish the polygon and calculate its area and center of mass
		/// </summary>
		public void Finish()
		{
			float signedArea = this.CalculateSignedArea();
			this.Area = (this.orientation == Orientation.CounterClockwise) ? signedArea : -signedArea;
			this.CalculateCenterOfMass(signedArea);
		}

		private float CalculateSignedArea()
		{
			int N = this.edges.Count;
			float area = 0f;

			for (int i = 0; i < N; i++)
			{
				int j = (i + 1) % N;
				area += edges[i].X * edges[j].Y;
				area -= edges[i].Y * edges[j].X;
			}

			area /= 2f;

			return area;
		}

		private void CalculateCenterOfMass(float signedArea)
		{
			float cx = 0, cy = 0;
			int N = this.edges.Count;
			float A = signedArea;
			Vector2 res = Vector2.Zero;

			float factor = 0;

			for (int i = 0; i < N; i++)
			{
				int j = (i + 1) % N;

				factor = (edges[i].X * edges[j].Y - edges[j].X * edges[i].Y);
				cx += (edges[i].X + edges[j].X) * factor;
				cy += (edges[i].Y + edges[j].Y) * factor;
			}

			A *= 6.0f;
			factor = 1 / A;
			cx *= factor;
			cy *= factor;
			res.X = cx;
			res.Y = cy;

			this.CenterOfMass = res;
		}

		public enum Orientation
		{
			Clockwise,
			CounterClockwise
		};
	}
}
