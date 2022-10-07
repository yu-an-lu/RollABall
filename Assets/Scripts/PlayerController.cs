using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{   
    [SerializeField] float maxSpeed = 3f;

    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    public Vector3 jump;
    public float jumpForce = 1f;
    private bool isGrounded;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);

        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnCollisionStay() {
        isGrounded = true;
    }

    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText() {

        countText.text = "Count: " + count.ToString();
        if (count >= 12) {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement*speed);

        EnforceMaxSpeed();

        // Stop movement if speed is too low
        if (Input.GetAxisRaw("Horizontal") == 0f && Input.GetAxisRaw("Vertical") == 0f) {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (transform.position.y <= 0.5f){
             SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }

    }

    void EnforceMaxSpeed() {
        if (rb.velocity.magnitude > maxSpeed) {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
    }

}
