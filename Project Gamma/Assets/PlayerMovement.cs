using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Camera cam;
	public Rigidbody2D rb;
    // Update is called once per frame
    void Update()
    {
		rb.MovePosition(cam.ScreenToWorldPoint(Input.mousePosition));
    }
}
