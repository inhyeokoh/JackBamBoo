using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int life = 5;
    private float timeCount = 0;
    public int timeScore = 0;

    GameObject player;
    Transform beginPos;

    public GameObject UIcanvas;
    public GameObject notification;
    public GameObject pauseUI;
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


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        // ������ 60���� ����
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        life1Img = life1.GetComponent<Image>();
        life2Img = life2.GetComponent<Image>();
        life3Img = life3.GetComponent<Image>();
        life4Img = life4.GetComponent<Image>();
        life5Img = life5.GetComponent<Image>();
        // OnClick�� ������ ����߸� �۵��ϴµ� ���� ��ư�� ���� ��� ������ �ʿ��ϹǷ� �̺�Ʈ Ʈ���� ������ �ٿ� ���
        EventTrigger eventTrigger = GameObject.Find("JumpBtn").GetComponent<EventTrigger>();
        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);
        timeCount = 0;

        // ����Ƽ �����Ϳ����� ������ �� ��ư ��Ȱ��ȭ(���̽�ƽ ��Ȱ��ȭ�� �÷��̾ ��ũ��Ʈ���� �ٷ�)
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

    // ���� �ε�� �� ���� �����ϴ� �޼���
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.Find("Player");
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Destroy(gameObject);
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
        // ������ ���� �� ī��Ʈ �� �ð��� timeScore ������ ����. EndingManager ��ũ��Ʈ���� �� ������ ����ؼ� ������ �����ְ� ������ ��Ͻ�Ŵ.
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
        // ������ �����ϴ� ������ ������
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
        // �� ���� ��� setactive(false)�� ���
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

    // ������ ������Ű�� �޴��� ��õ� ��ư�� �ִ� �˸�â�� ���
    private void Dead()
    {
        Time.timeScale = 0.0f;
        notification.gameObject.SetActive(true);
    }

    // �޴��� �̵��ϴ� ��ư
    public void GotoMenu()
    {       
        Time.timeScale = 1f;
        notification.gameObject.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    // �׾��� �� "��õ� ��ư"�� �Ͻ����� ��ư ���� "�� ���� ��ư"�� ������ �����°� ü�� �̹��� ������ ��, ��1�� �̵�
    public void Retry()
    {
        Time.timeScale = 1f;
        notification.gameObject.SetActive(false);
        life = 5;
        // for������ �����ұ�?
        life1Img.color = new Color32(255, 255, 255, 220);
        life2Img.color = new Color32(255, 255, 255, 220);
        life3Img.color = new Color32(255, 255, 255, 220);
        life4Img.color = new Color32(255, 255, 255, 220);
        life5Img.color = new Color32(255, 255, 255, 220);
        // ���� ��꿡 �� timeCount �ʱ�ȭ
        timeCount = 0;
        SceneManager.LoadScene("Stage1");
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }

    // �Ͻ����� Ǯ���ֱ�
    public void GobackToGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    void OnPointerDown(PointerEventData data)
    {
        player.GetComponent<PlayerMove>().PlayerJump();
    }

    public void TimeCount()
    {
        timeCount += Time.deltaTime;
    }

}