using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject CanvasUI;
    private Node _target;
    public TextMeshProUGUI UpgradeCostText;
    public TextMeshProUGUI SellAmountText;
    public Button UpgradeButton;

    public void SetTarget(Node target)
    {
        _target = target;

        if (!_target.IsUpgraded)
        {
            UpgradeButton.interactable = true;
            UpgradeCostText.text = $"${_target.TurretBlueprint.UpgradeCost}";
        }
        else
        {
            UpgradeButton.interactable = false;
            UpgradeCostText.text = $"---";
        }

        SellAmountText.text = $"${_target.TurretBlueprint.GetSellAmount()}";

        transform.position = _target.GetBuildPosition();
        CanvasUI.SetActive(true);
    }

    public void Hide()
    {
        CanvasUI.SetActive(false);
    }

    public void Upgrade()
    {
        _target.UpgradeTurret();
        BuildManager.Instance.ResetSelectedNode();
    }

    public void Sell()
    {
        _target.SellTurret();
        BuildManager.Instance.ResetSelectedNode();
    }
}
