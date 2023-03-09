using UnityEngine;
using System.Collections.Generic;

public class Enemy : Entity
{
    public GameObject powerUpPrefab;

    public float powerUpChance = 0.5f;
    
    protected override void OnDie()
    {
        base.OnDie();
        GameController.Instance.enemies.Remove(this);
        if (Random.value <= powerUpChance)
        {
            Destroy(gameObject);
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        }
    }

}