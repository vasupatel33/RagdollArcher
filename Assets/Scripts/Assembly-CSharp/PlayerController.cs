using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static PlayerController instance;

	[SerializeField]
	private GameObject Arrow;

	[SerializeField]
	private GameObject LeftLowerHand;

	[SerializeField]
	private GameObject originalArrow;

	[SerializeField]
	private float arrowSpeed;

	[SerializeField]
	private SpriteRenderer[] allRenderers;

	[SerializeField]
	private GameObject[] objectsToExclude;

	private bool isSpawn = true;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		SpriteRenderer[] componentsInChildren = GetComponentsInChildren<SpriteRenderer>();
		allRenderers = ExcludeObjects(componentsInChildren);
		InvokeRepeating("SetRenderersColor", 4f, 5f);
	}

	private SpriteRenderer[] ExcludeObjects(SpriteRenderer[] renderers)
	{
		GameObject[] array = objectsToExclude;
		foreach (GameObject obj in array)
		{
			renderers = Array.FindAll(renderers, (SpriteRenderer r) => r.gameObject != obj);
		}
		return renderers;
	}

	private void SetRenderersColor()
	{
		SpriteRenderer[] array = allRenderers;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].color = Color.white;
		}
	}

	public void ArrowLaunch(Vector3 pos)
	{
		if (isSpawn)
		{
			GameObject obj = UnityEngine.Object.Instantiate(Arrow, originalArrow.transform.position, originalArrow.transform.rotation, base.transform);
			obj.GetComponent<Rigidbody2D>().velocity = originalArrow.transform.right * arrowSpeed;
			isSpawn = false;
			StartCoroutine(WaitUntillSpawn());
			UnityEngine.Object.Destroy(obj, 5f);
		}
	}

	private IEnumerator WaitUntillSpawn()
	{
		yield return new WaitForSeconds(0.5f);
		isSpawn = true;
	}
}
