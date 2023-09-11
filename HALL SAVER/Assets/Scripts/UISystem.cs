using System.Collections;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    [Header("MENUs")]
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject defenseBar;
    [SerializeField] private GameObject waveSpawner;

    [Header("DEFENCEs")]
    public GameObject cannon;
    public GameObject catapult;
    public GameObject xbow;
    public GameObject doubleCannon;

    public GameObject defenseToSpawn;
    public int defenseToSpawnBudget;

    [Header("GAME CURRENCYs")]
    public int gameCurrency = 0;
    public int increaseBy = 3;
    public int increaseWithTime = 3;
    public float currentTime = 0f;
    public TMPro.TMP_Text gameCurrencyText;


    [Header("EXTRAs")]

    private bool isMatchStarted = false;

    private Camera cam;

    public GameObject cantSpawnThere;

    [SerializeField] private float minOrtho = 3.5f;
    [SerializeField] private float maxOrtho = 9.5f;
    [SerializeField] private float currentOrtho = 4.75f;

    [SerializeField] private LayerMask whatIsDefense;

    public static UISystem instance;
    UISystem() => instance = this;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        StartCoroutine(IncreaseMoneyTo4());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMatchStarted) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (defenseToSpawn != null)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                Physics.Raycast(ray, out hit);

                bool canSpawn = Physics.CheckSphere(hit.point, 1f, whatIsDefense);

                if (!canSpawn)
                {
                    if (gameCurrency > defenseToSpawnBudget)
                    {
                        AudioManager.instance.Play("UI CLICK");
                        Instantiate(defenseToSpawn, hit.point, Quaternion.identity);
                        gameCurrency -= defenseToSpawnBudget;
                    }
                }
                else
                {
                    cantSpawnThere.SetActive(true);
                    StartCoroutine(DisableCantSpawnThere());
                }
            }
        }

        if (currentTime > increaseWithTime)
        {
            gameCurrency += increaseBy;
            gameCurrencyText.text = "$ " + gameCurrency.ToString();
            currentTime = 0f;
        }

        currentTime += Time.deltaTime;

        currentOrtho -= Input.mouseScrollDelta.y;
        currentOrtho = Mathf.Clamp(currentOrtho, minOrtho, maxOrtho);

        cam.orthographicSize = currentOrtho;
    }

    public void Pause()
    {
        AudioManager.instance.Play("UI CLICK");
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Play()
    {
        AudioManager.instance.Play("UI CLICK");

        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void Exit()
    {
        AudioManager.instance.Play("UI CLICK");

        Application.Quit(0);
    }

    public void MatchStart()
    {
        AudioManager.instance.Play("UI CLICK");

        startMenu.SetActive(false);
        defenseBar.SetActive(true);
        waveSpawner.SetActive(true);
        isMatchStarted = true;
    }

    public void Heal()
    {
        CentralHouse.instance.Heal();
    }

    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MAIN");
    }

    // COROUNTINEs

    IEnumerator DisableCantSpawnThere()
    {
        yield return new WaitForSeconds(2.5f);
        cantSpawnThere.SetActive(false);
    }

    IEnumerator IncreaseMoneyTo4()
    {
        yield return new WaitForSeconds(20f);
        increaseBy = 6;

        StartCoroutine(IncreaseMoneyTo6());
    }

    IEnumerator IncreaseMoneyTo6()
    {
        yield return new WaitForSeconds(20f);
        increaseBy = 8;
    }
}
