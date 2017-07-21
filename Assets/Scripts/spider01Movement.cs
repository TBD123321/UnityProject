using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider01Movement : MonoBehaviour {
	
	private GameObject player;
	private Animator anim;
	private bool detectedForMove, detectedForAttack, isMoving;
    //private float moveX, moveY, lastMoveX, lastMoveY;
    private Vector2 move, lastMove;

	public float speed;
	public int spiderDamage;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");	
		anim = GetComponent<Animator> ();
	}
	

	void Update () {
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
        }
	}

	private void CalculateAngleForAnimMove(Vector2 slimePos, Vector2 playerPos)
	{
		float angleBetween = AngleBetweenVector2 (slimePos, playerPos);

		//anim.ResetTrigger ("MoveLeft");
		//anim.ResetTrigger ("MoveDown");
		//anim.ResetTrigger ("MoveRight");
		//anim.ResetTrigger ("MoveUp");

		if (angleBetween >= 45 && angleBetween < 135)
		{
            //anim.SetTrigger ("MoveUp");
            //lastMove = LastMove.MoveUp;
            move = new Vector2(0f, 1f);
            lastMove = new Vector2(0f, 1f);
            //moveY = 1f;
            //lastMoveY = 1f;
            //anim.SetFloat("MoveY", moveY);
		}
		else if (angleBetween >= 135 || angleBetween < -135)
		{
            //anim.SetTrigger ("MoveLeft");
            //lastMove = LastMove.MoveLeft;
            move = new Vector2(-1f, 0f);
            lastMove = new Vector2(-1f, 0f);
            //moveX = -1f;
            //lastMoveX = -1f;
            //anim.SetFloat("MoveX", moveX);
        }
		else if (angleBetween >= -135 && angleBetween < -45)
		{
            //anim.SetTrigger ("MoveDown");
            //lastMove = LastMove.MoveDown;
            move = new Vector2(0f, -1f);
            lastMove = new Vector2(0f, -1f);
            //moveY = -1f;
            //lastMoveY = -1f;
            //anim.SetFloat("MoveY", moveY);
        }
		else if (angleBetween >= -45 && angleBetween < 45)
		{
            //anim.SetTrigger ("MoveRight");
            //lastMove = LastMove.MoveRight;
            move = new Vector2(1f, 0f);
            lastMove = new Vector2(1f, 0f);
            //moveX = 1f;
            //lastMoveX = 1f;
            //anim.SetFloat("MoveX", moveX);
        }
	}

    private void CalculateAngleForAnimIdle(Vector2 slimePos, Vector2 playerPos) {
        float angleBetween = AngleBetweenVector2(slimePos, playerPos);

        //anim.ResetTrigger("IdleLeft");
        //anim.ResetTrigger("IdleDown");
        //anim.ResetTrigger("IdleRight");
        //anim.ResetTrigger("IdleUp");

        if (angleBetween >= 45 && angleBetween < 135) {
            //anim.SetTrigger("IdleUp");
            //lastMove = LastMove.MoveUp;
            lastMove = new Vector2(0f, 1f);
            //anim.SetFloat("LastMoveY", lastMoveY);
        }
        else if (angleBetween >= 135 || angleBetween < -135) {
            //anim.SetTrigger("IdleLeft");
            //lastMove = LastMove.MoveLeft;
            lastMove = new Vector2(-1f, 0f);
            //lastMoveX = -1f;
            //anim.SetFloat("LastMoveX", lastMoveX);
        }
        else if (angleBetween >= -135 && angleBetween < -45) {
            //anim.SetTrigger("IdleDown");
            //lastMove = LastMove.MoveDown;
            lastMove = new Vector2(0f, -1f);
            //lastMoveY = -1f;
            //anim.SetFloat("LastMoveY", lastMoveY);
        }
        else if (angleBetween >= -45 && angleBetween < 45) {
            //anim.SetTrigger("IdleRight");
            //lastMove = LastMove.MoveRight;
            lastMove = new Vector2(1f, 0f);
            //lastMoveX = 1f;
            //anim.SetFloat("LastMoveX", lastMoveX);
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
        //anim.ResetTrigger ("IdleDown");
        //anim.ResetTrigger ("IdleUp");
        //anim.ResetTrigger ("IdleLeft");
        //anim.ResetTrigger ("IdleRight");
        anim.SetTrigger("IsMoving");
		detectedForMove = true;
	}
	private void UndetectedPlayerForMove()
	{
		detectedForMove = false;
        //anim.ResetTrigger ("MoveLeft");
        //anim.ResetTrigger ("MoveDown");
        //anim.ResetTrigger ("MoveRight");
        //anim.ResetTrigger ("MoveUp");
        anim.ResetTrigger("IsMoving");
        //if(lastMove.y == 1f)
        //{
        //	anim.SetTrigger ("IdleDown");
        //}
        //if(lastMove.x == -1f)
        //{
        //	anim.SetTrigger ("IdleUp");
        //}
        //if(lastMove.y == -1f)
        //{
        //	anim.SetTrigger ("IdleLeft");
        //}
        //if(lastMove.x == 1f)
        //{
        //	anim.SetTrigger ("IdleRight");
        //}
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



    //void OnCollisionEnter2D(Collision2D other) {

    //	if (other.gameObject.CompareTag("Player")) 
    //	{
    //		player.SendMessage ("Event_Damaged",spiderDamage);
    //	}
    //}
}
