using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("Screen")]
    public GameObject hud;
    public GameObject pauseScreen;
    public GameObject helpPanel;
    public GameObject blackScreen;

    [Header("Element")]
    public GameObject miniMap;

    [Header("Task List")]
    public TextMeshProUGUI taskText;
    public TextMeshProUGUI[] taskHint;

    [Header("Weapon")]
    public Image weaponImage;
    public GameObject bulletUI;
    public TextMeshProUGUI ammoText;
    public GameObject[] ammoNum;

    [Header("Location")]
    public Image locationImg;
    public TextMeshProUGUI locationName;

    [Header("Spatial/Diegetic")]
    public TextMeshPro spatialHealth;
    public TextMeshPro bulletAmmo;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        taskHint[0].gameObject.SetActive(false);
        taskHint[1].gameObject.SetActive(false);
        taskHint[2].gameObject.SetActive(false);
        taskHint[3].gameObject.SetActive(false);
    }
}
