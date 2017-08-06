using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

    public static Player instance;
	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	Animator animator;

    Stat_MovementSpdScript moveScript;

	Stat_HealthScript healthScript;

    public float gravityScale = 1;
	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	public Controller2D controller;
	Vector2 directionalInput;

	GameManager gM;
    bool playSound;

	void Awake() {

	}

	void Start() {
        if (instance && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        controller = GetComponent<Controller2D> ();
		animator = GetComponent<Animator> ();
        moveScript = GetComponent<Stat_MovementSpdScript> ();
		healthScript = GetComponent<Stat_HealthScript> ();

		gM = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager>();
        playSound = true;

        gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

	void Update() {
		CalculateVelocity ();

		controller.Move (velocity * Time.deltaTime, directionalInput);
		if (controller.collisions.above || controller.collisions.below) {
			animator.SetBool ("Jump", false);

			if (controller.collisions.slidingDownMaxSlope) {
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
			} else {
				velocity.y = 0;
			}
		}

		animator.SetFloat ("Speed", Mathf.Abs(velocity.x));

		if(!healthScript.isAlive ()) {
			animator.SetBool ("Dead" , true);
			velocity = Vector3.zero;
            if(playSound)
            {
                AudioManager.instance.PlaySound(GetComponents<AudioSource>()[1]);
                playSound = false;
            }
		}

		if (animator.GetBool ("Dead") == true) {
			GetComponent<Player_Input> ().enabled = false;
		}

	}

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {
		if (controller.collisions.below) {
			animator.SetBool ("Jump", true);

			if (controller.collisions.slidingDownMaxSlope) {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					velocity.y = jumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = jumpVelocity * controller.collisions.slopeNormal.x;
				}
			} else {
				velocity.y = jumpVelocity;
			}
		}
	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveScript.GetBaseMS();
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, 
			(controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		
		velocity.y += gravity * gravityScale * Time.deltaTime;
	}

	public void OnInteraction() {
		if (animator.GetBool ("Attacking"))
			return;
		if (EventSystem.current && EventSystem.current.IsPointerOverGameObject())
			return;
		
		animator.SetBool ("Attacking", true);
	}

	void StartFading() {
		StartCoroutine(SceneTransitManager.instance.ReloadSceneWithFade (1));
	}

	void Resurrect() {
		gM.OnPlayerDead ();
        playSound = true;
		StartCoroutine(SceneTransitManager.instance.ReloadSceneWithFade (-1));
	}
}
