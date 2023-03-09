using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{   
    public List<string> triggerTag;
    public void OnTriggerEnter2D(Collider2D other)
     {
        if (triggerTag.Contains(other.tag))
        {
            if(other.TryGetComponent<Player>(out Player player))
            {
                player.powerUpLevel++;
            }
            Destroy(gameObject);
        }
    }

}
