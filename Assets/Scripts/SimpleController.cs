using UnityEngine;

public class SimpleController : MonoBehaviour {

    private VirtualJoystick vJoystick;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;

    private bool jump = false;
    private bool facingRight = false;
    private Rigidbody2D rBody2D;

    private void Awake()
    {
        rBody2D = GetComponent<Rigidbody2D>();
        vJoystick = GameObject.Find("BackgroundImage").GetComponent<VirtualJoystick>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        float horizontal = vJoystick.HorizontalMovement();
        Vector2 currentVelocity = rBody2D.velocity;

        if (horizontal * currentVelocity.x < maxSpeed)
        {
            rBody2D.AddForce(Vector2.right * horizontal * moveForce);
        }

        if (Mathf.Abs(currentVelocity.x) > maxSpeed)
            rBody2D.velocity = new Vector2(Mathf.Sign(currentVelocity.x) * maxSpeed, currentVelocity.y);

        if (horizontal > 0 && !facingRight)
            FlipPlayer();
        else if (horizontal < 0 && facingRight)
            FlipPlayer();

        if (jump)
        {
            rBody2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 direction = transform.localScale;
        direction.x *= -1;
        transform.localScale = direction;
    }
}
