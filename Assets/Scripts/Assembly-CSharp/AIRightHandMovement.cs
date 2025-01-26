using System.Collections;
using UnityEngine;

public class AIRightHandMovement : MonoBehaviour
{
	[SerializeField]
	private int speed;

	[SerializeField]
	private Rigidbody2D rb;

	[SerializeField]
	private float rotationDuration = 1f;

	[SerializeField]
	private float rotationInterval;

	private float startRotation;

	private float targetRotation;

	private void Start()
	{
		rb = base.transform.GetComponent<Rigidbody2D>();
		StartCoroutine(RotateBetweenAngles());
	}

	private IEnumerator RotateBetweenAngles()
	{
		while (true)
		{
			startRotation = base.transform.eulerAngles.z;
			targetRotation = Random.Range(80f, 250f);
			float elapsedTime = 0f;
			while (elapsedTime < rotationDuration)
			{
				float angle = Mathf.Lerp(startRotation, targetRotation, elapsedTime / rotationDuration);
				rb.MoveRotation(angle);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			rb.MoveRotation(targetRotation);
			yield return new WaitForSeconds(rotationInterval);
			Vector3 launchDirection = -(PlayerController.instance.transform.position - base.transform.position).normalized;
			LaunchArrow(launchDirection);
		}
	}

	private void LaunchArrow(Vector3 launchDirection)
	{
		AIPlayerController.Instance.ArrowLaunch(launchDirection);
	}
}
