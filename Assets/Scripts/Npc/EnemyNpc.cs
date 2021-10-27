using System.Collections;
using UnityEngine;
using Pathfinding;

public class EnemyNpc : MonoBehaviour, IEnemyNpc
{
    public Canvas healthBarCanvas;
    public GameObject deathParticle;
    public float meleeDamage;
    public float maxHealth;

    private float health;
    private enum State {
        Normal,
        Dead
    };
    private State state;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private NpcHealthBar healthBar;
    private AIDestinationSetter aiDestinationSetter;
    private AIPath aiPath;
    private bool facingRight = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        healthBar = healthBarCanvas.GetComponent<NpcHealthBar>();
        aiPath = GetComponent<AIPath>();
        aiDestinationSetter = GetComponent<AIDestinationSetter>();

        health = maxHealth;
        state = State.Normal;

        healthBarCanvas.gameObject.SetActive(true);
        healthBar.SetHealth(health, maxHealth);

        aiDestinationSetter.target = GameManager.instance.player.transform;
    }

    private void Update()
    {
        CheckFlip();
    }

    public void ReceiveDamage(float damage)
    {
        if (state != State.Normal)
            return;

        health -= damage;
        healthBar.SetHealth(health, maxHealth);
        if (health <= 0)
        {
            Death();
            return;
        }
        Hit();
    }

    private void Hit()
    {
        animator.SetTrigger("Hit");
    }

    private void Death()
    {
        state = State.Dead;
        boxCollider2D.enabled = false;
        aiPath.isStopped = true;
        StartCoroutine(playDeathAnimation());
        GameManager.instance.addEnnemyDead();
    }

    private IEnumerator playDeathAnimation()
    {
        animator.SetTrigger("Death");

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        DropManager.instance.DropAnItem(transform.position);
        GameObject particle = Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(particle, 0.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().ReceiveDamage(meleeDamage);
        }
    }

    private void CheckFlip()
    {
        if ((aiPath.desiredVelocity.x >= 0.01f && !facingRight) || (aiPath.desiredVelocity.x <= -0.01f && facingRight))
            Flip();
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }
}
