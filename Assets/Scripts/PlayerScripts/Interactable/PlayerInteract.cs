using UnityEngine;

/// <summary>
/// Ermöglicht ein GameObjekt mit einem Interactable zu interagieren
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    private Camera camera;

    [Tooltip("die Entfernung, ab der ein Infotext angezeigt wird")] [SerializeField]
    private float distance = 3f;

    [SerializeField] private LayerMask mask;

    private PlayerUI playerUI;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<PlayerLook>().camera;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        playerUI.UpdateText(string.Empty);

        var transform1 = camera.transform;
        Ray ray = new Ray(transform1.position, transform1.forward);

        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();

                playerUI.UpdateText(interactable.promptMessage, interactable.promptColor);

                if (inputManager.OnFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}