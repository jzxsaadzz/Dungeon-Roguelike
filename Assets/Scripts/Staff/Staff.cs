using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{   
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;

    [SerializeField] private float _fireRate = 0.5f;

    private float _fireTimer;

    public event EventHandler OnStaffShoot;
    public void Attack(){
        OnStaffShoot?.Invoke(this, EventArgs.Empty);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && _fireTimer <= 0){
            Shoot();
            _fireTimer = _fireRate;
        } 
        else {
            _fireTimer -= Time.deltaTime;
        }
    }

    private void Shoot(){
        Instantiate(_bulletPrefab,_firePoint.position,_firePoint.rotation);
    }
}
