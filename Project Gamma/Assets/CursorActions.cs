using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorActions : MonoBehaviour
{
	public Camera cam;
	public Rigidbody2D rb;
	public GameObject waypoint;
	
	private void makeWaypoint() {
		GameObject a = Instantiate(waypoint) as GameObject;
		a.transform.position = rb.position;
	}
    // Update is called once per frame
    void Update()
    {
		rb.MovePosition(cam.ScreenToWorldPoint(Input.mousePosition));
		if(Input.GetMouseButtonDown(1)) {
			makeWaypoint();
		}
    }
}
