using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Range(0.1f, 3f)]
    [SerializeField] private float shootRate = .75f;
    private float currentTime = 0f;

    [SerializeField] private Transform GFX;

    [SerializeField] private Transform[] shootPos;

    [SerializeField] private GameObject projectile;

    [Range(2f, 30f)]
    [SerializeField] private float radius;

    [SerializeField] private LayerMask whoAreZombies;

    [SerializeField] private Transform target;

    [SerializeField] private Animator animator;

    [SerializeField] private float health;

    [SerializeField] private GameObject explodeEffect;

    private AudioSource audioSource;
 
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(target == null)
        {   
            if(Physics.CheckSphere(transform.position, radius, whoAreZombies))
            {
                Collider[] collider = Physics.OverlapSphere(transform.position, radius, whoAreZombies);

                if(collider != null)
                    target = collider[0].transform;
            }
        }
        else
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

            if(currentTime > shootRate)
            {
                if(animator) animator.SetTrigger("SWING");

                foreach (Transform T in shootPos)
                {
                    Instantiate(projectile, T.position, GFX.rotation);
                    audioSource.Play();
                }

                currentTime = 0f;
            }
        }

        currentTime += Time.deltaTime;
    }

    public void DefenseDamage(float damageAmount)
    {
        health -= damageAmount;

        if(health <= 0f)
        {
            Instantiate(explodeEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
