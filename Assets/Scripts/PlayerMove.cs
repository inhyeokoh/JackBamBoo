using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private VariableJoystick joy;
    public static PlayerMove Instance;

    private Animator ani;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameObject gmObject;
    private AudioSource jumpSound;
    FixedJoint2D fixJoint;
    GameManager gm;

    public Transform groundCheck;
    public LayerMask GroundLayer;

    public float speed = 3f;
    private float jumpPower = 5f;
    public int life = 3;

    private bool isGround = true;
    private bool isLadder;
    private bool isRope = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    // �ȵ���̵忡���� ���̽�ƽ�� ã�Ƽ� ���, �����Ϳ����� ���̽�ƽ ��Ȱ��ȭ
#if UNITY_ANDROID
        joy = GameObject.Find("VariableJoystick").GetComponent<VariableJoystick>();
        GameObject.Find("VariableJoystick").SetActive(true);
#elif UNITY_EDITOR || UNITY_STANDALONE
        GameObject.Find("VariableJoystick").SetActive(false);
#endif

        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        jumpSound = GetComponent<AudioSource>();
        fixJoint = GetComponent<FixedJoint2D>();
        gmObject = GameObject.Find("GameManager");
        gm = gmObject.GetComponent<GameManager>();
    }

    private void Update()
    {
        Move();
    }


    private void Move()
    {
    // xMove ������ �ȵ���̵忡���� ���̽�ƽ �Է°�, �����Ϳ����� ����Ű���� ��  
#if UNITY_ANDROID
        float xMove = joy.Horizontal;
#elif UNITY_EDITOR || UNITY_STANDALONE
        float xMove = Input.GetAxis("Horizontal");
#endif
        // flipX�� ���� �¿� ��������Ʈ ����
        rb.velocity = new Vector2(xMove * speed, rb.velocity.y);
        if (xMove < 0)
            sr.flipX = true;
        else
            sr.flipX = false;
        ani.SetFloat("speed", Mathf.Abs(xMove));

    
        // ���� �ٽ� �ʿ�
        if (isLadder)
        {
#if UNITY_ANDROID
        float yMove = joy.Vertical;
#elif UNITY_EDITOR || UNITY_STANDALONE
        float yMove = Input.GetAxis("Vertical");
#endif
            rb.velocity = new Vector2(rb.velocity.x, yMove * speed);
            rb.gravityScale = 7f;
        }        
        else if (isRope && Input.GetKeyDown(KeyCode.Space))
        {
            jumpSound.Play();
            rb.gravityScale = 1;
            Invoke("DelayRopeJump", 0.3f);
            fixJoint.connectedBody = null;
            fixJoint.enabled = false;
            rb.velocity = Vector2.up * jumpPower;
        }
        else
        {
            rb.gravityScale = 1;
            if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
            {
                jumpSound.Play();
                rb.velocity = Vector2.up * jumpPower;
            }
        }
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, GroundLayer);
        ani.SetBool("ground", isGround);     
    }

    // ����Ͽ��� ���� ��ư�� ���� �� ����
    public void PlayerJump()
    {
        jumpSound.Play();
        if (isGround)
            rb.velocity = Vector2.up * jumpPower;
        else if (isRope)
        {
            rb.gravityScale = 1;
            // ������ ���� ���� ��Ű�� ���� ���� �ٸ� fixedJoint ��ü�� �����ع����� ��ٸ� Ż���� �ȵ�. ���ó�� fixedJoint�� ��Ȱ��ȭ ��Ŵ
            Invoke("DelayRopeJump", 0.3f);
            fixJoint.connectedBody = null;
            fixJoint.enabled = false;
            rb.velocity = Vector2.up * jumpPower;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // JumpZone �±��̰� ���� ��ü���� ù��° �浹 ���� �븻 ���Ͱ��� y��ǥ�� 2���� ���� ��? �̰� �ٽ� Ȯ�� �ʿ�
        // �������� �� 2���� 10���� �����ϰ� �ڵ� ����
        if (collision.gameObject.CompareTag("JumpZone") && collision.contacts[0].normal.y < 2f)
        {
            jumpSound.Play();
            jumpPower = 10f;
            rb.velocity = Vector2.up * jumpPower;
        }
        else if (collision.gameObject.CompareTag("Dead"))
        {
            gm.Revive();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpZone"))
        {
            jumpPower = 5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            ani.SetBool("isLadder", true);
        }
        // ������ �� ź ���¿��� ������ �����ϸ� �ÿ��̾��� fixedJoint Ȱ��ȭ
        else if (collision.CompareTag("Rope") && !isRope)
        {
            Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
            fixJoint.enabled = true;
            fixJoint.connectedBody = rig;
            isRope = true;
        }
        else if (collision.gameObject.CompareTag("Dead"))
        {
            gm.Revive();
        }

        // ������ ������ �ڽ� ������Ʈ�� ������ٰ� Ǯ���� �� DontDestroyOnLoad�� Ǯ���µ�, ����Ʈ�� �����ϸ� �缳�����ִ� �ڵ�
        if (collision.gameObject.CompareTag("Gate"))
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            ani.SetBool("isLadder", false);
        }
    }

    private void DelayRopeJump()
    {
        isRope = false;
    }

}