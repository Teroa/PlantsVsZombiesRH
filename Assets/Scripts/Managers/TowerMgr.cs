using UnityEngine;

public class TowerMgr : MonoBehaviour
{
	public static void SetBox(int level)
	{
		Board instance = Board.Instance;
		switch (level)
		{
		case 35:
			instance.boxType[0, 1] = 2;
			instance.boxType[1, 1] = 2;
			instance.boxType[2, 0] = 2;
			instance.boxType[2, 1] = 2;
			instance.boxType[2, 2] = 2;
			instance.boxType[2, 3] = 2;
			instance.boxType[2, 4] = 2;
			instance.boxType[3, 0] = 2;
			instance.boxType[3, 4] = 2;
			instance.boxType[4, 0] = 2;
			instance.boxType[4, 4] = 2;
			instance.boxType[5, 0] = 2;
			instance.boxType[5, 4] = 2;
			instance.boxType[6, 0] = 2;
			instance.boxType[6, 1] = 2;
			instance.boxType[6, 3] = 2;
			instance.boxType[6, 4] = 2;
			instance.boxType[7, 1] = 2;
			instance.boxType[7, 3] = 2;
			instance.boxType[8, 1] = 2;
			instance.boxType[8, 3] = 2;
			instance.boxType[8, 4] = 2;
			instance.boxType[9, 1] = 2;
			instance.boxType[9, 4] = 2;
			break;
		case 36:
			instance.boxType[0, 1] = 2;
			instance.boxType[0, 4] = 2;
			instance.boxType[1, 0] = 2;
			instance.boxType[1, 1] = 2;
			instance.boxType[1, 4] = 2;
			instance.boxType[2, 0] = 2;
			instance.boxType[2, 4] = 2;
			instance.boxType[3, 0] = 2;
			instance.boxType[3, 2] = 2;
			instance.boxType[3, 3] = 2;
			instance.boxType[3, 4] = 2;
			instance.boxType[4, 0] = 2;
			instance.boxType[4, 2] = 2;
			instance.boxType[5, 0] = 2;
			instance.boxType[5, 1] = 2;
			instance.boxType[5, 2] = 2;
			instance.boxType[5, 3] = 2;
			instance.boxType[5, 4] = 2;
			instance.boxType[6, 4] = 2;
			instance.boxType[7, 0] = 2;
			instance.boxType[7, 1] = 2;
			instance.boxType[7, 2] = 2;
			instance.boxType[7, 4] = 2;
			instance.boxType[8, 0] = 2;
			instance.boxType[8, 2] = 2;
			instance.boxType[8, 3] = 2;
			instance.boxType[8, 4] = 2;
			instance.boxType[9, 0] = 2;
			instance.boxType[9, 4] = 2;
			break;
		}
	}

	public static int GetZombieRow(int level, int wave)
	{
		int result = 0;
		switch (level)
		{
		case 35:
			result = ((wave >= 20) ? ((Random.Range(0, 2) == 0) ? 1 : 4) : 4);
			break;
		case 36:
			result = ((wave >= 20) ? ((Random.Range(0, 2) != 0) ? 4 : 0) : 0);
			break;
		}
		return result;
	}
}
