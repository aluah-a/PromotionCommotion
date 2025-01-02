using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
 
    private Vector2 movementInput;
    private Vector3 velocity;
    private float gravity = -9.8f;
    private CharacterController controller;

    public float speed = 5f;
    public float rotationSpeed = 700f; 
    public Animator animator;
    public bool isWalking;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (animator == null) 
        {
            animator = GetComponent<Animator>();
        }
    }
        private void Update()
          {
        Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
    
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            controller.Move(direction * speed * Time.deltaTime);

       
            animator.SetFloat("Speed", direction.magnitude);
            isWalking = false;

        }
        else
        {
            isWalking = true;
            animator.SetFloat("Speed", 0);
        }

        controller.Move(velocity * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
      
        }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
}