using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeBullet : MonoBehaviour
{
    private Entity entitySrouce;
    private Bullet bullet;
    bool isCanMakeDamage = true;

    public void SetData(Entity _entity, Bullet _butlletData)
    {
        entitySrouce = _entity;
        bullet = _butlletData;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (entitySrouce.Ally)
        {
            if (collision.CompareTag("brick"))
            {
                Brick _brick = collision.gameObject.GetComponent<Brick>();
                bullet.DealDamageToMono(_brick);
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
                            bullet.DealDamageToEntity(entityView.entity);
                        }
                    }
                }
            }
        }
        else if (!entitySrouce.Ally)
        {
            if (collision.CompareTag("entity"))
            {
                EntityView entityView = collision.GetComponent<EntityView>();
                if (entityView != null)
                {
                    if (entityView.entity.Ally != entitySrouce.Ally)
                    {
                        bullet.DealDamageToEntity(entityView.entity);
                    }
                }
            }
        }

    }
}
