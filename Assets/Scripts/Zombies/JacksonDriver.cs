using UnityEngine;

public class JacksonDriver : SuperDriverZombie
{
	private bool setDancer;

	public override void Die(int reason = 0)
	{
		if (reason != 1 && !isMindControlled && !board.isEveStarted)
		{
			CreateZombie.Instance.SetZombie(0, theZombieRow, 16, shadow.transform.position.x - 1f);
			CreateZombie.Instance.SetZombie(0, theZombieRow, 18, shadow.transform.position.x + 1f);
			CreateZombie.Instance.SetZombie(0, theZombieRow, 10, shadow.transform.position.x);
		}
		base.Die(reason);
	}

	public override void TakeDamage(int theDamageType, int theDamage)
	{
		if (theDamage > 0)
		{
			theDamage /= 4;
		}
		base.TakeDamage(theDamageType, theDamage);
	}

	protected override void DriverPositionUpdate()
	{
		float x = base.transform.GetChild(4).position.x;
		if (board.iceRoadX[theZombieRow] > x)
		{
			if (board.isEveStarted)
			{
				base.transform.Translate(-0.1f * Time.deltaTime, 0f, 0f);
			}
			else
			{
				base.transform.Translate(-0.5f * Time.deltaTime, 0f, 0f);
			}
		}
		else
		{
			base.transform.Translate((0f - currentSpeed) * Time.deltaTime, 0f, 0f);
		}
	}

	protected override void BodyTakeDamage(int theDamage)
	{
		theHealth -= theDamage;
		if (theHealth >= (float)theMaxHealth / 3f && theHealth < (float)theMaxHealth * 2f / 3f)
		{
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/InTravel/JacksonDriver/damage1");
			base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/InTravel/JacksonDriver/Zombie_zamboni_2_damage1");
		}
		if (theHealth < (float)theMaxHealth / 3f)
		{
			if (board.isEveStarted && !setDancer && !isMindControlled)
			{
				setDancer = true;
				CreateZombie.Instance.SetZombie(0, theZombieRow, 7, shadow.transform.position.x - 3f);
				Object.Instantiate(GameAPP.particlePrefab[11], new Vector3(shadow.transform.position.x - 5f, shadow.transform.position.y + 1f, 0f), Quaternion.identity).transform.SetParent(GameAPP.board.transform);
			}
			anim.SetTrigger("shake");
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/InTravel/JacksonDriver/damage2");
			base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/InTravel/JacksonDriver/Zombie_zamboni_2_damage2");
			base.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>()
				.sprite = GameAPP.spritePrefab[37];
			base.transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>()
				.sprite = GameAPP.spritePrefab[37];
			base.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			GameObject obj = base.transform.GetChild(1).GetChild(0).gameObject;
			obj.SetActive(value: true);
			foreach (Transform item in obj.transform)
			{
				item.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = $"zombie{theZombieRow}";
				item.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = baseLayer + 29;
			}
		}
		if (theHealth <= 0f)
		{
			DieAndExplode();
		}
	}

	public override void KillByCaltrop()
	{
		if (theHealth > 0.5f * (float)theMaxHealth)
		{
			TakeDamage(0, (int)((float)theMaxHealth * 0.5f));
			return;
		}
		anim.SetTrigger("shake");
		anim.SetTrigger("GoDie");
		base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/InTravel/JacksonDriver/damage2");
		base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/InTravel/JacksonDriver/Zombie_zamboni_2_damage2");
		base.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>()
			.sprite = GameAPP.spritePrefab[37];
		base.transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>()
			.sprite = GameAPP.spritePrefab[37];
		base.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
		GameObject obj = base.transform.GetChild(1).GetChild(0).gameObject;
		obj.SetActive(value: true);
		foreach (Transform item in obj.transform)
		{
			item.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = $"zombie{theZombieRow}";
			item.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = baseLayer + 29;
		}
		GetComponent<BoxCollider2D>().enabled = false;
		theStatus = 1;
		Invoke("DieAndExplode", 2f);
	}
}
