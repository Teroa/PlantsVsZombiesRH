using UnityEngine;

public class CherryPeaShooter : Shooter
{
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 1, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 60;
		GameAPP.PlaySound(Random.Range(3, 5));
		return obj;
	}
}
