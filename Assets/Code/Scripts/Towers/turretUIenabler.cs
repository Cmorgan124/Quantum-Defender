using UnityEngine;
using UnityEngine.EventSystems;

public class turretUIenabler : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Refrences")]
    [SerializeField] GameObject upgradeCanvas;
    [SerializeField] Canvas canvas;

    [SerializeField] private Material outline;
    [SerializeField] private Material normal;
    [SerializeField] private Renderer _renderer;
    Camera maincamera;


    void Awake()
    {
        canvas.worldCamera = maincamera;
        upgradeCanvas.SetActive(false);    
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       upgradeCanvas.SetActive(true); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.material = outline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _renderer.material = normal;
    }

}
