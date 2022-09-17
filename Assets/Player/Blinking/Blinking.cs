using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class Blinking : MonoBehaviour
{

    public bool eyesClosed;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] float animationDuration;

    [SerializeField] Image topEyelid;
    [SerializeField] Image bottomEyelid;

    bool eyelidLerpTrigger;
    bool isLerping;
    float startTimestamp;

    public UnityEvent onEyesClosed;
    public UnityEvent onEyesOpen;

    void Start() {
        if(onEyesClosed == null) {
            onEyesClosed = new UnityEvent();
        }
        if(onEyesOpen == null) {
            onEyesOpen = new UnityEvent();
        }
    }


    void Update() {
        if(eyelidLerpTrigger == true && isLerping == false) {
            startLerping();
            eyelidLerpTrigger = false;
        }
        if(isLerping == true) {
            EyelidLerp();
        }
    }

    // Triggers the lerp animation
    void startLerping() {
        isLerping = true;
        startTimestamp = Time.time;
    }

    // Lerping calculations
    void EyelidLerp() {
        // Calculates the percentage of the remaining distance based on time and the animation curve 
        float timeSinceStarted = Time.time - startTimestamp;
        float percentageComplete = timeSinceStarted / animationDuration;
        float percentageToMove = animationCurve.Evaluate(percentageComplete);

        // Moves the eyelid
        if(eyesClosed == true) {
            topEyelid.fillAmount = Mathf.Lerp(0f, 1f, percentageComplete);
            bottomEyelid.fillAmount = Mathf.Lerp(0f, 1f, percentageComplete);
        } else {
            topEyelid.fillAmount = Mathf.Lerp(1f, 0f, percentageComplete);
            bottomEyelid.fillAmount = Mathf.Lerp(1f, 0f, percentageComplete);
        }
        if(percentageComplete >= 1.0f) {
            isLerping = false;
        }
    }

    public void InputHandler(InputAction.CallbackContext context) {
        if (context.started) {
            CloseEyes();
        }
        else if (context.canceled) {
            OpenEyes();
        }
    }

    void CloseEyes() {
        eyesClosed = true;
        eyelidLerpTrigger = true;
        onEyesClosed.Invoke();
    }

    void OpenEyes() {
        eyesClosed = false;
        eyelidLerpTrigger = true;
        onEyesOpen.Invoke();
    }
}
