using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[AddComponentMenu("Pooling/PoolManager")]
public class PoolManager : MonoBehaviour {

	public List<PooledPrefabEntry> pooledPrefabs; 

	private Dictionary<string, PrefabPool> pools;

	void Awake() 
	{
		pools = new Dictionary<string, PrefabPool>();

		if (pooledPrefabs == null)
			pooledPrefabs = new List<PooledPrefabEntry>();

		BuildPools();
	}

	private void BuildPools()
	{
		foreach(PooledPrefabEntry entry in pooledPrefabs)
		{
			AddPool (entry.prefab, entry.poolSize);
		}
	}

	public void AddPool(GameObject prefab, int poolSize = 32) 
	{
		if (pools.ContainsKey(prefab.name)) 
		{
			Debug.LogWarning(String.Format("Pool for GameObject of name {0} already exists!", prefab.name));
			return;
		}

		var pool = new PrefabPool(prefab, poolSize);
		pools.Add(prefab.name, pool);
	}

	public GameObject SpawnObjectByPrefab(GameObject prefab) 
	{
		return SpawnObjectByName(prefab.name);
	}

	public GameObject SpawnObjectByName(string prefabName) 
	{
		if (!pools.ContainsKey(prefabName)) 
		{
			Debug.LogWarning(String.Format("GameObject with name {0} is not managed by this PoolManager!", prefabName));
			return null;
		}
		
		return pools[prefabName].Spawn();
	}

	public void DespawnObject(GameObject gameObject) 
	{
		if (!pools.ContainsKey(gameObject.name))
		{
			Debug.LogWarning(String.Format("GameObject with name {0} is not managed by this PoolManager and cannot be despawned!", gameObject.name));
			return;
		}

		pools[gameObject.name].Despawn(gameObject);
	}
}
