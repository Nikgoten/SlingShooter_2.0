using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	// Fields set in the Unity Inspector pane
	public GameObject prefabProjectile;
	public GameObject prefabArrow;
	public float velocityMult = 4f;
	
	// Fields set dynamically
	private GameObject launchPoint;
	private Vector3 launchPos;
	private GameObject projectile;
	private GameObject arrow;
	private bool aimingMode;
	
	void Awake(){
		//print ("Awake()");
		Transform launchPointTrans = transform.FindChild("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive(false);
		launchPos = launchPointTrans.position;
	}
	
	void OnMouseEnter() {
		//print ("Enter");
		launchPoint.SetActive(true);
	}
	
	void OnMouseExit() {
		//print ("Exit");
		if(!aimingMode) 
			launchPoint.SetActive(false);
	}

	void OnMouseDown(){
		//print ("Down");

		// Player pressed mouse while over Slingshot
		aimingMode = true;

		// Instantiate a projectile
		projectile = Instantiate(prefabProjectile) as GameObject;
		arrow = Instantiate(prefabArrow) as GameObject;

		// Start it at launch position
		projectile.transform.position = launchPos;
		arrow.transform.position = launchPos;

		//Give the projectile a launchPos transform
		//projectile.GetComponent<ProjectileRotation> ().launchPos = launchPos;


		// Set it to kinematic for now
		projectile.GetComponent<Rigidbody>().isKinematic = true;

	}

	void Update() {
		// If the Slingshot is not in aiming mode, don't run this code
		if(!aimingMode) return;

		// Get the current mouse position in 2D screen coordinates
		Vector3 mousePos = Input.mousePosition;
		// Convert the mouse position to 3D world coordinates
		mousePos.z = - Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos);

		// Find the delta from launch position to 3D mouse position
		Vector3 mouseDelta = mousePos3D - launchPos;
		// Limit mouseDelta to the radius of the Slingshot SphereCollider
		float maxMagnitude = GetComponent<SphereCollider>().radius;
		mouseDelta = Vector3.ClampMagnitude(mouseDelta, maxMagnitude);

		// Now move the projectile to this new position
		projectile.transform.position = launchPos + mouseDelta;
		arrow.transform.position = projectile.transform.position;

		arrow.transform.rotation = Quaternion.LookRotation (-mouseDelta * velocityMult);
		arrow.transform.Rotate (0, -90, 0);
		//arrow.transform.rotation = Quaternion.LookRotation (newMouseDelta);
		//Debug.Log (arrow.transform.rotation.eulerAngles);

		if(Input.GetMouseButtonUp(0)) {
			// The mouse has been released
			aimingMode = false;
			Destroy (arrow);
			// Fire off the projectile with given velocity
			projectile.GetComponent<Rigidbody>().isKinematic = false;
			projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
			Debug.Log("Velocity: " + -mouseDelta * velocityMult);

			// Set the Followcam's target to our projectile
			FollowCamera.S.poi = projectile;

			// Set the reference to the projectile to null as early as possible
			projectile = null;
			arrow = null;

			GameController.ShotFired();
		}
		
	}

	private float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}
