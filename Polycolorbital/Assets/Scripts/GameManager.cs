using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static int level = 1;

    public bool gameOver;

    public float restartDelay = 2f;

	public void GameOver ()
    {
        if (!gameObject)
        {
            Debug.Log("GAME OVER");
            gameOver = true;

            Invoke("Restart", restartDelay);
        }
    }

    public void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
