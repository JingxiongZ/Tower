using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretBuild : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    public Animator moneyAnimator;

    public GameObject turretChoiceUI;
    public Button buttonUpgrade;
    private TurretData selectedTurretData;
    private MapCube selectedCube;
    private Animator choiceUICanvasAnimator;
    public Text moneyText;
    private int money = 1000;


    void UpdateMoney(int change = 0)
    {
        money += change;
        moneyText.text = "¥ " + money.ToString();
    }


    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }

    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }

    }

    void Start()
    {
        choiceUICanvasAnimator = turretChoiceUI.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isColliding = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isColliding)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTurretData != null && mapCube.turretOnIt == null)
                    {
                        if (money >= selectedTurretData.cost)
                        {
                            money -= selectedTurretData.cost;
                            UpdateMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData);
                        }
                        else
                        {
                            moneyAnimator.SetTrigger("Wink");
                        }
                    }
                    else if (mapCube.turretOnIt != null)
                    {
                        if (mapCube == selectedCube && turretChoiceUI.activeInHierarchy)
                        {
                            StartCoroutine(HideUI());
                        }
                        else
                        {
                            ShowUI(mapCube.transform.position, mapCube.isUpgraded);

                        }
                        selectedCube = mapCube;
                    }
                }
            }
        }
    }

    void ShowUI(Vector3 position, bool isDisableUpgrade = false)
    {
        StopCoroutine("HideUI");
        turretChoiceUI.SetActive(false);
        turretChoiceUI.SetActive(true);
        turretChoiceUI.transform.position = position;
        buttonUpgrade.interactable = !isDisableUpgrade;

    }

    IEnumerator HideUI()
    {
        choiceUICanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.8f);
        turretChoiceUI.SetActive(false);
    }

    public void OnUpgradeButtonDown()
    {
        if (money >= selectedCube.turretData.upgradePrice)
        {
            UpdateMoney(-selectedCube.turretData.upgradePrice);
            selectedCube.UpgradeTurret();
        }
        else
        {
            moneyAnimator.SetTrigger("Wink");
        }
        selectedCube.UpgradeTurret();
        StartCoroutine(HideUI());

    }

    public void OnDestroyButtonDown()
    {
        selectedCube.DestroyTurret();
        StartCoroutine(HideUI());
    }
}
