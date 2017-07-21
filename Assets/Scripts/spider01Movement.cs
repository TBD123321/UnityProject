using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider01Movement : MonoBehaviour {
	
	private GameObject player;
	private Animator anim;
	private bool moveUp, detectedForMove = false, detectedForAttack = false;
    private enum LastMove {MoveLeft,MoveDown,MoveRight,MoveUp};
	private LastMove lastMove;

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
			Vector2 slimePos = transform.position;
			Vector2 playerPos = player.transform.position;
			CalculateAngleForAnim (slimePos, playerPos);
		}
        if (detectedForAttack) {

        }
	}

	private void CalculateAngleForAnim(Vector2 slimePos, Vector2 playerPos)
	{
		float angleBetween = AngleBetweenVector2 (slimePos, playerPos);

		anim.ResetTrigger ("MoveLeft");
		anim.ResetTrigger ("MoveDown");
		anim.ResetTrigger ("MoveRight");
		anim.ResetTrigger ("MoveUp");

		if (angleBetween >= 45 && angleBetween < 135)
		{
			anim.SetTrigger ("MoveUp");

			lastMove = LastMove.MoveUp;
		}
		else if (angleBetween >= 135 || angleBetween < -135)
		{
			anim.SetTrigger ("MoveLeft");
			lastMove = LastMove.MoveLeft;
		}
		else if (angleBetween >= -135 && angleBetween < -45)
		{
			anim.SetTrigger ("MoveDown");
			lastMove = LastMove.MoveDown;
		}
		else if (angleBetween >= -45 && angleBetween < 45)
		{
			anim.SetTrigger ("MoveRight");
			lastMove = LastMove.MoveRight;
		}
	}

	private void CalculateAngleForAnimIdle(Vector2 slimePos, Vector2 playerPos)
	{
		float angleBetween = AngleBetweenVector2 (slimePos, playerPos);

		anim.ResetTrigger ("MoveLeft");
		anim.ResetTrigger ("MoveDown");
		anim.ResetTrigger ("MoveRight");
		anim.ResetTrigger ("MoveUp");

		if (angleBetween >= 45 && angleBetween < 135)
		{
			anim.SetTrigger ("MoveUp");
		}
		else if (angleBetween >= 135 || angleBetween < -135)
		{
			anim.SetTrigger ("MoveLeft");
		}
		else if (angleBetween >= -135 && angleBetween < -45)
		{
			anim.SetTrigger ("MoveDown");
		}
		else if (angleBetween >= -45 && angleBetween < 45)
		{
			anim.SetTrigger ("MoveRight");
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
		anim.ResetTrigger ("IdleDown");
		anim.ResetTrigger ("IdleUp");
		anim.ResetTrigger ("IdleLeft");
		anim.ResetTrigger ("IdleRight");
		detectedForMove = true;
	}
	private void UndetectedPlayerForMove()
	{
		detectedForMove = false;
		anim.ResetTrigger ("MoveLeft");
		anim.ResetTrigger ("MoveDown");
		anim.ResetTrigger ("MoveRight");
		anim.ResetTrigger ("MoveUp");
		if(lastMove == LastMove.MoveDown)
		{
			anim.SetTrigger ("IdleDown");
		}
		if(lastMove == LastMove.MoveUp)
		{
			anim.SetTrigger ("IdleUp");
		}
		if(lastMove == LastMove.MoveLeft)
		{
			anim.SetTrigger ("IdleLeft");
		}
		if(lastMove == LastMove.MoveRight)
		{
			anim.SetTrigger ("IdleRight");
		}
	}

    private void DetectedPlayerForAttack() {
        UndetectedPlayerForMove();
    }

    private void UndetectedPlayerForAttack() {
        detectedForMove = true;
        detectedForAttack = false;
    }



    //void OnCollisionEnter2D(Collision2D other) {

    //	if (other.gameObject.CompareTag("Player")) 
    //	{
    //		player.SendMessage ("Event_Damaged",spiderDamage);
    //	}
    //}
}
