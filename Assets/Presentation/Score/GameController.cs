using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameWritter gameWritter;

    private void Start()
    {
        gameWritter = GameObject.Find("Main Camera").gameObject.GetComponent<GameWritter>();
    }

    public void ShowEndgame()
    {
        gameWritter.PlayExplosion();
        gameWritter.ShowWinner();
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void About()
    {
        gameWritter.ShowAbout();
    }
}
