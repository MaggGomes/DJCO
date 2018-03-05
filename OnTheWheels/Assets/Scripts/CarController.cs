using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour {

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
	[Range(0.0f, 1.0f)]
	public float roadModifier = 1.0f;
	[Range(0.0f, 1.0f)]
	public float grassModifier = 0.8f;
	[Range(0.0f, 1.0f)]
	public float dirtModifier = 0.4f;

	[Range(1.0f, 5.0f)]
	public float nitroPower = 2f;
	[Range(0.0f, 1.0f)]
	public float nitroTank = 1f;

   
    [Range(0.0f, 1.0f)]
    public float damage = 0f;
    [Range(0.0f, 1.0f)]
    public float resistance = 1f;

    public Sprite sprite;
    public bool playerControlled = true;
    public bool isCop = false;
    private Transform Player;

    // INPUT VARIABLES
    public KeyCode throttleKey = KeyCode.UpArrow;
    public KeyCode brakeKey = KeyCode.DownArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow;
    public KeyCode handbrakeKey = KeyCode.Space;
    public KeyCode nitroKey = KeyCode.RightControl;
    

    private Rigidbody2D rb2d;
    private SpriteRenderer brokenSprite;
    private float throttle; // 1 forward; negative backwards; 0 static
	private int turn; // 1 left; -1 right; 0 none
	private int handbrake; // 1 on; 0 off
	private int nitro; // 1 on; 0 off
	private float terrainModifier = 1.0f;
	public Dictionary<string, bool> terrain;
	private int cheatsheetsCaught = 0;

	void Start ()
	{
        Player = GameObject.FindGameObjectWithTag("Player").transform;
		rb2d = GetComponent<Rigidbody2D> ();
		GetComponent<SpriteRenderer> ().sprite = sprite;
        brokenSprite = this.transform.Find("BrokenSprite").gameObject.GetComponent<SpriteRenderer>();
        terrain = new Dictionary<string, bool> ();
		terrain.Add ("dirt", false);
		terrain.Add ("road", false);
	}

	void Update()
	{
        if (playerControlled)
        {
            // Throttle controls
            if (Input.GetKey(throttleKey))
            {
                throttle = 1;
            }
            else if (Input.GetKey(brakeKey))
            {
                throttle = -brakingConstant;
            }
            else
            {
                throttle = 0;
            }

            // Turning controls
            if (Input.GetKey(leftKey))
            {
                turn = 1;
            }
            else if (Input.GetKey(rightKey))
            {
                turn = -1;
            }
            else
            {
                turn = 0;
            }

            // Handbrake control
            if (Input.GetKey(handbrakeKey))
            {
                handbrake = 1;
            }
            else
            {
                handbrake = 0;
            }

            // Nitro control
            if (Input.GetKey(nitroKey) && nitroTank > 0)
            {
                nitro = 1;
            }
            else
            {
                nitro = 0;
            }
        }
        else
        {
            //TODO AI implementation
            //transform.position = Vector2.MoveTowards(transform.position, Player.position,  100 * Time.deltaTime);
            
            //var dir = Player.position - transform.position;
            //var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            throttle = 1;
            var dir = Player.position - transform.position;
            dir = transform.InverseTransformDirection(dir);
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            Debug.Log(angle);
            //Debug.Log(Vector3.forward);
            if (angle > 90 || angle < -90)
            {
                turn = 1;
            } else if( angle > -90 && angle < 90) {
                turn = -1;
            } else
            {
                turn = 0;
            }

        }
    }

	void FixedUpdate ()
	{
		if (terrain ["road"]) {
			terrainModifier = roadModifier;
		} else if (terrain ["dirt"]) {
			terrainModifier = dirtModifier;
		} else {
			terrainModifier = grassModifier;
		}

		// Longitudinal forces
		Vector2 tractionForce = rb2d.transform.up * acceleration * throttle * terrainModifier;
		tractionForce = tractionForce + tractionForce * nitro * nitroPower;
		nitroTank -= 0.05f * nitro;
		Vector2 dragForce = -dragConstant * rb2d.velocity * rb2d.velocity.magnitude;
		Vector2 frictionForce = -frictionConstant * rb2d.velocity;
		Vector2 brakingForce = -handbrakingConstant * Vector2.Dot(rb2d.transform.up, rb2d.velocity.normalized) * rb2d.transform.up * acceleration * handbrake;

		Vector2 longitudinalForce;
		if (handbrake == 1) {
			longitudinalForce = brakingForce + dragForce + frictionForce;
		} else {
			longitudinalForce = tractionForce + dragForce + frictionForce;
		}

		rb2d.AddForce (longitudinalForce);

		// Turning
		float turnRadius = distanceBetweenAxles / Mathf.Sin(Mathf.Deg2Rad * turningAngle * turn);
		rb2d.angularVelocity = rb2d.velocity.magnitude / turnRadius;

        // change broken transparency
        brokenSprite.color = new Color(1f, 1f,1f, this.damage/1000);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.gameObject.tag);
		if(other.gameObject.tag == "PowerUp")
		{
			Destroy(other.gameObject);
			this.nitroTank += 0.2f;
		}

        if (other.gameObject.tag == "Cheatsheet" && !isCop)
		{
			Destroy(other.gameObject);
			this.cheatsheetsCaught++;
		}
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.tag);
        this.damage += (rb2d.velocity).magnitude / this.resistance;

    }
}
