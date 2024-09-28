using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Test : MonoBehaviour
{
	private ObjectPool<GameObject> pool;

	public GameObject circle;

	private List<GameObject> objlist = new List<GameObject>();

	private void Awake()
	{
		pool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, collectionCheck: true, 10, 1000);
	}

	private GameObject CreateFunc()
	{
		return Object.Instantiate(circle, base.transform);
	}

	private void ActionOnGet(GameObject obj)
	{
		obj.gameObject.SetActive(value: true);
	}

	private void ActionOnRelease(GameObject obj)
	{
		obj.gameObject.SetActive(value: false);
	}

	private void ActionOnDestory(GameObject obj)
	{
		Object.Destroy(obj);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameObject item = pool.Get();
			objlist.Add(item);
		}
		if (Input.GetMouseButtonDown(1) && objlist.Count > 0)
		{
			GameObject element = objlist[0];
			objlist.RemoveAt(0);
			pool.Release(element);
		}
	}
}
