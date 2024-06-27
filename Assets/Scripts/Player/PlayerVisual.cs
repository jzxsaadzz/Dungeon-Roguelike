using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{   
    
    private Animator _animator;

    private SpriteRenderer _spriteRenderer;

    private const string IS_DIE = "IsDie";
    private const string TAKEHIT = "TakeHit";


    private void Start() {
        PlayerStats.Instance.OnDeath += Player_OnDeath;
        PlayerStats.Instance.OnTakeHit+= Player_OnTakeHit;
    }

    private void Awake() {

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update() {
        _animator.SetBool("IsRunning", PlayerController.Instance.IsRunning());
    }




    private void FixedUpdate() {
        if(PlayerStats.Instance.IsAlive() == true){
            AdjustFacingPosition();  
        }
        
    }

    private void AdjustFacingPosition(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x) {
            _spriteRenderer.flipX = true;
        } else {
            _spriteRenderer.flipX = false;
        }
    
    }


    private void Player_OnDeath(object sender, System.EventArgs e)
    {
        _animator.SetBool(IS_DIE, true);
        _spriteRenderer.sortingOrder = 4;
    }

    private void Player_OnTakeHit(object sender, System.EventArgs e)
    {
        _animator.SetTrigger(TAKEHIT);
    }
}
