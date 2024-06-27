using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    private float _lifeTime = 0.5f;

    void Start(){
        Destroy(gameObject, _lifeTime);

    }
    
}
