using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed = 50f;

    [SerializeField] private int damageAmount = 20;

    [SerializeField] private GameObject explodeEffect;

    private bool isDone = true;

    [SerializeField] private bool isCatapult;

    [SerializeField] private ParticleSystem ps;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if(isCatapult) rb.AddForce((transform.forward + transform.up) * 5, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        if(!isCatapult) rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isDone)
        {
            Instantiate(explodeEffect, transform.position, Quaternion.identity);

            speed = 0f;

            isDone = false;

            ps.Stop();

            if(other.CompareTag("Zombies"))
                other.GetComponent<Zombies>().DamageEnemy(damageAmount);

            Destroy(gameObject, 0.5f);
        }
    }
}
