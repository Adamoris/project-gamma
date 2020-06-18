using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVelocity : MonoBehaviour
{
	public TestingMove test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = test.rb.position;
		transform.eulerAngles = Vector3.forward * Vector2.Angle(Vector2.right, test.dir);
    }
}
