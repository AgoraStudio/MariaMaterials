using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public GameObject Main;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    void Update()
    {

    }
    public void Damage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth<=0)
        {
            Destroy(gameObject);
        }
    }
}