using UnityEngine;

public class DollZombie : ConeZombie
{
	protected override void FirstArmorFall()
	{
		Vector3 position = shadow.transform.position;
		int num = theZombieType;
		switch (theZombieType)
		{
		case 21:
			num = 22;
			break;
		case 22:
			num = 23;
			break;
		case 23:
			num = 2;
			break;
		}
		Zombie component = CreateZombie.Instance.SetZombie(0, theZombieRow, num, shadow.transform.position.x).GetComponent<Zombie>();
		if (isMindControlled)
		{
			component.SetMindControl(mustControl: true);
		}
		base.FirstArmorFall();
		Object.Instantiate(GameAPP.particlePrefab[11], new Vector3(base.transform.position.x, position.y + 1f, 0f), Quaternion.identity).transform.SetParent(GameAPP.board.transform);
		Die(2);
	}

	protected override void FirstArmorBroken()
	{
		switch (theZombieType)
		{
		case 21:
			DiamondSpirte();
			break;
		case 22:
			GoldSpirte();
			break;
		case 23:
			SilverSpirte();
			break;
		}
	}

	private void DiamondSpirte()
	{
		if (theFirstArmorHealth < theFirstArmorMaxHealth * 2 / 3 && theFirstArmorBroken < 1)
		{
			theFirstArmorBroken = 1;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[44];
		}
		if (theFirstArmorHealth < theFirstArmorMaxHealth / 3 && theFirstArmorBroken < 2)
		{
			theFirstArmorBroken = 2;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[45];
		}
	}

	private void GoldSpirte()
	{
		if (theFirstArmorHealth < theFirstArmorMaxHealth * 2 / 3 && theFirstArmorBroken < 1)
		{
			theFirstArmorBroken = 1;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[46];
		}
		if (theFirstArmorHealth < theFirstArmorMaxHealth / 3 && theFirstArmorBroken < 2)
		{
			theFirstArmorBroken = 2;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[47];
		}
	}

	private void SilverSpirte()
	{
		if (theFirstArmorHealth < theFirstArmorMaxHealth * 2 / 3 && theFirstArmorBroken < 1)
		{
			theFirstArmorBroken = 1;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[48];
		}
		if (theFirstArmorHealth < theFirstArmorMaxHealth / 3 && theFirstArmorBroken < 2)
		{
			theFirstArmorBroken = 2;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[49];
		}
	}
}
