using UnityEngine;

public class SunShroom : Producer
{
	private bool isGrowen;

	protected override void Start()
	{
		base.Start();
		attributeCountdown = 120f;
	}

	protected override void Update()
	{
		base.Update();
		if (!isGrowen && GameAPP.theGameStatus == 0 && attributeCountdown <= 0f)
		{
			Grow();
		}
	}

	protected override void ProduceSun()
	{
		if (isGrowen)
		{
			base.ProduceSun();
			return;
		}
		GameAPP.PlaySound(Random.Range(3, 5), 0.3f);
		board.GetComponent<CreateCoin>().SetCoin(thePlantColumn, thePlantRow, 2, 0);
	}

	public void Grow()
	{
		isGrowen = true;
		anim.SetTrigger("grow");
		GameAPP.PlaySound(56);
	}
}
