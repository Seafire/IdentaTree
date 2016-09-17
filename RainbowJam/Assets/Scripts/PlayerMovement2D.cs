using UnityEngine;
using System.Collections;

public class PlayerMovement2D : MonoBehaviour 
{
	public Sprite playerDown;
	public Sprite playerDownCarrying;
	public Sprite playerUp;
	public Sprite playerUpCarrying;

	public float maxSpeed = 20.0f;				// Sets the max speed for the player
	public float rotSpeed = 180.0f;				// Sets the rotation speed for the player

	private float playerBoundsRad = 1.75f;				// To stop the player going off the screen
	[HideInInspector] public bool poweredUpPlayer = false;
	[HideInInspector] public bool pickUpPlayer = false;
	[HideInInspector] public bool curPickUp = false;
	[HideInInspector] public bool shoot = false;
	[HideInInspector] public float shootTimer = 0.0f;
	[HideInInspector] public bool rainDropHit = false;
	private bool pickUpRepeat = false;
	private int isHappy, isCreative, isLove, isStylish = 0;
	private PickUp pickUp;
	private EnemyRespawn2D enemyKill;
	private PowerUp powerUp;
	private PauseScreen pauseScreen;
	private Score score;
	private float delayTime = 0.1f;
	private SpriteRenderer playerSprite;
	private GameObject eyes;


	private float yOld = 0.0f;
	private float yCurrent = 0.0f;


	private PickUp.SeedList currentSeed;

	// Use this for initialization
	void Start () 
	{
		
		pauseScreen = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<PauseScreen>();
		playerSprite = GameObject.Find ("PlayerSprite").GetComponent<SpriteRenderer> ();
		eyes = GameObject.Find ("Eyes Open");
		//pickUp = GameObject.FindGameObjectWithTag("PowerUp").GetComponent<PickUp>();
		//enemyKill = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyRespawn2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (pauseScreen.gamePaused == false)
		{
			//ROTATE
			// Set the variable to the current rotation values
			//Quaternion rot = transform.rotation;
			// Calculate the rotation of the z-axis
			//float z = rot.eulerAngles.z;
			// Alter the z value with input and the max rotation variable over time
			//z -= Input.GetAxis ("Horizontal") * rotSpeed * Time.deltaTime;
			// Set the rotation value by passing though all changes
			//rot = Quaternion.Euler (0, 0, z);
			// Set the objects rotation to the new rotation calculated
			//transform.rotation = rot;

			//Move the player
			//Vector3 pos = transform.position;
			//Vector3 velocity = new Vector3(0, Input.GetAxis ("Vertical") * maxSpeed * Time.deltaTime, 0);
			//pos += rot * velocity;

			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");

			yCurrent = y;

			Vector3 pos = transform.position;
			Vector3 velocity = new Vector3(x * maxSpeed * Time.deltaTime, y * maxSpeed * Time.deltaTime, 0);
			pos += velocity;

			//if (x != 0)
			//{
				if (yCurrent < yOld)
				{
					FlipSprite(x);
				}
				else if (yCurrent > yOld)
				{
					FlipSprite(-x);
				}

				//if (yCurrent == yOld)
				//{
				//	FlipSprite(x);
				//	playerSprite.sprite = playerDown;
				//	eyes.gameObject.SetActive(true);
				//}
			//}


			if(y > 0)
			{
				FlipSprite(-x);
				eyes.gameObject.SetActive(false);
				if(poweredUpPlayer || pickUpPlayer)
				{
					playerSprite.sprite = playerUpCarrying;
				}
				else
				{
					playerSprite.sprite = playerUp;
				}
			}
			else if(y <= 0)
			{
				FlipSprite(x);
				eyes.gameObject.SetActive(true);
				if(poweredUpPlayer || pickUpPlayer)
				{
					playerSprite.sprite = playerDownCarrying;
				}
				else
				{
					playerSprite.sprite = playerDown;
				}

			}

			if (pos.y + playerBoundsRad > Camera.main.orthographicSize) 
			{
				pos.y = Camera.main.orthographicSize - playerBoundsRad;
			}
			if (pos.y - playerBoundsRad < -Camera.main.orthographicSize) 
			{
				pos.y = -Camera.main.orthographicSize + playerBoundsRad;
			}

			// Calculate the ratio of the main camera
			float screenRatio = (float)Screen.width / (float)Screen.height;
			//Using the ratio calculate the the camera max width
			float cameraWidth = Camera.main.orthographicSize * screenRatio;

			if (pos.x + playerBoundsRad > cameraWidth) 
			{
				pos.x = cameraWidth - playerBoundsRad;
			}
			if (pos.x - playerBoundsRad < -cameraWidth) 
			{
				pos.x = -cameraWidth + playerBoundsRad;
			}

			transform.position = pos;

			if (shoot == false)
			{
				NowShooting();
			}

			yOld = y;
		}

		if (rainDropHit && pickUpPlayer) 
		{

			rainDropHit = false;
			pickUp.pickUpUsed = true;
			pickUpPlayer = false;
			curPickUp = false;
		}
	}

