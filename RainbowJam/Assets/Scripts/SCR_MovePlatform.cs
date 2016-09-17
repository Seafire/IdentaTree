using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCR_MovePlatform : SCR_RaycastMaster 
{

	//Layer for passanger - this can allow platforms to only give access to both or one player as well as objects
	public LayerMask passengerMask;

	
	public float speed;
	public bool cycle;
	//public bool oneCycle;
	public float waitTime;
	[Range(0,4)]
	public float pulseDelay;
	//allows for multiple waypoints
	public Vector3[] localWaypoints;
	Vector3[] globalWaypoints;
	
	int fromWaypointIndex;
	float percentBetweenWaypoints;
	float nextMoveTime;

	//list to store the values of the contructor
	List<PassengerMovement> passengerMovement;
	//Used to remove the number of getComponent used
	//Dictionary<Transform, SCR_Raycasting2D> passengerDictionary = new Dictionary<Transform, SCR_Raycasting2D>();

	// Functions.
	//////////////////////////////////////////////////
	//                    Start                 	//
	//==============================================//
	// This function will be called for             //
	// initialisation.								//
	//////////////////////////////////////////////////
	//Allows both a start fuction in the MovePlatform and the RaycastMaster
	public override void Start () 
	{
		base.Start ();

		//setting each local waypoint to a global waypoint so the waypoints do not update when the application is playing
		globalWaypoints = new Vector3[localWaypoints.Length];
		for (int i =0; i < localWaypoints.Length; i++) 
		{
			globalWaypoints[i] = localWaypoints[i] + transform.position;
		}
	}
	
	//////////////////////////////////////////////////
	//             		Update              		//
	//==============================================//
	// Called every frame, we will check to see if  //
	// we are moving the object, if we are,			//
	// activate the base object activated flag for  //
	// the standard response.						//
	//////////////////////////////////////////////////
	void Update () 
	{
		//calls function from RaycastMaster
		UpdateRaycastOrigins ();
		
		Vector3 velocity = CalculatePlatformMovement();
		
		CalculatePassengerMovement(velocity);
		
		//MovePassengers (true);
		transform.Translate (velocity);
		//MovePassengers (false);
	}

	//////////////////////////////////////////////////
	//             		Delay	            		//
	//==============================================//
	// Allows a delay to be added 					//
	// to the moving platform						//
	//////////////////////////////////////////////////
	float Delay(float x) 
	{
		float a = pulseDelay + 1;
		return Mathf.Pow(x,a) / (Mathf.Pow(x,a) + Mathf.Pow(1-x,a));
	}

	//////////////////////////////////////////////////
	//          Calculate Platform Movement         //
	//==============================================//
	// Calculates the platfrom position 			//
	// every frame									//
	//////////////////////////////////////////////////
	Vector3 CalculatePlatformMovement() 
	{
		
		if (Time.time < nextMoveTime) 
		{
			return Vector3.zero;
		}
		
		fromWaypointIndex %= globalWaypoints.Length;
		int toWaypointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;
		float distanceBetweenWaypoints = Vector3.Distance (globalWaypoints [fromWaypointIndex], globalWaypoints [toWaypointIndex]);
		percentBetweenWaypoints += Time.deltaTime * speed/distanceBetweenWaypoints;
		percentBetweenWaypoints = Mathf.Clamp01 (percentBetweenWaypoints);
		float easedPercentBetweenWaypoints = Delay (percentBetweenWaypoints);
		
		Vector3 newPos = Vector3.Lerp (globalWaypoints [fromWaypointIndex], globalWaypoints [toWaypointIndex], easedPercentBetweenWaypoints);
		
		if (percentBetweenWaypoints >= 1) 
		{
			percentBetweenWaypoints = 0;
			fromWaypointIndex ++;
			
			if (cycle) 
			 {
				if (fromWaypointIndex >= globalWaypoints.Length-1) 
				{
					fromWaypointIndex = 0;
					System.Array.Reverse(globalWaypoints);
				}
			}

			if (!cycle)
			{
				if (fromWaypointIndex >= globalWaypoints.Length-1) 
				{
					speed = 0;
				}
			}
			nextMoveTime = Time.time + waitTime;
		}
		
		return newPos - transform.position;
	}

	//Anything that is being moved by the platform
/*	void MovePassengers(bool beforeMovePlatform)
	{
		foreach (PassengerMovement passenger in passengerMovement) 
		{
			if (!passengerDictionary.ContainsKey(passenger.transform))
			{
				passengerDictionary.Add(passenger.transform,passenger.transform.GetComponent<SCR_Raycasting2D>());
			}
			
			if (passenger.moveBeforePlatform == beforeMovePlatform)
			{
				passengerDictionary[passenger.transform].Move(passenger.velocity, passenger.standingOnPlatform);
			}
		}
	}*/
	//////////////////////////////////////////////////
	//          Calculate Passenger Movement        //
	//==============================================//
	// Calculates the position 						//
	// of the player on the platform				//
	//////////////////////////////////////////////////
	void CalculatePassengerMovement(Vector3 velocity) 
	{
		//checks the raycast to stop the passenger being moved multiple times ie interacting with multiple rays
		HashSet<Transform> movedPassengers = new HashSet<Transform> ();
		passengerMovement = new List<PassengerMovement> ();

		//object x and y speed moved by platform
		float directionX = Mathf.Sign (velocity.x);
		float directionY = Mathf.Sign (velocity.y);
		
		// Vertically moving platform
		if (velocity.y != 0) 
		{
			float rayLength = Mathf.Abs (velocity.y) + skinWidth;
			
			for (int i = 0; i < verticalRayCount; i ++)
			{
				Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
				rayOrigin += Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, passengerMask);

				//if collosion is dectected with the passenger mask Layer
				if (hit) 
				{
					//checks to stop the player being moved multiple times in the one frame
					if (!movedPassengers.Contains(hit.transform))
					{
						movedPassengers.Add(hit.transform);
						//sets the x-axis speed to 0
						float pushX = (directionY == 1)?velocity.x:0;
						//setting to the value of platform velocity along the y-axis
						float pushY = velocity.y - (hit.distance - skinWidth) * directionY;


						passengerMovement.Add(new PassengerMovement(hit.transform,new Vector3(pushX,pushY), directionY == 1, true));
					}
				}
			}
		}
		
		// Horizontally moving platform
		if (velocity.x != 0)
		{
			float rayLength = Mathf.Abs (velocity.x) + skinWidth;
			
			for (int i = 0; i < horizontalRayCount; i ++)
			{
				Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
				rayOrigin += Vector2.up * (horizontalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, passengerMask);

				//if collosion is dectected with the passenger mask Layer
				if (hit)
				{
					//checks to stop the player being moved multiple times in the one frame
					if (!movedPassengers.Contains(hit.transform))
					{
						movedPassengers.Add(hit.transform);
						//setting to the value of platform velocity along the x-axis
						float pushX = velocity.x - (hit.distance - skinWidth) * directionX;
						//sets the y-axis speed to 0
						//set to skinwidths to give it a small downforce - this allows the player to still jump
						float pushY = -skinWidth;

						//inposable that the player is standing on the platform
						passengerMovement.Add(new PassengerMovement(hit.transform,new Vector3(pushX,pushY), false, true));
					}
				}
			}
		}
		
		// detects if the player is on top of the moving platform
		if (directionY == -1 || velocity.y == 0 && velocity.x != 0) 
		{
			//small ray detection to see if anything is on top
			float rayLength = skinWidth * 2;
			
			for (int i = 0; i < verticalRayCount; i ++) 
			{
				//raycasting only casting up
				Vector2 rayOrigin = raycastOrigins.topLeft + Vector2.right * (verticalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, passengerMask);
				
				if (hit)
				{
					if (!movedPassengers.Contains(hit.transform)) 
					{
						//setting the speed of the passenger equal to to the player
						movedPassengers.Add(hit.transform);
						float pushX = velocity.x;
						float pushY = velocity.y;

						//player must be standing on the platform
						passengerMovement.Add(new PassengerMovement(hit.transform,new Vector3(pushX,pushY), true, false));
					}
				}
			}
		}
	}

	//stores information for the movement of the passenger and platform
	struct PassengerMovement 
	{
		//transform of the passenger
		public Transform transform;
		// desired velocity of the passenger
		public Vector3 velocity;
		//checks to see if the player is actually standing on the platform
		public bool standingOnPlatform;
		//check to see if the platform should be moved first
		public bool moveBeforePlatform;

		//public constructor
		public PassengerMovement(Transform _transform, Vector3 _velocity, bool _standingOnPlatform, bool _moveBeforePlatform)
		{
			transform = _transform;
			velocity = _velocity;
			standingOnPlatform = _standingOnPlatform;
			moveBeforePlatform = _moveBeforePlatform;
		}
	}

	//////////////////////////////////////////////////
	//             	  On Draw Gizmos              	//
	//==============================================//
	// This function is more of a benefit for the	//
	// person editing the prefab in the editor. 	//
	// This will draw a marker where the transform	//
	// position of this marker is given.			//
	// Allowing us to track where the empty game	//
	// object would be a lot easier.				//
	//////////////////////////////////////////////////
	void OnDrawGizmos()
	{
		//only draws if there is local waypoints
		if (localWaypoints != null) 
		{
			Gizmos.color = Color.red;
			float size = 0.5f;
			
			for (int i =0; i < localWaypoints.Length; i ++)
			{
				Vector3 globalWaypointPos = (Application.isPlaying)?globalWaypoints[i] : localWaypoints[i] + transform.position;
				Gizmos.DrawLine(globalWaypointPos - Vector3.up * size, globalWaypointPos + Vector3.up * size);
				Gizmos.DrawLine(globalWaypointPos - Vector3.left * size, globalWaypointPos + Vector3.left * size);
			}
		}
	}
}