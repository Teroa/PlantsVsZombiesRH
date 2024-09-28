using TMPro;
using UnityEngine;

public class UIMgr : MonoBehaviour
{
	public enum SceneType
	{
		Day = 0,
		Night = 1,
		Pool = 2,
		NightPool = 3,
		Roof = 4,
		NightRoof = 5,
		Day_6 = 6,
		Night_6 = 7
	}

	public static void EnterMainMenu()
	{
		ClearCanvas();
		Camera.main.transform.position = new Vector3(0f, 0f, -200f);
		Object.Destroy(GameObject.Find("Tutor"));
		Time.timeScale = GameAPP.gameSpeed;
		GameAPP.theBoardLevel = -1;
		GameAPP.theBoardType = -1;
		GameAPP.theGameStatus = -1;
		GameAPP.ChangeMusic(0);
		GameAPP.PlaySoundNotPause(38);
		GameObject gameObject = Resources.Load<GameObject>("UI/MainMenu/MainMenuFHD");
		Object.Instantiate(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	public static void EnterAdvantureMenu()
	{
		if (GameAPP.theBoardType == 1)
		{
			EnterChallengeMenu();
			return;
		}
		if (GameAPP.theBoardType == 2)
		{
			EnterIZEMenu();
			return;
		}
		if (GameAPP.theBoardType == 3)
		{
			EnterSurvivalEMenu();
			return;
		}
		MenuNormalSettings();
		GameObject gameObject = Resources.Load<GameObject>("UI/AdvantureMenu/AdvantureMenuFHD");
		Object.Instantiate(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	public static void EnterChallengeMenu()
	{
		MenuNormalSettings();
		GameObject gameObject = Resources.Load<GameObject>("UI/AdvantureMenu/ChallengeMenuFHD");
		Object.Instantiate(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	public static void EnterIZEMenu()
	{
		MenuNormalSettings();
		GameObject gameObject = Resources.Load<GameObject>("UI/AdvantureMenu/IZEMenuFHD");
		Object.Instantiate(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	public static void EnterSurvivalEMenu()
	{
		MenuNormalSettings();
		GameObject gameObject = Resources.Load<GameObject>("UI/AdvantureMenu/SurvivalMenuFHD");
		Object.Instantiate(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	private static void MenuNormalSettings()
	{
		Time.timeScale = GameAPP.gameSpeed;
		GameAPP.theBoardLevel = -1;
		GameAPP.theBoardType = -1;
		GameAPP.theGameStatus = -1;
		GameAPP.ChangeMusic(1);
	}

	public static void EnterPauseMenu(int place)
	{
		if (place == 0)
		{
			GameAPP.PlaySoundNotPause(30);
			GameAPP.theGameStatus = 1;
			Time.timeScale = 0f;
		}
		GameObject gameObject = Resources.Load<GameObject>("UI/PauseMenu/PauseMenuFHD");
		GameObject gameObject2 = Object.Instantiate(gameObject, GameAPP.canvas.transform);
		gameObject2.name = gameObject.name;
		if (place == 1)
		{
			gameObject2.transform.GetChild(2).gameObject.SetActive(value: false);
			gameObject2.transform.GetChild(3).gameObject.SetActive(value: false);
			gameObject2.transform.GetChild(4).gameObject.SetActive(value: false);
			gameObject2.transform.GetChild(5).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "确定";
			gameObject2.transform.GetChild(6).gameObject.SetActive(value: true);
		}
	}

	public static void EnterGame(int levelType, int levelNumber, GameObject menu = null)
	{
		ClearCanvas();
		Time.timeScale = GameAPP.gameSpeed;
		for (int i = 0; i < GameAPP.unlocked.Length; i++)
		{
			GameAPP.unlocked[i] = false;
		}
		if (levelType == 3 && SaveMgr.ExistSaves(levelNumber) && levelNumber == 8)
		{
			EnterTravelGame(levelType, levelNumber, SaveMgr.boardData[2]);
			return;
		}
		if (levelType == 2)
		{
			EnterIZGame(levelNumber);
			return;
		}
		GameAPP.ChangeMusic(1);
		GameAPP.theBoardType = levelType;
		GameAPP.theBoardLevel = levelNumber;
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>(GetMapName(levelType, levelNumber)));
		gameObject.name = "Background";
		GameAPP.board = gameObject;
		GameAPP.board.AddComponent<Board>();
		if (levelType == 3)
		{
			switch (levelNumber)
			{
			case 7:
				Board.Instance.EnterEndlessGame();
				break;
			case 8:
				Board.Instance.isTravel = true;
				Board.Instance.theCurrentSurvivalRound = 1;
				break;
			default:
				Board.Instance.theCurrentSurvivalRound = 1;
				break;
			}
			if (SaveMgr.ExistSaves(levelNumber) && levelNumber != 7)
			{
				SaveMgr.LoadBoard(levelNumber);
			}
			InitZombieList.InitZombie(levelType, levelNumber, Board.Instance.theCurrentSurvivalRound);
		}
		else
		{
			InitZombieList.InitZombie(levelType, levelNumber);
		}
		GameAPP.theGameStatus = 2;
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "Default";
		gameObject.AddComponent<InitBoard>();
	}

	public static void EnterTravelGame(int levelType, int levelNumber, int theRound)
	{
		Time.timeScale = GameAPP.gameSpeed;
		GameAPP.ChangeMusic(1);
		GameAPP.theBoardType = levelType;
		GameAPP.theBoardLevel = levelNumber;
		string path = "Background/SixRow/Day";
		if (theRound < 4)
		{
			path = "Background/SixRow/Day";
		}
		else if (theRound >= 4 && theRound < 7)
		{
			path = "Background/SixRow/Night";
		}
		else if (theRound >= 7 && theRound < 10)
		{
			path = "Background/background3";
		}
		else
		{
			Debug.LogError("旅行模式关卡错误");
		}
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>(path));
		gameObject.name = "Background";
		GameAPP.board = gameObject;
		Board board = GameAPP.board.AddComponent<Board>();
		board.isTravel = true;
		if (theRound >= 4 && theRound < 7)
		{
			board.isNight = true;
		}
		else if (theRound >= 7 && theRound < 10)
		{
			board.roadNum = 6;
			board.roadType[2] = 1;
			board.roadType[3] = 1;
			for (int i = 0; i < board.boxType.GetLength(0); i++)
			{
				board.boxType[i, 2] = 1;
				board.boxType[i, 3] = 1;
			}
		}
		SaveMgr.LoadBoard(levelNumber);
		InitZombieList.InitZombie(levelType, levelNumber, Board.Instance.theCurrentSurvivalRound);
		GameAPP.theGameStatus = 2;
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "Default";
		gameObject.AddComponent<InitBoard>();
	}

	private static void EnterIZGame(int levelNumber)
	{
		GameAPP.ChangeMusic(15);
		GameAPP.theBoardType = 2;
		GameAPP.theBoardLevel = levelNumber;
		if (levelNumber > 12)
		{
			GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Background/backgroundIZPool"));
			obj.transform.position = new Vector3(-10f, 5f, 0f);
			obj.name = "Background";
			GameAPP.board = obj;
			obj.AddComponent<Board>().isIZ = true;
			Board.Instance.roadNum = 6;
			Board.Instance.roadType[2] = 1;
			Board.Instance.roadType[3] = 1;
			for (int i = 0; i < Board.Instance.boxType.GetLength(0); i++)
			{
				Board.Instance.boxType[i, 2] = 1;
				Board.Instance.boxType[i, 3] = 1;
			}
			GameObject gameObject = Resources.Load<GameObject>("UI/InGameMenu/IZE/InGameUIIZE");
			GameObject obj2 = Object.Instantiate(gameObject, GameAPP.canvas.transform);
			obj2.transform.position = new Vector3(0f, 0f, -1f);
			obj2.name = gameObject.name;
		}
		else
		{
			GameObject obj3 = Object.Instantiate(Resources.Load<GameObject>("Background/backgroundIZE"));
			obj3.transform.position = new Vector3(-10f, 5f, 0f);
			obj3.name = "Background";
			GameAPP.board = obj3;
			obj3.AddComponent<Board>().isIZ = true;
			GameObject gameObject2 = Resources.Load<GameObject>("UI/InGameMenu/IZE/InGameUIIZE");
			GameObject obj4 = Object.Instantiate(gameObject2, GameAPP.canvas.transform);
			obj4.transform.position = new Vector3(0f, 0f, -1f);
			obj4.name = gameObject2.name;
		}
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "Default";
		GameAPP.theGameStatus = 0;
	}

	public static void EVEAuto(int road)
	{
		GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Background/backgroundIZE"));
		obj.transform.position = new Vector3(-10f, 5f, 0f);
		obj.name = "Background";
		GameAPP.board = obj;
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "Default";
		obj.AddComponent<Board>().isIZ = true;
		obj.GetComponent<Board>().isAutoEve = true;
		obj.GetComponent<Board>().isEveStarted = true;
		obj.GetComponent<Board>().isEveStart = true;
		GameAPP.theGameStatus = 0;
		GameObject gameObject = Resources.Load<GameObject>("UI/InGameMenu/IZE/InGameUIIZE");
		GameObject obj2 = Object.Instantiate(gameObject, GameAPP.canvas.transform);
		obj2.name = gameObject.name;
		obj2.transform.GetChild(4).GetChild(0).GetComponent<EveBtn>()
			.OpenEveGame();
		EveBtn.LoadPlants();
		GameObject[] plantArray = obj.GetComponent<Board>().plantArray;
		foreach (GameObject gameObject2 in plantArray)
		{
			if (gameObject2 != null)
			{
				Plant component = gameObject2.GetComponent<Plant>();
				if (component.thePlantRow != road)
				{
					component.Die();
				}
			}
		}
		EveBtn.SetPlant();
		EveBtn.AutoGame();
	}

	public static void BackToGame(GameObject menu)
	{
		if (GameAPP.theGameStatus == -2)
		{
			GameAPP.theGameStatus = -1;
			Time.timeScale = GameAPP.gameSpeed;
			Object.Destroy(menu);
			return;
		}
		GameAPP.theGameStatus = 0;
		Time.timeScale = GameAPP.gameSpeed;
		GameAPP.gameAPP.GetComponent<AudioSource>().UnPause();
		Camera.main.GetComponent<AudioSource>().UnPause();
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "Default";
		Object.Destroy(menu);
	}

	public static void EnterLoseMenu()
	{
		GameAPP.theGameStatus = 1;
		Time.timeScale = 0f;
		GameAPP.gameAPP.GetComponent<AudioSource>().Pause();
		Camera.main.GetComponent<AudioSource>().Pause();
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "up";
		Object.Destroy(GameAPP.board.GetComponent<InitBoard>().theInGameUI);
		GameObject gameObject = Resources.Load<GameObject>("UI/InGameMenu/Lose/LoseMenu");
		Object.Instantiate(gameObject, GameAPP.canvas.transform).name = gameObject.name;
		GameAPP.PlaySoundNotPause(52);
		Object.Destroy(GameAPP.board.GetComponent<Mouse>().theItemOnMouse);
	}

	public static void EnterHelpMenu()
	{
		GameObject gameObject = Resources.Load<GameObject>("UI/MainMenu/HelpMenu");
		Object.Instantiate(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	public static void EnterOtherMenu()
	{
		GameObject gameObject = Resources.Load<GameObject>("UI/MainMenu/OtherMenu");
		Object.Instantiate(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	public static void EnterAlmanac(bool changeMusic = false)
	{
		if (changeMusic)
		{
			GameAPP.ChangeMusic(1);
		}
		GameObject gameObject = Resources.Load<GameObject>("UI/Almanac/Almanac");
		Object.Instantiate(gameObject).name = gameObject.name;
	}

	public static void LookPlant()
	{
		GameObject gameObject = Resources.Load<GameObject>("UI/Almanac/AlmanacPlant");
		Object.Instantiate(gameObject).name = gameObject.name;
	}

	private static void ClearCanvas()
	{
		foreach (Transform item in GameAPP.canvasUp.transform)
		{
			if (item != null)
			{
				Object.Destroy(item.gameObject);
			}
		}
		foreach (Transform item2 in GameAPP.canvas.transform)
		{
			if (item2 != null)
			{
				Object.Destroy(item2.gameObject);
			}
		}
	}

	private static string GetMapName(int theLevelType, int theLevelNumber)
	{
		int sceneType = GetSceneType(theLevelType, theLevelNumber);
		string towerDefenceMap = GetTowerDefenceMap(theLevelNumber);
		if (towerDefenceMap != null)
		{
			return towerDefenceMap;
		}
		switch (sceneType)
		{
		case 0:
			return "Background/background1";
		case 1:
			return "Background/background2";
		case 2:
			return "Background/background3";
		case 6:
			return "Background/SixRow/Day";
		case 7:
			return "Background/SixRow/Night";
		default:
			return "Background/background1";
		}
	}

	private static string GetTowerDefenceMap(int theLevelNumber)
	{
		switch (theLevelNumber)
		{
		case 35:
			return "Background/TowerMaps/Tower1";
		case 36:
			return "Background/TowerMaps/Tower2";
		default:
			return null;
		}
	}

	public static int GetSceneType(int theLevelType, int theLevelNumber, int sceneType = -1)
	{
		int num = 0;
		if (sceneType != -1 && Board.Instance.isTravel)
		{
			return sceneType;
		}
		switch (theLevelType)
		{
		case 0:
			switch (theLevelNumber)
			{
			case 10:
			case 11:
			case 12:
			case 13:
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
				return 1;
			case 19:
			case 20:
			case 21:
			case 22:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
				return 2;
			default:
				return 0;
			}
		case 1:
			switch (theLevelNumber)
			{
			case 3:
			case 4:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
				return 1;
			case 36:
				return 7;
			case 5:
			case 6:
			case 32:
			case 33:
			case 34:
				return 2;
			case 38:
			case 39:
				return 6;
			default:
				return 0;
			}
		case 3:
			switch (theLevelNumber)
			{
			case 2:
			case 5:
				return 1;
			case 3:
			case 6:
			case 7:
				return 2;
			case 8:
				if (Board.Instance != null)
				{
					if (Board.Instance.theCurrentSurvivalRound <= 6 && Board.Instance.theCurrentSurvivalRound > 3)
					{
						return 7;
					}
					if (Board.Instance.theCurrentSurvivalRound > 6)
					{
						return 2;
					}
					return 6;
				}
				return 6;
			default:
				return 0;
			}
		default:
			return 0;
		}
	}
}
