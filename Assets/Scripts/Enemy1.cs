using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class Enemy1 : MonoBehaviour
{
    public float speed = 10;
    public float curHp = 150;
    public float totalHp;

    public GameObject destroyedEffect;

    public Slider hpSlider;
    private Transform[] wayPoints;
    private int i = 0;
    void Start()
    {
        totalHp = curHp;
        wayPoints = WayPoints.wayPoints;
    }

    void Update()
    {
        Move();
    }
    void Move()
    {
        transform.Translate((wayPoints[i].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, wayPoints[i].position) < 0.1f)
        {
            i++;
            if (i >= wayPoints.Length)
            {
                reachDestination();
                return;
            }
        }
    }

    void reachDestination()
    {
        GameObject.Destroy(this.gameObject);
        Finish.Instance.Fail();
    }

    void OnDestroy()
    {
        EnemySpawner.aliveEnemy--;
    }

    public void TakeDamage(float damage)
    {
        if (curHp <= 0) return;
        curHp -= damage;
        hpSlider.value = (float)curHp / totalHp;
        if (curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(destroyedEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }
}
