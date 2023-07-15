using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    private TurretBlueprint _turretBlueprintToBuild;
    private Node _selectedNode;
    public NodeUI NodeUI;

    public GameObject BuildEffect;
    public GameObject SellEffect;

    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CanBuild => _turretBlueprintToBuild != null;

    public void ResetTurretBuilt()
    {
        _turretBlueprintToBuild = null;
    }

    public void SelectNode(Node node)
    {
        if (node == _selectedNode)
        {
            ResetSelectedNode();
            return;
        }
        _selectedNode = node;
        ResetTurretBuilt();
        NodeUI.SetTarget(node);
    }

    public void SelectTurretToBuild(TurretBlueprint turretBlueprint)
    {
        ResetSelectedNode();
        if (!CanBuy(turretBlueprint.Cost))
            return;
        _turretBlueprintToBuild = turretBlueprint;
    }

    public void ResetSelectedNode()
    {
        _selectedNode = null;
        NodeUI.Hide();
    }

    public bool CanBuy(int cost) => PlayerStats.Money >= cost;

    public TurretBlueprint GetTurretToBuild()
    {
        return _turretBlueprintToBuild;
    }
}
