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

    int life = 5;
    public float timeCount = 0;

    [SerializeField] GameObject player;
    [SerializeField] GameObject UIcanvas;
    [SerializeField] GameObject notification;
    [SerializeField] GameObject closeBtn;
    [SerializeField] GameObject gameOverImg;

    Stage1gate1 st1g1;
    Stage2gate1 st2g1;
    Stage2gate2 st2g2;
    Stage3gate1 st3g1;
    Stage3gate2 st3g2;
    Transform beginPos;

    [SerializeField] Image[] lives = new Image[5];

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
        // 60���������� ����
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        LoadVolume();

        // OnClick�� ������ ����߸� �۵��ϴµ� ���� ��ư�� ���� ��� ������ �ʿ��ϹǷ� �̺�Ʈ Ʈ���� ������ �ٿ� ���
        EventTrigger eventTrigger = GameObject.Find("JumpBtn").GetComponent<EventTrigger>();
        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);
        timeCount = 0;

        // ����Ƽ �����Ϳ����� ������ �� ��ư ��Ȱ��ȭ(���̽�ƽ ��Ȱ��ȭ�� �÷��̾� ��ũ��Ʈ���� �ٷ�)
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
        // beginpos ������ ��ġ��Ű��
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            // Dontdestroy �ߴ��͵� �ı�
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
            if (frontDoor)
            {
                GameObject.Find("Scene2Manager").SetActive(false);
                player.transform.position = GameObject.Find("Stage2gate1").transform.position;
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
            if (!smokestack)
            {
                player.transform.position = GameObject.Find("Stage3gate1").transform.position;
            }
            else
            {
                player.transform.position = beginPos.position;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Ending")
        {  
            Destroy(player);
            Destroy(UIcanvas);            
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ��ũ��Ʈ 5������ ���� �Ŵ������� �� �����ϴ°� ���
    public void CheckDoor()
    {

        // ����Ͽ��� �� ��ư ������ ������ �����ϴ� ������ ������
        if (SceneManager.GetActiveScene().name == "Stage1")
            st1g1.DoorOpen();
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {
/*            st2g1.DoorOpen();*/
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

        if (life < 1)
        {
            Dead();
        }

        // ����¸�ŭ�� ��Ʈ�� �����ϰ� ȸ������ ����
        for (int i = 5; i > life; i--)
        {
            player.transform.position = beginPos.position;
            lives[i - 1].color = new Color32(0, 0, 0, 150);
        }
    }

    // ������ ������Ű�� �޴��� ��õ� ��ư�� �ִ� �˸�â�� ���
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

    // �Ͻ����� Ǯ���ֱ�
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
