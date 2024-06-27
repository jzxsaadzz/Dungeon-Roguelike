using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    [SerializeField] private int damage = 4;


    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            PlayerStats.Instance.TakeDamage(damage);
        }
    }


}
