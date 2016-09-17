using UnityEngine;
using System.Collections;

public class RainDrop : MonoBehaviour 
{
	
	public GameObject rainDrop;
	public float fireDelay = 2.0f;
	private float coolDownTimer = 0.0f;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		coolDownTimer -= Time.deltaTime;
			
	if (coolDownTimer < 0.0f)
		{
			Instantiate (rainDrop, transform.position, transform.rotation);
			coolDownTimer = fireDelay;
		}
	}	
}
