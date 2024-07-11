using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponSwitchForBossBattle : MonoBehaviour
{
    private Image[] weaponSwitchSprite;

    [Header("Weapon Switch Delay Settings")]
    public float weaponSwitchUITimer = 0f;
    public float weaponSwitchUITime = 1f;

    private int spriteIndex = 0;
    private int spriteLoadIndex = 0;

    [HideInInspector] public bool isUsingWeaponOne = true;
    [HideInInspector] public bool isUsingWeaponTwo = false;
    [HideInInspector] public bool isSwitchedWeapon = false;

    private bool canSwitchWeapon = true;

    [Header("Other Script / Source References")]
    public PlayerMovementForBossBattle playerMovement;
    [SerializeField] private AudioSource weaponSwitchSound;
    [SerializeField] private InputActionReference switchWeapon;


    public void Start() //캐릭터 머리위에 있는 총구 UI Sprite 들만 투명화
    {
        weaponSwitchSprite = GetComponentsInChildren<Image>();

        foreach (Image weaponSwitchUIRenderer in weaponSwitchSprite)
        {
            if (weaponSwitchUIRenderer != GetComponent<Image>())
            {
                if (spriteLoadIndex == 0) weaponSwitchUIRenderer.color = new Color(1, 1, 1, 1);
                else weaponSwitchUIRenderer.color = new Color(1, 1, 1, 0);

                spriteLoadIndex++;
            }
        }
    }

    void Update()
    {
        if (!playerMovement.is_stun)
        {
            if (switchWeapon.action.IsPressed() && canSwitchWeapon)
            {
                weaponSwitchSound.Play();
                if (isUsingWeaponOne == true)
                {
                    spriteIndex = 1;
                    weaponSwitchSprite[spriteIndex].color = new Color(1, 1, 1, 1); // Set the color of the second sprite to be opaque

                    spriteIndex = 0;
                    weaponSwitchSprite[spriteIndex].color = new Color(1, 1, 1, 0); // Set the color of the first sprite to be transparent

                    isUsingWeaponOne = false; isUsingWeaponTwo = true;
                    isSwitchedWeapon = true; canSwitchWeapon = false;
                }
                else if (isUsingWeaponTwo == true)
                {
                    spriteIndex = 0;
                    weaponSwitchSprite[spriteIndex].color = new Color(1, 1, 1, 1); // Set the color of the first sprite to be opaque

                    spriteIndex = 1;
                    weaponSwitchSprite[spriteIndex].color = new Color(1, 1, 1, 0); // Set the color of the second sprite to be transparent

                    isUsingWeaponTwo = false; isUsingWeaponOne = true;
                    isSwitchedWeapon = true; canSwitchWeapon = false;
                }
            }
        }
        

        if (isSwitchedWeapon && !canSwitchWeapon)
        {
            weaponSwitchUITimer += Time.deltaTime;

            if (weaponSwitchUITimer >= weaponSwitchUITime)
            {
                isSwitchedWeapon = false;
                canSwitchWeapon = true;
                weaponSwitchUITimer = 0f;
            }
        }
    }
}
