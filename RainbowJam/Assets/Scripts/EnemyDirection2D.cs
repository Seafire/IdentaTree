using UnityEngine;
using System.Collections;

public class EnemyDirection2D : MonoBehaviour 
{

	private Transform treePos;

	public float rotSpeed = 180.0f;

	//public GameObject[] respawnPoint;

	// Use this for initialization
	void Start () 
	{
		//startPos = transform;
		//startPos = transform.position;


	}
	
	// Update is called once per frame
	void Update () 
	{
		if (treePos == null) 
		{
			GameObject tree = GameObject.Find ("IdentaTree");

			if(tree != null)
			{
				treePos = tree.transform;
			}
		}

		//if (treePos = null)
		//	return;				// Try again

		// We are now sure the tree is in the scene

		Vector3 dir = treePos.position - transform.position;
		//if (Mathf.Abs (dir) < 5.0f) 
		//{

		//}
		dir.Normalize ();
		float facing = dir.x;
		FlipSprite (facing);
		float zAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;

		transform.rotation = Quaternion.Euler (0, 0, zAngle);

	}

	public void FlipSprite(float facing)
	{
		// Check to see if input is disabled
		
		// If we are moving to the right.
		if (facing > 0f) 
		{
			// Swapping the sprite around.
			Vector3 scale = transform.localScale;
			
			// Reversing the scale.
			scale.x = 1.0f;
			
			// Setting the new scale for the sprite.
			transform.localScale = scale;
		} 
		// Otherwise, if we are moving to the left.
		else if (facing < 0f) 
		{
			// Swapping the sprite around.
			Vector3 scale = transform.localScale;
			
			// Reversing the scale.
			scale.x = -1.0f;
			
			// Setting the new scale for the sprite.
			transform.localScale = scale;
		}
	}

	protected void Flip()
	{

	}

}
