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
		#endregion		

		public List<Vector2> Edges { get { return this.edges; } }
		public Color Color { get; set; }

		public Polygon()
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
			for (int i = 0; i < this.edges.Count; i++)
			{
				Vector2 v = this.edges[i];
				v = Vector2.Transform(v, transform);
				this.edges[i] = v;
			}
		}

		public void Transform(float rotation, float scale = 1.0f)
		{
			Matrix transform = Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(new Vector3(scale, scale, 1.0f));
			this.Transform(transform);
		}

		public void Rotate(float angle, Vector2? origin = null)
		{
			origin = origin ?? Vector2.Zero;

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
	}
}
