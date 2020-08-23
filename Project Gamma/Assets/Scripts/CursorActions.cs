using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorActions : MonoBehaviour
{
	public Camera cam;
	public Rigidbody2D rb;
	public GameObject waypoint;
	
	private void makeWaypoint() {
		GameObject point = Instantiate(waypoint);
		point.transform.position = rb.position;
	}

    private void Start()
    {
		Cursor.visible = false;
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
