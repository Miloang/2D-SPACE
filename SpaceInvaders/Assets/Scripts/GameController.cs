using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public List<Enemy> enemies;

    [Header("Enemy Movement")]
    public float movingSpeed = 1f;
    public GameObject enemyContainer;
    public Player player;
    public float horizontalLimit = 3f;
    public float verticalLimit = 1.4f;
    private float movingDirection = 1;
    private Vector2 targetPosition;

    [Header("Enemy Attack")]
    public float shootingInterval = 3f;
    public float shootingSpeed = 2f;
    public GameObject enemyLaserPrefab;
    public bool allowShoot = true;
    private float shootingTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Enemy[] _enemies = GetComponentsInChildren<Enemy>();
        enemies = new List<Enemy>();
        enemies.AddRange(_enemies);

        targetPosition = enemyContainer.transform.position;
        shootingTimer = shootingInterval;
    }

    private void Update()
    {
        if (enemies != null && enemies.Count > 0)
        {
            EnemyAttack();
            EnemyMovement();
        }
        else
            SceneController.Instance.ChangeScene("Menu",1f);
    }

    private void EnemyAttack()
    {
        if (!allowShoot) return;

        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0)
        {
            shootingTimer = shootingInterval;
            Enemy randomEnemy = enemies[Random.Range(0, enemies.Count)];

            if (randomEnemy != null)
            {
                GameObject laser = EnemyLaserPool.Instance.Get();
                if (laser != null)
                {
                    laser.transform.position = randomEnemy.transform.position;
                    if (laser.TryGetComponent<Projectile>(out Projectile p))
                    {
                        p.Init();
                        p.CancelInvoke();
                        p.Invoke("Release", p.lifetime);
                    }
                }
            }
            else shootingTimer = 0;
        }
    }

    private void EnemyMovement()
    {
        enemyContainer.transform.position = Vector2.MoveTowards(
            enemyContainer.transform.position, targetPosition, Time.deltaTime * movingSpeed
        );

        float endMostPosition = 0f;
        foreach (Enemy enemy in enemies)
        {
            if (movingDirection > 0)
                endMostPosition = enemy.transform.position.x > endMostPosition
                 ? enemy.transform.position.x : endMostPosition;
            else
                endMostPosition = enemy.transform.position.x < endMostPosition
                 ? enemy.transform.position.x : endMostPosition;
        }

        if (Mathf.Abs(endMostPosition) > horizontalLimit)
        {
            movingDirection *= -1;
            targetPosition = new Vector2(endMostPosition, enemyContainer.transform.position.y - 0.2f);
        }

        targetPosition = new Vector2((horizontalLimit * movingDirection) + endMostPosition, targetPosition.y);
    }

}