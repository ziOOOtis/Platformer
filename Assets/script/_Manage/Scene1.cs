using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Scene1 : MonoBehaviour
{


    public IEnumerator LoadToEnd()
    {
        // Wait for the specified respawn time (5 seconds)
        yield return new WaitForSeconds(5f);

        // Spawn a new water object
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



}
