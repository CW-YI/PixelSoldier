using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementForBossBattle : MonoBehaviour
{

    private float moveInput;

    #region Jump variables
    [Header("Movement Controls")]

    public float maxSpeed;
    public float jumpPower = 100f;
    public float slidingSpeed;
    private int maxJumps = 1;

    private int remainingJumps;
    [Header("Ground Layer Setting")]
    public LayerMask groundLayer;

    #endregion

    #region sliding variables
    [HideInInspector] public bool isSliding = false;
    [HideInInspector] public bool isGrounded = false;
    private bool canSlide = true;
    private bool finishedSliding = false;
    private int slidingSoundCount = 1;

    [Header("Sliding Controls")]
    public float slidingJumpPower = 200f;
    public float slidingTimer = 0f;
    public float maxSlidingTimer = 1f;
    public float slidingCoolTimeTimer = 0f;
    public float maxSlidingCoolTime = 1f;
    #endregion

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRenderer;

    [Header("Sound References")]
    public AudioSource jumpStartSound;
    public AudioSource jumpEndSound;
    public AudioSource slideSound;

    [Header("Other Scripts / Objects References")]
    public Animator animator;
    public WeaponSwitchForBossBattle weaponSwitch;
    [SerializeField] private InputActionReference attack, slide, jump;
    [SerializeField] private InputActionReference moveLeftAction, moveRightAction;


    public GameObject playerObject;
    private string normalTag = "Player";
    private string invincibleTag = "Invincible Player";

    [HideInInspector] public bool is_web = false;
    [HideInInspector] public bool is_stun = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        remainingJumps = maxJumps;
    }

    private void Update()
    {
        moveInput = moveRightAction.action.ReadValue<float>() - moveLeftAction.action.ReadValue<float>();
        // Slide implementation
        if (!is_stun)
        {
            moveInput = moveRightAction.action.ReadValue<float>() - moveLeftAction.action.ReadValue<float>();
            // Slide implementation
            if (slide.action.IsPressed() && CheckSlideConditions())
            {
                playSlidingSound();
                isSliding = true;
                playerObject.tag = invincibleTag;
                spriteRenderer.color = new Color(1, 1, 1, 0.6f);

                #region animation triggers
                if (weaponSwitch.isUsingWeaponOne) animator.SetBool("isSlidingWeaponOne", true);
                else if (weaponSwitch.isUsingWeaponTwo) animator.SetBool("isSlidingWeaponTwo", true);
                #endregion

                finishedSliding = false;
            }
            else if ((!slide.action.IsPressed() || slidingTimer >= maxSlidingTimer || moveInput == 0f) && !finishedSliding)
            {
                #region animation triggers
                animator.SetBool("isSlidingWeaponOne", false);
                animator.SetBool("isSlidingWeaponTwo", false);
                #endregion

                spriteRenderer.color = new Color(1, 1, 1, 1);
                isSliding = false;
                canSlide = false;
                slidingTimer = 0f;

                playerObject.tag = normalTag;

                finishedSliding = true;

                slidingSoundCount = 1;
            }

            if (!isSliding && !canSlide)
            {
                playerObject.tag = normalTag; //슬라이딩 꾹 눌러도 tag가 원래대로 돌아오게 함
                spriteRenderer.color = new Color(1, 1, 1, 1);

                slidingCoolTimeTimer += Time.deltaTime;

                if (slidingCoolTimeTimer >= maxSlidingCoolTime)
                {
                    slidingTimer = 0f;
                    slidingCoolTimeTimer = 0f;
                    canSlide = true;
                    slidingSoundCount = 1;
                }
            }
            if (isSliding)
            {
                slidingTimer += Time.deltaTime;
                if (slidingTimer >= maxSlidingTimer)
                {
                    #region animation triggers
                    animator.SetBool("isSlidingWeaponOne", false);
                    animator.SetBool("isSlidingWeaponTwo", false);
                    #endregion

                    spriteRenderer.color = new Color(1, 1, 1, 1);
                    isSliding = false;
                    canSlide = false;
                    slidingTimer = 0f;

                    playerObject.tag = normalTag;

                    finishedSliding = true;
                    slidingSoundCount = 1;
                }
            }

            // Jump implementation
            if (jump.action.IsPressed())
            {
                jumpStartSound.Play();
                if (remainingJumps > 0 && !isSliding)
                {
                    Jump();
                }
                else if (remainingJumps > 0 && isSliding)
                {
                    slidingJump();
                }
            }

            // Stop when key is released
            if (Input.GetButtonUp("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }
        }
    }

    private void playSlidingSound()
    {
        if (slidingSoundCount == 1)
        {
            slideSound.Play();
            slidingSoundCount = 0;
        }

    }
    public IEnumerator PlayerStun()
    {
        if (!is_stun)
        {
            if (weaponSwitch.isUsingWeaponOne) animator.SetBool("isHitWeaponOne", true);
            else if (weaponSwitch.isUsingWeaponTwo) animator.SetBool("isHitWeaponTwo", true);
            is_stun = true;
            yield return new WaitForSeconds(1f);

            if (weaponSwitch.isUsingWeaponOne) animator.SetBool("isHitWeaponOne", false);
            else if (weaponSwitch.isUsingWeaponTwo) animator.SetBool("isHitWeaponTwo", false);
            is_stun = false;
        }

    }

    public bool PlayerDirection()
    {
        if (spriteRenderer.flipX) return true; //left
        else return false; // right
    }

    private void Jump()
    {
        if (weaponSwitch.isUsingWeaponOne) animator.SetBool("isJumpingGunOne", true);
        else if (weaponSwitch.isUsingWeaponTwo) animator.SetBool("isJumpingGunTwo", true);

        rigid.velocity = new Vector2(rigid.velocity.x, 0f); // Reset vertical velocity
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        remainingJumps--;
    }

    private void slidingJump()
    {
        if (weaponSwitch.isUsingWeaponOne) animator.SetBool("isJumpingGunOne", true);
        else if (weaponSwitch.isUsingWeaponTwo) animator.SetBool("isJumpingGunTwo", true);

        rigid.velocity = new Vector2(rigid.velocity.x, 0f);
        rigid.AddForce(Vector2.up * slidingJumpPower, ForceMode2D.Impulse);
        remainingJumps--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (groundLayer == (groundLayer | (1 << collision.gameObject.layer))) remainingJumps = maxJumps;

        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            jumpEndSound.Play();
            animator.SetBool("isJumpingGunOne", false);
            animator.SetBool("isJumpingGunTwo", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform")) isGrounded = false;
    }

    private void FixedUpdate()
    {
        if (!is_stun)
        {
            if (isSliding)
            {
                rigid.velocity = new Vector2(moveInput * slidingSpeed, rigid.velocity.y);
            }
            else
            {
                rigid.velocity = new Vector2(moveInput * maxSpeed, rigid.velocity.y);
            }

            // Flip sprite horizontally
            if (moveInput < 0f)
            {
                spriteRenderer.flipX = true;
                if (weaponSwitch.isUsingWeaponOne) animator.SetBool("isWalkingWeaponOne", true);
                else if (weaponSwitch.isUsingWeaponTwo) animator.SetBool("isWalkingWeaponTwo", true);
            }
            else if (moveInput > 0f)
            {
                spriteRenderer.flipX = false;
                if (weaponSwitch.isUsingWeaponOne) animator.SetBool("isWalkingWeaponOne", true);
                else if (weaponSwitch.isUsingWeaponTwo) animator.SetBool("isWalkingWeaponTwo", true);
            }
            else if (moveInput == 0)
            {
                animator.SetBool("isWalkingWeaponOne", false);
                animator.SetBool("isWalkingWeaponTwo", false);
            }

            // Limit horizontal velocity
            if (isSliding == false && Mathf.Abs(rigid.velocity.x) > maxSpeed)
            {
                rigid.velocity = new Vector2(Mathf.Sign(rigid.velocity.x) * maxSpeed, rigid.velocity.y);
            }
            else if (isSliding == true && Mathf.Abs(rigid.velocity.x) > slidingSpeed)
            {
                rigid.velocity = new Vector2(Mathf.Sign(rigid.velocity.x) * slidingSpeed, rigid.velocity.y);
            }
        }
    }

    private bool CheckSlideConditions()
    {
        if (isGrounded && !is_web && !is_stun && moveInput != 0 && canSlide) return true;
        else return false;
    }
}
