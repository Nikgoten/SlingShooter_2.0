using UnityEngine;
using System.Collections;

public class CollisionKlotz : MonoBehaviour {

	// Use this for initialization
 void Start () {


	}

 void Update () {

		if (transform) {
			
			GameController.KlotzDestroyed();
		}
	}
}