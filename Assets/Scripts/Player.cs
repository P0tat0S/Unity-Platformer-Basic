using System.Collections ;
using System.Collections.Generic ;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

    [SerializeField] private float jumpHeight = 4;
    [SerializeField] private float timeToJumpApex = .4f;
    [SerializeField] private float moveSpeed = 6;
    [SerializeField] private float accelerationTimeAirborne = .2f;
    [SerializeField] private float accelerationTimeGrounded = .1f;
    private float gravity;
    private float jumpVelocity;

    private bool jumpPressed = false;
    private Vector3 velocity;
    private float velocityXSmoothing;
    private Vector2 inputValue;
    private Controller2D controller;

    private void Start() {
        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + " Jump Velocity" + jumpVelocity);
    }

    /***********
        Inputs
    ***********/
    private void OnMove(InputValue value) { inputValue = value.Get<Vector2>(); } //Move right and left
    private void OnJump() { if (controller.collisions.below) jumpPressed = true; }

    //Update
    private void Update() {
        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }

        if (jumpPressed) {
            velocity.y = jumpVelocity;
            jumpPressed = false;
        }

        float targetVelocityX = inputValue.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
