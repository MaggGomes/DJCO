using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour {

	[Range(1000.0f, 3000.0f)]
	public float acceleration = 2000f;
	[Range(0.0f, 1.0f)]
	public float dragConstant = 0.05f;
	[Range(0.0f, 0.1f)]
	public float frictionConstant = 0.01f;
	[Range(0.0f, 1.0f)]
	public float brakingConstant = 0.1f;
	[Range(0.0f, 1.0f)]
	public float handbrakingConstant = 0.7f;
	[Range(0.0f, 1.0f)]
	public float distanceBetweenAxles = 0.3f;
	[Range(0.0f, 45.0f)]
	public float turningAngle = 15f;
	[Range(0.0f, 1.0f)]
	public float roadModifier = 1.0f;
	[Range(0.0f, 1.0f)]
	public float grassModifier = 0.7f;
	[Range(0.0f, 1.0f)]
	public float dirtModifier = 0.4f;
	[Range(1.0f, 5.0f)]
	public float nitroPower = 2f;
	[Range(0.0f, 1.0f)]
	public float maxNitroTank = 1f;  
	public float minNitroTank = 0f;
	[Range(0.0f, 1.0f)]
	public float nitroTank = 1f;   
    [Range(0.0f, 1000f)]
	public float maxLifePoints = 1000f;
	public float minLifePoints = 0f;
	[Range(0.0f, 1000f)]
    public float lifePoints = 1000f;
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
	private float turn; // 1 left; -1 right; 0 none
	private int handbrake; // 1 on; 0 off
	private int nitro; // 1 on; 0 off
	private float terrainModifier = 1.0f;
	public Dictionary<string, bool> terrain;
	public int cheatsheetsCaught = 0;

	public float instantNitroTimer = 0;
	public float instantSlowDownTimer = 0;
	public float shieldTimer = 0;

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
				if (turn <= 0) {
					turn = 1;
				}
				else if (turn < 50){
					turn += 1.5f;
				}
            }
            else if (Input.GetKey(rightKey))
			{
				if (turn >= 0) {
					turn = -1;
				}
				else if (turn > -50){
					turn -= 1.5f;
				}
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
			if (Input.GetKey(nitroKey) && nitroTank > 0 && throttle != 0)
            {
				Debug.Log ("nitroactivated");
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
            //Debug.Log(angle);
            if (angle > 90 || angle < -90)
            {
                turn = 50;
            } else if( angle > -90 && angle < 90) {
                turn = -50;
            } else
            {
                turn = 0;
            }
        }
		if (instantNitroTimer > 0) {
			instantNitroTimer -= Time.deltaTime;
		}
		instantSlowDownTimer -= Time.deltaTime;
		shieldTimer -= Time.deltaTime;
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
		if (instantNitroTimer > 0) {
			tractionForce += tractionForce * nitroPower;
		} else {
			instantNitroTimer = 0;
		}

		if (instantSlowDownTimer > 0) {
			tractionForce -= tractionForce * 0.5f;
		} else {
			instantSlowDownTimer = 0;
		}

		nitroTank -= 0.01f * nitro;

		// nitrotank can't be less than zero
		if (nitroTank < minNitroTank)
			nitroTank = minNitroTank;
		
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
		float turnRadius = distanceBetweenAxles / Mathf.Sin(Mathf.Deg2Rad * turningAngle * (turn/50));
		rb2d.angularVelocity = rb2d.velocity.magnitude / turnRadius;

        // change broken transparency
		brokenSprite.color = new Color(1f, 1f,1f, (this.maxLifePoints-this.lifePoints)/1000);

		//Debug.Log (rb2d.velocity.magnitude);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log(other.gameObject.tag);
		if(other.gameObject.tag == "PowerUp" && nitroTank < 1f)
		{
			MapController.PlaceNewPowerUp(other.gameObject.transform.position);
			Destroy(other.gameObject);
			this.nitroTank += 0.2f;

			if (nitroTank > maxNitroTank)
				nitroTank = maxNitroTank;
		}

		if(other.gameObject.tag == "ResistancePowerUp" && lifePoints < 1000f)
		{
			MapController.PlaceNewPowerUp(other.gameObject.transform.position);
			Destroy(other.gameObject);
			this.lifePoints += 200;

			if (lifePoints > maxLifePoints)
				lifePoints = maxLifePoints;
		}

		if(other.gameObject.tag == "InstantNitro")
		{
			MapController.PlaceNewPowerUp(other.gameObject.transform.position);
			Destroy(other.gameObject);
			this.instantNitroTimer = 2f;
		}

		if(other.gameObject.tag == "InstantSlowDown")
		{
			MapController.PlaceNewPowerUp(other.gameObject.transform.position);
			Destroy(other.gameObject);
			this.instantSlowDownTimer = 2f;
		}

		if(other.gameObject.tag == "Shield")
		{
			MapController.PlaceNewPowerUp(other.gameObject.transform.position);
			Destroy(other.gameObject);
			this.shieldTimer = 2f;
		}

        if (other.gameObject.tag == "Cheatsheet" && !isCop)
		{
			Destroy(other.gameObject);
			this.cheatsheetsCaught++;
		}

		if (other.gameObject.tag == "End" && !isCop && this.cheatsheetsCaught == MapController.nCheatsheets) //gameover condition
		{
			this.GameOver();
		}
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.tag);
		//Debug.Log ((rb2d.velocity).magnitude / this.resistance);
		if(shieldTimer <= 0){
		lifePoints -= (rb2d.velocity).magnitude / this.resistance;

		if (lifePoints < minLifePoints) {
			lifePoints = minLifePoints;
			if (!isCop) {
				this.GameOver ();
			}
		} else if (lifePoints > maxLifePoints) {
			lifePoints = maxLifePoints;
			}
		}
    }

	void GameOver(){
		SceneManager.LoadScene ("GameOver", LoadSceneMode.Single);
	}
}
