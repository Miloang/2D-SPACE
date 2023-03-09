using UnityEngine;


// Handle pooling for all EnemyLaser objects 
public class ExplosionPool: ObjectPool
{
    public static ExplosionPool Instance;

    private void Awake()
    {
        Instance = this;
    }

}