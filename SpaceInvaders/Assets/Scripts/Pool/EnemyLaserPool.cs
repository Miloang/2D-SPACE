using UnityEngine;


// Handle pooling for all EnemyLaser objects 
public class EnemyLaserPool : ObjectPool
{
    public static EnemyLaserPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    protected override GameObject CreateNew()
    {
        GameObject obj = base.CreateNew();
        if (obj.TryGetComponent<Projectile>(out Projectile p))
            p.pool = this;
        return obj;
    }
}