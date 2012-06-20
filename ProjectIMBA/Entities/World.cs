using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ProjectIMBA
{
	public class World
	{
		private readonly LinkedList<WorldEntity> entities;
		private readonly LinkedList<StaticEntity> staticEntities;
		private readonly LinkedList<DynamicEntity> dynamicEntities;

		public Vector2 Gravity
		{
			get;
			set;
		}

		public World(Vector2 gravity)
		{
			this.Gravity = gravity;

			this.entities = new LinkedList<WorldEntity>();
			this.staticEntities = new LinkedList<StaticEntity>();
			this.dynamicEntities = new LinkedList<DynamicEntity>();
		}

		public void Step(float dt)
		{
			//throw new System.NotImplementedException();
		}

		public void AddEntity()
		{
			throw new System.NotImplementedException();
		}
	}
}
