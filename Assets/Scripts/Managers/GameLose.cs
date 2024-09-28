using UnityEngine;

public class GameLose : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Zombie"))
		{
			Zombie component = collision.GetComponent<Zombie>();
			if (!component.isMindControlled && component.theStatus != 1)
			{
				UIMgr.EnterLoseMenu();
			}
		}
	}
}
