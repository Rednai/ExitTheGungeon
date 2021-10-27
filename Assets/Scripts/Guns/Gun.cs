using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    public GameObject bulletPrefab;
    public float bulletDamage = 10f;
    public float bulletForce = 15f;
    [Range(0.0f, 1.0f)]
    public float flashingTime = 0.05f;
    public float fireRate;
    public string shootSound;

    private Transform firePoint;
    private GameObject flash;
    private Animator animator;
    private float readyForNextshoot;

    void Start()
    {
        animator = GetComponent<Animator>();
        firePoint = transform.Find("FirePoint");
        flash = GameObject.Find("Flash");
        flash.SetActive(false);
    }

    void OnEnable()
    {
        if (flash)
            flash.SetActive(false);
    }

    public void Shoot()
    {
        // Check the gun fire rate
        if (Time.time < readyForNextshoot || InGameMenu.instance.GameIsPaused)
            return;
        readyForNextshoot = Time.time + 1 / fireRate;

        // Bullet instantiation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Bullet>().setBulletDamage(bulletDamage);
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), transform.root.GetComponent<Collider2D>());

        // Muzzle flash
        if (!flash.activeSelf)
        {
            StartCoroutine(DisplayFlash());
        }

        // Camera shake
        CameraShake.instance.ShakeCamera(1f, 0.1f);

        // Recoil animation
        animator.SetTrigger("Shoot");

        // audio
        if (shootSound != "")
        {
            AudioManager.instance.Play(shootSound);
        }
    }

    private IEnumerator DisplayFlash()
    {
        flash.SetActive(true);

        yield return new WaitForSeconds(flashingTime);

        flash.SetActive(false);
    }
}
