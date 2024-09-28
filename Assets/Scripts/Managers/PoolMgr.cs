using UnityEngine;
using UnityEngine.Pool;

public class PoolMgr : MonoBehaviour
{
	public static PoolMgr Instance;

	private ObjectPool<GameObject> particle;

	private void Awake()
	{
		particle = new ObjectPool<GameObject>(CreateParticle, GetParticle, ReleaseParticle, DestoryParticle, collectionCheck: true, 10, 1000);
	}

	private GameObject CreateParticle()
	{
		return Object.Instantiate(GameAPP.particlePrefab[0], base.transform);
	}

	private void GetParticle(GameObject obj)
	{
		obj.SetActive(value: true);
	}

	private void ReleaseParticle(GameObject obj)
	{
		obj.SetActive(value: false);
	}

	private void DestoryParticle(GameObject obj)
	{
		Object.Destroy(obj);
	}
}
