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
    // 안드로이드에서는 조이스틱을 찾아서 사용, 에디터에서는 조이스틱 비활성화
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
    // xMove 변수에 안드로이드에서는 조이스틱 입력값, 에디터에서는 방향키값을 줌  
#if UNITY_ANDROID
        float xMove = joy.Horizontal;
#elif UNITY_EDITOR || UNITY_STANDALONE
        float xMove = Input.GetAxis("Horizontal");
#endif
        // flipX를 통해 좌우 스프라이트 변경
        rb.velocity = new Vector2(xMove * speed, rb.velocity.y);
        if (xMove < 0)
            sr.flipX = true;
        else
            sr.flipX = false;
        ani.SetFloat("speed", Mathf.Abs(xMove));

    
        // 이해 다시 필요
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

    // 모바일에서 점프 버튼을 누를 때 실행
    public void PlayerJump()
    {
        jumpSound.Play();
        if (isGround)
            rb.velocity = Vector2.up * jumpPower;
        else if (isRope)
        {
            rb.gravityScale = 1;
            // 딜레이 없이 점프 시키면 로프 내의 다른 fixedJoint 물체에 접촉해버려서 사다리 탈출이 안됨. 평시처럼 fixedJoint를 비활성화 시킴
            Invoke("DelayRopeJump", 0.3f);
            fixJoint.connectedBody = null;
            fixJoint.enabled = false;
            rb.velocity = Vector2.up * jumpPower;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // JumpZone 태그이고 접촉 물체와의 첫번째 충돌 지점 노말 벡터값의 y좌표가 2보다 작을 때? 이건 다시 확인 필요
        // 점프력을 약 2배인 10으로 설정하고 자동 점프
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
        // 로프에 안 탄 상태에서 로프에 접촉하면 플에이어의 fixedJoint 활성화
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

        // 구름과 새에서 자식 오브젝트로 만들었다가 풀어줄 때 DontDestroyOnLoad가 풀리는데, 게이트에 도달하면 재설정해주는 코드
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