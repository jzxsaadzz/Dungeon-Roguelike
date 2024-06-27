using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffVisual : MonoBehaviour
{   

    [SerializeField] private Staff _staff;
    private Animator _animator;

    private ActiveWeapon _activeWeapon;

    

    private void Awake() {
        _animator = GetComponent<Animator>();
        _activeWeapon = GetComponent<ActiveWeapon>();
        
    }

    private void Start() {
        _staff.OnStaffShoot += Staff_OnStaffShoot;

    }

    private void Staff_OnStaffShoot(object sender, EventArgs e){
        _animator.SetTrigger("Attack");
    }


}
