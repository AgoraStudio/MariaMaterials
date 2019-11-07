using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float Range;//范围
    public string Enemytag;//敌人标签
    public float AttackSpeed;//攻速
    public float Speed;//子弹速度
    public float Damage;//攻击力
    private Transform Target;//目标
    public float CountDown;//倒计时
    public bool IsLocked;//是否锁定
    public GameObject BulletPrefab;
    private bool CanAttack;//是否可以攻击
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        CountDown -= Time.deltaTime;
        if (Target != null && Vector3.Distance(transform.position, Target.position) < Range)
        {
            if (CountDown <= 0)
            {
                Shoot();
            }
        }
    }
    private void Shoot()
    {
        GameObject bulletSet = Instantiate(BulletPrefab, transform.position, new Quaternion());
        bulletAttack bullet = bulletSet.GetComponent<bulletAttack>();
        //将组件的属性导入子弹
        bullet.setSpeed(Speed);
        bullet.setDamage(Damage);
        bullet.setTarget(Target);
        bullet.setRange(Range);
        bullet.setEnemyTag(Enemytag);
        bullet.setLocked(IsLocked);
        CountDown = 1 / AttackSpeed;
    }
    private void UpdateTarget()
    {
        List<GameObject> enemies = new List<GameObject>();
        //可能会出错
        enemies.Add(GameObject.FindGameObjectWithTag(Enemytag));
        Transform NearestEnemy = null;
        float minDistance = Mathf.Infinity;
        if (enemies[0]!=null)
        {
            foreach (var enemy in enemies)
            {
                float distance = Vector3.Distance(enemy.transform.position, transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    NearestEnemy = enemy.transform;//找到最近的敌人
                }
                if (minDistance <= Range)
                {
                    Target = NearestEnemy;
                }
                else Target = null;
            }
        }

    }
}
