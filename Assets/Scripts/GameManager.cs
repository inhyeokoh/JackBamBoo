using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool restart;

    public int life = 5;
    private float timeCount = 0;
    public int timeScore = 0;

    GameObject player;
    Transform beginPos;

    public GameObject UIcanvas;
    public GameObject notification;
    public GameObject closeBtn;
    public GameObject gameOverImg;

    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public GameObject life4;
    public GameObject life5;
    Stage1gate1 st1g1;
    Stage2gate1 st2g1;
    Stage2gate2 st2g2;
    Stage3gate1 st3g1;
    Stage3gate2 st3g2;


    private Image life1Img;
    private Image life2Img;
    private Image life3Img;
    private Image life4Img;
    private Image life5Img;

    public bool frontDoor;
    public bool smokestack;

    [SerializeField] Slider volumeSlider;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        // 60프레임으로 고정
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        LoadVolume();

        life1Img = life1.GetComponent<Image>();
        life2Img = life2.GetComponent<Image>();
        life3Img = life3.GetComponent<Image>();
        life4Img = life4.GetComponent<Image>();
        life5Img = life5.GetComponent<Image>();
        // OnClick은 눌렀다 떼어야만 작동하는데 점프 버튼은 누른 즉시 실행이 필요하므로 이벤트 트리거 포인터 다운 사용
        EventTrigger eventTrigger = GameObject.Find("JumpBtn").GetComponent<EventTrigger>();
        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);
        timeCount = 0;

        // 유니티 에디터에서는 점프와 문 버튼 비활성화(조이스틱 비활성화는 플레이어 스크립트에서 다룸)
#if UNITY_EDITOR || UNITY_STANDALONE
        GameObject.Find("JumpBtn").SetActive(false);
        GameObject.Find("DoorBtn").SetActive(false);
#endif
    }

    private void Update()
    {
        TimeCount();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 씬이 로드될 때 마다 실행하는 메서드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.Find("Player");
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            // Dontdestroy 했던것들 파괴
            Destroy(gameObject);
            Destroy(UIcanvas);
            Destroy(player);
        }
        else if (SceneManager.GetActiveScene().name == "Stage1")
        {
            st1g1 = GameObject.Find("Stage1gate1").GetComponent<Stage1gate1>();
            beginPos = GameObject.Find("BeginPos").transform;
            player.transform.position = beginPos.position;
        }
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {            
            st2g1 = GameObject.Find("Stage2gate1").GetComponent<Stage2gate1>();
            st2g2 = GameObject.Find("Stage2gate2").GetComponent<Stage2gate2>();
            beginPos = GameObject.Find("BeginPos").transform;
            if (frontDoor == true)
            {
                GameObject.Find("Scene2Manager").SetActive(false);
                player.transform.position = new Vector3(3.5f, 9.2f, 0f);
            }
            else
            {
                player.transform.position = beginPos.position;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            st3g1 = GameObject.Find("Stage3gate1").GetComponent<Stage3gate1>();
            st3g2 = GameObject.Find("Stage3gate2").GetComponent<Stage3gate2>();
            beginPos = GameObject.Find("BeginPos").transform;
            if (smokestack == false)
            {
                float playerX = PlayerPrefs.GetFloat("PlayerX");
                float playerY = PlayerPrefs.GetFloat("PlayerY");
                player.transform.position = new Vector2(playerX, playerY);
            }
            else
            {
                player.transform.position = beginPos.position;
            }
        }
        // 엔딩씬 열릴 때 카운트 된 시간을 timeScore 변수에 저장. EndingManager 스크립트에서 이 변수를 사용해서 점수를 보여주고 서버에 등록시킴.
        else if (SceneManager.GetActiveScene().name == "Ending")
        {
            timeScore = (int)timeCount;          
            Destroy(player);
            Destroy(UIcanvas);            
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void CheckDoor()
    {
        // 모바일에서 문 버튼 누르면 씬마다 존재하는 포털을 열어줌
        if (SceneManager.GetActiveScene().name == "Stage1")
            st1g1.DoorOpen();
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {
            st2g1.DoorOpen();
            st2g2.DoorOpen();
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            st3g1.DoorOpen();
            st3g2.DoorOpen();
        }
    }

    public void Revive()
    {
        life--;
        // 색 변경 대신 setactive(false)는 어떨까
        if (life == 4)
        {
            player.transform.position = beginPos.position;
            life5Img.color = new Color32(0, 0, 0, 150);
        }
        else if (life == 3)
        {
            player.transform.position = beginPos.position;
            life4Img.color = new Color32(0, 0, 0, 150);
        }
        else if (life == 2)
        {  
            player.transform.position = beginPos.position;
            life3Img.color = new Color32(0, 0, 0, 150);
        }
        else if (life == 1)
        {
            player.transform.position = beginPos.position;
            life2Img.color = new Color32(0, 0, 0, 150);
        }
        else
        {
            life1Img.color = new Color32(0, 0, 0, 150);
            Dead();
        }
    }

    // 죽으면 정지시키고 메뉴와 재시도 버튼이 있는 알림창을 띄움
    private void Dead()
    {
        Time.timeScale = 0.0f;
        notification.gameObject.SetActive(true);
        closeBtn.SetActive(false);
        gameOverImg.SetActive(true);
    }

    public void GotoMenu()
    {       
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        restart = true;
        SceneManager.LoadScene("Menu");
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        notification.SetActive(true);
    }

    // 일시정지 풀어주기
    public void GobackToGame()
    {
        Time.timeScale = 1;
        notification.SetActive(false);
    }

    void OnPointerDown(PointerEventData data)
    {
        player.GetComponent<PlayerMove>().PlayerJump();
    }

    public void TimeCount()
    {
        timeCount += Time.deltaTime;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolume();
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.6f);
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

}
