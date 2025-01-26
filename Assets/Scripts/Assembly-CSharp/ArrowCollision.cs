using System.Collections;
using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
	private AIHealth aiHealth;

	private bool isAbleToCollide = true;

	private void OnEnable()
	{
		aiHealth = new AIHealth(1);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == 11)
		{
			Debug.Log("<color=red><b>Game OVer</b></color>");
			collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
			HighlightCollidedPart(collision.gameObject);
			base.transform.GetComponent<SpriteRenderer>().enabled = false;
			base.transform.GetComponent<BoxCollider2D>().enabled = false;
			aiHealth.DecreaseHealth(2f);
		}
		if (collision.gameObject.layer == 12)
		{
			Debug.Log("<color=red><b>Game OVer</b></color>");
			collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
			HighlightCollidedPart(collision.gameObject);
			base.transform.GetComponent<SpriteRenderer>().enabled = false;
			base.transform.GetComponent<BoxCollider2D>().enabled = false;
			GameManager.instance.ShowGameOverPanel();
		}
		if (collision.gameObject.layer == 6)
		{
			Debug.Log("<b>Body Part Collided</b>");
			HighlightCollidedPart(collision.gameObject);
			base.transform.GetComponent<SpriteRenderer>().enabled = false;
			base.transform.GetComponent<BoxCollider2D>().enabled = false;
			GameManager.instance.HealthDecrease();
		}
		if (collision.gameObject.layer == 8 && isAbleToCollide)
		{
			isAbleToCollide = false;
			StartCoroutine(WaitUntillCollide());
			AIPlayerController.Instance.DecreaseAiHealth();
			HighlightCollidedPart(collision.gameObject);
			base.transform.GetComponent<SpriteRenderer>().enabled = false;
			base.transform.GetComponent<BoxCollider2D>().enabled = false;
		}
		if (collision.gameObject.layer == 14)
		{
			Debug.Log("<color=red><b>COLLISION WITH PLATFORM</b></color>");
			Object.Destroy(base.transform.gameObject);
		}
	}

	private IEnumerator WaitUntillCollide()
	{
		yield return new WaitForSeconds(0.2f);
		isAbleToCollide = true;
	}

	public void HighlightCollidedPart(GameObject collision)
	{
		SpriteRenderer component = collision.gameObject.GetComponent<SpriteRenderer>();
		if (component != null)
		{
			component.color = Color.red;
			StartCoroutine(GameManager.instance.ResetBodyPart(component));
			return;
		}
		component = collision.gameObject.GetComponentInChildren<SpriteRenderer>();
		if (component != null)
		{
			component.color = Color.red;
			StartCoroutine(GameManager.instance.ResetBodyPart(component));
			return;
		}
		component = collision.gameObject.GetComponentInParent<SpriteRenderer>();
		if (component != null)
		{
			component.color = Color.red;
			StartCoroutine(GameManager.instance.ResetBodyPart(component));
		}
	}
}
