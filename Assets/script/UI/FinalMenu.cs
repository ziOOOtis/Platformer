using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalMenu : MonoBehaviour
{
    private Spice spice;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void RePlayGame()
    {
        GameManager.SetSpiceScore(0);//reset the spice index
        spice.spiceIndex = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit(); // this quits the game.
    }
}
