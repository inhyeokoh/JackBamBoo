/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Google
using Google.Play.AppUpdate;
using Google.Play.Common;
using UnityEngine.UI;
//Internet Check
using System;
using System.Web;
using System.Net.NetworkInformation;
using UnityEngine.Networking;

public class InAppUpdate : MonoBehaviour
{
    AppUpdateManager appUpdateManager;
    // Start is called before the first frame update
    void Start()
    {
        appUpdateManager = new AppUpdateManager();
        StartCoroutine(CheckInternetConnections(isConnected =>
        {
            if (isConnected)
            {
                Debug.Log("Internet Available!");
                StartCoroutine(CheckForUpdate());
            }
            else
            {
                Debug.Log("Internet Not Available");
            }
        }));
    }

    private IEnumerator CheckForUpdate()
    {
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =
        appUpdateManager.GetAppUpdateInfo();
        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();

            if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                Debug.Log("Update Available!");
            }
            else
            {
                Debug.Log("Update Not Available!");
            }

            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
            StartCoroutine(StratImmediateUpdate(appUpdateInfoResult, appUpdateOptions));
        }
    }

    IEnumerator StratImmediateUpdate(AppUpdateInfo appUpdateInfo_i, AppUpdateOptions appUpdateOptions_i)
    {
        var startUpdateRequest = appUpdateManager.StartUpdate(appUpdateInfo_i, appUpdateOptions_i);
        yield return startUpdateRequest;
    }
    IEnumerator CheckInternetConnections(Action<bool> action)
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            Debug.Log("Error");
            action(false);
        }
        else
        {
            Debug.Log("Success");
            action(true);
        }
    }
}*/