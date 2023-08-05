using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance;

    [SerializeField] VariableJoystick joystick;
    [SerializeField] AudioSource jumpSound;

    Animator ani;
    Rigidbody2D rb;
    SpriteRenderer sr;
    FixedJoint2D fixJoint;

    public Transform groundCheck;
    public LayerMask GroundLayer;

    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    bool isGround = true;
    bool isLadder;
    bool isRope;

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
        // 안드로이드에서는 조이스틱을 찾아서 사용, 에디터에서는 조이스틱 비활성화
#if UNITY_ANDROID        
        joystick.gameObject.SetActive(true);     
#endif

        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>(); 
        fixJoint = GetComponent<FixedJoint2D>();
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }
    }


    private void Move()
    {
        // xMove 변수에 안드로이드에서는 조이스틱 입력값, 에디터에서는 방향키값을 줌  
#if UNITY_ANDROID
        float xMove = joystick.Horizontal;
        float yMove = joystick.Vertical;
#elif UNITY_EDITOR || UNITY_STANDALONE
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
#endif

        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, GroundLayer);
        ani.SetBool("ground", isGround);     
 
        rb.velocity = new Vector2(xMove * speed, rb.velocity.y);
        sr.flipX = xMove < 0;
        ani.SetFloat("speed", Mathf.Abs(xMove));
    
        // 사다리에서 중력 조절
        if (isLadder)
        {
            rb.velocity = new Vector2(rb.velocity.x, yMove * speed);
            rb.gravityScale = 7f;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    public void PlayerJump()
    {
        jumpSound.Play();
        if (isGround)
        {
            rb.velocity = Vector2.up * jumpPower;
        }
        else if (isRope)
        {
            // 로프에서 점프를 눌렀을 때 딜레이 없이 isRope를 false로 바꿔주면
            // 로프 내의 다른 fixedJoint 물체에 접촉해버려서 사다리 탈출이 안됨.
            Invoke("DelayRopeState", 0.3f);
            fixJoint.connectedBody = null;
            fixJoint.enabled = false;
            rb.velocity = Vector2.up * jumpPower;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("JumpZone") && collision.contacts[0].normal.y < 2f)
        {
            jumpSound.Play();
            jumpPower *= 2;
            rb.velocity = Vector2.up * jumpPower;
        }
        else if (collision.gameObject.CompareTag("Dead"))
        {
            GameManager.Instance.Revive();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("JumpZone"))
        {
            jumpPower /= 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            ani.SetBool("isLadder", true);
        }
        // 로프에 접촉하면 플레이어의 fixedJoint 활성화.
        else if (collision.CompareTag("Rope") && !isRope)
        {
            Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
            fixJoint.enabled = true;
            fixJoint.connectedBody = rig;
            isRope = true;
        }
        else if (collision.gameObject.CompareTag("Dead"))
        {
            GameManager.Instance.Revive();
        }
        // 올라타는 새와 구름에 탑승 시, 플레이어가 자식 오브젝트로 들어가는 코드가 있는데
        // 이 때 DontDestroyOnLoad가 풀리는 것을 Stage1 종점에 있는 게이트에 도달하면 재설정해줌.
        else if (collision.gameObject.CompareTag("Gate"))
        {
            DontDestroyOnLoad(gameObject);
        }
        else if (collision.gameObject.CompareTag("Life"))
        {
            collision.gameObject.SetActive(false);
            if (GameManager.Instance.life <= 4)
            {
                GameManager.Instance.life++;
            }
        }
    }
    private void DelayRopeState()
    {
        isRope = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            ani.SetBool("isLadder", false);
        }
    }
}