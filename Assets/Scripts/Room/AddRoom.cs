using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    [HideInInspector] public List<GameObject> enemies; 

    public Transform[] spawnPoints; 
    public GameObject[] enemyTypes;
    public GameObject doorVfx;
    private BoxCollider2D boxCollider;

    private bool spawned;
    private bool doorsOpen;
    public GameObject boss;



    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !spawned)
        {
            spawned = true;

            foreach(Transform spawner in spawnPoints){
                int rand = Random.Range(0,11);

                if(rand< 9){
                    GameObject enemyType = enemyTypes[Random.Range(0,enemyTypes.Length)];
                    GameObject enemy = Instantiate(enemyType,spawner.position,Quaternion.identity) as GameObject;
                    enemy.transform.parent = transform;
                    enemies.Add(enemy);
                }

            }
            Instantiate(boss,transform.position,Quaternion.identity);
            
        }
    }

}
