using System.Collections.Generic;
using UnityEngine;

public class Present : Plant
{
	private readonly List<int> list = new List<int>();

	private readonly int basePlantNum = 19;

	protected override void Start()
	{
		base.Start();
		anim.Play("idle");
	}

	public void AnimEvent()
	{
		Object.Instantiate(GameAPP.particlePrefab[11], base.transform.position, Quaternion.identity).transform.SetParent(GameAPP.board.transform);
		Die();
		if (board.isEveStarted)
		{
			Board.Instance.SetEvePlants(thePlantColumn, thePlantRow);
		}
		else
		{
			RandomPlant();
		}
	}

	private void RandomPlant()
	{
		for (int i = 0; i < basePlantNum; i++)
		{
			list.Add(i);
		}
		int num = Random.Range(0, basePlantNum);
		while (CreatePlant.Instance.IsWaterPlant(num))
		{
			num = Random.Range(0, basePlantNum);
		}
		bool flag = false;
		GameObject[] plantArray = board.plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (!(gameObject != null))
			{
				continue;
			}
			Plant component = gameObject.GetComponent<Plant>();
			if (component.thePlantRow != thePlantRow || component.thePlantColumn != thePlantColumn || component.thePlantType == 12)
			{
				continue;
			}
			flag = true;
			int num2 = 1000;
			while (num2-- >= 0 && list.Count != 0)
			{
				int index = Random.Range(0, list.Count);
				num = list[index];
				if (MixData.data[component.thePlantType, num] != 0)
				{
					break;
				}
				list.RemoveAt(index);
			}
		}
		if (!flag && GameAPP.theBoardType == 1 && GameAPP.theBoardLevel == 39)
		{
			SuperRandomPlant();
			return;
		}
		if (num == 6)
		{
			CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, num);
			CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, num);
		}
		if (CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, num) == null)
		{
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, base.transform.position);
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, base.transform.position);
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, base.transform.position);
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, base.transform.position);
		}
	}

	private void SuperRandomPlant()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < GameAPP.plantPrefab.Length; i++)
		{
			if (GameAPP.plantPrefab[i] != null)
			{
				list.Add(i);
			}
		}
		int theSeedType = list[Random.Range(0, list.Count)];
		while (CreatePlant.Instance.IsWaterPlant(theSeedType))
		{
			theSeedType = list[Random.Range(0, list.Count)];
		}
		if (CreatePlant.Instance.IsPuff(theSeedType))
		{
			CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, theSeedType);
			CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, theSeedType);
		}
		if (CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, theSeedType, null, default(Vector2), isFreeSet: true) == null)
		{
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, base.transform.position);
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, base.transform.position);
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, base.transform.position);
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, base.transform.position);
		}
	}
}
