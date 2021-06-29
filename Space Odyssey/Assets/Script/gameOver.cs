using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _DeathReason;

    [TextArea]
    public string _lifeDeath,EnergieDeath;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
        if(PlayerManager.Instance.Vie <= 0f)
        {
            _DeathReason.text = _lifeDeath;
        }

        if (PlayerManager.Instance.Energie <= 0f)
        {
            _DeathReason.text = EnergieDeath;
        }
    }
}
