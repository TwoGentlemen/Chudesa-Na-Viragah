using UnityEngine;
using UnityEngine.UI;

public class ScoreTime : MonoBehaviour
{
    public Text textScore;

    private float minutes = 0;
    private float seconds = 0;
    private float deSeconds = 0;
    private bool isStopedTimer = false;


    private string FormationTimeOutput(int minut, int secon,int deSec)
    {
        string t = "";
        t += (minut < 10) ? "0" + minut.ToString("f0") : minut.ToString("f0");
        t += ":";
        t += (secon < 10) ? "0" + secon : "" + secon;
        t+=":";
        t += (deSec < 10) ? "0" + deSec : "" + deSec;

        return t;
    }

    void FixedUpdate()
    {
        if (!isStopedTimer)
        {
          
            deSeconds+=1.2f;
            if (deSeconds >= 60)
            {
                deSeconds = 0;
                seconds++;
            }
            if (seconds >= 60)
            {
                minutes += 1;
                seconds = 0;
            }
           

            textScore.text = FormationTimeOutput((int)minutes,(int)seconds,(int)deSeconds);
        }

    }

    public string GetTime()
    {
        isStopedTimer = true;
        return textScore.text;
    }

    //public void CountingTime(string curentLevel)
    //{
    //    stopedTimer = false;
    //    float recordMinutes = PlayerPrefs.GetFloat("minutes"+curentLevel);
    //    float recordSeconds = PlayerPrefs.GetFloat("seconds"+curentLevel);
    //    float recordDeSeconds = PlayerPrefs.GetFloat("Deseconds"+curentLevel);

    //    if (recordMinutes<=0 && recordSeconds<=0 && recordDeSeconds<=0)
    //    {
    //        recordMinutes = 1000;
    //        recordSeconds = 1000;
    //        recordDeSeconds = 1000;
    //    }

    //    if (recordMinutes > minutes)
    //    {
    //        recordMinutes = minutes;
    //        recordSeconds = seconds;
    //        recordDeSeconds = deSeconds;
    //    }else
    //    if (recordMinutes == minutes)
    //    {
    //        if (recordSeconds > seconds)
    //        {
    //            recordSeconds = seconds;
    //            recordDeSeconds = deSeconds;
    //        }else
    //        if (recordDeSeconds > deSeconds)
    //        {
    //            recordDeSeconds = deSeconds;
    //        }
    //    }

    //    //Выводим рекорд
    //    textRecord.text = "Record: " + FormationTimeOutput((int)recordMinutes, (int)recordSeconds,(int)deSeconds);
    //    //выводим текущий счет
    //    textCurentScore.text ="Current time: "+ FormationTimeOutput((int)minutes, (int)seconds,(int)deSeconds);
    //    //Выводим кол-во смертей
    //    int die = 0;
    //    if (PlayerPrefs.HasKey("diaCount" + curentLevel))
    //    {
    //        die = PlayerPrefs.GetInt("diaCount" + curentLevel);
    //    }
    //    textDia.text = "Death: "+die;
    //    //сохраняем рекорд
    //    PlayerPrefs.SetFloat("minutes" + curentLevel, recordMinutes);
    //    PlayerPrefs.SetFloat("seconds" + curentLevel, recordSeconds);
    //    PlayerPrefs.SetFloat("Deseconds" + curentLevel, recordDeSeconds);
    //    PlayerPrefs.Save();


    //}
}
