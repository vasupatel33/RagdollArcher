using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AIPlayerController : MonoBehaviour
{
	[SerializeField]
	private GameObject Arrow;

	[SerializeField]
	private GameObject originalArrow;

	[SerializeField]
	private GameObject AIHealthModifier;

	[SerializeField]
	private float arrowSpeed;

	public static AIPlayerController Instance;

	private bool isSpawn = true;

	private AIHealth aiHealth;

	private void Awake()
	{
		Instance = this;
	}

	private void OnEnable()
	{
		aiHealth = new AIHealth(1);
		AIHealthModifier.transform.DOScaleX(1f, 1f);
	}

	public void DecreaseAiHealth()
	{
		aiHealth.DecreaseHealth(0.3f);
		float healthOfAI = aiHealth.GetHealthOfAI();
		if (healthOfAI <= 0f)
		{
			AIHealthModifier.transform.DOScaleX(0f, 1f);
		}
		else
		{
			AIHealthModifier.transform.DOScaleX(healthOfAI, 1f);
		}
	}

	public void ArrowLaunch(Vector3 launchDirection)
	{
		if (isSpawn)
		{
			GameObject obj = Object.Instantiate(Arrow, originalArrow.transform.position, originalArrow.transform.rotation, base.transform);
			obj.GetComponent<Rigidbody2D>().velocity = originalArrow.transform.right * arrowSpeed;
			isSpawn = false;
			Debug.Log("Arrow launched");
			StartCoroutine(WaitUntillSpawn());
			Object.Destroy(obj, 5f);
		}
	}

	private IEnumerator WaitUntillSpawn()
	{
		yield return new WaitForSeconds(0.5f);
		isSpawn = true;
	}
}
