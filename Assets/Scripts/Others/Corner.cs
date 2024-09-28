using UnityEngine;

public class Corner : MonoBehaviour
{
	public enum Towards
	{
		right = 180,
		left = 0
	}

	public int row;

	public int targetRow;

	public int targetRow2 = -1;

	public bool forHypono;

	public Towards towards;

	public bool lookAnotherSide;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (!collision.TryGetComponent<Zombie>(out var component) || component.isChangingRow)
		{
			return;
		}
		if (!forHypono)
		{
			if (component.theZombieRow != row || component.isMindControlled || !(Mathf.Abs(component.shadow.transform.position.x - base.transform.position.x) < 1.5f))
			{
				return;
			}
			if (lookAnotherSide)
			{
				float x = base.transform.GetChild(0).transform.position.x;
				Vector3 position = component.shadow.transform.position;
				component.transform.rotation = Quaternion.Euler(0f, (float)towards, 0f);
				component.AdjustPosition(component.gameObject, new Vector3(x, position.y, position.z));
			}
			if (targetRow2 != -1)
			{
				if (Random.Range(0, 2) == 1)
				{
					component.ChangeRow(targetRow);
				}
				else
				{
					component.ChangeRow(targetRow2);
				}
			}
			else
			{
				component.ChangeRow(targetRow);
			}
		}
		else
		{
			if (component.theZombieRow != row || !component.isMindControlled || !(Mathf.Abs(component.shadow.transform.position.x - base.transform.position.x) < 1.5f))
			{
				return;
			}
			if (lookAnotherSide)
			{
				float x2 = base.transform.GetChild(0).transform.position.x;
				Vector3 position2 = component.shadow.transform.position;
				component.transform.rotation = Quaternion.Euler(0f, (float)towards, 0f);
				component.AdjustPosition(component.gameObject, new Vector3(x2, position2.y, position2.z));
			}
			if (targetRow2 != -1)
			{
				if (Random.Range(0, 2) == 1)
				{
					component.ChangeRow(targetRow);
				}
				else
				{
					component.ChangeRow(targetRow2);
				}
			}
			else
			{
				component.ChangeRow(targetRow);
			}
		}
	}
}
