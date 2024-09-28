using UnityEngine;

public class SaveMgr : MonoBehaviour
{
	public static int[] plant = new int[1024];

	public static int[] boardData = new int[3];

	public static bool[] travelData = new bool[5];

	public static bool ExistSaves(int level)
	{
		for (int i = 0; i < boardData.Length; i++)
		{
			boardData[i] = 0;
		}
		for (int j = 0; j < travelData.Length; j++)
		{
			travelData[j] = false;
		}
		ClearPlant();
		SaveInfo.Instance.LoadSurvivalData(level);
		if (boardData[0] == 0)
		{
			return false;
		}
		return true;
	}

	public static void ClearBoard(int level)
	{
		for (int i = 0; i < boardData.Length; i++)
		{
			boardData[i] = 0;
		}
		for (int j = 0; j < travelData.Length; j++)
		{
			travelData[j] = false;
		}
		ClearPlant();
		SaveInfo.Instance.SaveSurvivalData(level);
	}

	public static void SaveBoard(int level)
	{
		boardData[0] = 1;
		boardData[1] = Board.Instance.theSun;
		boardData[2] = Board.Instance.theCurrentSurvivalRound;
		travelData = GameAPP.unlocked;
		SavePlants(level);
		Debug.Log("关卡已保存");
		SaveInfo.Instance.SaveSurvivalData(level);
	}

	public static void LoadBoard(int level)
	{
		SaveInfo.Instance.LoadSurvivalData(level);
		LoadPlant(level);
		Board.Instance.theSun = boardData[1];
		Board.Instance.theCurrentSurvivalRound = boardData[2];
		GameAPP.unlocked = travelData;
	}

	private static void SavePlants(int level)
	{
		ClearPlant();
		GameObject[] plantArray = Board.Instance.plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				AddPlant(component.thePlantColumn, component.thePlantRow, component.thePlantType);
			}
		}
	}

	private static void AddPlant(int theColumn, int theRow, int thePlantType)
	{
		for (int i = 0; i < plant.Length; i += 4)
		{
			if (plant[i] == 0)
			{
				plant[i] = thePlantType + 1;
				plant[i + 1] = theColumn;
				plant[i + 2] = theRow;
				if (thePlantType == 12)
				{
					plant[i + 3] = 1;
				}
				else
				{
					plant[i + 3] = 0;
				}
				break;
			}
		}
	}

	private static void LoadPlant(int level)
	{
		for (int i = 0; i < plant.Length; i += 4)
		{
			if (plant[i] != 0 && Board.Instance.boxType[plant[i + 1], plant[i + 2]] == 1 && !CreatePlant.Instance.IsWaterPlant(plant[i] - 1) && !CreatePlant.Instance.OnHardLand(plant[i] - 1))
			{
				CreatePlant.Instance.SetPlant(plant[i + 1], plant[i + 2], 12);
			}
		}
		for (int j = 0; j < plant.Length; j += 4)
		{
			if (plant[j] != 0 && (Board.Instance.boxType[plant[j + 1], plant[j + 2]] != 1 || !CreatePlant.Instance.OnHardLand(plant[j] - 1)))
			{
				if (CreatePlant.Instance.SpecialPlant(plant[j] - 1))
				{
					CreatePlant.Instance.SetPlant(plant[j + 1], plant[j + 2], plant[j] - 1, null, default(Vector2), isFreeSet: true);
				}
				else
				{
					CreatePlant.Instance.SetPlant(plant[j + 1], plant[j + 2], plant[j] - 1);
				}
			}
		}
	}

	private static void ClearPlant()
	{
		for (int i = 0; i < plant.Length; i++)
		{
			plant[i] = 0;
		}
	}
}
