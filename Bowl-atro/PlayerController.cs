using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; // Player rigidbody

    private float movementX; // movement on x axis
    private float movementY; // movement on y axis

    public float speed; // speed of movement

    private int count; // count of picked up objects
    public TextMeshProUGUI countText; // Variable for the text displaying the count

    public GameObject winTextObject; // Object for declaring that you win


    // Start is called once before the first execution of FixedUpdate after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    // On Move function to move the player object
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Set the text on screen to match the count
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 8)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    // Fixed update function 
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    // OnTriggerEnter checks if the player touched a trigger object
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp")) // Check if collider is tagged as PickUp
        {
            other.gameObject.SetActive(false); // If so then deactivate the object
            count++;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject);

            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

}
