using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AlternateShipMovement : NetworkBehaviour
{

    //Public Variables
    [SerializeField]
    bool networked;
    public CapsuleCollider2D capsuleCollider;

    [Header("Base Stats")]
    //public float acceleration = 3.0f;
    public float speed = 30f;
    public float turnRate = 3.0f;

    [Header("Boost Parameters")]
    public float boostDuration = 5.0f;
    public float boostCooldown = 30.0f;
    
    [Header("Runtime Information")]
    [ReadOnly]
    public float currentBoostDuration;
    [ReadOnly]
    public float currentBoostCooldown;
    [ReadOnly]
    public int moveSetting;
    [ReadOnly]
    public float speedLimit;

    //Physics Functions
    [Header("Physics Elements")]
    public Rigidbody2D body;

    private void OnValidate()
    {
        if (capsuleCollider == null)
        {
            capsuleCollider = GetComponent<CapsuleCollider2D>();
        }
    }

    void Turn(float force)
    {
        body.AddTorque(-force * turnRate);
    }

    void Strafe(float force)
    {
        Vector2 movement = new Vector2(force, 0);
        body.AddRelativeForce(movement * speed/2);
    }

    //Private Variables
    bool canBoost = true;

    void Start()
    {
        currentBoostCooldown = boostCooldown;
        currentBoostDuration = boostDuration;
        capsuleCollider.enabled = isServer;
    }

    private void Update()
    {
        if (networked && !isLocalPlayer)
            return;

        float horizontalMovement = Input.GetAxis("Horizontal");

        //Boosting
        if (moveSetting == 3 && currentBoostDuration > 0)
        {
            currentBoostDuration -= Time.deltaTime;
        }
        else if (currentBoostDuration <= 0)
        {
            if (moveSetting == 3)
            {
                moveSetting--;
            }
            
            currentBoostCooldown -= Time.deltaTime;
        }

        if (currentBoostCooldown <= 0)
        {
            currentBoostCooldown = boostCooldown;
            currentBoostDuration = boostDuration;
            canBoost = true;
        }

        //Turning
        if (horizontalMovement != 0.0f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Strafe(horizontalMovement);                
            }
            else
            {
                Turn(horizontalMovement);
            }
        }

        if (Input.GetKeyDown("w") && moveSetting < 3)
        {
            if ((canBoost && moveSetting == 2) || moveSetting < 2)
            {
                moveSetting++;
            }
            
            if (currentBoostDuration <= 0)
            {
                canBoost = false;
            }
        }
        else if (Input.GetKeyDown("s") && moveSetting > -1)
        {
            moveSetting--;
        }

        //Acceleration State
        if (moveSetting == -1) //Reverse
        {
            speedLimit = -speed / 2;
        }
        else if (moveSetting == 1) //Slow
        {
            speedLimit = speed / 2;
        }
        else if (moveSetting == 2) //Normal
        {
            speedLimit = speed;
        }
        else if (moveSetting == 3) //Boosted
        {
            speedLimit = speed * 2;
        }        
    }

    private void FixedUpdate()
    {
        if (networked && !isLocalPlayer)
            return;

        if (moveSetting != 0)
        {
            Vector2 movement = new Vector2(0, speedLimit);
            body.AddRelativeForce(movement);
        }
    }
}
