using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PrefabPool {

	private int PoolSize { get; set; }

	private Stack<GameObject> Pooled { get; set; }

	private GameObject Prefab { get; set; }

	public PrefabPool(GameObject prefab, int poolSize = 32)
	{
		Pooled = new Stack<GameObject>(poolSize);
		PoolSize = poolSize;
		Prefab = prefab;
		RefillPool();
	}

	protected void RefillPool() 
	{
		for (int i = 0; i < PoolSize; i++)
		{
			var newGameObject = GameObject.Instantiate(Prefab) as GameObject;
			newGameObject.name = Prefab.name;
			newGameObject.SetActive(false);
			Pooled.Push(newGameObject);
		}
	}

	public GameObject Spawn() 
	{
		return Spawn(Vector3.zero, Quaternion.identity);
	}

	public GameObject Spawn(Vector3 position, Quaternion rotation) 
	{
		if (Pooled.Count <= 0) 
		{
			RefillPool();
		}

		var gameObject = Pooled.Pop();
		gameObject.SetActive(true);
		gameObject.transform.position = position;
		gameObject.transform.rotation = rotation;

		return gameObject;
	}

	public bool Despawn(GameObject gameObject)
	{
		if (!gameObject.name.Equals(Prefab.name))
		{
			Debug.LogWarning(String.Format("PrefabPoolManaged tried to despawn GameObject of name {0} but pools {1}", gameObject.name, Prefab.name));
			return false;
		}

		gameObject.SetActive(false);
		Pooled.Push(gameObject);

		return true;
	}
}
