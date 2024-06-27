using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance {get; private set;}

    private PlayerInputActions _playerActions;

    public event EventHandler OnStaffAtack;

    public event EventHandler OnPlayerDash;

    public EventHandler OnPause;

    private void Awake() {
        Instance = this;
        _playerActions = new PlayerInputActions();
        _playerActions.Enable();
        _playerActions.Combat.Attack.started += PlayerAttack_started;
        _playerActions.Combat.Dash.performed += PlayerDash_performed;
        _playerActions.Player.Menu.performed += Pause_performed;
        
    }

    public Vector2 GetMovementVector(){
        Vector2 inputVector = _playerActions.Player.Move.ReadValue<Vector2>();

        return inputVector;
    }

    private void PlayerAttack_started(InputAction.CallbackContext obj){
        OnStaffAtack?.Invoke(this, EventArgs.Empty);
    }


    private void PlayerDash_performed(InputAction.CallbackContext obj){
        OnPlayerDash?.Invoke(this, EventArgs.Empty);
    }
    
    
    
    private void Pause_performed(InputAction.CallbackContext obj){
        OnPause?.Invoke(this, EventArgs.Empty); // вызываем событие OnPause
    }




   
}
