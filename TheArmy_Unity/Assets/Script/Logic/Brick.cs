using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class Brick : MonoBehaviour
{
    private int Money;
    public float health { get;private set;} = 1;
    public ParticleSystem vfxDestroy;
    public SpriteRenderer spr;
    public BoxCollider2D collider2D;
    public TMP_Text txtHealth;
    Tweener tweener1, tweener2;
    private void Awake()
    {
        collider2D = GetComponent<BoxCollider2D>();
        spr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        vfxDestroy = transform.GetChild(1).GetComponent<ParticleSystem>();
        txtHealth = transform.GetChild(2).GetComponent<TMP_Text>();
    }
    public void SetHealth(float _health) {
        Money = (int)_health;
        health = _health;
        txtHealth.text = $"{Math.Ceiling(health)}";
    }
    public void TakeDamage(float _damage) {
        if (tweener1 != null) {
            tweener1.Kill();
        }
        if (tweener2 != null)
        {
            tweener2.Kill();
        }

        health -=_damage;
        if (health <=0) {
            health = 0;
            Dead();
        }
        txtHealth.text = $"{Math.Ceiling(health)}";
        tweener1 = transform.DOScale(Vector2.one * 0.8f, 0.1f).OnComplete(() => {
            tweener2 = transform.DOScale(Vector2.one, 0.05f);
        });
    }
    public void Dead() {
        UserData.Money += Money;
        collider2D.enabled = false;
        spr.gameObject.SetActive(false);
        txtHealth.gameObject.SetActive(false);
        vfxDestroy.Play();
        GameController.Instance.RemoveMono(this);
    }
}
