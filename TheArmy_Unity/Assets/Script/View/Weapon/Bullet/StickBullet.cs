using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickBullet : MonoBehaviour
{
    private Entity entitySrouce;
    private Bullet bullet;
    private Transform target;
    bool isCanMakeDamage = true;
    Vector2 direction = Vector2.zero;
    Rigidbody2D rb;
    [SerializeField] CircleCollider2D circleCollider;
    [SerializeField] ParticleSystem vfx;
    Vector3 endPos;
    public void SetData(Entity _entity, Bullet _butlletData, Vector2 _direction, Transform _target)
    {
        entitySrouce = _entity;
        bullet = _butlletData;
        direction = _direction;
        target = _target;
        rb = GetComponent<Rigidbody2D>();

        //Vector2 directiontarget = (target.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = targetRotation;
        circleCollider.radius = bullet.areaDamageRadius.currentValue;

        Invoke(nameof(ForceDestroy), 10);
        endPos = _target.transform.position;
    }

    private bool Move = false;
    public void SetMove(bool _isMove)
    {
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

    public void MoveToTarget()
    {
        Vector2 direction = endPos - transform.position;

        Vector2 normalizedDirection = direction.normalized;



        Vector2 movement = normalizedDirection * bullet.projectileSpeed.currentValue * Time.deltaTime * GameVariableConfig.MoveBulletSpeed_Bonus;
        rb.velocity = movement;
        if (Vector2.Distance(transform.position, endPos) <= 0.1f)
        {
            //Debug.LogError("BUM");
            StartCoroutine(OnDealDmg());
            //ForceDestroy();

        }

    }

    private IEnumerator OnDealDmg()
    {
        circleCollider.enabled = true;
        Vector2 center = circleCollider.bounds.center;
        Vector2 size = circleCollider.bounds.size;
        Collider2D[] overlapColliders = Physics2D.OverlapBoxAll(center, size, 0f);
        yield return new WaitForFixedUpdate();
        for (int i = 0; i < overlapColliders.Length; i++)
        {
            if (overlapColliders[i].CompareTag("entity"))
            {
                EntityView entityView = overlapColliders[i].GetComponent<EntityView>();
                if (entityView != null)
                {
                    if (entityView.entity.Ally != entitySrouce.Ally)
                    {
                        bullet.DealDamageToEntity(entityView.entity);
                    }
                }
            }
        }

        ForceDestroy();

    }

    private void ForceDestroy()
    {
        vfx.transform.parent = null;
        vfx.gameObject.SetActive(true);
        Destroy(gameObject);


    }
}
