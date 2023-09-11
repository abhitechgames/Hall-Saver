using UnityEngine;

public class CentralHouse : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    
    [SerializeField] private Transform healthBar;

    [SerializeField] private GameObject abandonedHome;

    [SerializeField] private GameObject explodeEffect;

    [SerializeField] private ParticleSystem healEffect;

    [SerializeField] private GameObject retryMenu;

    [SerializeField] private LayerMask whoAreZombies;

    public static CentralHouse instance;

    private Vector3 healthScale;

    CentralHouse() => instance = this;

    private void Start()
    {
        healthScale = new Vector3(health / 100f, healthBar.localScale.y, healthBar.localScale.z);
    }

    private void Update()
    {
        healthBar.localScale = Vector3.Lerp(healthBar.localScale, healthScale, Time.deltaTime * 8f);
    }

    public void DamageCentralHall(float damageAmount)
    {
        health -= damageAmount;

        healthScale = new Vector3(health / 100f, healthBar.localScale.y, healthBar.localScale.z);

        if(health <= 0f)
        {
            ExplodeHall();
        }
    }

    public void ExplodeHall()
    {
        AudioManager.instance.Play("MASS DESTRUCTION");

        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, whoAreZombies);

        foreach (Collider C in colliders)
        {
            C.GetComponent<Zombies>().DamageEnemy(1000);
        }

        Instantiate(explodeEffect, transform.position, Quaternion.identity);
        abandonedHome.SetActive(true);
        retryMenu.SetActive(true);
        Destroy(gameObject);
    }

    public void Heal() { health = 100f; healEffect.Play(); }
}
