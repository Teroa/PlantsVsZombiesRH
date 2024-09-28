public class Jalatang : Tanglekelp
{
	public override void Die(int reason = 0)
	{
		board.CreateFireLine(thePlantRow);
		base.Die(0);
	}
}
