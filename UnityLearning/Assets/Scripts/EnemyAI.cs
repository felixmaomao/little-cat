using UnityEngine;
using System.Collections;

public class EnemyAI: MonoBehaviour
{
	private GameObject player;
	private GameObject enemy;
	private Animator anim;
	private AudioClip enemyAttackClip;
	// Use this for initialization
	void Awake ()
	{
		enemy = GameObject.Find ("enemy");
		player = GameObject.Find ("player");
		anim = enemy.GetComponent<Animator> ();
		enemyAttackClip = Resources.Load ("enemyAttack") as AudioClip;
	}
	
	// Update is called once per frame
	void Update ()
	{
		anim.SetInteger ("enemyBlood",100);
		if (Vector2.Distance (enemy.transform.position, player.transform.position) < 5) {
			anim.SetBool ("playerSeen", true);
			enemy.transform.localPosition = Vector2.MoveTowards (enemy.transform.localPosition,new Vector2(player.transform.localPosition.x,-2.3f),0.01f);
			AudioSource audioSource =enemy.GetComponent<AudioSource>();
			audioSource.clip = enemyAttackClip;
			audioSource.time = 0;
			audioSource.Play ();
		} else {
			anim.SetBool ("playerSeen", false);
			enemy.GetComponent<AudioSource> ().Stop ();
		}
	}
}

