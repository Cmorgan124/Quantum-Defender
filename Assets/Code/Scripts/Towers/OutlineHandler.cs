using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Material outline;
    [SerializeField] private Material normal;
    [SerializeField] private Renderer _renderer;



    public void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.material = outline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _renderer.material = normal;
    }

}
