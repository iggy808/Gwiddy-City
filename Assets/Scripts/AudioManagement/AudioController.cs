using UnityEngine;

namespace AudioManagement
{
	public class AudioController : MonoBehaviour
	{
		[SerializeField]
		AudioManager Manager;
		[SerializeField]
		AudioSource PlayerSource;
		
		void Awake()
		{
			PlayerSource.clip = Manager.WorldMusic;
			PlayerSource.loop = true;
			PlayerSource.Play();
		}
	}
}
