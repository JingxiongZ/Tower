using UnityEngine;

[System.Serializable]
public class TurretData
{
    public GameObject turretPrefab;
    public int cost;
    public GameObject turretUpgradePrefab;
    public int upgradePrice;
    public TurretType turretType;
}

public enum TurretType
{
    Laser,
    Missile,
    Standard
}