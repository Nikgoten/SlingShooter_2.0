using UnityEngine;
using System.Collections;

public class CollisionLimit : MonoBehaviour {

	// Use this for initialization
	void onTriggerEnter(Collider other) {
	
//		if (other.gameObject.tag == "Projectile") {
			Destroy (other.gameObject);
//		FollowCamera.S.poi = null;
//		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
