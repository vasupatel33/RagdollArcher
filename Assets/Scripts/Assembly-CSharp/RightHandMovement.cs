using UnityEngine;

public class RightHandMovement : MonoBehaviour
{
	[SerializeField]
	private int speed = 5;

	[SerializeField]
	private Rigidbody2D rb;

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private KeyCode mouseButton;

	public bool isRotating;

	public static bool isAbleToClick;

	private float rotationModifier;

	private float rotationZ;

	private Vector3 playerPos;

	private float targetRotation = 50f;

	private float rotationSpeed = 30f;

	private void Start()
	{
		rb = base.transform.GetComponent<Rigidbody2D>();
		cam = Camera.main;
	}

	private void FixedUpdate()
	{
		Debug.Log(isAbleToClick);
		if (isAbleToClick)
		{
			return;
		}
		playerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		playerPos.z = 0f;
		Vector3 vector = playerPos - base.transform.position;
		float num = Mathf.Atan2(vector.y, 0f - vector.x) * 57.29578f * 0.5f;
		Quaternion b = Quaternion.Euler(0f, 0f, 0f - num);
		if (Input.GetMouseButton(0))
		{
			Quaternion a = Quaternion.Euler(0f, 0f, rb.rotation);
			rb.MoveRotation(Quaternion.Lerp(a, b, speed));
		}
		if (isRotating)
		{
			RotateBetweenLimits();
			if (Input.GetMouseButtonDown(0))
			{
				RotateTo(b.eulerAngles.z);
			}
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			PlayerController.instance.ArrowLaunch(playerPos);
		}
	}

	private void RotateTo(float targetRotation)
	{
		this.targetRotation = targetRotation;
		isRotating = true;
	}

	private void RotateBetweenLimits()
	{
		float num = Mathf.Sign(targetRotation);
		float angle = Mathf.Clamp(rb.rotation + num * rotationSpeed * Time.fixedDeltaTime, 10f, 170f);
		rb.MoveRotation(angle);
		if (Mathf.Approximately(rb.rotation, 100f) || Mathf.Approximately(rb.rotation, 170f))
		{
			targetRotation *= -1f;
		}
	}
}
