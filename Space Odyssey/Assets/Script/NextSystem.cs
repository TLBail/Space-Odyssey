using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSystem : MonoBehaviour
{
    [SerializeField] private GameObject _particleFTl;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("Next", 5f);
            Camera.main.orthographicSize = 5;
            _particleFTl.SetActive(true);
        }

    }


    private void Next()
    {
        _particleFTl.SetActive(false);
        GameManager.Instance.backToGalaxyView();
    }
}
