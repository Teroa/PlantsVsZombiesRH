using UnityEngine;

public class HyponoEmperor : Plant
{
	public int restHealth = 5;

	[SerializeField]
	private float summonZombieTime = 30f;

	protected override void Update()
	{
		base.Update();
		if (GameAPP.theGameStatus == 0)
		{
			SummonUpdate();
		}
		if (restHealth == 0)
		{
			Die();
		}
	}

	private void SummonUpdate()
	{
		if (summonZombieTime > 0f)
		{
			summonZombieTime -= Time.deltaTime;
			if (summonZombieTime <= 0f)
			{
				anim.SetTrigger("summon");
				GameAPP.PlaySound(83);
				summonZombieTime = 30f;
			}
		}
	}

	private void Summon()
	{
		if (GameAPP.theGameStatus != 0)
		{
			return;
		}
		if (board.isEveStarted)
		{
			CreateZombie.Instance.SetZombieWithMindControl(0, thePlantRow, 105, shadow.transform.position.x, withEffect: true);
			return;
		}
		if (board.roadType[thePlantRow] == 1)
		{
			CreateZombie.Instance.SetZombieWithMindControl(0, thePlantRow, 14, shadow.transform.position.x, withEffect: true);
			return;
		}
		int num;
		switch (Random.Range(0, 4))
		{
		case 0:
			num = 15;
			break;
		case 1:
			num = 109;
			break;
		case 2:
			num = 18;
			break;
		default:
			num = 104;
			break;
		}
		Zombie component = CreateZombie.Instance.SetZombieWithMindControl(0, thePlantRow, num, shadow.transform.position.x, withEffect: true).GetComponent<Zombie>();
		if (num == 104)
		{
			component.TakeDamage(0, component.theSecondArmorMaxHealth);
		}
	}
}
