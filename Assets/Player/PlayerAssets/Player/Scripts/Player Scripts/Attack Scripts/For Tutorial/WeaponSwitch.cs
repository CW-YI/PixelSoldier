using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    private Image[] weaponSwitchSprite;
    public TutorialStageTwo tutorialStageTwo;

    public float weaponSwitchUITimer = 0f;
    public float weaponSwitchUITime = 1f;

    private int spriteIndex = 0;
    private int spriteLoadIndex = 0;

    public bool isUsingWeaponOne = true;
    public bool isUsingWeaponTwo = false;
    public bool isSwitchedWeapon = false;

    private bool canSwitchWeapon = true;

    public PlayerMovement playerMovement;

    [SerializeField] private InputActionReference switchWeapon;
    [SerializeField] private AudioSource weaponSwitchSound;


    public void Start() //ĳ���� �Ӹ����� �ִ� �ѱ� UI Sprite �鸸 ����ȭ
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
        
        if (tutorialStageTwo.canSwitchWeapon == true && canSwitchWeapon)
        {
            if (switchWeapon.action.IsPressed() && isUsingWeaponOne)
            {
                weaponSwitchSound.Play();
                spriteIndex = 1;
                weaponSwitchSprite[spriteIndex].color = new Color(1, 1, 1, 1); // Set the color of the second sprite to be opaque

                spriteIndex = 0;
                weaponSwitchSprite[spriteIndex].color = new Color(1, 1, 1, 0); // Set the color of the first sprite to be transparent

                isUsingWeaponOne = false; isUsingWeaponTwo = true;
                isSwitchedWeapon = true; canSwitchWeapon = false;
            }
            else if (switchWeapon.action.IsPressed() && isUsingWeaponTwo)
            {
                weaponSwitchSound.Play();
                spriteIndex = 0;
                weaponSwitchSprite[spriteIndex].color = new Color(1, 1, 1, 1); // Set the color of the first sprite to be opaque

                spriteIndex = 1;
                weaponSwitchSprite[spriteIndex].color = new Color(1, 1, 1, 0); // Set the color of the second sprite to be transparent

                isUsingWeaponTwo = false; isUsingWeaponOne = true;
                isSwitchedWeapon = true; canSwitchWeapon = false;
            }

            weaponSwitchUITimer = 0f;
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
