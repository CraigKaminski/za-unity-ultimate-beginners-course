﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public AudioSource coinSound;
    public float walkSpeed;
    public float jumpForce;
    public float cameraDistZ = 6;
    Rigidbody rb;
    Collider col;
    bool pressedJump = false;
    Vector3 size;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        size = col.bounds.size;
        CameraFollowPlayer();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        WalkHandler();
        JumpHandler();
        CameraFollowPlayer();
        FallHandler();
	}

    void WalkHandler()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(hAxis, 0, vAxis) * walkSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + movement;
        rb.MovePosition(newPos);

        if (hAxis != 0 || vAxis != 0)
        {
            Vector3 direction = new Vector3(hAxis, 0, vAxis);
            //transform.forward = direction;
            rb.rotation = Quaternion.LookRotation(direction);
        }
    }

    void JumpHandler()
    {
        float jAxis = Input.GetAxis("Jump");

        if (jAxis > 0)
        {
            bool isGrounded = CheckGrounded();

            if (!pressedJump && isGrounded)
            {
                pressedJump = true;
                Vector3 jumpVector = new Vector3(0, jAxis, 0) * jumpForce;
                rb.AddForce(jumpVector, ForceMode.VelocityChange);
            }
        }
        else
        {
            pressedJump = false;
        }
    }

    bool CheckGrounded()
    {
        Vector3 corner1 = transform.position + new Vector3(size.x / 2, -size.y / 2 + 0.01f, size.z / 2);
        Vector3 corner2 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.01f, size.z / 2);
        Vector3 corner3 = transform.position + new Vector3(size.x / 2, -size.y / 2 + 0.01f, -size.z / 2);
        Vector3 corner4 = transform.position + new Vector3(-size.x / 2, -size.y / 2 + 0.01f, -size.z / 2);

        bool grounded1 = Physics.Raycast(corner1, Vector3.down, 0.02f);
        bool grounded2 = Physics.Raycast(corner2, Vector3.down, 0.02f);
        bool grounded3 = Physics.Raycast(corner3, Vector3.down, 0.02f);
        bool grounded4 = Physics.Raycast(corner4, Vector3.down, 0.02f);

        return (grounded1 || grounded2 || grounded3 || grounded4);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GameManager.instance.IncreaseScore(1);
            coinSound.Play();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            GameManager.instance.GameOver();
        }
        else if (other.CompareTag("Goal"))
        {
            GameManager.instance.IncreaseLevel();
        }
    }

    void CameraFollowPlayer()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        cameraPos.z = transform.position.z - cameraDistZ;

        Camera.main.transform.position = cameraPos;
    }

    void FallHandler()
    {
        if (transform.position.y < -10)
        {
            GameManager.instance.GameOver();
        }
    }
}
