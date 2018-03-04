using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [Range(1000.0f, 3000.0f)]
    public float acceleration = 2000f;
    [Range(0.0f, 1.0f)]
    public float dragConstant = 0.02f;
    [Range(0.0f, 0.1f)]
    public float frictionConstant = 0.01f;
    [Range(0.0f, 1.0f)]
    public float brakingConstant = 0.2f;
    [Range(0.0f, 1.0f)]
    public float handbrakingConstant = 0.7f;
    [Range(0.0f, 1.0f)]
    public float distanceBetweenAxles = 0.3f;
    [Range(0.0f, 45.0f)]
    public float turningAngle = 15f;


    [Range(1.0f, 5.0f)]
    public float nitroPower = 2f;
    [Range(0.0f, 1.0f)]
    public float nitroTank = 1f;

    private Rigidbody2D rb2d;

    private int cheatsheetsCaught = 0;


    public Sprite sprite;

    private float throttle; // 1 forward; negative backwards; 0 static
    private int turn; // 1 left; -1 right; 0 none
    private int handbrake; // 1 on; 0 off
    private bool nitro; // 1 on; 0 off

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void Update()
    {
        // Throttle controls
        if (Input.GetKey(KeyCode.UpArrow))
        {
            throttle = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            throttle = -brakingConstant;
        }
        else
        {
            throttle = 0;
        }

        // Turning controls
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            turn = 1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            turn = -1;
        }
        else
        {
            turn = 0;
        }

        // Handbrake control
        if (Input.GetKey(KeyCode.Space))
        {
            handbrake = 1;
        }
        else
        {
            handbrake = 0;
        }


        // nitro control
        if (Input.GetKey(KeyCode.RightControl) && nitroTank > 0)
        {
            nitro = true;
        }
        else
        {
            nitro = false;
        }
    }

    void FixedUpdate()
    {
        // Longitudinal forces
        Vector2 tractionForce = rb2d.transform.up * acceleration * throttle;
        Vector2 dragForce = -dragConstant * rb2d.velocity * rb2d.velocity.magnitude;
        Vector2 frictionForce = -frictionConstant * rb2d.velocity;
        Vector2 brakingForce = -handbrakingConstant * Vector2.Dot(rb2d.transform.up, rb2d.velocity.normalized) * rb2d.transform.up * acceleration * handbrake;


        if (nitro)
        {
            tractionForce *= nitroPower;
            nitroTank -= 0.05f;
        }

        Vector2 longitudinalForce;
        if (handbrake == 1)
        {
            longitudinalForce = brakingForce + dragForce + frictionForce;
        }
        else
        {
            longitudinalForce = tractionForce + dragForce + frictionForce;
        }

        rb2d.AddForce(longitudinalForce);

        // Turning
        float turnRadius = distanceBetweenAxles / Mathf.Sin(Mathf.Deg2Rad * turningAngle * turn);
        rb2d.angularVelocity = rb2d.velocity.magnitude / turnRadius;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "PowerUp")
        {
            Destroy(other.gameObject);
            this.nitroTank += 0.2f;
        } else if (other.gameObject.tag == "Cheatsheet")
        {
            Destroy(other.gameObject);
            this.cheatsheetsCaught++;
        }
    }
}
