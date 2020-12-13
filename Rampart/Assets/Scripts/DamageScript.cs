using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour, IDamageable
{
    public int health { get; set; }

    

    public void TakeDamage() {
        Destroy(gameObject);
    }

    
}
