using UnityEngine;

public class Bandit : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    private Animator m_animator;
    private Rigidbody m_body;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;

    private Vector3 initialScale;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body = GetComponent<Rigidbody>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

        // Save the initial scale to prevent unintended scaling
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Prevent scale changes
        transform.localScale = initialScale;

        // Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // Handle input and movement
        float inputX = Input.GetAxis("Horizontal");

        // Adjust facing direction without affecting scale
        if (inputX > 0)
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        else if (inputX < 0)
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);

        // Move the character
        m_body.velocity = new Vector3(inputX * m_speed, m_body.velocity.y, 0);

        // Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body.velocity.y);

        // Handle animations
        if (Input.GetKeyDown("e"))
        {
            if (!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }
        else if (Input.GetKeyDown("q"))
        {
            m_animator.SetTrigger("Hurt");
        }
        else if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("Attack");
        }
        else if (Input.GetKeyDown("f"))
        {
            m_combatIdle = !m_combatIdle;
        }
        else if (Input.GetKeyDown("space") && m_grounded)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body.velocity = new Vector3(m_body.velocity.x, m_jumpForce, 0);
            m_groundSensor.Disable(0.2f);
        }
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            m_animator.SetInteger("AnimState", 2);
        }
        else if (m_combatIdle)
        {
            m_animator.SetInteger("AnimState", 1);
        }
        else
        {
            m_animator.SetInteger("AnimState", 0);
        }
    }
}
