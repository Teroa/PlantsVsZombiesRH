using UnityEngine;

public class EndoFlame : Plant
{
	public override void Die(int reason = 0)
	{
		Vector2 vector = shadow.transform.position;
		Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.5f), original: Resources.Load<GameObject>("Items/Fertilize/Ferilize"), rotation: Quaternion.identity, parent: GameAPP.board.transform);
		base.Die();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Bullet>(out var component) && (component.theBulletRow == thePlantRow || component.theMovingWay == 2))
		{
			if (thePlantHealth > 0)
			{
				GameAPP.PlaySound(Random.Range(59, 61));
				GameAPP.board.GetComponent<CreateCoin>().SetCoin(0, 0, 0, 0, base.transform.position);
			}
			component.hasHitTarget = true;
			TakeDamage(50);
			component.Die();
		}
	}

	public override void Crashed()
	{
		Vector2 vector = shadow.transform.position;
		Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.5f), original: Resources.Load<GameObject>("Items/Fertilize/Ferilize"), rotation: Quaternion.identity, parent: GameAPP.board.transform);
		base.Crashed();
	}
}
