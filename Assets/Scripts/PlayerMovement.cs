using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpHeight = 1.0f;
    public float gravity = -9.8f;
    public float mouseSensitivity = 100.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Animator animator;
    private Transform playerBody; // Transform za rotiranje tijela igrača
    private float xRotation = 0f; // Početna rotacija kamere

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerBody = transform;

        // Zaključavanje kursora miša u centru zaslona
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Upravljanje rotacijom igrača pomoću miša
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ograničavanje vertikalne rotacije

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotacija kamere
        playerBody.Rotate(Vector3.up * mouseX); // Rotacija tijela igrača oko Y osi

        // Upravljanje kretanjem
        if (controller.isGrounded)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            moveDirection = transform.right * moveHorizontal + transform.forward * moveVertical;
            moveDirection *= speed;

            // Upravljanje hodanjem
            animator.SetFloat("Speed", moveDirection.magnitude);

            // Upravljanje skakanjem
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
                animator.SetBool("IsJumping", true);
            }
            else
            {
                animator.SetBool("IsJumping", false);
            }

            // Upravljanje saginjanjem
            if (Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("IsCrouching", true);
                moveDirection *= 0.5f;
            }
            else
            {
                animator.SetBool("IsCrouching", false);
            }
        }

        // Primjena gravitacije
        moveDirection.y += gravity * Time.deltaTime;

        // Pomicanje karaktera
        controller.Move(moveDirection * Time.deltaTime);
    }
}