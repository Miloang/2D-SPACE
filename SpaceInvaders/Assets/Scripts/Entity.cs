using UnityEngine;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
    public GameObject explosionPrefab;
    public List<string> triggerTag;
    private PointManager pointManager;

    void Start()
    {
        pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerTag.Contains(other.tag))
        {
            OnDie();
            Destroy(gameObject);
            if (other.TryGetComponent<Projectile>(out Projectile p))
                p.Release();
            pointManager.UpdateScore(100); 
        }
    }

    protected virtual void OnDie()
    {
        GameObject explosion = ExplosionPool.Instance.Get();    
        explosion.transform.position = transform.position;
    }
}