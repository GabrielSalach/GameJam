using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScreams : MonoBehaviour
{
    [SerializeField] List<AudioClip> screams;
    AudioSource source;
    float cooldown;

    bool isPaused;

    void Start() {
        source = GetComponent<AudioSource>();
        PlayScream();
        isPaused = false;
    }

    void Update() {
        if(isPaused == false) {
            cooldown -= Time.deltaTime;
        }
        if(cooldown < 0) {
            PlayScream();
        }
    }

    void PlayScream() {
        int id = Random.Range(0, 3);
        source.clip = screams[id];
        source.Play();
        cooldown = Random.Range(5f, 10f);
        //cooldown = 1f;
    }


    public void togglePause() {
        isPaused = !isPaused;
    }

}
