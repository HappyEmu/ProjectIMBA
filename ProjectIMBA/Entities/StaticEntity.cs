using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectIMBA
{
	public class StaticEntity : WorldEntity
	{
		public override EntityType Type
		{
			get { return EntityType.Static; }
		}
	}
}
