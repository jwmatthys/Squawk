using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquawkyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = -1f;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    BoxCollider2D groundCollider;
    // Start is called before the first frame update
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        groundCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f);
        myAnimator.SetBool("isWalking", true);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag=="Ground")
        {
            moveSpeed *= -1;
            float spriteDirection = Mathf.Sign(moveSpeed);
            transform.localScale = new Vector2(spriteDirection, 1);
        }
    }
}
