using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider01Movement : MonoBehaviour {
	
	private GameObject player;
	private Animator anim;
	private bool detectedForMove, detectedForAttack, isMoving;
    private Vector2 move, lastMove;
    private bool canShoot = true;

    public GameObject firePos;
    public GameObject bullet;
    public float health = 100;
	public float speed;
	public int spiderDamage;
    public float attackSpeed;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");	
		anim = GetComponent<Animator> ();
	}
	

	void Update () {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
		if (detectedForMove) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, step);
			Vector2 spiderPos = transform.position;
			Vector2 playerPos = player.transform.position;
			CalculateAngleForAnimMove(spiderPos, playerPos);
            anim.SetFloat("MoveX", move.x);
            anim.SetFloat("MoveY", move.y);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
        }
        if (detectedForAttack) {
            Vector2 spiderPos = transform.position;
            Vector2 playerPos = player.transform.position;
            CalculateAngleForAnimIdle(spiderPos, playerPos);
            anim.SetFloat("MoveX", move.x);
            anim.SetFloat("MoveY", move.y);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
            if (canShoot)
            {
                firePos.transform.rotation = Quaternion.LookRotation(transform.forward, lastMove);
                Instantiate(bullet, firePos.transform.position, firePos.transform.rotation);
            }
        }
	}

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(attackSpeed);
        canShoot = true;
    }


    private void CalculateAngleForAnimMove(Vector2 slimePos, Vector2 playerPos)
	{
		float angleBetween = AngleBetweenVector2 (slimePos, playerPos);

		if (angleBetween >= 45 && angleBetween < 135)
		{
            move = new Vector2(0f, 1f);
            lastMove = new Vector2(0f, 1f);
		}
		else if (angleBetween >= 135 || angleBetween < -135)
		{
            move = new Vector2(-1f, 0f);
            lastMove = new Vector2(-1f, 0f);
        }
		else if (angleBetween >= -135 && angleBetween < -45)
		{
            move = new Vector2(0f, -1f);
            lastMove = new Vector2(0f, -1f);
        }
		else if (angleBetween >= -45 && angleBetween < 45)
		{
            move = new Vector2(1f, 0f);
            lastMove = new Vector2(1f, 0f);
        }
	}

    private void CalculateAngleForAnimIdle(Vector2 slimePos, Vector2 playerPos) {
        float angleBetween = AngleBetweenVector2(slimePos, playerPos);

        if (angleBetween >= 45 && angleBetween < 135) {
            lastMove = new Vector2(0f, 1f);
        }
        else if (angleBetween >= 135 || angleBetween < -135) {
            lastMove = new Vector2(-1f, 0f);
        }
        else if (angleBetween >= -135 && angleBetween < -45) {
            lastMove = new Vector2(0f, -1f);
        }
        else if (angleBetween >= -45 && angleBetween < 45) {
            lastMove = new Vector2(1f, 0f);
        }

    }

    private float  AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
	{
		Vector2 diference = vec2 - vec1;
		float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
		return Vector2.Angle(Vector2.right, diference) * sign;
	}

	private void DetectedPlayerForMove()
	{
        anim.SetTrigger("IsMoving");
		detectedForMove = true;
	}
	private void UndetectedPlayerForMove()
	{
		detectedForMove = false;
        anim.ResetTrigger("IsMoving");
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }

    private void DetectedPlayerForAttack() {
        UndetectedPlayerForMove();
        detectedForAttack = true;
    }

    private void UndetectedPlayerForAttack() {
        DetectedPlayerForMove();
        detectedForAttack = false;
    }

    private void Event_GetDamage(int damage)
    {
        health -= damage;
    }



    //void OnCollisionEnter2D(Collision2D other) {

    //	if (other.gameObject.CompareTag("Player")) 
    //	{
    //		player.SendMessage ("Event_Damaged",spiderDamage);
    //	}
    //}
}
