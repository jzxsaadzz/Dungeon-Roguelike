using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSkript : MonoBehaviour
{
    private PlayerScore playerScore;

    private void Start()
    {
        playerScore = GameObject.FindObjectOfType<PlayerScore>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerScore.DoorTouched(); // вызываем метод DoorTouched

            // Получаем имя текущей сцены
            string currentSceneName = SceneManager.GetActiveScene().name;

            // Проверяем, на какой сцене мы находимся, и переходим на следующую
            if (currentSceneName == "NextLevel")
            {
                SceneManager.LoadScene("NextLevel1");
            }
            else if (currentSceneName == "NextLevel1")
            {
                SceneManager.LoadScene("Game");
            }
            else
            {
                SceneManager.LoadScene("NextLevel");
            }
        }
    }
}
