using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using UnityEngine;

public class Experiment_RandomLotteryTypes : MonoBehaviour
{
    public string m_textSeed = "I like potato";
    public CryptoParticipant[] m_participants = new CryptoParticipant[] {
        new CryptoParticipant("Eloi",10),
        new CryptoParticipant("Zorro",80)
    };
    public ProbabilityRange[] m_victoryRange;
    public float m_totalShares;
    public float m_randomWinner01;
    public int m_winnerIndex;

    [TextArea(0,5)]
    public string m_messageFromTheOrganizer;

    public string GetReportOfWhoWinAndWHy()
    {
        StringBuilder report = new StringBuilder();

        report.AppendLine("⛄>: " + "Hello guys :)-  I will be you guide.");
        report.AppendLine("⛄>: " + "Here are some information about the lottery.");
        report.AppendLine("");
        report.AppendLine("Participants: " +string.Join(" | ", m_participants.Select(k => k.m_name)) );
        report.AppendLine("Seed text: " + m_textSeed);
        report.AppendLine("Random winner as pourcent: " + m_randomWinner01);
        report.AppendLine("Random winner as index: " + m_winnerIndex);
        report.AppendLine("Winner name: " + m_winnerName);
        report.AppendLine("Total group value: " + m_totalShares);

        report.AppendLine("-------- Explaination -------- ");

        report.AppendLine("");
        report.AppendLine("");
        report.AppendLine("⛄>: " + "The following is the information you provided to generate the winner");
        report.AppendLine("");
        report.AppendLine("######## Given Info ######## ");
        for (int i = 0; i < m_participants.Length; i++)
        {
            report.AppendLine(string.Format("{0}| Name: {1} Participation Value: {2}", i,
                m_participants[i].m_name,
                m_participants[i].m_participationValue
                ));
        }

        report.AppendLine("");
        report.AppendLine("");
        report.AppendLine("⛄>: " + "The following is the information are who participated and what are they chance of winning. ");
        report.AppendLine("");
        report.AppendLine("######## Translation of given Info ######## ");
        for (int i = 0; i < m_victoryRange.Length; i++)
        {
            report.AppendLine(string.Format("{0}| Name: {1}   Win Range: {2}-{3}  Pourcent of Winning: {4} ", i,
                m_victoryRange[i].m_participantName,
                m_victoryRange[i].m_min,
                m_victoryRange[i].m_max,
                m_victoryRange[i].m_victoryPourcent
                ));
        }
        report.AppendLine("");
        report.AppendLine("");
        report.AppendLine("⛄>: " + "To decide of the winner. We convert the text you provided to a number called a 'seed' ");
        report.AppendLine("⛄>: " + "This number when put in System.Random() of C# will always generate the same number on any compute.");
        report.AppendLine("⛄>: " + "Feel free to try yourself: https://dotnetfiddle.net/6N9FSc");
        report.AppendLine("⛄>: " + "Coping this number "+ m_seedExplained.seedGenerated + " will give you "+ m_randomWinner01);

        report.AppendLine("");
        report.AppendLine("-------- Seed Explained -------- ");


        report.AppendLine("Given Text:" + m_textSeed);

        report.AppendLine("");
        report.AppendLine("⛄>: " + "We take the text and split it in pieces");
        report.AppendLine("Text Splitted:" + m_seedExplained.charSplit);

        report.AppendLine("");
        report.AppendLine("⛄>: " + "We convert the pieces to numbers base on ASCII ");
        report.AppendLine("Text as splitted number:" + m_seedExplained.charAsNumber);
        report.AppendLine("");
        report.AppendLine("⛄>: " + "We merge them in a big number ");
        report.AppendLine("Text as big integer:" + m_seedExplained.charAsNumberJoined);
        report.AppendLine("");
        report.AppendLine("⛄>: " + "We take the rest of the bit number to fit in an integer");
        report.AppendLine("⛄>: " + "Because number can't be bigger that "+int.MaxValue);
        report.AppendLine("Rest formula:" + m_seedExplained.restOfIntegerModulaFormula);


        report.AppendLine("");
        report.AppendLine("⛄>: " + "The rest of this division give us your seed to decide of the winner");
        report.AppendLine("Seed result:" + m_seedExplained.seedGenerated);

        report.AppendLine("");
        report.AppendLine("⛄>: " + "We give the seed to C# to have a random number between 0-1.");
        report.AppendLine("Put seed in System.Random("+ m_seedExplained.seedGenerated + ") of C# to have:" + m_randomWinner01);


        report.AppendLine("");
        report.AppendLine("⛄>: " + "Now this number is use to decide of who win base on the previous array.");
        report.AppendLine("⛄>: " + "Hope it helps you understand how the winner is decided.");
        report.AppendLine("⛄>: " + "Enjoy and have a good day :) ");
        report.AppendLine("");




        report.AppendLine("");
        report.AppendLine("-------- Organizer Message -------- ");

        report.AppendLine(m_messageFromTheOrganizer);

        report.AppendLine("");
        report.AppendLine("");
        report.AppendLine("-------- Developer Message -------- " );
        report.AppendLine("");
        report.AppendLine("Ping me here if you need: ");
        report.AppendLine("https://eloistree.page.link/discord");
        report.AppendLine("");
        report.AppendLine("Thanks for participation :) !");
        report.AppendLine("Have fun.");

        return report.ToString();
    }

