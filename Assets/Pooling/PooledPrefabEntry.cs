using System;
using UnityEngine;

[Serializable]
public class PooledPrefabEntry {

	[SerializeField]
	public GameObject prefab;

	[SerializeField]
	public int poolSize;
}
