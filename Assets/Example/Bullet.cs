using UnityEngine;
using System.Collections;
using System;

public class Bullet : MonoBehaviour {

	private bool initialized = false;

	public float MoveSpeed;

	public float MoveAngle;

	public delegate void BulletDestroyedDelegate(Bullet bullet);

	public event BulletDestroyedDelegate OnBulletDestroyed;

	void Start () 
	{
		if (rigidbody2D != null) 
		{
			rigidbody2D.velocity = new Vector2(0f, MoveSpeed);
		}	

		initialized = true;
	}

	void Update () 
	{
		if (!renderer.isVisible && OnBulletDestroyed != null) 
		{
			OnBulletDestroyed(this);
		}
	}

	//OnEnable is called when we reactivate the game object
	void OnEnable() 
	{
		if (initialized)
			Start();
	}
}
