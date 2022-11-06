using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradingBuildings : MonoBehaviour
{
    public Camera currentCamera;
    public GameObject[] buildingsToPlace;

    public Material yellowMaterial;
    public Material tdPaletteMaterial;
    protected Transform selectedObject;

    protected Tower tower;
    protected Vector3 oldTowerPos;
    protected Quaternion oldTowerRot;

    protected GameObject newTower;
    void Update()
    {
        SetNormalMaterial();

        RaycastHit[] hits;
        hits = Physics.RaycastAll(currentCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        foreach (RaycastHit hit in hits)
        {
            var selection = hit.transform;
            if (hit.collider.CompareTag("Towers"))
            {
                tower = hit.transform.gameObject.GetComponent<Tower>();
                if ((tower.towerId + 1) % 3 != 0)
                {
                    var selectionRenderers = selection.GetComponentsInChildren<MeshRenderer>();
                    foreach (var renderer in selectionRenderers)
                    {
                        renderer.material = yellowMaterial;
                    }

                    if (Mouse.current.leftButton.wasPressedThisFrame && !GetComponent<PlacingBuildings>().isBuild)
                    {
                        //tower = hit.transform.gameObject.GetComponent<Tower>();

                        oldTowerPos = hit.transform.position;
                        oldTowerRot = hit.transform.rotation;

                        Destroy(hit.transform.gameObject);
                        PlaceTower();
                    }

                    selectedObject = selection;
                }
            }
        }
    }

    void SetNormalMaterial()
    {
        if (selectedObject != null)
        {
            var selectionRenderers = selectedObject.GetComponentsInChildren<MeshRenderer>();
            foreach (var renderer in selectionRenderers)
            {
                renderer.material = tdPaletteMaterial;
            }
            selectedObject = null;
        }
    }

    protected void TowerSelection()//alternatywna metoda, jeśli wieże nie zawsze miałyby 2 ulepszenia.
    {
        switch (tower.towerId)//lista zamiany wież
        {
            case 0://wieża łucznika
                {
                    Instantiate(buildingsToPlace[1], oldTowerPos, oldTowerRot, transform);
                    break;
                }
            case 1://wieża kusznika
                {
                    Instantiate(buildingsToPlace[2], oldTowerPos, oldTowerRot, transform);
                    break;
                }
        }
    }

    protected void PlaceTower()
    {
        Debug.Log("Here now " + tower.towerId);
        newTower = Instantiate(buildingsToPlace[tower.towerId+1], oldTowerPos, oldTowerRot, transform);
        newTower.name = "Tower " + transform.childCount.ToString();
        SetAllChildrenTag(newTower, "Towers");
    }

    protected void SetAllChildrenTag(GameObject gameObject, string tag)
    {
        Transform[] allChildren = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in allChildren)
        {
            child.gameObject.tag = tag;
        }
    }
}
