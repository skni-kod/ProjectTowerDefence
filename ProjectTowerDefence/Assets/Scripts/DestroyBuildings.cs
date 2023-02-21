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
                foreach (var selectionRenderer in selection.GetComponentsInChildren<MeshRenderer>())
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
            foreach (var selectionRenderer in selectedObject.GetComponentsInChildren<MeshRenderer>())
            {
                selectionRenderer.material = tdPaletteMaterial;
            }
            selectedObject = null;
        }
    }
}
