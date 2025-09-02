using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pieces : MonoBehaviour
{
    public static Pieces instance;
    public int currentPiece;
    public string currentTask;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentPiece = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPiece == 4)
        {
            UIController.instance.taskText.text = " - <color=green>Collect Paper Pieces (" + currentPiece + "/4)</color=green>\n" +
                                                    " - " + currentTask;
        }
    }

    public void AddHint()
    {
        currentPiece++;
        UIController.instance.taskText.text = " - Collect <color=lightblue>Paper Pieces</color=lightblue> (" + currentPiece + "/4)";
        SetActiveAlphabet(currentPiece);
    }

    public void SetActiveAlphabet(int index)
    {
        if (currentPiece <= 4)
        {
            UIController.instance.taskHint[index - 1].gameObject.SetActive(true);
        }
    }
}
