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
	List<Vector2> waypoints = new List<Vector2>();
	Vector2 velocity;
	Vector2 goTo;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetMouseButtonDown(1)) {
			if(Input.GetKey("left shift")) {
				waypoints.Add(cam.ScreenToWorldPoint(Input.mousePosition));
			} else {
				waypoints.Clear();
				waypoints.Add(cam.ScreenToWorldPoint(Input.mousePosition));
			}
		}
		goTo = waypoints.Count > 0 ? waypoints[0] : goTo;
		dir = new Vector2(goTo.x-rb.position.x, goTo.y-rb.position.y);
		if(Vector2.Distance(rb.position, goTo) > 0.1) {
			velocity.x += Math.Sign(dir.x-velocity.x)*accel*Math.Abs(dir.x);
			velocity.y += Math.Sign(dir.y-velocity.y)*accel*Math.Abs(dir.y);
			if(velocity.magnitude > speed) {
				velocity.x = velocity.x/1.5f;
				velocity.y = velocity.y/1.5f;
			}
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		} else if( waypoints.Count > 0) {
			waypoints.RemoveAt(0);
		}
		transform.rotation = Quaternion.LookRotation(Vector3.forward, velocity);
    }
}
