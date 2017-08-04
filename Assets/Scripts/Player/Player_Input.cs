using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class Player_Input : MonoBehaviour {

	Player player;

	void Start () {
		player = GetComponent<Player> ();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetKey (KeyCode.Space)) {
			player.OnJumpInputDown ();
		}
		if (Input.GetButton ("Fire1")) {
			player.OnInteraction ();
		}
        ImageRotate();
    }

    void ImageRotate()
    {
        /*if (controller.collisions.faceDir == 1) {
			playerSpriteRenderer.flipX = true;
		} else {
			playerSpriteRenderer.flipX = false;
		}*/
        if (!GetComponent<Animator>().GetBool("Attacking"))
        {
            GetComponent<SpriteRenderer>().flipX = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x) ? false : true;
            player.controller.collisions.faceDir = (GetComponent<SpriteRenderer>().flipX == true) ? 1 : -1;
        }
    }

    public void StopMovement(int i = 0)
    {
        Vector2 directionalInput = new Vector2(i, Input.GetAxisRaw("Vertical"));
        player.SetDirectionalInput(directionalInput);
    }
}
