using System;
using System.Collections;
using UnityEngine;


public class SlimeHP : MonoBehaviour
{

    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;
    [SerializeField] private int damage = 4;
    [SerializeField] private int _maxHealth;
    [SerializeField] private GameObject deathVfx;
    private int _currentHealth;
    private bool isEnemyAlive = true;
    private BoxCollider2D _boxCollider2D;
    private CapsuleCollider2D _capsuleCollider2D;
    private SlimeAI _slimeAI;
    private PlayerScore playerScore;
  

    private void Awake()
    {

        _boxCollider2D = GetComponent<BoxCollider2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _slimeAI = GetComponent<SlimeAI>();

    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        playerScore = GameObject.FindObjectOfType<PlayerScore>();
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



    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            _boxCollider2D.enabled = false;
            _capsuleCollider2D.enabled = false;
            _slimeAI.SetDeathState();
            Instantiate(deathVfx, transform.position,Quaternion.identity);
            OnDeath?.Invoke(this, EventArgs.Empty);
            isEnemyAlive = false;
            playerScore.EnemyKilled();
        }
    }


    public bool IsEnemyAlive(){
        return isEnemyAlive;
    }


}