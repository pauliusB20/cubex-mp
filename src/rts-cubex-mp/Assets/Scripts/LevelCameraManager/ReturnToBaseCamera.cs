using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToBaseCamera : MonoBehaviour
{
    [SerializeField] string baseCameraCode = "M1";
//    [SerializeField] GameObject MainCamera;
//    [SerializeField] bool isInBase = false;
   
//    private void Update() {
//         if (MainCamera.activeSelf) 
//        {
//             isInBase = true;
//        }
//        if (!MainCamera.activeSelf)
//        {
//             isInBase = false;
//        }

//    }
   public void goToBaseCamera()
   {
       FindObjectOfType<LvlCameraManager>().setCamerCode(baseCameraCode);
    //    if (!isInBase)
    //    {
    //     var areaCameras = FindObjectsOfType<AreaCamera>();

    //     for (int aIndex = 0; aIndex < areaCameras.Length; aIndex++)
    //     {
    //         if (areaCameras[aIndex].getCameraCode() == "C1")
    //             areaCameras[aIndex].enableCamera(false);
    //     }

    //     MainCamera.SetActive(true);
    //     isInBase = true;
    //    }
   }
}
