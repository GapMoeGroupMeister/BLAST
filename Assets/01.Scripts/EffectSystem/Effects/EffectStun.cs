using EffectSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{
	public class EffectStun : EffectState
	{
		public EffectStun(Agent agent, bool isResist) : base(agent, isResist)
		{
		}

		public override void Over()
		{
			base.Over();
		}

		public override void Start(int level = 1, float duration = 10, float percent = 1f)
		{
			base.Start(level, duration);
		}

		public override void Update()
		{
			base.Update();
		}

		public override void UpdateBySecond()
		{
			base.UpdateBySecond();
		}
	}
}