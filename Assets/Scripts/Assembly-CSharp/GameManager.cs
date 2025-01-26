using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private List<GameObject> AllEnemies;

	[SerializeField]
	private TextMeshProUGUI killedEnemyText;

	[SerializeField]
	private TextMeshProUGUI GameOverKilledText;

	public static GameManager instance;

	[SerializeField]
	private GameObject gameOverPanel;

	[SerializeField]
	private GameObject gameWinPanel;

	[SerializeField]
	private GameObject currentEnemy;

	[SerializeField]
	private TextMeshProUGUI HealthText;

	public GameObject[] fruitPrefabs;

	private bool isAbleToThrow = true;

	private int killedEnemy;

	private void Start()
	{
		instance = this;
		PlayerDataPrefs.Health = 5;
		string text = "Health = " + 5;
		HealthText.text = text;
		InvokeRepeating("ThrowHealthObject", 1f, 5f);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ThrowHealthObject();
		}
	}

	public void SpawnNewEnemy()
	{
		GameObject gameObject;
		do
		{
			gameObject = AllEnemies[Random.Range(0, AllEnemies.Count)];
		}
		while (gameObject == currentEnemy);
		GameObject gameObject2 = Object.Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
		gameObject2.SetActive(value: true);
		Object.Destroy(currentEnemy);
		currentEnemy = gameObject2;
		IncreaseKilledEnemyCount();
	}

	public void ThrowHealthObject()
	{
		if (isAbleToThrow)
		{
			isAbleToThrow = false;
			StartCoroutine(WaitUntillOn());
			GameObject original = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
			Camera main = Camera.main;
			float num = main.orthographicSize * 2f * main.aspect;
			Vector3 vector = main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
			float num2 = num * 0.7f;
			float num3 = vector.x + num * 0.25f;
			Vector3 position = new Vector3(Random.Range(num3, num3 + num2), vector.y, 0f);
			GameObject obj = Object.Instantiate(original, position, Quaternion.identity);
			Rigidbody2D component = obj.GetComponent<Rigidbody2D>();
			if (component != null)
			{
				Vector2 up = Vector2.up;
				component.AddForce(up * Random.Range(10, 14), ForceMode2D.Impulse);
			}
			Object.Destroy(obj, 10f);
		}
	}

	private IEnumerator WaitUntillOn()
	{
		yield return new WaitForSeconds(1f);
		isAbleToThrow = true;
	}

	public void HealthIncrease()
	{
		int health = PlayerDataPrefs.Health;
		health += 3;
		Debug.Log("After INCREASE HEALTH = " + health);
		string text = "Health = " + health;
		HealthText.text = text;
		PlayerDataPrefs.Health = health;
	}

	public void ShowGameOverPanel()
	{
		gameOverPanel.SetActive(value: true);
		gameWinPanel.SetActive(value: false);
		string text = "Total Kills: " + killedEnemy;
		GameOverKilledText.text = text;
	}

	public void ShowGameWinPanel()
	{
		Time.timeScale = 0f;
		gameWinPanel.SetActive(value: true);
		gameOverPanel.SetActive(value: false);
	}

	public void HealthDecrease()
	{
		int health = PlayerDataPrefs.Health;
		health--;
		CheckGameOver(health);
		Debug.Log("UPDATED HEALTH = " + health);
		string text = "Health = " + health;
		HealthText.text = text;
		PlayerDataPrefs.Health = health;
	}

	public void CheckGameOver(int currentHealth)
	{
		if (currentHealth == 0)
		{
			ShowGameOverPanel();
		}
	}

	public void SceneHome()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
	}

	public void SceneRestart()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(1);
	}

	public void OnClickBtn()
	{
		RightHandMovement.isAbleToClick = true;
	}

	public void OnReleaseBtn()
	{
		RightHandMovement.isAbleToClick = false;
	}

	public void IncreaseKilledEnemyCount()
	{
		killedEnemy++;
		string text = "Killed: " + killedEnemy;
		killedEnemyText.text = text;
	}

	public IEnumerator ResetBodyPart(SpriteRenderer bodyPart)
	{
		yield return new WaitForSeconds(2f);
		if (bodyPart != null)
		{
			bodyPart.color = Color.white;
		}
		else
		{
			SpriteRenderer componentInParent = bodyPart.gameObject.GetComponentInParent<SpriteRenderer>();
			if (componentInParent != null)
			{
				componentInParent.color = Color.white;
			}
			else
			{
				SpriteRenderer componentInChildren = bodyPart.gameObject.GetComponentInChildren<SpriteRenderer>();
				if (componentInChildren != null)
				{
					componentInChildren.color = Color.white;
				}
			}
		}
		if (bodyPart.gameObject.activeInHierarchy)
		{
			bodyPart.color = Color.white;
		}
		Debug.LogWarning("<color=yellow><b>AFTER 4 seconds</color></b>");
	}
}
