using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "main";
    public void newGameButton() {
            SceneManager.LoadScene(newGameLevel);
    }
}
