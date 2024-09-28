public class ZombieDuck : Zombie
{
	protected override void Start()
	{
		base.Start();
		if (GameAPP.theGameStatus == 0 || GameAPP.theGameStatus == 1)
		{
			anim.Play("swim");
			anim.SetBool("inWater", value: true);
			inWater = true;
			SetMaskLayer();
		}
	}
}
