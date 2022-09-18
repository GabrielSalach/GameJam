using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] Slider FearGauge;
    [SerializeField] GameObject ScreenPanel;
    [SerializeField] TextMeshProUGUI Message;
    [SerializeField] BoxCollider TargetLocationCollider;
    bool isActive;
    bool isSaved;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        isSaved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive) return;

        if(FearGauge.value >= 1)
        {
            ScreenPanel.SetActive(true);
            Message.text = "GAME OVER";
            StartCoroutine(BackToMenu());
        }

        else if(isSaved)
        {
            ScreenPanel.SetActive(true);
            Message.text = "VICTORY";
            StartCoroutine(BackToMenu());
        }
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(5);

        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("MainMenu", 0); 
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
        {
            isSaved = true;
        }
    }
}
