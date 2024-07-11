using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine; //UnityEngine.UI 등 다른 것들도 있음, 원하는것에 따라 라이브러리를 포함 해야 함. 이것만 쓴다고 유니티에 있는 모든 것 사용 X
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class WeaponForTutorialOne : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public Animator animator;


    private SpriteRenderer spriteRenderer;
    private SpriteRenderer[] weaponSwitchSprite;

    public bool canShootWeaponOne = true;
    private bool canShootWeaponTwo = true;
    public float shootingDelayWeaponOne = 0.2f;
    public float shootingDelayWeaponTwo = 5f;
    public float weaponOneTimer = 0f;
    public float weaponTwoTimer = 0f;
    public int shootCounter = 0;
    private bool weaponTwoAnimPlayed = false;

    public float animationDelayWeaponOnetoTwo = 0f;
    public float animationDelayWeaponTwotoOne = 0f;

    public int weaponOneDamage = 1;
    public int weaponTwoDamage = 5;

    public WeaponSwitch weaponSwitch;
    public PlayerMovement playerMovement;

    [SerializeField] private InputActionReference attack;

    [Header("Sound References")]
    [SerializeField] private AudioSource weaponOneAudio;
    [SerializeField] private AudioSource weaponTwoAudio;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        animator.SetBool("isUsingWeaponOne", weaponSwitch.isUsingWeaponOne);
        animator.SetBool("isUsingWeaponTwo", weaponSwitch.isUsingWeaponTwo);
        if (attack.action.inProgress && weaponSwitch.isUsingWeaponOne == true)
        {
            Shoot();
        }
        else if (attack.action.inProgress && weaponSwitch.isUsingWeaponTwo == true)
        {
            ShootWeaponTwo();
        }

        if (!attack.action.inProgress)
        {
            animator.SetBool("isShootingWeaponOne", false);
            animator.SetBool("isShootingWeaponTwo", false);
        }

        if (canShootWeaponOne == false)
        {
            weaponOneTimer += Time.deltaTime;

            if (weaponOneTimer >= shootingDelayWeaponOne)
            {
                canShootWeaponOne = true;
                weaponOneTimer = 0f;
            }
        }
        if (canShootWeaponTwo == false && !weaponSwitch.isUsingWeaponOne)
        {
            weaponTwoTimer += Time.deltaTime;

            if (weaponTwoTimer >= shootingDelayWeaponTwo)
            {
                canShootWeaponTwo = true;
                weaponTwoTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        if (canShootWeaponOne == true)
        {
            weaponOneAudio.Play();
            animator.SetBool("isShootingWeaponOne", true);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            canShootWeaponOne = false;
            shootCounter++;
        }
    }

    void ShootWeaponTwo()
    {
        if (canShootWeaponTwo == true)
        {
            weaponTwoAudio.Play();
            animator.SetBool("isShootingWeaponTwo", true);
            weaponTwoAnimPlayed = true;
            Instantiate(bulletPrefab2, firePoint.position, firePoint.rotation);
            StartCoroutine(ResetWeaponTwoAnimation());
            canShootWeaponTwo = false;

        }
    }

    IEnumerator ResetWeaponTwoAnimation()
    {
        yield return new WaitForSeconds(0.55f);

        animator.SetBool("isShootingWeaponTwo", false);
        weaponTwoAnimPlayed = false;
    }
}
