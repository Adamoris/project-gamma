using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror
{
	public class MultiplayerMovement : NetworkBehaviour
	{
		public float speed;
		public float accel;
		public float turnSpeed;
		float currentspeed = 0;
		public Camera cam;
		public Rigidbody2D rb;
		public Vector2 dir;
		List<Vector2> waypoints = new List<Vector2>();
		Vector2 velocity;
		Vector2 goTo;
		float total;
		// Start is called before the first frame update
		public void Start()
		{
			cam = FindObjectOfType<Camera>();
		}

		void Update()
		{
			// movement for local player
			if (!isLocalPlayer)
				return;

			if (Input.GetMouseButtonDown(1))
			{
				if (Input.GetKey("left shift"))
				{
					waypoints.Add(cam.ScreenToWorldPoint(Input.mousePosition));
				}
				else
				{
					waypoints.Clear();
					waypoints.Add(cam.ScreenToWorldPoint(Input.mousePosition));
				}
			}
		}

		void FixedUpdate()
		{
			// movement for local player
			if (!isLocalPlayer)
				return;

			total = 0;
			for (int i = 0; i < waypoints.Count; i++)
			{
				if (i == 0)
				{
					total += Vector2.Distance(waypoints[i], rb.position);
				}
				else
				{
					total += Vector2.Distance(waypoints[i], waypoints[i - 1]);
				}
			}
			if (currentspeed != total)
			{
				currentspeed += Math.Sign(total - currentspeed) * accel;
			}
			if (currentspeed < 0f)
			{
				currentspeed = 0f;
			}
			else if (currentspeed > speed)
			{
				currentspeed = speed;
			}
			goTo = waypoints.Count > 0 ? waypoints[0] : goTo;
			dir = new Vector2(goTo.x - rb.position.x, goTo.y - rb.position.y);
			if (Vector2.Distance(rb.position, goTo) > 0.2)
			{
				velocity = dir;
				velocity.Normalize();
				velocity *= currentspeed;
				rb.MovePosition(rb.position + velocity * Time.deltaTime);
			}
			else if (waypoints.Count > 0)
			{
				waypoints.RemoveAt(0);
			}
			else
			{
				velocity.x = 0f;
				velocity.y = 0f;
			}

			if (velocity.magnitude != 0)
			{
				transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, velocity), Time.deltaTime * turnSpeed);
			}
		}
	}
}

