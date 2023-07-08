using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
	public int health;

    private void Awake()
    {
        
    }

    public void TakeDamage(int amount)
    {
        this.health -= amount;
        if (this.health <= 0)
        {
            // Destroy the building
        }
    }
}