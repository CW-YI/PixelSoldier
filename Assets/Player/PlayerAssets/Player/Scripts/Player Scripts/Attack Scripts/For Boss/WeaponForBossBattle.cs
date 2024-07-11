using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine; 
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class WeaponForBossBattle : MonoBehaviour
{
    [Header("Other Scripts / Objects References")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public Animator animator;
    public WeaponSwitchForBossBattle weaponSwitch;
    public PlayerMovementForBossBattle playerMovement;
    [SerializeField] private InputActionReference attack;

    [Header("Sound References")]
    [SerializeField] private AudioSource weaponOneAudio;
    [SerializeField] private AudioSource weaponTwoAudio;

    private bool canShootWeaponOne = true;
    private bool canShootWeaponTwo = true;
    private bool weaponTwoAnimPlayed = false;

    [Header("Weapon Delay Settings")]
    public float weaponOneDelayTimer = 0f;
    public float weaponTwoDelayTimer = 0f;
    public float weaponOneDelayBetweenAttackse = 0.2f;
    public float weaponTwoDelayBetweenAttacks = 5f;
   

    [Header("Weapon Damage Settings")]
    public int weaponOneDamage = 1;
    public int weaponTwoDamage = 5;

    

    private void Awake()
    {

    }

    void Update()
    {
        if (!playerMovement.is_stun)
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
                weaponOneDelayTimer += Time.deltaTime;

                if (weaponOneDelayTimer >= weaponOneDelayBetweenAttackse)
                {
                    canShootWeaponOne = true;
                    weaponOneDelayTimer = 0f;
                }
            }
            if (canShootWeaponTwo == false && !weaponSwitch.isUsingWeaponOne)
            {
                weaponTwoDelayTimer += Time.deltaTime;

                if (weaponTwoDelayTimer >= weaponTwoDelayBetweenAttacks)
                {
                    canShootWeaponTwo = true;
                    weaponTwoDelayTimer = 0f;
                }
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
