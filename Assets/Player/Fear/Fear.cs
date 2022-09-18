using UnityEngine;
using UnityEngine.UI;

public class Fear : MonoBehaviour
{
    [SerializeField] Slider FearGauge;
    [SerializeField] GameObject Ghost;
    bool isBlinking;
    int nbChasingGhost;

    public void StartBlink()
    {
        isBlinking = true;
    }

    public void StopBlink()
    {
        isBlinking = false;
    }

    public void StartChasing()
    {
        nbChasingGhost++;
    }

    public void QuitChasing()
    {
        nbChasingGhost--;
    }

    // Start is called before the first frame update
    void Start()
    {
        FearGauge.value = 0;
        nbChasingGhost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlinking)
        {
            FearGauge.value += Time.deltaTime * 0.1f;
        }

        if (nbChasingGhost > 0)
        {
            FearGauge.value += Time.deltaTime * 0.01f * nbChasingGhost;
        }
    }
}
