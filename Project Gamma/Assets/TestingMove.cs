using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TestingMove : MonoBehaviour
{
    public float speed;
	public float accel;
	public Camera cam;
	public Rigidbody2D rb;
	public Vector2 dir;
	Vector2 goTo;
	Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetMouseButtonDown(1)) {
			goTo = cam.ScreenToWorldPoint(Input.mousePosition);
		}
		dir = new Vector2(goTo.x-rb.position.x, goTo.y-rb.position.y);
		if(Vector2.Distance(rb.position, goTo) > 0.1) {
			velocity.x += Math.Sign(dir.x-velocity.x)*accel*Math.Abs(dir.x);
			velocity.y += Math.Sign(dir.y-velocity.y)*accel*Math.Abs(dir.y);
			if(velocity.magnitude > speed) {
				velocity.x = velocity.x/1.5f;
				velocity.y = velocity.y/1.5f;
			}
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		}
    }
}
