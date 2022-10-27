using UnityEngine;
using UnityEngine.InputSystem;

public class DestroyBuildings : MonoBehaviour
{
    public Camera currentCamera;
    public Material redMaterial;
    public Material tdPaletteMaterial;
    protected Transform selectedObject;

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
                var selectionRenderer = selection.GetComponentInChildren<MeshRenderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = redMaterial;
                }
                if (Mouse.current.leftButton.wasPressedThisFrame && !GetComponent<PlacingBuildings>().isBuild)
                {

                    print(hit);
                    Destroy(hit.transform.gameObject);

                }
                selectedObject = selection;
            }
            
        }

    }

    void SetNormalMaterial()
    {
        if (selectedObject != null)
        {
            var selectionRenderer = selectedObject.GetComponentInChildren<MeshRenderer>();
            selectionRenderer.material = tdPaletteMaterial;
            selectedObject = null;
        }
    }
}
