using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour
{
    private Transform target;
    private Animator anim;
    private bool isTriggered;
    // Start is called before the first frame update
    void Start()
    {
        //initating target as the main character
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Transform>();
        anim = GetComponent<Animator>();
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 a = transform.position;
        Vector3 b = target.position;
        if(isTriggered == false)
        {
            if (Vector3.Distance(a, b) < 5)
            {
                //when character gets near monster is triggers it to lerp towards it
                isTriggered = true;
                anim.enabled = true;
            }
        }
        if (isTriggered == true)
        {
            //once you trigger the monster it's always going to come for you
            transform.position = Vector3.Lerp(a, b, 0.001f);
            transform.LookAt(target);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //if box collider collides with character, attack
        if (collision.gameObject.tag == "Player")
        {
            anim.Play("biteAnimation");
            //player should loose something but idk if it goes here
        }
        //if box collides with bullet twice, die, disappear
    }
}
