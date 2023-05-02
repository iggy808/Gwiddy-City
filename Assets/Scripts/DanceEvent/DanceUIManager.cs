using UnityEngine;

namespace DanceEvent
{
	// Need to set transform of worldspace UI canvas
	public class DanceUIManager : MonoBehaviour
	{
		[SerializeField]
		Transform UITransform; // set in insepector - Transform of canvas object, or gameObject.transform
		[SerializeField]
		Transform PlayerTransform;

		Transform TargetTransform;
		DanceRequestContext Context;


		public void SetUITransform(DanceRequestContext context)
		{
			// Only need to configure UI transform for worldspace UI, battle UI is ignored
			if (context.Environment == Environment.BattleDance)
			{
				Debug.Log("UI transform setup skipped for dance battle.");
				return;
			}
			Context = context;
			TargetTransform = Context.TargetObject.GetComponent<Transform>();
		}

		void Update()
		{
			if (Context.Environment != Environment.BattleDance)
			{
				UITransform.position = new Vector3(TargetTransform.position.x, TargetTransform.position.y+2f, TargetTransform.position.z);

				// To restrict axis rotation to y-axis: 
				// UITransform.LookAt(new Vector3(PlayerTransform.position.x, gameObject.transform.y, PlayerTransform.position.z))
				// or
				// UITransform.LookAt(new Vector3(PlayerTransform.position.x, UITransform.transform.y, PlayerTransform.position.z))
				UITransform.LookAt(PlayerTransform);
			}
		}
	}
}
