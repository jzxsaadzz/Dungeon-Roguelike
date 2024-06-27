using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{   
    public static ActiveWeapon Instance {get; private set;}
    [SerializeField] private Staff _staff;

    private void Awake() {
        Instance = this;
    }

    public Staff GetActiveWeapon(){
        return _staff;
    }

    private void Update() {
        MouseFollowOffset();
    }


    private void MouseFollowOffset(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        float angle = Mathf.Atan2(mousePos.y,mousePos.x) * Mathf.Rad2Deg;

        if(mousePos.x < playerScreenPoint.x){
            transform.rotation = Quaternion.Euler(0,-180,angle);
        }
        else{
            transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }
}
