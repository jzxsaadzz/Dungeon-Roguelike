using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class StormHP : MonoBehaviour
{
    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int damage = 4;
    private int _currentHealth;
    [SerializeField] private GameObject deathVfx;
    private CapsuleCollider2D _capsuleCollider2D;

    private PolygonCollider2D _polygonCollider2D;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D rb;
    private StormAI _enemyAI;
    private bool isEnemyAlive = true;
    private PlayerScore playerScore;

    public GameObject door; 
    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _enemyAI = GetComponent<StormAI>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        playerScore = GameObject.FindObjectOfType<PlayerScore>(); // добавьте эту строку
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
            playerScore.EnemyKilled();
            Destroy(gameObject);
            _enemyAI.SetDeathState();
            Instantiate(door, transform.position, Quaternion.identity); 
            OnDeath?.Invoke(this, EventArgs.Empty);
            isEnemyAlive = false;
        }
    }


    public bool IsEnemyAlive(){
        return isEnemyAlive;
    }


}