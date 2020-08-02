using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [HideInInspector] public PlayerManager playerManager;
    public float moveSpeed = 5f;
    public float acceleration = 0.2f;
    private Rigidbody2D rb;
    private Camera mainCamera;

    [HideInInspector] public Vector2 moveDirection;
    [HideInInspector] public Vector2 lookDirection;
    [HideInInspector] public Vector2 mousePos;
    public float angle;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (playerManager.canMove)
        {
            moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, (moveDirection * moveSpeed * Time.fixedDeltaTime), acceleration);
        lookDirection = mousePos - rb.position;
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = Mathf.Lerp(transform.rotation.z, angle, 0.95f * 100 * Time.deltaTime);

    }
}
