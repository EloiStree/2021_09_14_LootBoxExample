using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ToWinnerDemo : MonoBehaviour
{

    public Experiment_RandomLotteryTypes m_winnerAlgo;
    public InputField m_seedText;
    public InputField m_winnerName;
    public InputField m_report;

    public void ComputeTheWinner()
    {
        m_winnerAlgo.m_textSeed = m_seedText.text;
        m_winnerAlgo.ComputeWinner();
        m_winnerName.text = m_winnerAlgo.m_winnerName;
        m_report.text = m_winnerAlgo.GetReportOfWhoWinAndWHy();
    }
}
