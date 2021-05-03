﻿using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    [HideInInspector] public Vector3 velocity;
    private CharacterController cc;
    public NetworkedAnimatorView networkedAnimator;
    public Animator firstPersonAnimator;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        velocity.z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        velocity.y = 0;
        if (cc.isGrounded == false)
        {
            //Add our gravity Vecotr
            velocity += Physics.gravity * Time.deltaTime;
        }

        cc.Move(transform.TransformDirection(velocity));

        if (velocity.z != 0 || velocity.x != 0)
        {
            networkedAnimator.AnimatorBool("Running", true);
            firstPersonAnimator.SetBool("Running", true);
        }
        else
        {
            networkedAnimator.AnimatorBool("Running", false);
            firstPersonAnimator.SetBool("Running", false);
        }
    }
}
