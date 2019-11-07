using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletAttack : MonoBehaviour
{
    private Vector3 pos;
    private Transform m_Target;
    private float Speed;
    private float Damage;
    private Vector3 TargetPosition;
    private string EnemyTag;
    private HealthPoint EnemyHP;
    public float Range;
    private bool isLocked;
    public void setLocked(bool islocked)
    {
        isLocked = islocked;
    }
    public void setTarget(Transform target)
    {
        m_Target = target;
    }
    public void setSpeed(float speed)
    {
        Speed = speed;
    }
    public void setDamage(float damage)
    {
        Damage = damage;
    }
    public void setEnemyTag(string tag)
    {
        EnemyTag = tag;
    }
    public void setRange(float range)
    {
        Range = range;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (m_Target!=null)
        {
            pos = m_Target.position;
            TargetPosition = m_Target.position;
        }
        InvokeRepeating("CheckSelfDestroy", 0, 0.05f);
    }

    // Update is called once per frame
    void Update()
    { 
    if(m_Target!=null)
        {
            Fly();
        }
    }
    public void Hit()
    {
        Destroy(gameObject);
        EnemyDamage();
    }
    public void EnemyDamage()
    {
     
            EnemyHP = m_Target.GetComponent<HealthPoint>();
            if (EnemyHP!=null)
            {
                EnemyHP.Damage(Damage);
            }

    }
    public void Fly()
    {
        //判断是否锁定
        if (isLocked)
        {
            TargetPosition = m_Target.transform.position;
        }
        Vector3 dir = TargetPosition - transform.position;
        //判断是否到达目标或目标的位置超出攻击范围
        if (Vector3.Distance(TargetPosition, transform.position) > Speed * Time.deltaTime)
        {
            transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
        }
        else Destroy(gameObject);
    }
    public void CheckSelfDestroy() 
    {
        if (m_Target != null)
        {
            //判断目标是否超出攻击单位
            if (Vector3.Distance(m_Target.transform.position, pos) > Range)
            {
                m_Target = null;
                Destroy(gameObject);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == EnemyTag)
        {
            Hit();
        }
    }
}
