using System;
using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public float _moveSpeed = 4f;
    public float _dashSpeed = 4f;
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private ActiveWeapon _activeWeapon;
    private float _minSpeed = 0.1f;
    private bool _isRunning;
    private PlayerInputActions _playerActions;
    private float _startMoveSpeed;
    private bool _isDashing = false;
    private Rigidbody2D _rb;

    private void Start() {
        GameInput.Instance.OnStaffAtack += GameInput_OnStaffAttack;
        GameInput.Instance.OnPlayerDash += GameInput_OnPlayerDash;
        _startMoveSpeed = _moveSpeed;
    }

    private void Awake(){
        Instance = this;
        _rb = GetComponent<Rigidbody2D>();
        _playerActions = new PlayerInputActions();
        _playerActions.Enable();
    }

    private void FixedUpdate() {
        Vector2 inputVector  = GameInput.Instance.GetMovementVector();
        _rb.MovePosition(_rb.position + inputVector.normalized * (_moveSpeed * Time.fixedDeltaTime));
        if (Mathf.Abs(inputVector.x) > _minSpeed || Mathf.Abs(inputVector.y) > _minSpeed ) {
            _isRunning = true;
        }
        else {
            _isRunning = false;
        }
    }

    public bool IsRunning(){
        return _isRunning;
    }

    private void GameInput_OnStaffAttack(object sender, EventArgs e){
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    private void GameInput_OnPlayerDash(object sender, EventArgs e){
        if(!_isDashing){
            _isDashing = true;
            _moveSpeed *= _dashSpeed;
            _trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine(){
        float dashTime = 0.2f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(dashTime);
        _moveSpeed = _startMoveSpeed;
        _trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        _isDashing = false;
    }
}
