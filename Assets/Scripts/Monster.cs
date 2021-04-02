using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Sprite deadSprite;
    public ParticleSystem _particleSystem;
    private bool _hasDied = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(ShouldDieFromCollision(collision))
        {
            StartCoroutine(Die());
        }
    }

    private bool ShouldDieFromCollision(Collision2D collision)
    {
        if (_hasDied) return false;
        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null) return true;
        if (collision.contacts[0].normal.y < -0.5) return true;
        
        return false;
    }

    private IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
