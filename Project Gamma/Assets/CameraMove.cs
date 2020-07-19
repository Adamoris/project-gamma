using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public Rigidbody2D rb;
	public Camera cam;
	public float cameraSpeed;
	Vector2 vel = new Vector2(0, 0);
    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetMouseButton(2)) {
			Cursor.lockState = CursorLockMode.Locked;
			float horizontal = Input.GetAxis("Mouse X");
			float vertical = Input.GetAxis("Mouse Y");
			vel = new Vector2(horizontal, vertical);
		} else {
			Cursor.lockState = CursorLockMode.None;
		}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		rb.MovePosition(rb.position + vel * Time.deltaTime * cameraSpeed);
		vel = new Vector2(0, 0);
    }
}
