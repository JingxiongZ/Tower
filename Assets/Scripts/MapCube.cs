using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turretOnIt;
    public GameObject buildTurretEffect;
    [HideInInspector]
    public bool isUpgraded = false;
    [HideInInspector]
    public TurretData turretData;
    private Renderer renderer;
    private Color color;


    void Start()
    {
        renderer = GetComponent<Renderer>();
        color = renderer.material.color;
    }

    void Update()
    {

    }

    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isUpgraded = false;
        GameObject buildEffect = GameObject.Instantiate(buildTurretEffect, transform.position, Quaternion.identity);
        Destroy(buildEffect, 1f);
        turretOnIt = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
    }

    void OnMouseEnter()
    {
        if (turretOnIt == null && !EventSystem.current.IsPointerOverGameObject())
        {
            renderer.material.color = Color.red;
        }
    }

    void OnMouseExit()
    {
        renderer.material.color = color;
    }

    public void UpgradeTurret()
    {
        if (isUpgraded)
        {
            return;
        }
        Destroy(turretOnIt);
        isUpgraded = true;
        GameObject buildEffect = GameObject.Instantiate(buildTurretEffect, transform.position, Quaternion.identity);
        Destroy(buildEffect, 1f);
        turretOnIt = GameObject.Instantiate(turretData.turretUpgradePrefab, transform.position, Quaternion.identity);
    }

    public void DestroyTurret()
    {
        Destroy(turretOnIt);
        GameObject buildEffect = GameObject.Instantiate(buildTurretEffect, transform.position, Quaternion.identity);
        Destroy(buildEffect, 1f);
        isUpgraded = false;
        turretOnIt = null;
        turretData = null;
    }
}
