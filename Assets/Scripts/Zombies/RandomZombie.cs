using System.Collections.Generic;
using UnityEngine;

public class RandomZombie : ConeZombie
{
	protected override void FirstArmorFall()
	{
		Vector3 position = shadow.transform.position;
		GameObject gameObject = SetRandomZombie(position);
		while (gameObject == null)
		{
			gameObject = SetRandomZombie(position);
		}
		Zombie component = gameObject.GetComponent<Zombie>();
		if (isMindControlled)
		{
			component.SetMindControl(mustControl: true);
		}
		RandomEvent(component);
		base.FirstArmorFall();
		Object.Instantiate(GameAPP.particlePrefab[11], new Vector3(base.transform.position.x, position.y + 1f, 0f), Quaternion.identity).transform.SetParent(GameAPP.board.transform);
		Die(2);
	}

	protected virtual GameObject SetRandomZombie(Vector3 pos)
	{
		if (board.isEveStarted || (GameAPP.theBoardType == 1 && GameAPP.theBoardLevel == 39))
		{
			return GetEveZombie(pos);
		}
		if (Random.Range(0, 36) < 24)
		{
			return CreateZombie.Instance.SetZombie(0, theZombieRow, Random.Range(0, 24), pos.x);
		}
		return CreateZombie.Instance.SetZombie(0, theZombieRow, Random.Range(100, 112), pos.x);
	}

	private GameObject GetEveZombie(Vector3 pos)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < GameAPP.zombiePrefab.Length; i++)
		{
			if (GameAPP.zombiePrefab[i] != null)
			{
				list.Add(i);
			}
		}
		int index = Random.Range(0, list.Count);
		int num = list[index];
		return CreateZombie.Instance.SetZombie(0, theZombieRow, num, pos.x);
	}

	protected virtual void RandomEvent(Zombie zombie)
	{
		if (board.isEveStarted && zombie.theZombieType == 200)
		{
			switch (zombie.theZombieType)
			{
			case 20:
				zombie.theHealth *= 2f;
				zombie.theMaxHealth *= 2;
				break;
			case 200:
				zombie.theMaxHealth = 8000;
				zombie.theHealth = 8000f;
				break;
			}
		}
		float num = (float)Random.Range(1, 6) / 5f;
		zombie.theHealth *= num;
		zombie.theMaxHealth = (int)((float)zombie.theMaxHealth * num);
		zombie.theFirstArmorHealth = (int)((float)zombie.theFirstArmorHealth * num);
		zombie.theFirstArmorMaxHealth = (int)((float)zombie.theFirstArmorMaxHealth * num);
		zombie.theSecondArmorHealth = (int)((float)zombie.theSecondArmorHealth * num);
		zombie.theSecondArmorMaxHealth = (int)((float)zombie.theSecondArmorMaxHealth * num);
	}

	protected override void FirstArmorBroken()
	{
		if (theFirstArmorHealth < theFirstArmorMaxHealth * 2 / 3 && theFirstArmorBroken < 1)
		{
			theFirstArmorBroken = 1;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[12];
		}
		if (theFirstArmorHealth < theFirstArmorMaxHealth / 3 && theFirstArmorBroken < 2)
		{
			theFirstArmorBroken = 2;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[13];
		}
	}
}
