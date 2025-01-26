using UnityEngine;

public class HealthModifier : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == 7)
		{
			GameManager.instance.HealthIncrease();
			collision.gameObject.GetComponent<Rigidbody2D>();
			Object.Destroy(base.transform.gameObject);
			Object.Destroy(collision.gameObject);
		}
		if (collision.gameObject.tag == "platform")
		{
			GameManager.instance.ThrowHealthObject();
		}
	}
}
