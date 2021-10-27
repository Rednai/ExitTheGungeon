using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletImpact;

    private float bulletDamage;

    public void setBulletDamage(float damage)
    {
        bulletDamage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Npc")
        {
            collision.gameObject.GetComponent<IEnemyNpc>().ReceiveDamage(bulletDamage);
        }

        GameObject impact = Instantiate(bulletImpact, transform.position, Quaternion.identity);
        Destroy(impact, 0.5f);
        Destroy(gameObject);
    }
}
