using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bug : MonoBehaviour
{
    private Transform target;
    private float t;
    private Animator anim;
    void Start()
    {
        //make every time a different speed for funzies
        t = Random.Range(0.0001f, 0.001f);
        //initating target as the main character
        GameObject player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Transform>();
        //getting animator
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //getting position of character and bug
        Vector3 a = transform.position;
        Vector3 b = target.position;
        //going towards character
        transform.position = Vector3.Lerp(a, b, t);
        transform.LookAt(target);
    }

    void OnCollisionEnter(Collision collision)
    {
        //if box collider collides with character, attack
        if(collision.gameObject.tag == "Player")
        {
            anim.Play("biteAnimation");
            //player should loose something but idk if it goes here
        }
        //if box collides with bullet twice, die, disappear
    }
}
