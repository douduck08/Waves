using UnityEngine;

namespace TeamSignal.Utilities.ObjectPools
{
	public class EndToDestroy : MonoBehaviour
	{
		void OnDisable()
		{
			Destroy(gameObject);
		}
	}
}
