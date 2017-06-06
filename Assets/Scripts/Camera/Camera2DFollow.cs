using System;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
  	public Transform target;
  	public float damping = 1;
  	public float lookAheadFactor = 3;
  	public float lookAheadReturnSpeed = 0.5f;
  	public float lookAheadMoveThreshold = 0.1f;
	public float orthoSize = 10f;

  	private float m_OffsetZ;
  	private Vector3 m_LastTargetPosition;
  	private Vector3 m_CurrentVelocity;
  	private Vector3 m_LookAheadPos;
  	
  	// Use this for initialization
  	void Start()
	{
		m_LastTargetPosition = target.position;
		m_OffsetZ = (transform.position - target.position).z;
		transform.parent = null;
	}
		
        // Update is called once per frame
    void Update()
	{
		// only update lookahead pos if accelerating or changed direction
		float xMoveDelta = (target.position - m_LastTargetPosition).x;

		bool updateLookAheadTarget = Mathf.Abs (xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadTarget) {
			m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign (xMoveDelta);
		} else {
			m_LookAheadPos = Vector3.MoveTowards (m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
		}

		GetComponent<Camera> ().orthographicSize = Mathf.Max (GetComponent<Camera> ().orthographicSize - Time.deltaTime * 3f, orthoSize);

		Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
		Vector3 newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

		transform.position = newPos;

		m_LastTargetPosition = target.position;
	}
}
