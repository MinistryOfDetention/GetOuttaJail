using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerCollisionMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public GameObject penBulletPrefab;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float previousHorizontal;
    private float previousVertical;

    private Animator bodyAnimator;
    public CharacterAudio characterAudio;
    public bool walkingTimerActive = false;
    public float footStepInterval = 0.4f;

    void Awake()
    {
        bodyAnimator = GetComponentInChildren<Animator>();

        if (bodyAnimator == null)
        {
            Debug.LogError("Animator component not found on PlayerCollisionMovement script.");
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");


        bool horizontalChanged = horizontal != previousHorizontal;
        bool verticalChanged = vertical != previousVertical;

        if (Mathf.Abs(horizontal) > 0 && Mathf.Abs(vertical) > 0)
        {
            if (horizontalChanged && !verticalChanged)
            {
                movement = new Vector2(horizontal, 0);
            }
            else if (verticalChanged && !horizontalChanged)
            {
                movement = new Vector2(0, vertical);
            }
            else if (horizontalChanged && verticalChanged)
            {
                if (movement.x != 0)
                {
                    movement = new Vector2(horizontal, 0);
                }
                else
                {
                    movement = new Vector2(0, vertical);
                }
            }
        }
        else if (Mathf.Abs(horizontal) > 0)
        {
            movement = new Vector2(horizontal, 0);
        }
        else if (Mathf.Abs(vertical) > 0)
        {
            movement = new Vector2(0, vertical);
        }
        else
        {
            movement = Vector2.zero;
        }

        HandleAnimations();
        HandleFootstepAudio();

        previousHorizontal = horizontal;
        previousVertical = vertical;

        if (Input.GetKeyDown("right shift"))
        {
            var bulletRotationAngle = Vector3.SignedAngle(Vector3.up, movement, Vector3.forward);
            var bulletRotation = Quaternion.AngleAxis(bulletRotationAngle, Vector3.forward);
            GameObject penBullet = (GameObject)Instantiate(penBulletPrefab, transform.position, bulletRotation);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    void HandleAnimations()
    {
        if (movement.x > 0)
        {
            // Move Right
            bodyAnimator.Play("PlayerMoveRight");
        }
        else if (movement.x < 0)
        {
            // Move Left
            bodyAnimator.Play("PlayerMoveLeft");
        }
        else if (movement.y > 0)
        {
            // Move Up
            bodyAnimator.Play("PlayerMoveUp");
        }
        else if (movement.y < 0)
        {
            // Move Down
            bodyAnimator.Play("PlayerMoveDown");
        }
        else
        {
            // Idle state, you can set an idle animation if needed
            bodyAnimator.Play("PlayerIdle");
        }
    }
    private void HandleFootstepAudio()
    {
        if (movement == Vector2.zero || walkingTimerActive)
        {
            return;
        }
        else
        {
            StartCoroutine(FootstepTimer(footStepInterval));
        }
    }

    IEnumerator FootstepTimer(float time)
    {   
        walkingTimerActive = true;
        yield return new WaitForSeconds(time);
        walkingTimerActive = false;
        if (movement != Vector2.zero)
        {
            characterAudio.PlayClip("footsteps");
        }
    }
}
