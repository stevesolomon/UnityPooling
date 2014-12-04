using UnityEngine;
using System.Collections;

public class BulletSpawner : MonoBehaviour {

	public PoolManager poolManager;

	public float timeBetweenSpawns;

	// Use this for initialization
	void Start () {

		if (poolManager == null) 
		{
			poolManager = GameObject.Find("PoolManager").GetComponent<PoolManager>();
		}

		if (timeBetweenSpawns < 0.5f) 
		{
			timeBetweenSpawns = 0.5f;
		}

		StartCoroutine(SpawnBullets(timeBetweenSpawns));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnBulletDestroyed(Bullet bullet)
	{
		bullet.OnBulletDestroyed -= OnBulletDestroyed;
		poolManager.DespawnObject(bullet.gameObject);
	}

	IEnumerator SpawnBullets(float waitTime) 
	{
		while (true)
		{
			var bullet = poolManager.SpawnObjectByName("Bullet");
			bullet.GetComponent<Bullet>().OnBulletDestroyed += OnBulletDestroyed;

			yield return new WaitForSeconds(waitTime);
		}
	}
}
