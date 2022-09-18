using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class MonsterAI : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] float aggroRange;
    [SerializeField] float floatSpeed;
    [SerializeField] float floatHeight;

    public UnityEvent onPlayerAquired;
    public UnityEvent onPlayerLost;

    NavMeshAgent agent;
    Animator stateMachine;
    float roamTimer = 0;
    Transform model;
    GameObject hurtBox;
    bool isChasing = false;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<Animator>();
        model = transform.Find("Model").transform;
        if(onPlayerAquired == null) {
            onPlayerAquired = new UnityEvent();
        }
        if(onPlayerLost == null) {
            onPlayerLost = new UnityEvent();
        }
        hurtBox = transform.Find("HurtBox").gameObject;
    }

    void CheckDistance() {
        float range = Vector3.Distance(transform.position, player.position);

        if (!isChasing && range <= aggroRange) {
            stateMachine.SetBool("HasTarget", true);
            onPlayerAquired.Invoke();
            isChasing = true;
        } else if(isChasing && range > aggroRange){
            stateMachine.SetBool("HasTarget", false);
            onPlayerLost.Invoke();
            isChasing = false;
        }
    }

    void Update() {
        // Floating animation
        model.localPosition = new Vector3(0, 2 + Mathf.Sin(Time.time * floatSpeed) * floatHeight, 0);
        CheckDistance();
        StartCoroutine(ghostMovement());
    }

    IEnumerator ghostMovement() {
        // Chasing behavior
        if(stateMachine.GetBool("HasTarget") == true) {
            agent.SetDestination(player.position);
        }
        yield return null;

        // Roaming behavior
        if(stateMachine.GetBool("HasTarget") == false) {
            roamTimer -= Time.deltaTime;
            if(roamTimer <= 0) {
                Vector3 newDestination = new Vector3(Random.Range(0f, aggroRange), 0f, Random.Range(0f, aggroRange));
                agent.SetDestination(newDestination);
                roamTimer = Random.Range(3f, 5f);
            }
        }
        yield return null;
    }

    // AggroRange debug
    void OnDrawGizmosSelected() {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
