using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint StandardTurret;
    public TurretBlueprint MissileLauncherTurret;
    public TurretBlueprint LaserBeamerTurret;

    public void SelectStandardTurret()
    {
        BuildManager.Instance.SelectTurretToBuild(StandardTurret);
    }

    public void SelectMissileLauncherTurret()
    {
        BuildManager.Instance.SelectTurretToBuild(MissileLauncherTurret);
    }

    public void SelectLaserBeamerTurret()
    {
        BuildManager.Instance.SelectTurretToBuild(LaserBeamerTurret);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
