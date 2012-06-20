using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProjectIMBA
{
	public class Polygon
	{
		private readonly List<Vector2> edges;

		public List<Vector2> Edges { get { return this.edges; } }

		public Polygon()
		{
			this.edges = new List<Vector2>(20);
		}

		public void AddEdge(Vector2 edge)
		{
			this.edges.Add(edge);
		}


	}
}
