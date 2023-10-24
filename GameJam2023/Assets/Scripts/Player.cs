using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

    private float horizontal;
    private float movementSpeed = 10f;
    private float jumpPower = 15f;
    private bool isFacingRight = true;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameInput gameInput;

    private void Start () {
        gameInput.OnJumpAction += GameInput_OnJumpAction;
    }

    private void GameInput_OnJumpAction (object sender, System.EventArgs e) {
        if (isGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    private void Update () {
        horizontal = Input.GetAxisRaw("Horizontal");
        FlipTransform();
    }

    private void FixedUpdate () {
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
    }
    private bool isGrounded () {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FlipTransform () {
        if (!isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

