using UnityEngine;

public class Zombies : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [Range(0.01f, 50f)]
    [SerializeField] private float moveSpeed = 40f;

    [SerializeField] private Animator animator;

    [SerializeField] private Transform target;

    [Range(0f, 5f)]
    [SerializeField] private float startAttackRate = 1f;
    private float currentAttackRate;

    private bool houseIsTarget = true;

    [Range(0f, 15f)]
    [SerializeField] private float damage = 5f;

    [Range(10, 100)]
    [SerializeField] private int health = 30;
    private int maxHealth;

    [SerializeField] private Transform healthBar;

    private Cannon defenseToTarget;

    [SerializeField] private LayerMask whatAreDefences;

    private bool dead = false;

    private void Start()
    {
        currentAttackRate = startAttackRate;

        maxHealth = health / 2;

        healthBar.localScale = new Vector3(health / maxHealth, healthBar.localScale.y, healthBar.localScale.z);
    }

    private void FixedUpdate()
    {
        if(dead) return;

        if (target == null)
        {
            if (CentralHouse.instance == null) return;
            else target = CentralHouse.instance.transform;
        }

        if (Vector3.Distance(transform.position, target.position) < 2.75f)
        {
            if (currentAttackRate < 0f)
            {
                Attack();
                currentAttackRate = startAttackRate;
            }
            else
            {
                currentAttackRate -= Time.deltaTime;
            }
        }
        else
        {
            rb.position = Vector3.MoveTowards(rb.position, new Vector3(target.position.x, transform.position.y, target.position.z), moveSpeed * Time.deltaTime);
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Defense"))
        {
            target = other.transform;
            defenseToTarget = other.GetComponent<Cannon>();
            houseIsTarget = false;
        }
    }

    public void Attack()
    {
        animator.SetTrigger("ATTACK");

        if (houseIsTarget)
            CentralHouse.instance.DamageCentralHall(damage);
        else
        {
            defenseToTarget.DefenseDamage(damage);            
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        healthBar.localScale = new Vector3(Mathf.Lerp(health / maxHealth, 0f, 2f), healthBar.localScale.y, healthBar.localScale.z);

        if(health <= 0)
        {
            animator.SetTrigger("DIE");
            dead = true;

            Destroy(gameObject, 2.5f);
        }
    }
}
