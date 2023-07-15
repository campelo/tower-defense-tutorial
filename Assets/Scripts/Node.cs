using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color AvailableHoverColor;
    public Color UnavailableHoverColor;
    private Renderer _rend;
    private Color _startColor;
    public Vector3 PositionOffset;

    [HideInInspector]
    public GameObject Turret;
    [HideInInspector]
    public TurretBlueprint TurretBlueprint;
    [HideInInspector]
    public bool IsUpgraded = false;

    private bool _hasTurret => Turret != null;

    private void OnMouseDown()
    {
        if (!GameManager.IsOtherDevice)
            return;
        InteractWithNode();
    }

    public void InteractWithNode()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (_hasTurret)
        {
            BuildManager.Instance.SelectNode(this);
            return;
        }

        if (!BuildManager.Instance.CanBuild)
            return;

        BuildTurret(BuildManager.Instance.GetTurretToBuild());
    }

    public void UpgradeTurret()
    {
        if (IsUpgraded)
            return;

        if (!BuildManager.Instance.CanBuy(TurretBlueprint.UpgradeCost))
            return;
        
        PlayerStats.Money -= TurretBlueprint.UpgradeCost;

        Destroy(Turret);

        GameObject turret = Instantiate(TurretBlueprint.UpgradePrefab, GetBuildPosition(), transform.rotation);
        Turret = turret;

        GameObject buildEffect = Instantiate(BuildManager.Instance.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 5f);

        IsUpgraded = true;

        BuildManager.Instance.ResetTurretBuilt();
    }

    void BuildTurret(TurretBlueprint turretBlueprint)
    {
        if (!BuildManager.Instance.CanBuild)
            return;

        if (!BuildManager.Instance.CanBuy(turretBlueprint.Cost))
            return;

        PlayerStats.Money -= turretBlueprint.Cost;
        GameObject turret = Instantiate(turretBlueprint.Prefab, GetBuildPosition(), transform.rotation);
        Turret = turret;
        TurretBlueprint = turretBlueprint;

        GameObject buildEffect = Instantiate(BuildManager.Instance.BuildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 5f);

        BuildManager.Instance.ResetTurretBuilt();
    }

    public void SellTurret()
    {
        PlayerStats.Money += TurretBlueprint.GetSellAmount();
        Destroy(Turret);
        GameObject sellEffect = Instantiate(BuildManager.Instance.SellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(sellEffect, 5f);
        TurretBlueprint = null;
        IsUpgraded = false;
    }

    void OnMouseEnter()
    {
        if (!GameManager.IsOtherDevice)
            return;
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!BuildManager.Instance.CanBuild)
            return;
        if (_hasTurret)
        {
            _rend.material.color = UnavailableHoverColor;
            return;
        }
        _rend.material.color = AvailableHoverColor;
    }

    void OnMouseExit()
    {
        _rend.material.color = _startColor;
    }
    
    void Start()
    {
        _rend = GetComponent<Renderer>();
        _startColor = _rend.material.color;
    }

    void Update()
    {
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + PositionOffset;
    }
}
