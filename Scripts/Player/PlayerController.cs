using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    
    [SerializeField]
    private float distanceBetweenLanes = 3f;

    [SerializeField]
    private float forwardSpeed, maxSpeed;
    [SerializeField]
    private float jumpForce;

    private Vector3 direction;
    private float gravity = -20;
    private int desiredLane = 1;
    private bool isSliding = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        direction.z = forwardSpeed;
    }

    private void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
            direction.z = forwardSpeed;
        }
            
        SlideControl();
        HorizontalMovement();
        Jump();

        if (direction.y > -20 && !characterController.isGrounded)
            direction.y += gravity * Time.deltaTime;
        characterController.Move(direction * Time.deltaTime);
    }

    private void SlideControl()
    {
        if ((SwipeManager.swipeDown || Input.GetKeyDown(KeyCode.DownArrow)) && !isSliding)
        {
            direction.y += -10f;
            StartCoroutine(Slide());
        }
    }

    private IEnumerator Slide()
    {
        animator.SetBool("isSliding", true);
        isSliding = true;
        characterController.center = new Vector3(0, -0.5f, 0);
        characterController.height = 1f;
        yield return new WaitForSeconds(1);
        characterController.center = new Vector3(0, 0, 0);
        characterController.height = 2f;
        animator.SetBool("isSliding", false);
        isSliding = false;
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || SwipeManager.swipeUp) && characterController.isGrounded)
        {
            FindObjectOfType<AudioManager>().PlaySound("Jump");
            direction.y = jumpForce;
        }
        if (!characterController.isGrounded)
            animator.SetBool("IsGrounded", false);
        else
            animator.SetBool("IsGrounded", true);
    }

    private void HorizontalMovement()
    {

        Vector3 targetPosition = transform.position;
        if (Input.GetKeyDown(KeyCode.RightArrow) || SwipeManager.swipeRight)
        {
            if (desiredLane < 2)
            {
                desiredLane++;
                //animator.SetTrigger("SideMove");
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeManager.swipeLeft)
        {
            if (desiredLane > 0)
            {
                desiredLane--;
                //animator.SetTrigger("SideMove");
            }
        }

        if (desiredLane == 0)
            targetPosition.x = -distanceBetweenLanes;
        else if (desiredLane == 2)
            targetPosition.x = distanceBetweenLanes;
        else
            targetPosition.x = 0;

        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                characterController.Move(moveDir);
            else
                characterController.Move(diff);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Obstacle"))
        {
            FindObjectOfType<AudioManager>().PlaySound("Death");
            PlayerManager.GameOver();
        }
    }
}
