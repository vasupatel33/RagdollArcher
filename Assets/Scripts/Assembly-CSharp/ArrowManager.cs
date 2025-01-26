using UnityEngine;

public class ArrowManager : MonoBehaviour
{
	private Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		TrackMovement();
	}

	private void TrackMovement()
	{
		Vector2 velocity = rb.velocity;
		float angle = Mathf.Atan2(velocity.y, velocity.x) * 57.29578f;
		base.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
