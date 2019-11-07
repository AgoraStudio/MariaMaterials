using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierAttack : MonoBehaviour
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
        CountDown = 1 / AttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CountDown -= Time.deltaTime;
        if (CountDown <= 0&&CanAttack&&Target!=null && Vector3.Distance(transform.position, Target.position) < Range)
            {
                Shoot();
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
        CountDown = 1 / AttackSpeed;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //判断是否被触发，并且将触发者设为敌方
        CanAttack = (collision.tag == Enemytag);
        Target = collision.gameObject.transform;
    }
}
