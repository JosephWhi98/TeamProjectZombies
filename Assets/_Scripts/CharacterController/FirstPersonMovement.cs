using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    [HideInInspector] public Vector2 velocity;

    public NetworkedAnimatorView networkedAnimator;
    public Animator firstPersonAnimator;

    void FixedUpdate()
    {
        velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(velocity.x, 0, velocity.y);

        if (velocity.magnitude > 0)
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
