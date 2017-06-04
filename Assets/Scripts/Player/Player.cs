using UnityEngine;
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

	Stat_HealthScript healthScript;

    public float gravityScale = 1;
	float gravity;
	float jumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;
	Vector2 directionalInput;

	public LayerMask enemies;

	SpriteRenderer playerSpriteRenderer;

	void Start() {
		controller = GetComponent<Controller2D> ();
		animator = GetComponent<Animator> ();
		moveSpeed = GetComponent<Stat_MovementSpdScript> ().GetBaseMS ();
		healthScript = GetComponent<Stat_HealthScript> ();
		attackRange = GetComponent<Stat_AttackRangeScript> ().GetAttackRange ();

		playerSpriteRenderer = GetComponent<SpriteRenderer> ();

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
		animator.SetBool ("Dead" , !healthScript.isAlive ());

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
		
		animator.SetBool ("Attacking", true);
	}

	void Attack() {
		RaycastHit2D hit;
		hit = Physics2D.Raycast (transform.position, new Vector2 (Mathf.Clamp (controller.collisions.faceDir, -1, 1), 0), attackRange, enemies);

		if(hit.collider != null) {
			hit.collider.gameObject.SendMessage ("GetsHit", gameObject);
			TextPopupManager.ShowTextPopup (GameObject.Find("PlayerCanvas").GetComponent<Canvas>(), hit.collider.transform.position, "Hit!", TextPopupManager.TEXT_TYPE.DAMAGE);
		}
	}

	void ImageRotate() {
		if (controller.collisions.faceDir == 1) {
			playerSpriteRenderer.flipX = true;
		} else {
			playerSpriteRenderer.flipX = false;
		}
	}
}
