using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void StartBtn(string newGameLevel) {
        SceneManager.LoadScene(newGameLevel);
    }

    public void ExitBtn() {
        Application.Quit();
    }
}
