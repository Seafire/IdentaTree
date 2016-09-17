using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour 
{
	
	//private bool poweredUp = false;
	private PickUp pickUp;
	private EnemyRespawn2D enemyKill;

	protected PlayerMovement2D playerPower;
	
	public GameObject respawnHeader;
	private Transform[] respawnPoint;

	[HideInInspector] public bool powerUpUsed;
	protected bool powerUpDropped;
	private Animator anim;

	// Use this for initialization
	public void Start () 
	{

		powerUpUsed = false;
		powerUpDropped = false;
		respawnPoint = respawnHeader.GetComponentsInChildren<Transform>();
		playerPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement2D>();
		anim = gameObject.GetComponentInChildren <Animator> ();
	
	}
	
	// Update is called once per frame
	public void Update () 
	{
		if (powerUpUsed == true && !playerPower.shoot) 
		{
			anim.SetBool("isPickedUp", false);
			int sta = Random.Range(0, (respawnPoint.Length));
			transform.position = respawnPoint[sta].position;
			powerUpUsed = false;
		}
		
		if (powerUpDropped == true) 
		{
			
			int sta = Random.Range(0, (respawnPoint.Length));
			transform.position = respawnPoint[sta].position;
			powerUpDropped = false;
		}
		
	}

	void PickUpKill()
	{
		//delayTime -= Time.deltaTime;
		//if(delayTime < 0)
		//{
		if (playerPower.poweredUpPlayer)
		{
			Debug.Log("Hello darkness my old friend");
			anim.SetBool("isPickedUp", true);
			//curPickedUp = true;
		}
		//}
	}

	void OnTriggerStay2D(Collider2D coll)
	{	
		if (coll.tag == "Player") 
		{
			PickUpKill();
		}
	}


}
