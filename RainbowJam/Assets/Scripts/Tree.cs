using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour 
{
	private Score score;
	private Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = gameObject.GetComponent<Animator> ();
		score = gameObject.GetComponentInParent<Score> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (score.posScore == 200) 
		{
			anim.SetFloat("posScore", 200.0f);
		}
		else if (score.posScore == 300) 
		{
			anim.SetFloat("posScore", 300.0f);
		}
		else if (score.posScore == 400) 
		{
			anim.SetFloat("posScore", 400.0f);
		}
		else if (score.posScore == 500) 
		{
			anim.SetFloat("posScore", 500.0f);
		}
		else if (score.posScore == 600) 
		{
			anim.SetFloat("posScore", 600.0f);
		}
		else if (score.posScore == 700) 
		{
			anim.SetFloat("posScore", 700.0f);
		}
	}
}
