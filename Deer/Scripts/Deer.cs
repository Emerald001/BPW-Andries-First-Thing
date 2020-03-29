using UnityEngine;
using System.Collections;

public class Deer : MonoBehaviour {
    public Animator deer;
    private IEnumerator coroutine;
    public ParticleSystem dust;
    public ParticleSystem dustgallop;

    public GameObject Player;
    public GameObject ClaimText;
    public GameObject ThisDeer;

    public Gun Death;
    private bool isWandering = false;
    public bool Claimed = false;
    int rotateLorR;

    [System.Obsolete]
    void Update()
    {
        Durr();
    }

    [System.Obsolete]
    public void Durr()
    {
        // calculate the distance between the deer and the player
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        //claim the deer once dead
        if (Death.deadDeer && distance < 5)
        {
            ClaimText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Claimed = true;

                ClaimText.SetActive(false);
                Destroy(ThisDeer);
            }
        }
            else
            {
                ClaimText.SetActive(false);
            }
        
        //actually kill the deer
        if (Death.deadDeer){
            deer.SetBool("idle", false);
            deer.SetBool("walking", false);
            deer.SetBool("galloping", false);

            dust.GetComponent<ParticleSystem>().enableEmission = false;
            dustgallop.GetComponent<ParticleSystem>().enableEmission = false;

            deer.SetBool("died", true);
        }

        //Scare the deer
        if (!Death.deadDeer && distance < 50f)
        {
            deer.SetBool("idle", false);
            deer.SetBool("walking", false);
            deer.SetBool("turnright", false);
            deer.SetBool("turnleft", false);

            deer.SetBool("galloping", true);

            dust.GetComponent<ParticleSystem>().enableEmission = true;
            dustgallop.GetComponent<ParticleSystem>().enableEmission = true;
        }

        //deer walks around
        if (!Death.deadDeer && distance > 100f)
        {
            rotateLorR = Random.Range(1, 3);
            deer.SetBool("galloping", false);

            if (isWandering == false)
            {
                dust.GetComponent<ParticleSystem>().enableEmission = false;
                dustgallop.GetComponent<ParticleSystem>().enableEmission = false;

                deer.SetBool("idle", false);

                StartCoroutine(Wander());
            }
        }

        //deer idles
        if (!Death.deadDeer && distance < 100f)
        {
            StartCoroutine(idle());
        }

        // Idle Enumerator, making the actual Idle routine
        IEnumerator idle()
        {
            yield return new WaitForSeconds(0.1f);

            deer.SetBool("attack", false);
            deer.SetBool("idle", true);
            deer.SetBool("up", false);

            dust.GetComponent<ParticleSystem>().enableEmission = false;
            dustgallop.GetComponent<ParticleSystem>().enableEmission = false;
        }

        // Walk Enumerator, making the actual Walking routine
        IEnumerator Wander()
        {
            float rotTime = .6f;
            int rotateWait = Random.Range(1, 4);
            int walkWait = Random.Range(1, 4);
            int walkTime = Random.Range(1, 5);

            isWandering = true;

            yield return new WaitForSeconds(walkWait);
            deer.SetBool("walking", true);
            yield return new WaitForSeconds(walkTime);
            deer.SetBool("walking", false);

            yield return new WaitForSeconds(rotateWait);
            if (rotateLorR == 1)
            {
                deer.SetBool("turnright", true);
                yield return new WaitForSeconds(rotTime);
                deer.SetBool("turnright", false);
            }

            else
            {
                deer.SetBool("turnleft", true);
                yield return new WaitForSeconds(rotTime);
                deer.SetBool("turnleft", false);
            }
            deer.SetBool("idle", true);

            isWandering = false;
        }
    }
}
