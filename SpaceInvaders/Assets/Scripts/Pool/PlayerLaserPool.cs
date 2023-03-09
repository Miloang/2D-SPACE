using UnityEngine;

public class PlayerLaserPool : ObjectPool
{
    public static PlayerLaserPool Instance;

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