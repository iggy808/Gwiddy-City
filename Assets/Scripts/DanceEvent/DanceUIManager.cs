using UnityEngine;

namespace DanceEvent
{
	// Need to set transform of worldspace UI canvas
	public class DanceUIManager : MonoBehaviour
	{
		[SerializeField]
		Transform UITransform; // set in insepector - Transform of canvas object
		[SerializeField]
		Transform PlayerTransform;

		Transform TargetTransform;
		DanceRequestContext Context;


		public void SetUITransform(DanceRequestContext context)
		{
			Context = context;
			// Only need to configure UI transform for worldspace UI, battle UI is ignored
			if (context.Environment == Environment.BattleDance)
			{
				Debug.Log("UI transform setup skipped for dance battle.");
				return;
			}

			TargetTransform = context.TargetObject.GetComponent<Transform>();
		}

		void Update()
		{
			if (Context.Environment != Environment.BattleDance)
			{
				//UITransform.position = new Vector3(TargetTransform.position.x, TargetTransform.position.y+1f, TargetTransform.position.z-0.5f);
				//UITransform.LookAt(PlayerTransform);
			}
		}
	}
}
