using UnityEngine;

public class TravelData : MonoBehaviour
{
	public static bool[] travelData = new bool[256];

	public static void GetData()
	{
		for (int i = 0; i < travelData.Length && i < GameAPP.unlocked.Length; i++)
		{
			GameAPP.unlocked[i] = travelData[i];
		}
	}

	public static void SaveData()
	{
		for (int i = 0; i < travelData.Length && i < GameAPP.unlocked.Length; i++)
		{
			travelData[i] = GameAPP.unlocked[i];
		}
	}
}
