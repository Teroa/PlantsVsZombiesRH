using UnityEngine;

public class ObsidianSpike : SpikeRock
{
	protected override void Update()
	{
		base.Update();
		if (attributeCountdown <= 0f)
		{
			attributeCountdown = 15f;
			Recover(150);
		}
	}

	protected override void AnimAttack()
	{
		KillCar();
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(1f, 1f), 0f);
		bool flag = false;
		Collider2D[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].TryGetComponent<Zombie>(out var component) && component.theZombieRow == thePlantRow && SearchUniqueZombie(component))
			{
				flag = true;
				component.TakeDamage(4, 100);
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}
}
