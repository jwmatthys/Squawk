using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathConditions : MonoBehaviour
{
    PlayerMovement pm;

    private void Awake() {
        pm = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag=="Enemy")
        {
            Debug.Log("Dead!");
            pm.disableMovement();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag=="Water") {
            Debug.Log("Drowned!");
        }
    }
}
