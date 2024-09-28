using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
	public static Mouse Instance;

	public Renderer r;

	public int theMouseRow;

	public int theMouseColumn;

	public float theBoxXofMouse;

	public float theBoxYofMouse;

	public int thePlantTypeOnMouse = -1;

	public int theZombieTypeOnMouse = -1;

	public GameObject plantShadow;

	public GameObject[] plantShadows = new GameObject[5];

	public GameObject zombieShadow;

	public GameObject theItemOnMouse;

	public CardUI theCardOnMouse;

	public IZECard theIZECardOnMouse;

	public GameObject thePlantOnGlove;

	private bool existShadow;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		DrawItemOnMouse();
		PlantPreviewUpdate();
		theMouseColumn = GetColumnFromX(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
		theMouseRow = GetRowFromY(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		theBoxXofMouse = GetBoxXFromColumn(theMouseColumn);
		theBoxYofMouse = GetBoxYFromRow(theMouseRow);
		if (GameAPP.theGameStatus == 0)
		{
			MouseClick();
		}
	}

	public int GetColumnFromX(float x)
	{
		if (x < -5.6f)
		{
			return 0;
		}
		if (x > 8.2f)
		{
			return 9;
		}
		return (int)((x + 5.6f) / 1.37f);
	}

	public int GetRowFromY(float y)
	{
		if (Board.Instance.roadNum == 5)
		{
			if (y > 3.7f)
			{
				return 0;
			}
			if (y < -4.7f)
			{
				return 4;
			}
			return (int)((3.7f - y) / 1.7f);
		}
		if (y > 3.7f)
		{
			return 0;
		}
		if (y < -4.7f)
		{
			return 5;
		}
		return (int)((3.7f - y) / 1.5f);
	}

	public float GetBoxXFromColumn(int theColumn)
	{
		return -4.8f + 1.35f * (float)theColumn;
	}

	public float GetBoxYFromRow(int theRow)
	{
		if (Board.Instance.roadNum == 5)
		{
			return 2.3f - 1.67f * (float)theRow;
		}
		return 2.3f - 1.45f * (float)theRow;
	}

	private void CreatePlantOnMouse(int theSeedType)
	{
		if (GameAPP.prePlantPrefab[theSeedType] == null)
		{
			if (InGameText.Instance != null)
			{
				InGameText.Instance.EnableText("错误：尝试生成不存在的种植预览！", 3f);
			}
			Debug.LogError("尝试生成错误的种植预览");
			return;
		}
		GameObject gameObject = GameAPP.prePlantPrefab[theSeedType];
		GameObject gameObject2 = Object.Instantiate(gameObject);
		gameObject2.name = gameObject.name;
		gameObject2.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.GetComponent<SpriteRenderer>().sortingLayerName = "up";
		gameObject2.GetComponent<SpriteRenderer>().sortingOrder = 30000;
		theItemOnMouse = gameObject2;
	}

	private void CreateZombieOnMouse(int theZombieType)
	{
		GameObject gameObject = ((theZombieType != -5) ? GameAPP.preZombiePrefab[theZombieType] : GameAPP.prePlantPrefab[256]);
		GameObject gameObject2 = Object.Instantiate(gameObject);
		gameObject2.name = gameObject.name;
		gameObject2.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.GetComponent<SpriteRenderer>().sortingLayerName = "up";
		gameObject2.GetComponent<SpriteRenderer>().sortingOrder = 30000;
		theItemOnMouse = gameObject2;
	}

	private void DrawItemOnMouse()
	{
		if (theItemOnMouse != null)
		{
			if (theItemOnMouse.name == "Shovel")
			{
				theItemOnMouse.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				theItemOnMouse.transform.position = new Vector3(theItemOnMouse.transform.position.x + 0.4f, theItemOnMouse.transform.position.y + 0.4f, 0f);
			}
			else
			{
				theItemOnMouse.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				theItemOnMouse.transform.position = new Vector3(theItemOnMouse.transform.position.x, theItemOnMouse.transform.position.y, -3f);
			}
		}
	}

	private void PlantPreviewUpdate()
	{
		if (theItemOnMouse != null)
		{
			if (theItemOnMouse.CompareTag("Preview"))
			{
				if (!existShadow)
				{
					existShadow = true;
					GameObject gameObject = (plantShadow = Object.Instantiate(theItemOnMouse));
					gameObject.transform.SetParent(GameAPP.board.transform);
					SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
					Color color = component.color;
					color.a = 0.5f;
					component.color = color;
					component.sortingLayerName = "Default";
				}
			}
			else if (plantShadow != null)
			{
				Object.Destroy(plantShadow);
				plantShadow = null;
				existShadow = false;
			}
		}
		else if (plantShadow != null)
		{
			Object.Destroy(plantShadow);
			plantShadow = null;
			existShadow = false;
		}
		if (!(plantShadow != null))
		{
			return;
		}
		if (GetComponent<Board>().isIZ && theZombieTypeOnMouse != -5)
		{
			if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 3.7f && theMouseColumn > 4)
			{
				plantShadow.transform.position = new Vector3(theBoxXofMouse, theBoxYofMouse + 1f, 0f);
			}
			else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 3.7f && theMouseColumn <= 4)
			{
				plantShadow.transform.position = new Vector3(GetBoxXFromColumn(5), theBoxYofMouse + 1f, 0f);
			}
			else
			{
				plantShadow.transform.position = new Vector3(100f, 100f, 100f);
			}
		}
		else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 3.7f && GetComponent<CreatePlant>().CheckBox(theMouseColumn, theMouseRow, thePlantTypeOnMouse))
		{
			plantShadow.transform.position = new Vector3(theBoxXofMouse, theBoxYofMouse + 0.7f, 0f);
		}
		else
		{
			plantShadow.transform.position = new Vector3(100f, 100f, 100f);
		}
	}

	private void TryToSetPlantByCard()
	{
		if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 3.7f)
		{
			int theSeedType = thePlantTypeOnMouse;
			if (!(GetComponent<CreatePlant>().SetPlant(theMouseColumn, theMouseRow, theSeedType) != null))
			{
				return;
			}
			if (GameAPP.theBoardType == 1 && GameAPP.theBoardLevel == 38)
			{
				for (int i = 0; i < Board.Instance.roadNum; i++)
				{
					if (i != theMouseRow)
					{
						CreatePlant.Instance.SetPlant(theMouseColumn, i, theSeedType);
					}
				}
			}
			GameAPP.board.GetComponent<Board>().theSun -= theCardOnMouse.theSeedCost;
			theCardOnMouse.CD = 0f;
			theCardOnMouse.PutDown();
			Object.Destroy(theItemOnMouse);
			ClearItemOnMouse();
		}
		else
		{
			PutDownItem();
		}
	}

	private void TryToSetZombieByCard()
	{
		if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 3.7f)
		{
			int num = theMouseColumn;
			if (num < 5 && theZombieTypeOnMouse != -5)
			{
				num = 5;
			}
			float boxXFromColumn = GetBoxXFromColumn(num);
			int theRow = theMouseRow;
			int num2 = theZombieTypeOnMouse;
			GameObject gameObject = ((num2 != -5) ? CreateZombie.Instance.SetZombie(0, theRow, num2, boxXFromColumn) : CreatePlant.Instance.SetPlant(num, theRow, 256));
			if (gameObject != null)
			{
				if (Board.Instance.roadType[theMouseRow] == 1)
				{
					GameAPP.PlaySound(75);
				}
				else
				{
					GameAPP.PlaySound(Random.Range(22, 24));
				}
				GameAPP.board.GetComponent<Board>().theSun -= theIZECardOnMouse.theZombieCost;
				theIZECardOnMouse.PutDown();
				Object.Destroy(theItemOnMouse);
				ClearItemOnMouse();
			}
		}
		else
		{
			PutDownItem();
		}
	}

	private void TryToSetPlantByGlove()
	{
		if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 3.7f)
		{
			int theColumn = GetComponent<Mouse>().theMouseColumn;
			int theRow = GetComponent<Mouse>().theMouseRow;
			int theSeedType = thePlantTypeOnMouse;
			if (GetComponent<CreatePlant>().SetPlant(theColumn, theRow, theSeedType, thePlantOnGlove) != null)
			{
				GameObject.Find("Glove").GetComponent<GloveMgr>().CD = 0f;
				Object.Destroy(theItemOnMouse);
				ClearItemOnMouse();
			}
		}
		else
		{
			PutDownItem();
		}
	}

	private void PutDownItem()
	{
		if (!(theItemOnMouse != null))
		{
			return;
		}
		ShovelMgr component;
		GloveMgr component2;
		Bucket component3;
		if (theItemOnMouse.CompareTag("Preview"))
		{
			if (theCardOnMouse != null)
			{
				theCardOnMouse.PutDown();
			}
			if (theIZECardOnMouse != null)
			{
				theIZECardOnMouse.PutDown();
			}
			Object.Destroy(theItemOnMouse);
			ClearItemOnMouse();
		}
		else if (theItemOnMouse.TryGetComponent<ShovelMgr>(out component))
		{
			component.PutDown();
			ClearItemOnMouse();
		}
		else if (theItemOnMouse.TryGetComponent<GloveMgr>(out component2))
		{
			component2.PutDown();
			ClearItemOnMouse();
		}
		else if (theItemOnMouse.TryGetComponent<Bucket>(out component3))
		{
			component3.PutDown();
			ClearItemOnMouse();
		}
		else
		{
			Object.Destroy(theItemOnMouse);
			ClearItemOnMouse();
		}
		GameAPP.PlaySound(19);
	}

	private void MouseClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			LeftEvent();
		}
		if (Input.GetMouseButtonDown(1))
		{
			PutDownItem();
		}
	}

	private void LeftEvent()
	{
		if (theItemOnMouse == null)
		{
			LeftClickWithNothing();
		}
		else
		{
			LeftClickWithSomeThing();
		}
	}

	private void LeftClickWithNothing()
	{
		RaycastHit2D[] array = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		List<GameObject> list = new List<GameObject>();
		RaycastHit2D[] array2 = array;
		foreach (RaycastHit2D raycastHit2D in array2)
		{
			list.Add(raycastHit2D.collider.gameObject);
		}
		foreach (GameObject item in list)
		{
			if (item.TryGetComponent<Bucket>(out var component))
			{
				component.Pick();
				return;
			}
		}
		foreach (GameObject item2 in list)
		{
			if (item2.TryGetComponent<ShovelMgr>(out var component2))
			{
				if (!component2.isPickUp)
				{
					component2.PickUp();
					theItemOnMouse = component2.gameObject;
					GameAPP.PlaySound(21);
				}
				break;
			}
			if (item2.TryGetComponent<GloveMgr>(out var component3))
			{
				if (!component3.isPickUp && component3.avaliable)
				{
					component3.PickUp();
					theItemOnMouse = component3.gameObject;
					GameAPP.PlaySound(19);
				}
				break;
			}
			if (item2.TryGetComponent<CardUI>(out var component4))
			{
				ClickOnCard(component4);
				break;
			}
			if (item2.TryGetComponent<IZECard>(out var component5))
			{
				ClickOnIZECard(component5);
				break;
			}
			if (item2.TryGetComponent<Plant>(out var component6))
			{
				switch (component6.thePlantType)
				{
				case 1043:
					component6.GetComponent<DoomFume>().Shoot();
					break;
				case 905:
					component6.GetComponent<SuperSunNut>().SummonAndRecover();
					break;
				}
				break;
			}
		}
	}

	private void LeftClickWithSomeThing()
	{
		Bucket component;
		if (theItemOnMouse.CompareTag("Preview"))
		{
			if (thePlantOnGlove == null)
			{
				if (!GetComponent<Board>().isIZ)
				{
					TryToSetPlantByCard();
				}
				else
				{
					TryToSetZombieByCard();
				}
			}
			else
			{
				TryToSetPlantByGlove();
			}
		}
		else if (theItemOnMouse.name == "Shovel")
		{
			TryToRemovePlant();
		}
		else if (theItemOnMouse.name == "Glove")
		{
			TryToPickPlant();
		}
		else if (theItemOnMouse.TryGetComponent<Bucket>(out component))
		{
			component.Use();
			ClearItemOnMouse();
		}
	}

	private void TryToRemovePlant()
	{
		GameObject gameObject = theItemOnMouse;
		ClearItemOnMouse();
		RaycastHit2D raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, LayerMask.GetMask("Plant"));
		if (raycastHit2D.collider != null && raycastHit2D.collider.TryGetComponent<Plant>(out var component))
		{
			component.Die();
			GameAPP.PlaySound(23);
		}
		gameObject.GetComponent<ShovelMgr>().PutDown();
	}

	private void TryToPickPlant()
	{
		GameObject gameObject = theItemOnMouse;
		ClearItemOnMouse();
		RaycastHit2D raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, LayerMask.GetMask("Plant"));
		if (raycastHit2D.collider != null && raycastHit2D.collider.TryGetComponent<Plant>(out var component) && component.thePlantType != 255)
		{
			thePlantOnGlove = component.gameObject;
			thePlantTypeOnMouse = component.thePlantType;
			CreatePlantOnMouse(thePlantTypeOnMouse);
			GameAPP.PlaySound(25);
		}
		gameObject.GetComponent<GloveMgr>().PutDown();
	}

	public void ClickOnCard(CardUI card)
	{
		if (GameAPP.board.GetComponent<Board>().theSun >= card.theSeedCost)
		{
			if (card.isAvailable)
			{
				card.PickUp();
				theCardOnMouse = card;
				thePlantTypeOnMouse = card.theSeedType;
				CreatePlantOnMouse(thePlantTypeOnMouse);
				GameAPP.PlaySound(25);
			}
			else
			{
				GameAPP.PlaySound(26);
			}
		}
		else
		{
			GameAPP.PlaySound(26);
		}
	}

	public void ClickOnIZECard(IZECard card)
	{
		if (GameAPP.board.GetComponent<Board>().theSun >= card.theZombieCost)
		{
			card.PickUp();
			theIZECardOnMouse = card;
			theZombieTypeOnMouse = card.theZombieType;
			CreateZombieOnMouse(theZombieTypeOnMouse);
			GameAPP.PlaySound(25);
		}
		else
		{
			GameAPP.PlaySound(26);
		}
	}

	public void ClearItemOnMouse(bool clearItem = false)
	{
		if (clearItem)
		{
			Object.Destroy(theItemOnMouse);
		}
		thePlantTypeOnMouse = -1;
		theZombieTypeOnMouse = -1;
		theIZECardOnMouse = null;
		theItemOnMouse = null;
		theCardOnMouse = null;
		thePlantOnGlove = null;
	}
}
