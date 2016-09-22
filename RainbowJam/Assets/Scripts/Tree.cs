using UnityEngine;
using System.Collections;

public class Tree : AudioMaster 
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
		if (score.totalScore == 200) 
		{
			anim.SetFloat("posScore", 200.0f);
		}
		else if (score.totalScore == 300) 
		{
			anim.SetFloat("posScore", 300.0f);
		}
		else if (score.totalScore == 400) 
		{
			anim.SetFloat("posScore", 400.0f);
		}
		else if (score.totalScore == 500) 
		{
			anim.SetFloat("posScore", 500.0f);
		}
		else if (score.totalScore == 600) 
		{
			anim.SetFloat("posScore", 600.0f);
		}
		else if (score.totalScore == 700) 
		{
			anim.SetFloat("posScore", 700.0f);
			PlayEvent ("Tree_Full");
		}
		else if (score.totalScore < 50 ) 
		{
			anim.SetFloat("posScore", 0.0f);
		}
	}
}
