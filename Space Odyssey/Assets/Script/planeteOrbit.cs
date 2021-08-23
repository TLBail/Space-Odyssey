using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public enum TypePlanete
{
    DESERTE,
    GAZEUSE
}

public class planeteOrbit : MonoBehaviour
{

    [SerializeField] private float _rotationSpeed, _rotationAnimSpeed;
    [SerializeField] private GameObject _cercleAnimObj, _textInfo;
    [SerializeField] private int _planetIndex, _lifeCost, _EnergieCost;
    [SerializeField] public Sprite planeteSprite;
    [SerializeField] public Sprite groundSprite;
    [SerializeField] public TypePlanete TypePlanete;
    [SerializeField] private GameObject turret;
    [SerializeField, Tooltip("une valeur est tirer au sort entre 100 et 0 si elle est inférieur la tourell est invoker "), Range(0, 100)]
    private int chanceDeInvokerUneTurret;
    [Header("SpaceStation")]
    [SerializeField] private GameObject spaceStationPrefab;
    [SerializeField, Tooltip("une valeur est tirer au sort entre 100 et 0 si elle est inférieur la station spatial est invoker "), Range(0, 100)]
    private int chanceDeInvokerUneSpaceStation;


    [NonSerialized] public bool haveASpaceStation;
    
    private CircleCollider2D _circleCollider2D;
    private TextMeshPro _text;

    private bool playerIsNear;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _text = _textInfo.GetComponent<TextMeshPro>();
        _text.text = _text.text + "\n Cout en Vie  : " + _lifeCost + "\n Cout en Energie : " + _EnergieCost;

        spaceStationInvokeTEst(); 
        turretTest();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, _rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        if (playerIsNear) checkAndWaitForPlayerInput();
    }

    private void checkAndWaitForPlayerInput()
    {
        _cercleAnimObj.transform.Rotate(Vector3.forward * (Time.deltaTime * _rotationAnimSpeed));
        if (Input.GetKeyDown(KeyCode.E)) goToThePlanete();
    }

    private void goToThePlanete()
    {
        GameManager.Instance.actualPlanete = this;
        PlayerManager.Instance.Energie += _EnergieCost;
        PlayerManager.Instance.Vie += _lifeCost;
        GameManager.Instance.planeteView();

        if (haveASpaceStation) tellWehaveASpaceStation();

    }

    private void tellWehaveASpaceStation()
    {
        NotificationManager notificationManager = PlayerManager.Instance.notificationManager;
        notificationManager.newNotification(NotificationType.INFORMATION, "Amelioration Disponible (F) !!");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _cercleAnimObj.SetActive(true);
            _textInfo.SetActive(true);
            playerIsNear = true;
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _cercleAnimObj.SetActive(false);
            _textInfo.SetActive(false);
            playerIsNear = false;
        }
    }

    private void  turretTest()
    {
        int nbTirerAuSort = Random.Range(0, 100);
        if (nbTirerAuSort < chanceDeInvokerUneTurret)
        {
            GameObject turretInstancied = (GameObject) Instantiate(turret, 
                new Vector3(transform.position.x + 2, transform.position.y + 2, transform.position.z),
                Quaternion.identity);

            turretInstancied.transform.SetParent(transform);
            turretInstancied.GetComponent<TurretAI>().planeteTransform = this.transform;

        }
    }

    private void spaceStationInvokeTEst()
    {
        
        int nbTirerAuSort = Random.Range(0, 100);
        if (nbTirerAuSort < chanceDeInvokerUneSpaceStation)
        {
            GameObject spaceStationObj = (GameObject) Instantiate(spaceStationPrefab, 
                new Vector3(transform.position.x + 2, transform.position.y + 2, transform.position.z),
                Quaternion.identity);

            spaceStationObj.transform.SetParent(transform);
            haveASpaceStation = true;
            
            spaceStationObj.GetComponent<orbitMover>().setPlanetePosition(transform);
            
        }
    }

   
}
