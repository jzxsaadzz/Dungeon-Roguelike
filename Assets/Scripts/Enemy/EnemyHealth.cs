using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class EnemyHealth : MonoBehaviour
{
    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int damage = 4;
    private int _currentHealth;
    [SerializeField] private GameObject deathVfx;

    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private EnemyAI _enemyAI;
    private bool isEnemyAlive = true;
   
    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<EnemyAI>();

    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            PlayerStats.Instance.TakeDamage(damage);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }

    public void PolygonColliderTurnOff()
    {
        _polygonCollider2D.enabled = false;
    }

    public void PolygonColliderTurnOn()
    {
        _polygonCollider2D.enabled = true;
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            _enemyAI.SetDeathState();
            Instantiate(deathVfx, transform.position,Quaternion.identity);

            OnDeath?.Invoke(this, EventArgs.Empty);
            isEnemyAlive = false;
            
        }
    }


    public bool IsEnemyAlive(){
        return isEnemyAlive;
    }


}