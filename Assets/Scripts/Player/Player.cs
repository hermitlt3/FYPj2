using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	Animator animator;

	float moveSpeed;
	float health;
	float attackRange;
	float attackDamage;

	Stat_HealthScript healthScript;

    public float gravityScale = 1;
	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;
	Vector2 directionalInput;
	Canvas playerCanvas;

	public LayerMask enemies;

	SpriteRenderer playerSpriteRenderer;
	GameManager gM;

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
	}

	void Start() {
		controller = GetComponent<Controller2D> ();
		animator = GetComponent<Animator> ();
		moveSpeed = GetComponent<Stat_MovementSpdScript> ().GetBaseMS ();
		healthScript = GetComponent<Stat_HealthScript> ();
		attackRange = GetComponent<Stat_AttackRangeScript> ().GetAttackRange ();
		attackDamage = GetComponent<Stat_AttackScript> ().GetBaseAttackDamage ();
		playerSpriteRenderer = GetComponent<SpriteRenderer> ();
		playerCanvas = GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent<Canvas> ();
		gM = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager>();

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
		}

		if (animator.GetBool ("Dead") == true) {
			GetComponent<Player_Input> ().enabled = false;
		}

		ImageRotate ();
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
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, 
			(controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		
		velocity.y += gravity * gravityScale * Time.deltaTime;
	}

	public void OnInteraction() {
		if (animator.GetBool ("Attacking"))
			return;
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		
		animator.SetBool ("Attacking", true);
	}

	void Attack() {
		Collider2D hit = Physics2D.OverlapBox(new Vector2(transform.position.x + Mathf.Clamp (controller.collisions.faceDir, -1, 1) * attackRange / 2, transform.position.y), new Vector2(attackRange, GetComponent<SpriteRenderer>().size.y), 0, enemies);

		if(hit && hit.gameObject != null) {
			hit.gameObject.SendMessage ("GetsHit", gameObject, SendMessageOptions.DontRequireReceiver);
		
			hit.gameObject.GetComponent<Stat_HealthScript> ().DecreaseHealth (attackDamage);

			if (hit.gameObject.GetComponent<EnemyAI_Logic> () || hit.gameObject.GetComponent<Boss_AI>()) {
				TextPopupManager.ShowTextPopup (playerCanvas, hit.transform.position, "-" + attackDamage.ToString (), TextPopupManager.TEXT_TYPE.DAMAGE);
			}
		}
	}

	void StartFading() {
		StartCoroutine(SceneTransitManager.instance.ReloadSceneWithFade (1));
	}

	void Resurrect() {
		gM.OnPlayerDead ();
		StartCoroutine(SceneTransitManager.instance.ReloadSceneWithFade (-1));
	}

	void ImageRotate() {
		if (controller.collisions.faceDir == 1) {
			playerSpriteRenderer.flipX = true;
		} else {
			playerSpriteRenderer.flipX = false;
		}
	}
}