    public string  m_winnerName;
    //If the winner is 0.999999999 and last range is 0.9998 for example
    public bool m_floatMaxError;
    public string m_winnerReport;
    public SeedBasedTextExplained m_seedExplained;
    [ContextMenu("Compute a winner")]
    public void ComputeWinner()
    {
        CryptoShareLottery.ComputeRandomWinner(m_participants, out m_victoryRange, m_textSeed, out m_totalShares, out m_randomWinner01, out m_winnerIndex, out m_floatMaxError  );
        CryptoShareLottery.GenerateSeedBasedOnTextExplanation(m_textSeed, out m_seedExplained);

        m_winnerName = m_participants[m_winnerIndex].m_name;
    }


    private void Start()
    {
        ComputeWinner();
    }

}



[System.Serializable]
public class CryptoParticipant
{
    public string m_name = "";
    public float m_participationValue = 0;

    public CryptoParticipant() { }
    public CryptoParticipant(string name, int shares)
    {
        m_name = name;
        m_participationValue = shares;
    }
}

[System.Serializable]
public class ProbabilityRange
{
    public string m_participantName;
    public int m_participantIndex;
    public float m_min;
    public float m_max;
    public float m_victoryPourcent;
}
[System.Serializable]
public class SeedBasedTextExplained
{
    public string charSplit;
    public string charAsNumber;
    public string charAsNumberJoined;
    public string restOfIntegerModulaFormula;
    public string seedGenerated;
}
public class CryptoShareLottery
{
   

    public static void ComputeRandomWinner(CryptoParticipant[] participants, out ProbabilityRange[] particpantsShare, string seedText, out float totalValueUsed, out float giveRandomWinnerBetween01, out int winnderIndex, out bool hasFloatingError)
    {
        if (string.IsNullOrEmpty(seedText))
            seedText = "Default Winner";
        totalValueUsed = participants.Sum(k => k.m_participationValue); //90

        particpantsShare = new ProbabilityRange[participants.Length];

        float shareTrackIndex = 0;
        for (int i = 0; i < participants.Length; i++)
        {
            particpantsShare[i] = new ProbabilityRange();
            float winPourcent = participants[i].m_participationValue / (float)totalValueUsed;
            // 80 /90 = 0.88 for zorro ---  0.11 eloi
            particpantsShare[i].m_participantName = participants[i].m_name;
            particpantsShare[i].m_participantIndex = i;
            particpantsShare[i].m_min = shareTrackIndex;
            shareTrackIndex += winPourcent;
            particpantsShare[i].m_max = shareTrackIndex;
            particpantsShare[i].m_victoryPourcent = winPourcent;
        }


        giveRandomWinnerBetween01 = GetRandomNumber_BasedOnText(seedText); //0.
                                      
        winnderIndex = -1;
        for (int i = 0; i < particpantsShare.Length; i++)
        {
            if (giveRandomWinnerBetween01 >= particpantsShare[i].m_min && giveRandomWinnerBetween01 < particpantsShare[i].m_max) 
            {
                winnderIndex = particpantsShare[i].m_participantIndex;
                break;
            }
        }

        // In case of between 0.999999999 and 1;
        hasFloatingError = winnderIndex < 0;
        if (hasFloatingError)
        {
            winnderIndex = particpantsShare[particpantsShare.Length - 1].m_participantIndex;
            hasFloatingError = true;
        }
        
    }

    static private void GenerateSeedBasedOnText(string text, out string textAsInteger, out int seed)
    {

        char[] chars = text.ToArray();
        string textAsNumber = "";
        for (int i = 0; i < chars.Length; i++)
        {
            textAsNumber += "" + ((int)chars[i]);
        }
        textAsInteger = textAsNumber;
        // Eloi 20 10 50 60 >  20105060 
        //73321081051071013211211111697116111 /  28000000000 > rest > 857862949
        BigInteger bigSeed = BigInteger.Parse(textAsInteger);
        bigSeed %= int.MaxValue;
        seed = (int)bigSeed;
    }


   
    static public void GenerateSeedBasedOnTextExplanation(string text, out SeedBasedTextExplained report)
    {
        report = new SeedBasedTextExplained();

        char[] chars = text.ToArray();
        string textAsNumber = "";
        for (int i = 0; i < chars.Length; i++)
        {
            report.charSplit += chars[i] + " ";
            report.charAsNumber += (int)chars[i] + " ";
            textAsNumber += "" + ((int)chars[i]);
        }
        report.charAsNumberJoined = report.charAsNumber.Replace(" ", "");
        report.restOfIntegerModulaFormula= report.charAsNumberJoined + " % " + int.MaxValue;

        string  textAsInteger = textAsNumber;
        // Eloi 20 10 50 60 >  20105060 
        //73321081051071013211211111697116111 /  28000000000 > rest > 857862949
        BigInteger bigSeed = BigInteger.Parse(textAsInteger);
        bigSeed %= int.MaxValue;
        int seed = (int)bigSeed;

        report.seedGenerated = ""+seed;
    }

    static public float GetRandomNumber_BasedOnText(string text)
    {
        GenerateSeedBasedOnText(text, out string debugSeed, out int seed);
        Debug.Log("Seed generated: " + debugSeed + " > " + seed);
        return GetRandomNumber_CSharpSeed(seed);
    }

    static public float GetRandomNumber_UnityEngine()
    {
        return UnityEngine.Random.Range(0f, 1f);
    }
    static public float GetRandomNumber_CSharpSeed(int seed)
    {
        System.Random r = new System.Random(seed);
        return (float)r.NextDouble();
    }
    static public float GetRandomNumber_CSharpTime()
    {
        return GetRandomNumber_CSharpSeed(DateTime.Now.Millisecond);
    }
    static public float GetRandomNumber_CSharpTime(DateTime exacteDate)
    {
        return GetRandomNumber_CSharpSeed((int)exacteDate.Ticks);
    }
  
}
