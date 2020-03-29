using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public GameObject EndText;
    public Gun Death;
    public Deer DeerClaim;

    private void Start()
    {
        PauseMenu.GameIsPaused = false;
    }

    void Update()
    {
        if (Death.deadDeer && DeerClaim.Claimed)
        {
            EndText.SetActive(true);
            StartCoroutine(NextScreen());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator NextScreen()
    {
        yield return new WaitForSeconds(5);

        EndText.SetActive(false);
    }
}