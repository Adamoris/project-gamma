using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointRemoval : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "ship") {
			Object.Destroy(this.gameObject);
		}
	}
	// Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && !Input.GetKey("left shift")) {
			Object.Destroy(this.gameObject);
		}
    }
}
