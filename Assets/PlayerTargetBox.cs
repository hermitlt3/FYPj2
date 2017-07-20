using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetBox : MonoBehaviour {

    GameObject player;
	// Use this for initialization
	void Start () {
        player = transform.parent.gameObject;
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = player.transform.position + new Vector3(GetComponent<SpriteRenderer>().bounds.size.x * 0.5f * ((player.GetComponent<SpriteRenderer>().flipX == true) ? 1 : -1), 0);
        transform.localScale = new Vector2(player.GetComponent<Stat_AttackRangeScript>().GetAttackRange() * 0.5f, player.transform.localScale.y);
	}
}