	void WhatSeed()
	{
		switch (currentSeed) 
		{
		case PickUp.SeedList.Creative:
			isCreative ++;
			break;
		case PickUp.SeedList.Happy:
			isHappy ++;
			break;
		case PickUp.SeedList.Love:
			isLove ++;
			break;
		case PickUp.SeedList.Stylish:
			isStylish ++;
			break;
		}
	}

	void RemovePickUp()
	{
		if (!curPickUp)
		{
			delayTime -= Time.deltaTime;
			if (delayTime < 0.0f)
			{
				Debug.Log ("I'm here help");
				delayTime = 0.1f;
			}
		}
	}


	void OnTriggerEnter2D(Collider2D coll)
	{


		if (!poweredUpPlayer && !pickUpPlayer)
		{
			if(coll.tag == "PowerUpPeanut")
			{
				powerUp = coll.GetComponent <PowerUp> ();
				poweredUpPlayer = true;
				powerUp.powerUpUsed = false;
				shoot = true;
				shootTimer = 4.0f;
			}
		}
		if (!pickUpPlayer && !shoot)
		{
			if(coll.tag == "PickUp" && !poweredUpPlayer && !curPickUp)
			{
				Debug.Log ("I have made it into the Pick up");
				pickUp = coll.GetComponent <PickUp> ();
				curPickUp = true;
				pickUpPlayer = true;
			}
		}
		
		if (pickUpPlayer && !pickUpRepeat) 
		{
			if(coll.tag == "Tree")
			{

				score = coll.GetComponent <Score> ();
				
				if(pickUp.pickedUpBefore == 1)
				{
					score.posScore +=100;
				}
				if(pickUp.pickedUpBefore > 1)
				{
					score.posScore +=50;
				}

				pickUp.pickUpUsed = true;
				pickUpPlayer = false;
				curPickUp = false;
			}
		}

		if (coll.tag == "RainDrop") 
		{
			Debug.Log ("Hey I'm here!!!");
			if (poweredUpPlayer || pickUpPlayer)
			{
				rainDropHit = true;
				pickUpPlayer = true;
			}
		}
	}

	void NowShooting()
	{
		//Debug.Log ("Should Destroy Power Up");
		//powerUp.powerUpUsed = true;
		//poweredUpPlayer = false;
	}

	void FlipSprite(float facing)
	{
		// Check to see if input is disabled
		
		// If we are moving to the right.
		if (facing < 0f) {
			// Swapping the sprite around.
			Vector3 scale = transform.localScale;
			
			// Reversing the scale.
			scale.x = 1.0f;
			
			// Setting the new scale for the sprite.
			transform.localScale = scale;
		} 
		// Otherwise, if we are moving to the left.
		else if (facing > 0f) {
			// Swapping the sprite around.
			Vector3 scale = transform.localScale;
			
			// Reversing the scale.
			scale.x = -1.0f;
			
			// Setting the new scale for the sprite.
			transform.localScale = scale;
		}
	}

}
