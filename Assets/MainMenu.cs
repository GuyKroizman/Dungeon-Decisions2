using UnityEngine;

public class MainMenu : MonoBehaviour {

	public void ClickStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
}
