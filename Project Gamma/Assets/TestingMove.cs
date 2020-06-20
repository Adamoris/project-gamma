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
		cam = FindObjectOfType<Camera>();
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
			velocity.x += dir.x;
			velocity.y += dir.y;
			//velocity = dir;
			if(velocity.magnitude > speed) {
				velocity.Normalize();
				velocity *= speed;
			}
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		} else if( waypoints.Count > 0) {
			waypoints.RemoveAt(0);
		} else {
			velocity.x = 0f;
			velocity.y = 0f;
		}
		transform.rotation = Quaternion.LookRotation(Vector3.forward, velocity);
    }
}
