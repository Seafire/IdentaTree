using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class SCR_RaycastMaster : MonoBehaviour 
{
	
	public LayerMask collisionMask;			// layer mask for the objects for raycasting to collide with
	
	public const float skinWidth = .015f;	//constant value to put in the ray origins within the box collider
	public int horizontalRayCount = 4;		//number of rays horizontally
	public int verticalRayCount = 4;		//number of rays vertically
	
	[HideInInspector]
	public float horizontalRaySpacing;		//the spacing between each ray horizontally
	[HideInInspector]
	public float verticalRaySpacing;		//the spacing between each ray vertically
	
	[HideInInspector]
	public BoxCollider2D collision;				//reference to box collider 2D
	public RaycastOrigins raycastOrigins;		//raycast origins reference
	
	public virtual void Start() 
	{
		collision = GetComponent<BoxCollider2D> ();		
		CalculateRaySpacing ();							//call function for initialization
	}

	//////////////////////////////////////////////////
	//             Update Raycast Origins          	//
	//==============================================//
	// This function will update     	            //
	// The raycast origins							//
	/////////////////////////////////////////////////
	public void UpdateRaycastOrigins() 
	{
		Bounds bounds = collision.bounds;											//gets the bounds of the collider
		bounds.Expand (skinWidth * -2);												// offsets the bounds so they are inside the box colldier
		
		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);		//sets the bottom left
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);		//sets the bottom right
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);			//sets the top left
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);			//sets the top right
	}

	//////////////////////////////////////////////////
	//             Calculate Ray Spacing         	//
	//==============================================//
	// This function will update     	            //
	// The raycast origins							//
	/////////////////////////////////////////////////
	public void CalculateRaySpacing() 
	{
		Bounds bounds = collision.bounds;											//gets the bounds of the collider
		bounds.Expand (skinWidth * -2);												// offsets the bounds so they are inside the box colldier
		
		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);		// makes sure that the horizontal ray count is greater than 2
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);			// makes sure that the vertical ray count is greater than 2
		
		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);			//calculate the horizontal ray spacing
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);				//calculate the vertical ray spacing
	}

	//////////////////////////////////////////////////
	//                    Raycast Origins          	//
	//==============================================//
	// This function will store			            //
	// each corner of the box collider				//
	//////////////////////////////////////////////////
	public struct RaycastOrigins 
	{
		public Vector2 bottomLeft, bottomRight;
		public Vector2 topLeft, topRight;
	}
}