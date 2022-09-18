using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] RectTransform targetOpen;
    [SerializeField] RectTransform targetClosed;
    [SerializeField] InputSystemFirstPersonCharacter player;

    public void OpenMenu() {
        LeanTween.move(this.gameObject, targetOpen, 0.3f);
/*         Time.timeScale = 0; */
        player.enabled = false;
        Cursor.visible = true;
    }

    public void CloseMenu() {
        LeanTween.move(this.gameObject, targetClosed, 0.3f);
/*         Time.timeScale = 1; */
        player.enabled = true;
        Cursor.visible = false;
    }

    public void RestartGame() {
        
    }

    public void QuitGame() {

    }
}
