using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 50;
    public float speed = 20f;

    public GameObject explosionEffectPrefab;
    private Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void Start()
    {

    }

    void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "enemy")
        {
            collider.GetComponent<Enemy1>().TakeDamage(damage);
            Die();
        }

    }

    void Die()
    {
        GameObject explosion = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(explosion, 1);
        Destroy(this.gameObject);
    }

}
