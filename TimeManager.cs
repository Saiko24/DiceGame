using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SO;

public class TimeManager : MonoBehaviour
{
    public static TimeManager sharedInstance = null;
    public StringVariable LastCheckTime;
    public StringVariable LastCheckDate;
    private string _currentTime;
    private string _currentDate;
    private string _url = "http://leatonm.net/wp-content/uploads/2017/candlepin/getdate.php";

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        RequestCurrentDateAndTime();
    }

    public IEnumerator RequestCurrentDateAndTime()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(_url))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = _url.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                string _timeData = webRequest.downloadHandler.text;

                string[] words = _timeData.Split('/');

                _currentDate = words[0];
                _currentTime = words[1];

            }
        }
    }

    public void UpdateLastCheckDate()
    {
        RequestCurrentDateAndTime();

        LastCheckDate.value = _currentDate;
        LastCheckTime.value = _currentTime;
    }

    public string getCurrentDate()
    {
        return _currentDate;
    }

    public string getCurrentTime()
    {
        return _currentTime;
    }

}