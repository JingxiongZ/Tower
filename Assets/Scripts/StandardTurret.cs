using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StandardTurret : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();

    public float timePerAttack = 1;

    public Transform head;
    private float timer = 0;
    public GameObject missilePrefab;
    public Transform firePos;
    public bool useLaser = false;
    public float damageRate = 80;
    public LineRenderer laserRenderer;
    public GameObject laserEffect;
    void Start()
    {
        timer = timePerAttack;
    }

    void Update()
    {
        if (enemies.Count > 0 && enemies[0] != null)
        {
            Vector3 enemyPos = enemies[0].transform.position;
            enemyPos.y = head.position.y;
            head.LookAt(enemyPos);
        }
        if (!useLaser)
        {
            timer += Time.deltaTime;
            if (enemies.Count > 0 && timer >= timePerAttack)
            {
                timer = 0;
                Attack();
            }
        }
        else if (enemies.Count > 0)
        {
            laserRenderer.enabled = true;
            laserEffect.SetActive(true);
            if (enemies[0] == null)
            {
                UpdateEnemies();
            }
            if (enemies.Count > 0)
            {
                laserRenderer.SetPositions(new Vector3[] { firePos.position, enemies[0].transform.position });
                enemies[0].GetComponent<Enemy1>().TakeDamage(damageRate * Time.deltaTime);
                laserEffect.transform.position = enemies[0].transform.position;
                Vector3 pos = transform.position;
                pos.y = enemies[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }
        }
        else
        {
            laserEffect.SetActive(false);
            laserRenderer.enabled = false;
        }
    }

    void Attack()
    {
        if (enemies[0] == null)
        {
            UpdateEnemies();
        }
        if (enemies.Count > 0)
        {
            GameObject missile = GameObject.Instantiate(missilePrefab, firePos.position, firePos.rotation);
            missile.GetComponent<Bullet>().SetTarget(enemies[0].transform);
        }
        else
        {
            timer = timePerAttack;
        }

    }

    void UpdateEnemies()
    {
        enemies.RemoveAll(enemy => enemy == null);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "enemy")
        {
            enemies.Add(collider.gameObject);
        }

    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "enemy")
        {
            enemies.Remove(collider.gameObject);
        }
    }

}
