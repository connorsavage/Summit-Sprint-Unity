using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 8;
    public float leftRightSpeed = 5;
    private InputAction leftAction;
    private InputAction rightAction;
    public bool isJumping = false;
    public bool comingDown = false;
    public Animator anim;
    private bool isRolling = false;
    private bool isCrashing = false;
    private Vector3 originalScale;
    public GameObject hitbox;
    public GameObject levelControl;
    public GameObject mountain;

    void Start()
    {
        leftAction = new InputAction("left", InputActionType.Button, "<Keyboard>/LeftArrow");
        leftAction.Enable();

        rightAction = new InputAction("right", InputActionType.Button, "<Keyboard>/RightArrow");
        rightAction.Enable();

        // Store the original scale of the player object
        originalScale = hitbox.transform.localScale;

    }

    void Update()
    {
        mountain.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        if (leftAction.ReadValue<float>() > 0)
        {
            if (this.gameObject.transform.position.x > LevelBoundary.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
            }
        }
        if (rightAction.ReadValue<float>() > 0)
        {
            if (this.gameObject.transform.position.x < LevelBoundary.rightSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed * -1);
            }
        }

        if (Keyboard.current.upArrowKey.isPressed)
        {
            if (isJumping == false && isRolling == false)
            {
                isJumping = true;
                anim.Play("Jump");
                StartCoroutine(JumpSequence());
            }
        }

        if (isJumping == true)
        {
            if (comingDown == false)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 3, Space.World);
            }
            if (comingDown == true)
            {
                transform.Translate(Vector3.up * Time.deltaTime * -3, Space.World);
            }
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            if (isRolling == false)
            {
                isRolling = true;

                // Scale the player object down by half
                Vector3 newScale = originalScale;
                newScale.y = originalScale.y / 2f;
                hitbox.transform.localScale = newScale;

                // Move the player object down by half of the scale value
                Vector3 newPosition = hitbox.transform.position;
                newPosition.y -= newScale.y / 2f;
                hitbox.transform.position = newPosition;

                anim.Play("Stand To Roll");
                StartCoroutine(RollSequence());
            }
        }
    }

    IEnumerator JumpSequence()
    {
        yield return new WaitForSeconds(.35f);
        if (!isCrashing)
        {
            comingDown = true;
        }
        yield return new WaitForSeconds(.35f);
        if (!isCrashing)
        {
            isJumping = false;
            comingDown = false;
            anim.Play("Standard Run");
        }
    }

    IEnumerator RollSequence()
    {
        yield return new WaitForSeconds(1.0f);

        if (!isCrashing)
        {
            // Reset the player object's position and scale
            hitbox.transform.position += new Vector3(0, hitbox.transform.localScale.y / 2f, 0);
            hitbox.transform.localScale = originalScale;

            isRolling = false;
            anim.Play("Standard Run");
        }
    }

    public void Crash()
    {
        anim.Play("Stumble Backwards");
        isCrashing = true;
        this.enabled = false;
        if (isRolling)
        {
            StopCoroutine(RollSequence());
        }

        if (isJumping)
        {
            StopCoroutine(JumpSequence());
        }

        levelControl.GetComponent<LevelDistance>().enabled = false;
        levelControl.GetComponent<EndRunSequence>().enabled = true;
    }
}