using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public GameObject finishUI;
    public Text message;

    public static Finish Instance;
    private EnemySpawner enemySpawner;
    void Awake()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        Instance = this;
    }
    void Start()
    {

    }

    void Update()
    {

    }

    public void Win()
    {
        finishUI.SetActive(true);
        message.text = "Y O U  W I N !";

    }

    public void Fail()
    {
        enemySpawner.Stop();
        finishUI.SetActive(true);
        message.text = "G A M E  O V E R !";
    }

    public void OnButtonRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
}
