using System.Collections ;
using System.Collections.Generic ;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    //Movement Variables
    private Vector2 moveValue;
    public float moveSpeed;
    private Rigidbody2D rb;

    private void Start() {
        //Get player rigidbody to move Player
        rb = this.GetComponent<Rigidbody2D>();
    }

    /***********
        Inputs
    ***********/
    void OnMove(InputValue value) {
        //Code that executes when moved (animation)
        moveValue = value.Get<Vector2>();
    }

    // Update is called once per frame
    void Update() {
        //For non movement changes
    }

    //Fixed update for dashing and movement
    private void FixedUpdate() {
       Vector3 movement = new Vector3(moveValue.x, moveValue.y, 0f);
        rb.AddForce(movement * moveSpeed * Time.fixedDeltaTime);
    }

}
