using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;
    [HideInInspector] public Vector2 velocity;
    void FixedUpdate()
    {
        velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(velocity.x, 0, velocity.y);
    }
}
