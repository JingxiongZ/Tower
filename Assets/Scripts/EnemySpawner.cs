using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int aliveEnemy = 0;
    public Wave[] waves;
    public Transform start;
    public float waveRate = 0.2f;
    void Start()
    {
        StartCoroutine("Spawn");
    }

    void Update()
    {

    }

    IEnumerator Spawn()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, start.position, Quaternion.identity);
                aliveEnemy++;
                yield return new WaitForSeconds(wave.spawnRate);
            }
            while (aliveEnemy > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while (aliveEnemy > 0)
        {
            yield return 0;
        }
        Finish.Instance.Win();
    }

    public void Stop()
    {
        StopCoroutine("Spawn");
    }
}
