using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private CinemachineVirtualCamera followCam;
    public bool isPlaying;
    [SerializeField] private Image[] lives;
    [SerializeField] private int currentLives;

    public void StartGame()
    {
        AnimalsGameLoop.instance.GameStart();
        isPlaying = true;
        ManagerUI.instance.GuidePanel(true);
        currentLives = lives.Length;
    }

    private void OnEnable()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
    }
    
    
    public void SetFollowCam(Transform t)
    {
        followCam.LookAt = AnimalsGameLoop.instance.transform;
        followCam.Follow = t;
    }

    public void RestartScene()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
        
        if (AnimalsGameLoop.instance.footprints.Count > 8)
        {
            CallGameOver();
        }
        
        PlayerLogic();
    }

    private void PlayerLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /* Phone input
            var touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width/2)
            {
                Debug.Log ("Left click");
            }
            else if (touch.position.x > Screen.width/2)
            {
                Debug.Log ("Right click");
            }*/

            var mPos = Input.mousePosition;
            if (mPos.x < Screen.width/2)
            {
                ScreenTap(true);
            }
            else if (mPos.x > Screen.width/2)
            {
                ScreenTap(false);
            }

        }
    }

    private void ScreenTap(bool isLeft)
    {
        if (AnimalsGameLoop.instance.footprints.Count > 0)
        {
            var f = AnimalsGameLoop.instance.footprints[0];
            Debug.Log("Tap: " + isLeft);
            if (isLeft == f.GetComponent<Footprint>().isLeft)
            {
                Destroy(f);
                AnimalsGameLoop.instance.footprints.RemoveAt(0);
                ManagerUI.instance.GuidePanel(false);
            }
            else
            {
                lives[currentLives - 1].color = Color.black;
                currentLives--;
                if (currentLives <= 0)
                {
                    CallGameOver();
                }
            }
        }
    }

    private void CallGameOver()
    {
        ManagerUI.instance.GameOver();
        isPlaying = false;
        Time.timeScale = 0;
    }
}