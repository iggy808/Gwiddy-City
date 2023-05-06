using System.Collections.Generic;
using UnityEngine;

namespace DanceEvent
{
	public class PoseOrdering
	{	
		public Pose Pose;
		public List<Limb> LimbRotationOrder;

		public PoseOrdering(Pose pose)
		{
			Pose = pose;
			switch(pose)
			{
				case Pose.Splits:
					LimbRotationOrder = new List<Limb>()
					{
						Limb.ArmRight,
						Limb.LegRight,
						Limb.LegLeft,
						Limb.ArmLeft
					};
					break;
				case Pose.Cool:
					LimbRotationOrder = new List<Limb>()
					{
						Limb.ArmLeft,
						Limb.LegRight,
						Limb.LegLeft,
						Limb.ArmRight
					};
					break;
				default:
					break;
			}
		}
	}
}
