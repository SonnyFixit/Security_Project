using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameHackManager : MonoBehaviour
{
    [Header("Canvas")]
    public Canvas minigameCanvas;
    public TMP_Text mainText;

    [Header("Colors")]
    public Color goodColor;
    public Color wrongColor;
    public Color pickedColor;

    [Header("Timer")]
    public Slider timeSlider;
    public TMP_Text leftTimeText;
    private float fullTime;
    private float leftTime;

    [Header("Target")]
    public Transform targetLayout;
    public GameObject targetButtonPrefab;
    private int targetIterator = 0;

    [Header("PlayerTarget")]
    public Transform playerTargetLayout;

    [Header("Buttons")]
    public Transform buttonsLayout;
    public GameObject hackButtonPrefab;
    private Vector2 buttonsGrid = new Vector2(5, 6);
    private GameObject[,] hackButtons = new GameObject[5, 6];
    private bool[,] hackClicked = new bool[5, 6];

    private int currentClick = 0;
    private bool isPlaying = false;
    private bool startTimer = false;

    public bool PlayerWin { get; set; } = false;

    private bool CheckPathIntegrity(Vector2[] path, int iteration)
    {
        for (int i = 0; i < iteration; i++)
        {
            if (path[iteration].x == path[i].x && path[iteration].y == path[i].y)
            {
                return false;
            }
        }

        return true;
    }

    private Vector2[] CreateTargetPath(int pathLength)
    {
        Vector2[] targetPath = new Vector2[pathLength];

        targetPath[0].x = Random.Range(0, buttonsGrid.x);
        targetPath[0].y = 0;


        for (int i = 1; i < pathLength; i++)
        {
            if (i % 2 == 0)
            {
                targetPath[i].x = targetPath[i - 1].x;
                targetPath[i].y = Random.Range(0, buttonsGrid.y);
            }
            else
            {
                targetPath[i].x = Random.Range(0, buttonsGrid.x);
                targetPath[i].y = targetPath[i - 1].y;
            }

            if (!CheckPathIntegrity(targetPath, i))
            {
                i--;
            }
        }

        return targetPath;
    }

    private int HackOnPath(Vector2 hack, Vector2[] path)
    {
        for (int i = 0; i < path.Length; i++)
        {
            if (hack.x == path[i].x && hack.y == path[i].y)
            {
                return i;
            }
        }

        return -1;
    }

    private string GetNonPathHack(string[] hacks, string[] targets)
    {
        List<string> nonTargetHacks = new List<string>();
        List<string> targetHacks = new List<string>();

        for (int i = 0; i < targets.Length; i++)
        {
            if (!targetHacks.Contains(targets[i]))
            {
                targetHacks.Add(targets[i]);
            }
        }

        for (int i = 0; i < hacks.Length; i++)
        {
            if (!nonTargetHacks.Contains(hacks[i]) && !targetHacks.Contains(hacks[i]))
            {
                nonTargetHacks.Add(hacks[i]);
            }
        }


        if (Random.value < 0.7)
        {
            return hacks[Random.Range(0, targetHacks.Count)];
        }
        else
        {
            return targetHacks[Random.Range(0, targetHacks.Count)];
        }
    }

    public void StartGame(MinigameHackData data)
    {
        if (isPlaying)
            return;

        isPlaying = true;
        mainText.text = "W³am sie do systemu";

        minigameCanvas.gameObject.SetActive(true);

        fullTime = data.timeLeft;
        leftTime = data.timeLeft;

        timeSlider.maxValue = fullTime;
        timeSlider.value = leftTime;

        string textTime = $"{leftTime:00.00} s";
        leftTimeText.text = textTime;

        int targetLength = data.targets.Length > 6 ? 6 : data.targets.Length;
        Vector2[] targetPath = CreateTargetPath(targetLength);

        for (int i = 0; i < targetLength; i++)
        {
            GameObject targetButtonCopy = Instantiate(targetButtonPrefab, targetLayout, false);
            targetButtonCopy.GetComponentInChildren<TMP_Text>().text = data.targets[i];
        }

        hackButtons = new GameObject[(int)buttonsGrid.x, (int)buttonsGrid.y];
        hackClicked = new bool[(int)buttonsGrid.x, (int)buttonsGrid.y];
        currentClick = 0;

        for (int i = 0; i < buttonsGrid.x; i++)
        {
            for (int j = 0; j < buttonsGrid.y; j++)
            {
                int currentI = i;
                int currentJ = j;
                hackButtons[currentI, currentJ] = Instantiate(hackButtonPrefab, buttonsLayout, false);

                int targetHackIndex = HackOnPath(new Vector2(currentI, currentJ), targetPath);
                if (targetHackIndex > 0)
                {
                    hackButtons[currentI, currentJ].GetComponentInChildren<TMP_Text>().text = data.targets[targetHackIndex];
                }
                else
                {
                    hackButtons[currentI, currentJ].GetComponentInChildren<TMP_Text>().text = GetNonPathHack(data.availableHacks, data.targets);
                }

                hackButtons[currentI, j].GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (currentClick >= 6)
                    {
                        EndGame(false);
                        return;
                    }

                    hackClicked[currentI, currentJ] = true;

                    if (currentClick == 0)
                    {
                        startTimer = true;

                        string textTime = $"{leftTime:00.00} s";
                        leftTimeText.text = textTime;
                        timeSlider.value = data.timeLeft;

                        for (int x = 1; x < buttonsGrid.x; x++)
                        {
                            for (int y = 0; y < buttonsGrid.y; y++)
                            {
                                hackButtons[x, y].GetComponent<Button>().interactable = true;
                            }
                        }
                    }

                    if (currentClick % 2 == 0)
                    {
                        for (int x = 0; x < buttonsGrid.x; x++)
                        {
                            for (int y = 0; y < buttonsGrid.y; y++)
                            {
                                if (y == currentJ)
                                {
                                    if (!hackClicked[x, y])
                                        hackButtons[x, y].GetComponent<Button>().interactable = true;
                                }
                                else
                                {
                                    hackButtons[x, y].GetComponent<Button>().interactable = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int x = 0; x < buttonsGrid.x; x++)
                        {
                            for (int y = 0; y < buttonsGrid.y; y++)
                            {
                                if (x == currentI)
                                {
                                    if (!hackClicked[x, y])
                                        hackButtons[x, y].GetComponent<Button>().interactable = true;
                                }
                                else
                                {
                                    hackButtons[x, y].GetComponent<Button>().interactable = false;
                                }
                            }
                        }
                    }

                    GameObject thisButton = hackButtons[currentI, currentJ];
                    thisButton.GetComponent<Button>().interactable = false;
                    thisButton.GetComponent<Image>().color = pickedColor;

                    GameObject playerTargetCopy = Instantiate(targetButtonPrefab, playerTargetLayout, false);
                    string pickedHack = thisButton.GetComponentInChildren<TMP_Text>().text;
                    playerTargetCopy.GetComponentInChildren<TMP_Text>().text = pickedHack;

                    if (targetIterator < targetLength && pickedHack == data.targets[targetIterator])
                    {
                        targetIterator++;
                        playerTargetCopy.GetComponent<Image>().color = goodColor;
                    }
                    else
                    {
                        targetIterator = 0;
                        playerTargetCopy.GetComponent<Image>().color = wrongColor;
                    }
                    currentClick++;

                    if (targetIterator == targetLength)
                    {
                        EndGame(true);
                        return;
                    }

                    if (currentClick >= 6)
                    {
                        EndGame(false);
                        return;
                    }
                });
            }

        }

        for (int i = 1; i < buttonsGrid.x; i++)
        {
            for (int j = 0; j < buttonsGrid.y; j++)
            {
                hackButtons[i, j].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void EndGame(bool win)
    {
        Debug.Log("Result: " + win);
        PlayerWin = win;
        isPlaying = false;

        if (win)
        {
            mainText.text = "W³amano";
            StartCoroutine(LateCall(3, false));

        }
        else
        {
            mainText.text = "Nie uda³o siê";
            StartCoroutine(LateCall(3, false));

        }
    }

    IEnumerator LateCall(float seconds, bool active)
    {
        yield return new WaitForSeconds(seconds);

        minigameCanvas.gameObject.SetActive(active);
        //Do Function here...
    }

    private void Update()
    {
        if (!isPlaying)
            return;

        if (!startTimer)
            return;

        float time = fullTime - Time.time;
        if (time <= 0)
        {
            EndGame(false);
        }

        string textTime = $"{time:00.00} s";
        leftTimeText.text = textTime;

        timeSlider.value = time;
    }
}
