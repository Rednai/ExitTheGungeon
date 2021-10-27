using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    public HealthBar healthBar;
    public WeaponTime weaponTime;

    public void setMaxHealthBar(float health)
    {
        healthBar.setMaxHealth(health);
    }

    public void setHealthBar(float health)
    {
        healthBar.SetHealth(health);
    }

    public void setWeaponTime(float time)
    {
        weaponTime.setTime(time);
    }
}
