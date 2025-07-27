using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class PlayerCollisionMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    public GameObject penBulletPrefab;
    public DialogueManager dm;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastMovementDir;
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
        if (SceneManager.GetActiveScene().name == "bathroom")
        {
            dm = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        }
    }

    void Update()
    {
        HandleAnimations();

        // Stop controls if dialogue is happening
        if (dm && !dm.dialogueEnd)
        {
            movement = Vector2.zero;
            return;
        }

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
            lastMovementDir = movement;
        }
        else if (Mathf.Abs(vertical) > 0)
        {
            movement = new Vector2(0, vertical);
            lastMovementDir = movement;
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
            var inventory = GetComponent<Inventory>();
            var penItem = inventory.RemoveItem("pen item");

            if (penItem != null)
            {
                var bulletRotationAngle = Vector3.SignedAngle(Vector3.up, lastMovementDir, Vector3.forward);
                var bulletRotation = Quaternion.AngleAxis(bulletRotationAngle, Vector3.forward);
                GameObject penBullet = (GameObject)Instantiate(penBulletPrefab, transform.position, bulletRotation);
                penBullet.GetComponent<PenProjectile>().itemDrop = penItem;
            }

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
