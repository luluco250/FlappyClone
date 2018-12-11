using UnityEngine;

using static UnityEngine.Mathf;
using static UnityEngine.Time;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class BirdScript : MonoBehaviour {
	[Header("Gameplay Parameters")]

	[Tooltip("Vertical velocity achieved when flapping.")]
	public float flapVelocity = 1f;

	[Header("Animation Parameters")]

	[Tooltip("Start angle when flapping.")]
	public float flapAngle = 45f;

	[Tooltip("How many degrees per second to rotate when flapping.")]
	public float flapRotationSpeed = 90f;

	[Tooltip("End angle when falling.")]
	public float fallAngle = -60f;

	[Tooltip("How many degrees per second to rotate when falling.")]
	public float fallRotationSpeed = 90f;

	[Tooltip("How much time to wait before causing the bird to rotate after a flap.")]
	public float fallDelay = 1.0f;

	Rigidbody2D physics;
	Animator animator;

	int animTrigger_OnFlap;

	float rotation;
	float lastFlapTime = -1;

	void Awake() {
		physics = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		animTrigger_OnFlap = Animator.StringToHash("OnFlap");
	}

	void Update() {
		if (Input.GetButtonDown("Flap")) {
			lastFlapTime = time;
			animator.SetTrigger(animTrigger_OnFlap);
			physics.velocity = new Vector2(0f, flapVelocity);
		}

		if (time - lastFlapTime > fallDelay)
			rotation = Max(rotation - fallRotationSpeed * deltaTime, fallAngle);
		else
			rotation = Min(rotation + flapRotationSpeed * deltaTime, flapAngle);

		var rot = transform.rotation.eulerAngles;
		rot.z = rotation;
		transform.rotation = Quaternion.Euler(rot);
	}
}
