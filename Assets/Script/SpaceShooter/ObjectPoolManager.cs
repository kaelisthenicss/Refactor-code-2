using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [SerializeField] private GameObject playerBulletPrefab;
    [SerializeField] private GameObject enemyBulletPrefab;

    private ObjectPool<GameObject> playerBulletPool;
    private ObjectPool<GameObject> enemyBulletPool;

    private void Awake()
    {
        Instance = this;

        playerBulletPool = new ObjectPool<GameObject>(
            () => Instantiate(playerBulletPrefab),
            bullet => bullet.SetActive(true),
            bullet => bullet.SetActive(false),
            bullet => Destroy(bullet),
            false,
            3
        );

        enemyBulletPool = new ObjectPool<GameObject>(
            () => Instantiate(enemyBulletPrefab),
            bullet => bullet.SetActive(true),
            bullet => bullet.SetActive(false),
            bullet => Destroy(bullet),
            false,
            1
        );
    }


    public GameObject GetPlayerBullet()
    {
        return playerBulletPool.Get();
    }

    public void ReturnPlayerBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        playerBulletPool.Release(bullet);
    }

    public GameObject GetEnemyBullet()
    {
        return enemyBulletPool.Get();
    }

    public void ReturnEnemyBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        enemyBulletPool.Release(bullet);
    }
}
