using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth;

    private float health;
    private PlayerMovement playerMovement;
    private PlayerAnimations playerAnimations;
    private PlayerAimWeapon playerAimWeapon;
    private PlayerUi playerUi;
    private IPlayerInput playerInput;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
        playerAimWeapon = GetComponent<PlayerAimWeapon>();
        playerUi = GetComponent<PlayerUi>();
        playerInput = GetComponent<IPlayerInput>();
        health = maxHealth;

        // Events
        playerInput.OnRollEvent = playerMovement.handleRoll;
        playerInput.OnShootEvent = () => { playerAimWeapon.handleShoot(playerMovement.state); };
        playerInput.OnPauseEvent = InGameMenu.instance.PausePressed;

        // Animations
        playerMovement.playRollAnimation = playerAnimations.playRoll;

        // Init the ui
        playerUi.setMaxHealthBar(maxHealth);
    }

    void FixedUpdate()
    {
        // The player can only aim on normal state
        if (playerMovement.state == PlayerMovement.State.Normal)
        {
            playerAimWeapon.playerAim(playerInput.aimPointVector);
        }
        playerMovement.managePlayerMovement(playerInput.movementInputVector);
        playerAnimations.setSpeed(playerInput.movementInputVector);
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
        playerUi.setHealthBar(health);
    }

    public void ReceiveDamage(float damage)
    {
        if (playerMovement.state == PlayerMovement.State.Roll || health <= 0)
            return;

        health -= damage;
        playerUi.setHealthBar(health);
        if (health <= 0)
        {
            playerMovement.freezeMovement();
            playerAnimations.playDeath();
        } else
        {
            playerAnimations.playHit();
        }
    }

    public void equipWeapon(string name, float time)
    {
        playerUi.setWeaponTime(time);
        playerAimWeapon.equipWeapon(name, time);
    }
}
