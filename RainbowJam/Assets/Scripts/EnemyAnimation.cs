using UnityEngine;
using System.Collections;

// This Script has the purpose of making the enemy sprite go between white and red
public class EnemyAnimation : MonoBehaviour 
{
	
	public Color lerpedColor;
	public Color tmp;
//	Renderer render;


	// Use this for initialization
	void Start () 
	{
		lerpedColor = gameObject.GetComponent<SpriteRenderer>().color;

	}

	void Update() {
		//if (lerpedColor == Color.white)
	//	{
		tmp = Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1));

		lerpedColor = tmp;
		gameObject.GetComponent<SpriteRenderer> ().color = lerpedColor;


	
	//	}
	//	if (lerpedColor == Color.red)
	//	{
	//		lerpedColor = Color.Lerp(Color.red, Color.white, Mathf.PingPong(Time.time, 1));
	//	}

	}
}
