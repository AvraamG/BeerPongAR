using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

[ExecuteInEditMode]
public class ManoMotionSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //this will execute if something new comes up not every frame. This means though that even a click on the inspector it will make it fire.
        SetBuildSettingsAndAndroidPermissions();
    }

    void SetBuildSettingsAndAndroidPermissions()
    {

#if UNITY_EDITOR && UNITY_ANDROID
        Debug.Log("Setting up ManoMotion Library Requirements");
        PlayerSettings.Android.preferredInstallLocation = AndroidPreferredInstallLocation.PreferExternal;
        PlayerSettings.Android.forceInternetPermission = true;
        PlayerSettings.Android.forceSDCardPermission = true;
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);

        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;



#endif

#if UNITY_EDITOR && UNITY_IOS   

        PlayerSettings.Android.preferredInstallLocation = AndroidPreferredInstallLocation.PreferExternal;
        PlayerSettings.Android.forceInternetPermission = true;
        PlayerSettings.Android.forceSDCardPermission = true;
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        int arm64Architecture = 1;
        PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, arm64Architecture);
        if (PlayerSettings.iOS.cameraUsageDescription == "")
        {
            PlayerSettings.iOS.cameraUsageDescription = "This application requires camera permission in order to detect a hand when you place it in front of the camera and understand the gesture interaction. ";

        }





        //PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, arm64Architecture);


        PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
        PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, arm64Architecture);



#endif

    }
}
