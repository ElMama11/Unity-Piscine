using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 9f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;
	public Animator animator;
	public UInt16 health = 3;

    bool facingRight = true;
    float moveDirection = 0;
    public bool isGrounded = false;
	public bool isAlive = true;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    BoxCollider2D mainCollider;
    Transform t;
	public Image fadeToBlackImage;
	private UInt16 _playerHP = 3;
	public Vector2 originalPosition;
	private AudioSource audioSource;
	[SerializeField] private AudioClip jumpSound;
	[SerializeField] private AudioClip damageSound;
	[SerializeField] private AudioClip deathSound;
	[SerializeField] private AudioClip respawnSound;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<BoxCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
		originalPosition = transform.position;
		audioSource = GetComponent<AudioSource>();

        if (mainCamera)
            cameraPos = mainCamera.transform.position;
	}
    
    // Update is called once per frame
    void Update()
    {
		if (!isAlive)
			return;
        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }
		animator.SetFloat("Speed", Mathf.Abs(moveDirection));
        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
			animator.SetBool("IsJumping", true);
			audioSource.PlayOneShot(jumpSound);
        }

        // Camera follow
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
        }
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
		animator.SetBool("IsJumping", true);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
					animator.SetBool("IsJumping", false);
                    break;
                }
            }
        }

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }

	public void TakeDamage(UInt16 dmg) {
		_playerHP -= dmg;
		Debug.Log("HP: " + _playerHP);
		if (_playerHP <= 0) {
			audioSource.PlayOneShot(deathSound);
			isAlive = false;
			animator.SetBool("IsDead", true);
			StartCoroutine(fadeToBlack());
			Invoke("Respawn", 2);
		}
		else {
			animator.SetTrigger("DmgTriggerAnim");
			audioSource.PlayOneShot(damageSound);
		}
	}

	void Respawn() {
        _playerHP = 3;
        this.transform.position = originalPosition;
		animator.SetBool("IsDead", false);
    }

	void DelayToWakeUp() {
		audioSource.PlayOneShot(respawnSound);
		animator.SetBool("IsWakeUp", true);
		isAlive = true;
    }

	private IEnumerator fadeToBlack()
	{
		float fadeSpeed = 1f / 2.05f;
		Color startColor = fadeToBlackImage.color;
		while (fadeToBlackImage.color.a < 1f) {
			// Update the alpha channel of the image color
			startColor.a += Time.deltaTime * fadeSpeed;
			fadeToBlackImage.color = startColor;
			yield return null;
		}
		// Ensure the alpha is exactly 1 at the end of the coroutine
		startColor.a = 1f;
		fadeToBlackImage.color = startColor;
		Invoke("DelayToWakeUp", 0f);

		yield return new WaitForSeconds(.1f);

		while (fadeToBlackImage.color.a > 0f) {
			startColor.a -= Time.deltaTime * fadeSpeed;
			fadeToBlackImage.color = startColor;
			yield return null;
		}
		startColor.a = 0f;
		fadeToBlackImage.color = startColor;
		animator.SetBool("IsWakeUp", false);
	}
}