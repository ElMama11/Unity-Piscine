using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Ghost : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public NavMeshAgent agent;
    private bool goingB = false;
    private Vector3 originalPosition;

    private bool followingPlayer = false;
    private Transform playerTransform;
	private Vector3 playerpos;
    public float followDuration = 5f; // Duration to follow the player in seconds
    public float followTimer = 0f;
	private AudioSource audioSource;
	public FieldOfView fovScript;

    void Start()
    {
		fovScript = GetComponent<FieldOfView>();
        originalPosition = transform.position;
		agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(pointA.position);
		audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
		if (GameManager.Instance.alert == true) {
			agent.SetDestination(GameManager.Instance.posAlert);
			if (!agent.pathPending && agent.remainingDistance < 0.1f)
				return;
			StartCoroutine(CooldownSetAlertFalse());
		}
		if (GameManager.Instance.alert == false) {
			if (fovScript.canSeePlayer == true) {
				audioSource.Play();
				followTimer += Time.deltaTime;
				if (followTimer >= followDuration) {
					followingPlayer = false;
					agent.SetDestination(goingB ? pointA.position : pointB.position);
				}
				else {
					playerpos = GameManager.Instance.GetPlayerPos();
					followTimer = 0f;
					agent.SetDestination(playerpos);
				}
			}
			else {
				// Check if the agent has reached its destination
				if (!agent.pathPending && agent.remainingDistance < 0.1f) {
					agent.SetDestination(goingB ? pointA.position : pointB.position);
					goingB = !goingB;
				}
			}
		}
    }

	public IEnumerator CooldownSetAlertFalse() {
        yield return new WaitForSeconds(5);
        GameManager.Instance.alert = false;
    }

	void OnTriggerEnter(Collider other)
	{
		// Check if the collider belongs to the cone (child object of the ghost)
		// if (transform.IsChildOf(transform) && other.CompareTag("Player"))
		// {
		//     // Debug.Log("Collision with cone");
		//     // Handle collision with the ghost here
		//     followingPlayer = true;
		//     playerTransform = other.transform;
		//     followTimer = 0f;
		//     // Handle collision with the cone here
		// }
		// // Check if the collider belongs to the ghost itself
		if (other.CompareTag("Player")) {
			Debug.Log("You fainted");
			SceneManager.LoadScene(0);
		}
		// {
		//     Debug.Log("Collision with ghost");
		// }
		// else
		// {
		//     Debug.Log("Collision with something else");
		// }
	}
}