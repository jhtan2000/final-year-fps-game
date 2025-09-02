using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    private PlayerInteract interact;
    public float waitAfterDying = 2f;
    Vector3 respawnPosition;

    public GameObject tablet;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        interact = player.GetComponent<PlayerInteract>();
        Time.timeScale = 1f;
        respawnPosition = PlayerController.instance.transform.position;

        DisableCursor();
    }

    void Update()
    {
        #region Reassign from UI
        // screen
        GameObject pause = UIController.instance.pauseScreen;


        #endregion

        #region Pause Menu Input (Escape)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClosePanel(pause);
        }

        #endregion

        #region Open Tablet (M)
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (tablet.activeInHierarchy)
            {
                tablet.SetActive(false);
            }
            else
            {
                tablet.SetActive(true);
            }
        }
        #endregion

        #region Scan Enemy (`)
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            FindAnyObjectByType<EnemyHealthManager>().OnXRay();
        }
        #endregion


        ////test
        //#region TimeCount
        //// Get the current time
        //float currentTime = Time.timeSinceLevelLoad;

        //// Calculate minutes and seconds
        //float mins = Mathf.FloorToInt(currentTime / 60);
        //float secs = Mathf.FloorToInt(currentTime % 60);

        //timeText.text = string.Format("{0:00}:{1:00}", mins, secs);
        //#endregion
    }

    public void Respawn()
    {
        StartCoroutine(RespawnSeconds());
    }

    IEnumerator RespawnSeconds()
    {
        UIController.instance.blackScreen.SetActive(true);
        PlayerController.instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitAfterDying);
        PlayerController.instance.transform.position = respawnPosition;
        PlayerHealthController.instance.ResetHealth();
        PlayerController.instance.gameObject.SetActive(true);
        UIController.instance.blackScreen.SetActive(false);
        PlayerController.instance.IsEnding = false;
    }

    public void setCheckPoint(Vector3 newCheckPoint)
    {
        respawnPosition = newCheckPoint;
    }

    public void OpenClosePanel(GameObject screen)
    {
        if (screen.activeInHierarchy)
        {
            interact.enabled = true;
            screen.SetActive(false);
            UIController.instance.hud.SetActive(true);

            Time.timeScale = 1f;
            DisableCursor();
        }
        else
        {
            interact.enabled = false;
            screen.SetActive(true);
            UIController.instance.hud.SetActive(false);

            Time.timeScale = 0f;
            EnableCursor();
        }

    }

    #region Cursor
    public void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion
}
