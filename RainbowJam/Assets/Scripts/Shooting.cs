using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour 
{
	public GameObject loveBomb;
	public float fireDelay = 0.5f;
	private float coolDownTimer = 0.0f;

	private PlayerMovement2D playerShoot;

	// Use this for initialization
	void Start () 
	{
		playerShoot = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMovement2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		coolDownTimer -= Time.deltaTime;
	
		if (playerShoot.shoot)
		{
			playerShoot.shootTimer -= Time.deltaTime;

			if (playerShoot.shootTimer > 0.0f)
			{
				if (Input.GetButton ("Fire2") && coolDownTimer <= 0.0f) 
				{
					coolDownTimer = fireDelay;

					Instantiate (loveBomb, transform.position, transform.rotation);
				}
			}
			else
			{
				Debug.Log("I made it");
				playerShoot.shoot = false;
				playerShoot.poweredUpPlayer = false;
				//playerShoot.shootTimer = 0.0f;
			}
		}
	
	}
}
