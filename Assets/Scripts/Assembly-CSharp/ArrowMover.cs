using UnityEngine;

public class ArrowMover : MonoBehaviour
{
	[SerializeField]
	private int speed;

	[SerializeField]
	private Rigidbody2D rb;

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private KeyCode mouseButton;

	public bool isRotating;

	private Vector3 playerPos;

	private void Start()
	{
		rb = base.transform.GetComponent<Rigidbody2D>();
		cam = Camera.main;
	}

	private void FixedUpdate()
	{
		Input.GetMouseButtonDown(0);
		if (Input.GetMouseButtonUp(0))
		{
			PlayerController.instance.ArrowLaunch(playerPos);
		}
	}

	private void LateUpdate()
	{
		Vector3 vector = cam.ScreenToWorldPoint(Input.mousePosition);
		vector.z = 0f;
		Vector3 vector2 = vector - base.transform.position;
		float b = Mathf.Atan2(vector2.x, 0f - vector2.y);
		if (Input.GetKey(mouseButton))
		{
			rb.MoveRotation(Mathf.LerpAngle(rb.rotation, b, (float)speed * Time.deltaTime));
		}
	}
}
