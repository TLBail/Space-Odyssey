using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeteManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _backGroundArray;
    private void OnEnable()
    {
        foreach (var obj in _backGroundArray)
        {
            obj.SetActive(false);
        }

        _backGroundArray[GameManager.Instance.ActualPlaneteIndex].SetActive(true);

    }

}
