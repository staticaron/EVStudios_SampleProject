using UnityEngine;
using UnityEngine.SceneManagement;

public class FallTrigger : MonoBehaviour
{
    [SerializeField] float reloadTime = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            Invoke("ReloadLevel", reloadTime);
        }
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
