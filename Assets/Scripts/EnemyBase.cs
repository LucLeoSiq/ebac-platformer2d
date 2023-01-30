using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;
   
    [Header("GameObject Setup")] 
    public Animator animator;
    public HealthBase healthBase;
    public AudioSource audioSource;

    [Header("EnemyBase Setup")]
    public float timeToDestroy = 1f;

    [Header("Animation Triggers")]
    public string triggerAttack = "Attack";
    public string triggerDeath = "Death";

    private void Awake()
    {
        if(healthBase != null)
        {
            healthBase.OnKill += OnEnemyKill;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();

        if (health != null)
        {
            health.Damage(damage);
            PlayAttackAnimation();
        }
    }   

    private void OnEnemyKill()
    {
        healthBase.OnKill -= OnEnemyKill;
        PlayDeathAnimation();
        Destroy(gameObject, timeToDestroy); 
    }

    private void PlayAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }

    private void PlayDeathAnimation()
    {
        animator.SetTrigger(triggerDeath);
        audioSource.Play();
    }

    public void Damage(int amount)
    {
        healthBase.Damage(amount);
    }
}
