using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour {
    //Variables for a smooth camera
    private Transform playerTracked;
    [SerializeField] private float yOffset = 10f;
    private float xOffset, zOffset = -1.0f;
    public float followSpeed;

    //Variables for camera zoom
    [SerializeField] private Camera thisCamera;
    [SerializeField] private float zoomSpeed;
    private float zoom;

    private void Start() {//Fetch Player and Camera
        thisCamera = GetComponent<Camera>();
        playerTracked = GameObject.FindGameObjectWithTag("Player").transform;
        zoom = thisCamera.orthographicSize;
    }

    private void FixedUpdate() {//Fixed Update for smooth camera movement
        //Smoothing of camera movement
        float xPlayer = playerTracked.position.x + xOffset;
        float yPlayer = playerTracked.position.y + yOffset;
        float xNewPosition = Mathf.Lerp(transform.position.x, xPlayer, Time.deltaTime * followSpeed);
        transform.position = new Vector3(xNewPosition, yPlayer, playerTracked.position.z + zOffset);

        //Smoothing camera zoom
        thisCamera.orthographicSize = Mathf.Lerp(thisCamera.orthographicSize, zoom, Time.deltaTime * zoomSpeed);
    }

    private void Update() {//Update for Scroll wheel input
        zoom += Mouse.current.scroll.ReadValue().y * 0.25f;
        if(zoom >= 12.0f) {
            zoom = 12.0f;
        } else if(zoom <= 4.0f) {
            zoom = 4.0f;
        }
    }
}
