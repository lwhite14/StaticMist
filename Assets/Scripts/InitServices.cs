using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

class InitServices : MonoBehaviour
{
    public static bool isRecording = true;

    async void Awake()
    {
        if (isRecording)
        {
            try
            {
                var options = new InitializationOptions();
                options.SetEnvironmentName("self-test");
                await UnityServices.InitializeAsync(options);
                List<string> consentIdentifiers = await Events.CheckForRequiredConsents();
            }
            catch (ConsentCheckException e)
            {
                Debug.LogError(e);
            }
        }
    }
}
