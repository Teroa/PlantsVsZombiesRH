using UnityEngine;

public class SubmarineZombie : Zombie
{
	private Vector3 startPos;

	protected override void Start()
	{
		base.Start();
		inWater = true;
		startPos = base.transform.position;
		if (startPos.x > 9f)
		{
			startPos = new Vector3(9f, startPos.y, startPos.z);
		}
	}

	protected override void Update()
	{
		MoveUpdate();
		if (GameAPP.theGameStatus == 0 && ((isMindControlled && base.transform.position.x > 10f) || base.transform.position.x > 12f || base.transform.position.x < -10f))
		{
			Die(2);
		}
		if (board.isEveStarted && theZombieType == 200)
		{
			base.transform.position = startPos;
		}
	}

	protected override void BodyTakeDamage(int theDamage)
	{
		theHealth -= theDamage;
		if (theHealth >= (float)(theMaxHealth * 2) / 3f)
		{
			base.transform.GetChild(0).gameObject.SetActive(value: true);
			base.transform.GetChild(1).gameObject.SetActive(value: false);
			base.transform.GetChild(2).gameObject.SetActive(value: false);
			base.transform.GetChild(3).gameObject.SetActive(value: true);
			base.transform.GetChild(4).gameObject.SetActive(value: false);
			base.transform.GetChild(5).gameObject.SetActive(value: false);
		}
		if (theHealth >= (float)theMaxHealth / 3f && theHealth < (float)(theMaxHealth * 2) / 3f)
		{
			base.transform.GetChild(0).gameObject.SetActive(value: false);
			base.transform.GetChild(1).gameObject.SetActive(value: true);
			base.transform.GetChild(2).gameObject.SetActive(value: false);
			base.transform.GetChild(3).gameObject.SetActive(value: false);
			base.transform.GetChild(4).gameObject.SetActive(value: true);
			base.transform.GetChild(5).gameObject.SetActive(value: false);
		}
		if (theHealth < (float)theMaxHealth / 3f)
		{
			base.transform.GetChild(0).gameObject.SetActive(value: false);
			base.transform.GetChild(1).gameObject.SetActive(value: false);
			base.transform.GetChild(2).gameObject.SetActive(value: true);
			base.transform.GetChild(3).gameObject.SetActive(value: false);
			base.transform.GetChild(4).gameObject.SetActive(value: false);
			base.transform.GetChild(5).gameObject.SetActive(value: true);
		}
		if (theHealth <= 0f)
		{
			Die(2);
		}
	}

	protected override void DieEvent()
	{
		GameAPP.PlaySound(43);
		Vector2 vector = shadow.transform.position;
		Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.6f), original: GameAPP.particlePrefab[34], rotation: Quaternion.identity, parent: board.transform);
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (!isMindControlled && collision.TryGetComponent<Plant>(out var component))
		{
			if (TypeMgr.IsCaltrop(component.thePlantType))
			{
				return;
			}
			if (component.thePlantRow == theZombieRow)
			{
				if (component.thePlantType == 903)
				{
					component.TakeDamage(500);
					base.transform.Translate(1f, 0f, 0f);
					GameAPP.PlaySound(Random.Range(72, 75));
					return;
				}
				GameAPP.PlaySound(75);
				Vector2 vector = component.shadow.transform.position;
				vector = new Vector2(vector.x, vector.y - 0.4f);
				SetWaterSplat(vector, new Vector2(0.5f, 0.5f));
				component.Crashed();
			}
		}
		if (collision.TryGetComponent<Zombie>(out var component2) && component2.isMindControlled != isMindControlled && component2.theZombieRow == theZombieRow)
		{
			component2.TakeDamage(4, 20);
		}
	}

	private GameObject SetWaterSplat(Vector2 position, Vector2 scale)
	{
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Particle/Anim/Water/WaterSplashPrefab"), position, Quaternion.identity, GameAPP.board.transform);
		foreach (Transform item in gameObject.transform)
		{
			item.GetComponent<SpriteRenderer>().sortingLayerName = $"particle{theZombieRow}";
		}
		gameObject.transform.localScale = scale;
		Object.Instantiate(GameAPP.particlePrefab[32], position, Quaternion.identity, GameAPP.board.transform);
		return gameObject;
	}
}
