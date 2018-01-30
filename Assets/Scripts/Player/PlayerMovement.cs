using UnityEngine;
using CnControls;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidBody;
	int floorMask;
	float camRayLength = 100f;
	float rotAngle = 0f;

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidBody = GetComponent <Rigidbody> ();
	}

	void FixedUpdate()
	{
		float h = CnControls.CnInputManager.GetAxisRaw ("Horizontal");
		float v = CnControls.CnInputManager.GetAxisRaw ("Vertical");
		float fx = CnControls.CnInputManager.GetAxisRaw ("Submit");
		print ("PlayerMovement: " + h + ", " + v + ", " + fx);

		Move (h, v);
		Turning (fx);
		Animating (h, v); 
	}

	void Move(float h, float v)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidBody.MovePosition (transform.position + movement);
	}

	void Turning(float fx) 
	{
#if UNITY_ANDROID
		if (fx >  0.50f) { rotAngle += 2; }
		if (fx >  0.95f) { rotAngle += 4; }
		if (fx < -0.50f) { rotAngle -= 2; }
		if (fx < -0.95f) { rotAngle -= 4; }
		Vector3 eulerAngleVelocity = new Vector3 (0, rotAngle, 0);
		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity);
		playerRigidBody.MoveRotation(deltaRotation);
#else
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidBody.MoveRotation(newRotation);
		}
#endif
	}

	void Animating(float h, float v)
	{
		bool walking = ((h != 0f) || (v != 0f));
		anim.SetBool ("IsWalking", walking);
	}

}
