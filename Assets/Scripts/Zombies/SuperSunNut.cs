using UnityEngine;

public class SuperSunNut : WallNut
{
	public override void TakeDamage(int damage)
	{
		damage = 50;
		CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 2, 0);
		base.TakeDamage(damage);
	}

	public void SummonAndRecover()
	{
		if (board.theSun > 500)
		{
			board.theSun -= 500;
			Recover(1500);
			GameObject gameObject = CreatePlant.Instance.SetPlant(thePlantColumn + 1, thePlantRow, 251);
			if (gameObject != null)
			{
				Vector3 position = gameObject.GetComponent<Plant>().shadow.transform.position;
				Object.Instantiate(GameAPP.particlePrefab[11], position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, board.transform);
			}
		}
	}
}
