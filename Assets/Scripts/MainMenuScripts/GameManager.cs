using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject panelResult;

    private void Start() {
        panelResult.SetActive(false);
    }

    public void Play(){
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }


    public void Back(){
        SceneManager.LoadScene("Menu");
    }
    
    public void Result(){
        if(panelResult.activeSelf == false){
            panelResult.SetActive(true);
        }
        else if (panelResult.activeSelf == true){
            panelResult.SetActive(false);
        }
    }

    public void Exit(){
        Application.Quit();
    }

}
