using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour 
{
	private EnemyRespawn2D enemyKill;
	
	protected PlayerMovement2D playerPower;
	
	public GameObject respawnHeader;
	public enum SeedList {Happy, Love, Creative, Stylish};
	private Transform[] respawnPoint;
	
	[HideInInspector] public bool pickUpUsed;
	[HideInInspector] public int pickedUpBefore = 0;
	[HideInInspector] public bool curPickedUp;
	protected bool pickUpDropped;
	private Animator anim;
	private PlayerMovement2D player;
	
	// Use this for initialization
	void Start () 
	{
		
		pickUpUsed = false;
		pickUpDropped = false;
		curPickedUp = false;
		respawnPoint = respawnHeader.GetComponentsInChildren<Transform>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement2D>();
		anim = gameObject.GetComponentInChildren <Animator> ();

		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (pickUpUsed == true) 
		{
			anim.SetBool("isPickedUp", false);
			int sta = Random.Range(0, (respawnPoint.Length));
			Debug.Log (sta);
			transform.position = respawnPoint[sta].position;
			pickUpUsed = false;
			//curPickedUp = false;
		}
		
		if (pickUpDropped == true) 
		{
			anim.SetBool("isPickedUp", false);	
			int sta = Random.Range(0, (respawnPoint.Length));
			transform.position = respawnPoint[sta].position;
			pickUpDropped = false;
		}
		
	}

	void PickUpKill()
	{
		//delayTime -= Time.deltaTime;
		//if(delayTime < 0)
		//{
		if (!player.curPickUp && !player.shoot)
		{
			Debug.Log("Hello darkness my old friend");
			anim.SetBool("isPickedUp", true);
			pickedUpBefore ++;
			Debug.Log (pickedUpBefore);
			//curPickedUp = true;
		}
		//}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Player") 
		{
			PickUpKill();
		}

	}

}
