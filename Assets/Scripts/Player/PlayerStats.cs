using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    public Text healthText;
    public Slider healthSlider;

    public float _maxHealth = 20;
    private float _currentHealth;
    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;

    [SerializeField] private PlayerController _playerController;

    private BoxCollider2D boxCollider2D;
    private CapsuleCollider2D capsuleCollider2D;

    public GameObject activeWeapon;

    private bool isAlive = true;


    private void Awake() {
        Instance = this;
        boxCollider2D = GetComponent<BoxCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    private void Start() {
        _currentHealth = _maxHealth;
        healthSlider.value = 1;
        UIHealthChange();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
        UIHealthChange();

    }

    public void HealPlayer(float heal){
        _currentHealth += heal;
        CheckOverHeal();
        UIHealthChange();
    }

    private void CheckOverHeal(){
        if(_currentHealth > _maxHealth){
            _currentHealth = _maxHealth;
        }
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            isAlive = false;
            Die(); 
            _currentHealth = 0;
        }
    }

    private void Die()
    {
        Destroy(activeWeapon); 
        OnDeath?.Invoke(this, EventArgs.Empty);
        _playerController.enabled = false;
        boxCollider2D.enabled = false;
        capsuleCollider2D.enabled = false;
    }

    private void UIHealthChange(){
        healthSlider.value = CalculateHealth();
        healthText.text = Mathf.Ceil(_currentHealth).ToString() + "/" + Mathf.Ceil(_maxHealth).ToString();
    }

    public bool IsAlive(){
        return isAlive;
    }

    private float CalculateHealth(){
        return _currentHealth/_maxHealth;
    }
}
