using System.Collections;
using UnityEngine;
 
public class PlayerController : MonoBehaviour {
	public float moveSpeed = 5f;
    public float jumpForce = 2f;

    private Rigidbody2D _rb;
    public Transform groundCheckPoint, groundCheckPoint2;
	public LayerMask whatIsGround;
	[SerializeField] private bool isGrounded;
	public Animator anim;
	public SpriteRenderer playerSR;
	public float hangTime = 0.2f;
	private float hangCounter;

	void Start () {
		_rb = GetComponent<Rigidbody2D>();
	}

 	void Update () {
		// Move
		_rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, _rb.velocity.y);
		isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, whatIsGround) || Physics2D.OverlapCircle(groundCheckPoint2.position, 0.1f, whatIsGround);
		
		if (isGrounded)
			hangCounter = hangTime;
		else
			hangCounter -= hangTime;

		// Jump
		if (Input.GetButtonDown("Jump") && hangCounter > 0f)
			_rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
		if (Input.GetButtonUp("Jump") && _rb.velocity.y > 0)
			_rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);

		// Flip the player
		if (Input.GetAxisRaw("Horizontal") > 0)
			playerSR.flipX = false;
		else if (Input.GetAxisRaw("Horizontal") < 0)
			playerSR.flipX = true;

		
	}
}