using UnityEngine;

public class SniperPea : Shooter
{
	private int attackCount;

	private GameObject ac;

	private SpriteRenderer r;

	protected override void Awake()
	{
		base.Awake();
		ac = base.transform.GetChild(0).gameObject;
		r = ac.GetComponent<SpriteRenderer>();
	}

	protected override void Update()
	{
		base.Update();
		AcPositionUpdate();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (targetZombie == null)
		{
			SearchZombie();
		}
	}

	protected override void PlantShootUpdate()
	{
		thePlantAttackCountDown -= Time.deltaTime;
		if (thePlantAttackCountDown < 0f)
		{
			thePlantAttackCountDown = thePlantAttackInterval;
			thePlantAttackCountDown += Random.Range(-0.1f, 0.1f);
			if (targetZombie != null)
			{
				anim.SetTrigger("shoot");
			}
		}
	}

	private void AcPositionUpdate()
	{
		if (targetZombie != null && targetZombie.theStatus != 1)
		{
			ac.SetActive(value: true);
			r.sortingLayerName = $"bullet{targetZombie.theZombieRow}";
			ac.transform.position = targetZombie.GetComponent<Collider2D>().bounds.center;
		}
		else
		{
			ac.SetActive(value: false);
		}
	}

	public override GameObject AnimShoot()
	{
		attackCount++;
		GameAPP.PlaySound(40, 0.2f);
		if (targetZombie != null)
		{
			if (SearchUniqueZombie(targetZombie))
			{
				if (attackCount % 8 == 0)
				{
					targetZombie.TakeDamage(4, 100000);
					Object.Instantiate(GameAPP.particlePrefab[0], ac.transform.position, Quaternion.identity, board.transform);
				}
				else
				{
					targetZombie.TakeDamage(4, 1800);
					Object.Instantiate(GameAPP.particlePrefab[0], ac.transform.position, Quaternion.identity, board.transform);
				}
				if (targetZombie.theStatus == 1)
				{
					targetZombie = null;
				}
			}
			else
			{
				if (SearchZombie() != null)
				{
					attackCount++;
					GameAPP.PlaySound(40);
					if (attackCount % 10 == 0)
					{
						targetZombie.TakeDamage(4, int.MaxValue);
						Object.Instantiate(GameAPP.particlePrefab[0], ac.transform.position, Quaternion.identity, board.transform);
					}
					else
					{
						targetZombie.TakeDamage(4, 1800);
						Object.Instantiate(GameAPP.particlePrefab[0], ac.transform.position, Quaternion.identity, board.transform);
					}
				}
				if (targetZombie.theStatus == 1)
				{
					targetZombie = null;
				}
			}
		}
		return null;
	}

	protected override GameObject SearchZombie()
	{
		zombieList.Clear();
		foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > shadow.transform.position.x && SearchUniqueZombie(component))
				{
					zombieList.Add(component);
				}
			}
		}
		if (zombieList.Count > 1)
		{
			zombieList.Sort((Zombie a, Zombie b) => b.theHealth.CompareTo(a.theHealth));
			targetZombie = zombieList[0];
			return zombieList[0].gameObject;
		}
		if (zombieList.Count == 1)
		{
			targetZombie = zombieList[0];
			return zombieList[0].gameObject;
		}
		return null;
	}

	protected override bool SearchUniqueZombie(Zombie zombie)
	{
		if (zombie == null)
		{
			return false;
		}
		if (zombie.isMindControlled)
		{
			return false;
		}
		int theStatus = zombie.theStatus;
		if (theStatus == 1 || theStatus == 7)
		{
			return false;
		}
		return true;
	}
}
