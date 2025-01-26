using UnityEngine;

public class AIHealth
{
	private float health;

	public AIHealth(int health)
	{
		this.health = health;
	}

	public float GetHealthOfAI()
	{
		return health;
	}

	public void DecreaseHealth(float amount)
	{
		health -= amount;
		if (health <= 0f)
		{
			health = 0f;
			GameManager.instance.SpawnNewEnemy();
		}
		Debug.Log("Current Health = " + health);
	}
}
