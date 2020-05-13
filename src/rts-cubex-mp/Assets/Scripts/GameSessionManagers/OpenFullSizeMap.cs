using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFullSizeMap : MonoBehaviour
{
    [SerializeField] GameObject smallMiniMap;
    [SerializeField] GameObject bigSizeMap;

    public void openBigMap()
    {
        smallMiniMap.SetActive(false);
        bigSizeMap.SetActive(true);
    }

    public void openSmallMap()
    {
        smallMiniMap.SetActive(true);
        bigSizeMap.SetActive(false);
    }
}
