using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player movement
    public float rotationSpeed = 3f; // Speed of camera rotation
    public float inputSmoothing = 0.1f; // Smoothing factor for input

    private CharacterController controller;
    private Transform mainCameraTransform; // Reference to the main camera's transform
    public GameObject thirdPersonCamera;
    public GameObject firstPersonCamera;
    private bool isFirstPerson = false;
    public Animator animator;
	[SerializeField] private AudioClip faintSound;
	[SerializeField] private AudioClip stepSound;
	private AudioSource audioSource;

    private Vector3 smoothedInput = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCameraTransform = Camera.main.transform;
		audioSource = GetComponent<AudioSource>();
        firstPersonCamera.SetActive(false);
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = CalculateMovement(horizontalInput, verticalInput);
        if (movement == Vector3.zero)
            animator.SetFloat("Speed", 0);
        else {
            animator.SetFloat("Speed", 0.2f);
			if (stepSound != null)
				audioSource.PlayOneShot(stepSound);
		}
        // Smooth the input values
        smoothedInput = Vector3.Lerp(smoothedInput, movement, inputSmoothing);
        controller.Move(smoothedInput * moveSpeed * Time.deltaTime);

        // Camera rotation with mouse
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up * mouseX);
        if (Input.GetKeyDown(KeyCode.C))
            SwitchCamera();
    }

Vector3 CalculateMovement(float horizontalInput, float verticalInput)
{
    Vector3 cameraForward = mainCameraTransform.forward;
    cameraForward.y = 0;
    Vector3 cameraRight = mainCameraTransform.right;
    Vector3 movement = (cameraForward * verticalInput) + (cameraRight * horizontalInput + new Vector3(0,0 ,0));
    movement.Normalize();
    return movement;
}

    void SwitchCamera()
    {
        isFirstPerson = !isFirstPerson;
        if (isFirstPerson) {
            thirdPersonCamera.SetActive(false);
            firstPersonCamera.SetActive(true);
        }
        else {
            firstPersonCamera.SetActive(false);
            thirdPersonCamera.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Ghost")) {
			audioSource.PlayOneShot(faintSound);
            Debug.Log("You fainted");
            SceneManager.LoadScene(0);
        }
    }
}