using UnityEngine;
using System.Collections;

public class CheckIfKlotzLeft : MonoBehaviour 
{
	public Collider connectedKlotz;

	private void OnTriggerExit(Collider c)
	{
		if (c == connectedKlotz) 
		{

			GameController.KlotzDestroyed();
			Destroy(this);
		}
	}
}
