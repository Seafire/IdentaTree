using UnityEngine;
using System.Collections;

public class DestroyBullet : MonoBehaviour 
{
	public float timer = 5.0f;
	public enum bulletType {playerBullet, rainDrop};
	public bulletType bullet;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer -= Time.deltaTime;

		if (timer <= 0.0f)
		{
			Destroy(gameObject);
		}

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Enemy") 
		{
			if (bullet == bulletType.playerBullet) 
			{
				Destroy (gameObject);
			}
		}
		if (coll.tag == "Player") 
		{
			if (bullet == bulletType.rainDrop) 
			{
				Destroy (gameObject);
			}
		}
	}
}
