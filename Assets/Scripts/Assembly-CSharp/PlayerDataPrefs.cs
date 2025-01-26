using UnityEngine;

public class PlayerDataPrefs
{
	public static int Health
	{
		get
		{
			return PlayerPrefs.GetInt("Coins", 0);
		}
		set
		{
			PlayerPrefs.SetInt("Coins", value);
		}
	}

	public static int Diamonds
	{
		get
		{
			return PlayerPrefs.GetInt("Coins", 0);
		}
		set
		{
			PlayerPrefs.SetInt("Coins", value);
		}
	}

	public static int SelectedCaptionIndex
	{
		get
		{
			return PlayerPrefs.GetInt("captainIndex", 0);
		}
		set
		{
			PlayerPrefs.SetInt("captainIndex", value);
		}
	}
}
