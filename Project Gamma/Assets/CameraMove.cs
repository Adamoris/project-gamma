using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public Rigidbody2D rb;
	public Camera cam;
	public float cameraSpeed;
	public float dist;
	public float maxZoomIn;
	public float maxZoomOut;
	float defaultCameraSize;
	Vector2 vel = new Vector2(0, 0);
	void Start() {
		defaultCameraSize = cam.orthographicSize;
	}
    void Update()
    {
        if(Input.GetMouseButton(2)) {
			Cursor.lockState = CursorLockMode.Locked;
			float horizontal = Input.GetAxis("Mouse X");
			float vertical = Input.GetAxis("Mouse Y");
			vel = new Vector2(horizontal, vertical);
		} else {
			//bottom left of screen is 0,0
			Cursor.lockState = CursorLockMode.Confined;
			if(Input.GetKey("up") || Input.mousePosition.y > (Screen.height - dist)) {
				vel += Vector2.up;
			}
			if(Input.GetKey("down") || Input.mousePosition.y < dist) {
				vel += Vector2.down;
			}
			if(Input.GetKey("right") || Input.mousePosition.x > (Screen.width - dist)) {
				vel += Vector2.right;
			}
			if(Input.GetKey("left") || Input.mousePosition.x < dist) {
				vel += Vector2.left;
			}
			vel.Normalize();
		}
		if(Input.GetAxis("Mouse ScrollWheel") > 0f && cam.orthographicSize > maxZoomIn) {
			cam.orthographicSize--;
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0f && cam.orthographicSize < maxZoomOut) {
			cam.orthographicSize++;
		}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		rb.MovePosition(rb.position + vel * Time.deltaTime * cameraSpeed * cam.orthographicSize / defaultCameraSize);
		vel = new Vector2(0, 0);
    }
}
