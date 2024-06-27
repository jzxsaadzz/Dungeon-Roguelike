using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public Vector3 cameraChange;
    public Vector3 playerChange;
    private Camera cam;
    private bool hasTransitioned = false;
    private float transitionCooldown = 1.0f; // время задержки в секундах
    private float lastTransitionTime;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
        lastTransitionTime = Time.time - transitionCooldown; // инициализируем время последнего перехода
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTransitioned && Time.time >= lastTransitionTime + transitionCooldown)
        {
            cam.transform.position += cameraChange;
            other.transform.position += playerChange;
            hasTransitioned = true;
            lastTransitionTime = Time.time; // обновляем время последнего перехода
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasTransitioned = false;
        }
    }
}
