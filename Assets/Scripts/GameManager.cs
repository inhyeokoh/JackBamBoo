using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject player;
    [SerializeField] GameObject UIcanvas;
    [SerializeField] GameObject notification;
    [SerializeField] GameObject closeBtn;
    [SerializeField] GameObject gameOverImg;
    [SerializeField] Stage1gate st1gate;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Image[] lives = new Image[5];

    Transform beginPos;

    public int life = 5;
    public float timeCount = 0;
    public bool restart;
    public bool visitStage;
    public bool frontDoor;
    public bool smokeStack;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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

    // ���� �ε�� ������ �����ϴ� �޼���
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.Find("BeginPos") != null)
        {
            beginPos = GameObject.Find("BeginPos").transform;
            player.transform.position = beginPos.position;
        }

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            // DontDestroyOnLoad �ߴ��͵� �ı�
            Destroy(gameObject);
            Destroy(UIcanvas);
            Destroy(player);
        }
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {       
            if (frontDoor)
            {
                player.transform.position = GameObject.Find("Stage2gate1").transform.position;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Stage3")
        {
            if (!smokeStack)
            {
                player.transform.position = GameObject.Find("Stage3gate1").transform.position;
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

    public void CheckDoor()
    {
        // ����Ͽ��� �� ��ư ������ ������ �����ϴ� ������ ������
        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            st1gate.DoorOpen();
        }
        else
        {
            GateManager.Instance.DoorsOpen();
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
