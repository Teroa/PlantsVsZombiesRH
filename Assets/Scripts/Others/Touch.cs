using UnityEngine;

public class Touch : MonoBehaviour
{
	public static void CheckTouchUp()
	{
		UnityEngine.Touch touch = Input.GetTouch(0);
		if (touch.phase != 0)
		{
			return;
		}
		RaycastHit2D raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
		if (raycastHit2D.collider != null)
		{
			MonoBehaviour component = raycastHit2D.collider.GetComponent<MonoBehaviour>();
			if (component != null && component.GetType().GetMethod("OnMouseUp") != null)
			{
				raycastHit2D.collider.SendMessage("OnMouseUp");
			}
		}
	}
}
