using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public TextMeshProUGUI score;
    public Button NewGameBt;
    public Button ResetScoreBt;
    public Button BackBt;
    public Button[] buttons = new Button[9];
    public Sprite[] icons = new Sprite[3];
    public Image[] lines = new Image[8];

    int turn = 0;
    int moves = 0;
    char status = 'r';
    int[] result = new int[2] { 0, 0 };
    int[] storeMove = new int[9] { -1, -2, -3, -4, -5, -6, -7, -8, -9 };

    private void Start()
    {
        result[0] = PlayerPrefs.GetInt("resX");
        result[1] = PlayerPrefs.GetInt("resO");
        score.text = $"{result[0]} - {result[1]}";

        if (result[0] + result[1] >= 1)
        {
            ResetScoreBt.interactable = true;
        }
    }

    public void Move(int btIndex)
    {
        buttons[btIndex].image.sprite = icons[turn];
        buttons[btIndex].interactable = false;

        storeMove[btIndex] = turn;
        moves++;
        if (moves == 1)
        {
            NewGameBt.interactable = true;
        }
        else if (moves >= 5)
        {
            status = WinCheck(btIndex);
            if (status == 'W')
            {
                for (int i = 0; i < 9; i++)
                {
                    buttons[i].interactable = false;
                }
                if (turn == 0)
                {
                    result[0]++;
                }
                else
                {
                    result[1]++;
                }
                ResetScoreBt.interactable = true;
                NewGameBt.interactable = false;
                BackBt.interactable = false;
                Invoke("ResetGame", 1.5f);
            }
            else if (status == 'D')
            {
                NewGameBt.interactable = false;
                BackBt.interactable = false;
                score.text = "DRAW";
                Invoke("ResetGame", 3);
            }
        }

        if (turn == 0)
        {
            turn = 1;
            return;
        }
        turn = 0;
    }

    private char WinCheck(int btIndex)
    {
        if (storeMove[0] == storeMove[1] && storeMove[0] == storeMove[2])
        {
            lines[0].gameObject.SetActive(true);
            return 'W';
        }
        else if (storeMove[3] == storeMove[4] && storeMove[3] == storeMove[5])
        {
            lines[1].gameObject.SetActive(true);
            return 'W';
        }
        else if (storeMove[6] == storeMove[7] && storeMove[6] == storeMove[8])
        {
            lines[2].gameObject.SetActive(true);
            return 'W';
        }
        else if (storeMove[0] == storeMove[3] && storeMove[0] == storeMove[6])
        {
            lines[3].gameObject.SetActive(true);
            return 'W';
        }
        else if (storeMove[1] == storeMove[4] && storeMove[1] == storeMove[7])
        {
            lines[4].gameObject.SetActive(true);
            return 'W';
        }
        else if (storeMove[2] == storeMove[5] && storeMove[2] == storeMove[8])
        {
            lines[5].gameObject.SetActive(true);
            return 'W';
        }
        else if (storeMove[0] == storeMove[4] && storeMove[0] == storeMove[8])
        {
            lines[6].gameObject.SetActive(true);
            return 'W';
        }
        else if (storeMove[2] == storeMove[4] && storeMove[2] == storeMove[6])
        {
            lines[7].gameObject.SetActive(true);
            return 'W';
        }

        else if (moves == 9)
        {
            Debug.Log("Draw");
            return 'D';
        }
        Debug.Log("no win yet");
        return 'E';
    }

    public void ResetGame()
    {
        for (int i = 0; i < 9; i++)
        {
            buttons[i].image.sprite = icons[2];
            buttons[i].interactable = true;
            storeMove[i] = (i * -1) - 1;
        }
        for (int i = 0; i < 8; i++)
        {
            lines[i].gameObject.SetActive(false);
        }
        moves = 0;
        if (turn % 2 == 0)
        {
            turn = 0;
        }
        else
        {
            turn = 1;
        }
        score.text = $"{result[0]} - {result[1]}";
        BackBt.interactable = true;
    }

    public void ResetScore()
    {
        result = new int[2] { 0, 0 };
        PlayerPrefs.SetInt("resX", result[0]);
        PlayerPrefs.SetInt("resO", result[1]);
        score.text = $"{result[0]} - {result[1]}";
        ResetScoreBt.interactable = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("App closed");
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("resX", result[0]);
        PlayerPrefs.SetInt("resO", result[1]);
    }
}

