using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    private float defaultGravity;

    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float initialJumpForce = 2f;
    [SerializeField] float incrementalJumpForce = 1f;
    [SerializeField] float maxJumpButtonTime = 0.3f;
    private bool jumpButtonPressed = false;
    private bool onGround = true;
    private float startJumpTime = -1f;

    // Start is called before the first frame update
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
    }

    void LateUpdate() {
        Walk();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            myAnimator.SetBool("isJumping", false);
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground")
        {
            myAnimator.SetBool("isJumping", true);
            onGround = false;
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        jumpButtonPressed = value.isPressed;
    }

    private void Walk()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * walkSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        if (Mathf.Abs(moveInput.x) > Mathf.Epsilon)
        {
            float spriteDirection = Mathf.Sign(moveInput.x);
            transform.localScale = new Vector2(spriteDirection, 1);
            myAnimator.SetBool("isWalking", true);
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
        }
    }

    void Jump()
    {
        float elapsedTime = Time.time - startJumpTime;
        if (onGround && jumpButtonPressed)
        {
            Vector2 jumpVelocity = new Vector2(myRigidbody.velocity.x, initialJumpForce);
            myRigidbody.velocity = jumpVelocity;
            startJumpTime = Time.time;
        }
        else if (jumpButtonPressed && elapsedTime < maxJumpButtonTime)
        {
            Vector2 additionalJump = transform.up * incrementalJumpForce * Time.deltaTime;
            myRigidbody.AddForce(additionalJump, ForceMode2D.Impulse);
        }
    }
}
