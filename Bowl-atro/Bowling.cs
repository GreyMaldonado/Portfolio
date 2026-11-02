using UnityEngine;
using UnityEngine.InputSystem;

public class Bowling : MonoBehaviour
{
    private Rigidbody rb; // Bowling ball rigidbody

    private PlayerInput ballMove;
    private InputActionMap actionMap;
    private InputAction press;
    private InputAction forward;
    private InputAction backward;
    private InputAction turnLeft;
    private InputAction turnRight;
    private InputAction spaceFire;
    public float maxForce = 10.0f;
    public float minForce = 0.0f;

    public float movementX; // movement on the x axis
    public float movementZ; // movement on the y axis


    public float speed; // movement speed
    public float rollSpeed;
    //public int flag = 0;
    //public bool holding = true;
    //public float holdTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rollSpeed = speed * rb.mass;
        ballMove = GetComponent<PlayerInput>();
        //m_InputUser.ActivateControlScheme("Bowling");
    }

    private void OnEnable()
    {
        //InputActions.FindActionMap("Player").Enable();
        rb.isKinematic = false;
        actionMap = ballMove.actions.FindActionMap("Bowling");
        actionMap.Enable();
        forward = actionMap.FindAction("Forward");
        backward = actionMap.FindAction("Backward");
        turnLeft = actionMap.FindAction("TurnLeft");
        turnRight = actionMap.FindAction("TurnRight");
        press = actionMap.FindAction("Press");
        spaceFire = actionMap.FindAction("Fire");
        Debug.Log(actionMap);
        Debug.Log(forward);
        Debug.Log(backward);
        Debug.Log(turnLeft);
        Debug.Log(turnRight);
        Debug.Log(press);
    }

    private void OnDisable()
    {
        //InputActions.FindActionMap("Player").Disable();
        rb.isKinematic = true;
        speed = 0;
        rollSpeed = 0;
        //flag = 0;
        actionMap.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rollSpeed = speed * rb.mass;
        //ballMove = GetComponent<PlayerInput>();
        //moveAction = ballMove.actions.FindAction("Hit");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (moveAction.IsPressed() && flag == 0)
        {
            Roll();
        }*/

        if(press.IsPressed())
        {
            if (forward.WasPressedThisFrame() && movementZ < 10)
                movementZ += 1;
            else if(backward.WasPressedThisFrame() && movementZ > 0)
                movementZ -= 1;
        }

        //if(Mouse.current.leftButton.wasPressedThisFrame && holding == true)
        //{
        //    flag = 1;
        //    holdTime = 0;
        //}

        //if(flag == 1)
        //{
        //    holdTime += Time.deltaTime;
        //}

        //if(Mouse.current.leftButton.wasReleasedThisFrame && flag == 1)
        //{
        //    flag = 0;
        //    holding = false;
        //    Roll();
        //}
        if(spaceFire.IsPressed())
        {
            Roll();
        }
    }

    void Roll()
    {
        Vector3 movement = new Vector3(0.0f, 0.0f, movementZ * rollSpeed);
        rb.AddForce(movement);
    }
}
