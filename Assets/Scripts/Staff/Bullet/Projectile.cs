using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float _speed = 20f; 
    public float _lifeTime = 5f;
    private Vector3 _direction; 
    public LayerMask wallLayer; // добавьте эту строку
    public int damageAmount = 8;
    
    void Start()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = transform.position.z;
        _direction = (mousePosition - transform.position).normalized;
        Destroy(gameObject, _lifeTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (wallLayer == (wallLayer | (1 << other.gameObject.layer))) { // проверка на столкновение со стеной
            Destroy(gameObject);
            return;
        }
        StormHP enemyHealth = other.gameObject.GetComponent<StormHP>();
        SlimeHP slimeHP = other.gameObject.GetComponent<SlimeHP>();
        enemyHealth?.TakeDamage(damageAmount);
        slimeHP?.TakeDamage(damageAmount);
    }
}
