using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon_Control : MonoBehaviour
{
	Animator Main_Animator;
	Transform Player;
	public Transform balloon_explode;
	public bool balloon_shake;
	public float MoveSpeed;
	void Start ()
	{
		Player = gameObject.transform;
		Main_Animator = Player.GetComponent<Animator> ();
		MoveSpeed = 10f;
	}
	void Update ()
	{
		Main_Animator.SetBool ("shake", balloon_shake);
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			Player.eulerAngles = new Vector3 (0, 180, 0);
			Player.Translate (MoveSpeed * Time.deltaTime, 0, 0);
		}
		if (Input.GetKey (KeyCode.RightArrow)) 
		{  
			Player.eulerAngles = Vector3.zero; 
			Player.Translate (MoveSpeed * Time.deltaTime, 0, 0);
		} 
		if (Input.GetKey (KeyCode.UpArrow)) 
		{  
			Player.Translate (0, MoveSpeed * Time.deltaTime, 0);
		}       
	}
	void OnCollisionEnter2D (Collision2D c)
	{
		if (c.gameObject.name == "钉子阵") {
			balloon_shake = true;
		}
	}
	void OnCollisionExit2D (Collision2D c)
	{
		if (c.gameObject.name == "钉子阵") 
		{
			balloon_shake = false;
		}
	}
	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.gameObject.name == "橘子皮") 
		{
			Destroy (gameObject); 
			Instantiate (balloon_explode, this.transform.position, Quaternion.identity);       
		} 
	}
}
