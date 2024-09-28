using UnityEngine;
using UnityEngine.UI;

public class StartGameBtn : MonoBehaviour
{
	public Sprite highLightSprite;

	private Sprite originSprite;

	private Image image;

	private Vector3 originPosition;

	private RectTransform rectTransform;

	private bool clicked;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		originPosition = rectTransform.anchoredPosition;
		image = GetComponent<Image>();
		originSprite = image.sprite;
	}

	private void OnMouseEnter()
	{
		base.transform.GetChild(2).gameObject.SetActive(value: true);
		CursorChange.SetClickCursor();
	}

	private void OnMouseExit()
	{
		rectTransform.anchoredPosition = originPosition;
		base.transform.GetChild(2).gameObject.SetActive(value: false);
		CursorChange.SetDefaultCursor();
	}

	private void OnMouseDown()
	{
		GameAPP.PlaySound(19);
		rectTransform.anchoredPosition = new Vector2(originPosition.x + 1f, originPosition.y - 1f);
	}

	private void OnMouseUp()
	{
		if (clicked)
		{
			return;
		}
		GameAPP.lastCards.Clear();
		GameObject[] cardOnBank = InGameUIMgr.Instance.cardOnBank;
		foreach (GameObject gameObject in cardOnBank)
		{
			if (gameObject != null)
			{
				CardUI component = gameObject.GetComponent<CardUI>();
				if (component.theSeedType != 256)
				{
					GameAPP.LastCards lastCards = default(GameAPP.LastCards);
					lastCards.theSeedType = component.theSeedType;
					lastCards.isExtra = component.isExtra;
					GameAPP.LastCards item = lastCards;
					GameAPP.lastCards.Add(item);
				}
			}
		}
		clicked = true;
		CursorChange.SetDefaultCursor();
		rectTransform.anchoredPosition = originPosition;
		InitBoard.Instance.RemoveUI();
	}
}
