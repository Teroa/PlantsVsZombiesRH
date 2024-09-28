using UnityEngine;

public class PresentCard : MonoBehaviour
{
	private void Start()
	{
		if (GameAPP.theBoardType == 1)
		{
			int theBoardLevel = GameAPP.theBoardLevel;
			if (theBoardLevel == 15 || theBoardLevel == 17 || theBoardLevel == 39)
			{
				base.gameObject.SetActive(value: true);
			}
			else
			{
				base.gameObject.SetActive(value: false);
			}
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
