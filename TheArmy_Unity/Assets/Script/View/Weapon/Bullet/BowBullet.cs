using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowBullet : MonoBehaviour
{
    private Entity entitySrouce;
    private Bullet bullet;
    private Transform target;
    bool isCanMakeDamage = true;
    Vector2 direction = Vector2.zero;
    Rigidbody2D rb;

    public void SetData(Entity _entity, Bullet _butlletData,Vector2 _direction,Transform _target)
    {
        entitySrouce = _entity;
        bullet = _butlletData;
        direction = _direction;
        target = _target;
        rb = GetComponent<Rigidbody2D>();

        Vector2 directiontarget = (target.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = targetRotation;

        Invoke(nameof(ForceDestroy),10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (entitySrouce.Ally && isCanMakeDamage)
        {
            if (collision.CompareTag("brick"))
            {
                
                isCanMakeDamage = false;
                Brick _brick = collision.gameObject.GetComponent<Brick>();
                bullet.DealDamageToMono(_brick);
                Destroy(gameObject);
            }
            else
            {
                if (collision.CompareTag("entity"))
                {
                   
                    EntityView entityView = collision.GetComponent<EntityView>();
                    if (entityView != null)
                    {
                        if (entityView.entity.Ally != entitySrouce.Ally)
                        {
                            isCanMakeDamage = false;
                            bullet.DealDamageToEntity(entityView.entity);
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
        else if (!entitySrouce.Ally && isCanMakeDamage)
        {
            if (collision.CompareTag("entity"))
            {
                isCanMakeDamage = false;
                EntityView entityView = collision.GetComponent<EntityView>();
                if (entityView != null)
                {
                    if (entityView.entity.Ally != entitySrouce.Ally)
                    {
                        bullet.DealDamageToEntity(entityView.entity);
                        Destroy(gameObject);
                    }
                }
            }
        }

    }

    private bool Move = false;
    public void SetMove(bool _isMove) {
        Move = _isMove;
    }
    public void ResetDealDamage()
    {
        isCanMakeDamage = true;
    }

    public void FixedUpdate()
    {
        if (Move)
        {
            MoveToTarget();
        }
        
    }

    public void MoveToTarget() {
        //Vector2 direction = (Target.transform.position - transform.position);

        Vector2 normalizedDirection = direction.normalized;



        Vector2 movement = normalizedDirection * bullet.projectileSpeed.currentValue * Time.deltaTime * GameVariableConfig.MoveBulletSpeed_Bonus;
        rb.velocity = movement;
        
    }

    private void ForceDestroy() {
        Destroy(gameObject);
    }

}
