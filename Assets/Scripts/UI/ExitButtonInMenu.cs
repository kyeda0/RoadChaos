using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonInMenu : MonoBehaviour
{
    public void ExitInMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
