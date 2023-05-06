using UnityEngine;
using System.Collections.Generic;

namespace DanceEvent
{
	public class DanceRequestContext
	{
		// To be expanded as needed
		public Environment Environment;		
		public List<Pose> DesiredMoves;
		public List<PoseOrdering> DesiredPoseOrders => ConvertDesiredMovesToPoseOrders();
		public GameObject TargetObject;

		List<PoseOrdering> ConvertDesiredMovesToPoseOrders()
		{
			List<PoseOrdering> poseOrders = new List<PoseOrdering>();
			foreach (Pose pose in DesiredMoves)
			{
				poseOrders.Add(new PoseOrdering(pose));
			}
			return poseOrders;
		}
	}

}
