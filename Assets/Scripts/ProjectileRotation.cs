using UnityEngine;
using System.Collections;

public class ProjectileRotation : MonoBehaviour 
{
	[HideInInspector]
	public Vector3 launchPos;

	private void Update () 
	{

		transform.LookAt (launchPos);
	}
}
