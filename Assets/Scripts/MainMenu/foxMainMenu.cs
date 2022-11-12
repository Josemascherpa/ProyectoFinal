using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foxMainMenu : MonoBehaviour
{
    [SerializeField] private Rigidbody rbFox;
    private Animator anim;
    private bool walk = true;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        rbFox = this.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Walk();        
    }
    private void Walk()
    {
        if (walk)
        {
            rbFox.velocity += new Vector3(0, 0, -1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Stop"))
        {
            anim.SetBool("Sentarse", true);
            walk = false;
        }
        
    }
}
