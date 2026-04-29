using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class XRSocket : MonoBehaviour
{
    public RectTransform Container => _container;
    [SerializeField] private RectTransform _container; // Объект с VerticalLayoutGroup
    [SerializeField] private float _snapDistance = 0.2f;

    private VerticalLayoutGroup _layoutGroup;
    public List<XRGrabInteractable> SnapPanels => _snappedPanels;
    private List<XRGrabInteractable> _snappedPanels = new List<XRGrabInteractable>();

    void Awake()
    {
        if (_container == null) _container = GetComponent<RectTransform>();
        _layoutGroup = _container.GetComponent<VerticalLayoutGroup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что в триггер попала панель с XRGrabInteractable
        if (other.TryGetComponent<XRGrabInteractable>(out var interactable))
        {
            // Подписываемся на событие отпускания (Select Exited)
            interactable.selectExited.AddListener(OnPanelReleased);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<XRGrabInteractable>(out var interactable))
        {
            interactable.selectExited.RemoveListener(OnPanelReleased);
        }
    }

    private void OnPanelReleased(SelectExitEventArgs args)
    {
        XRGrabInteractable panel = args.interactableObject as XRGrabInteractable;
        if (panel == null) return;

        // Примагничиваем к Vertical Layout Group
        SnapToContainer(panel);
    }

    public void SnapToContainer(XRGrabInteractable panel)
    {
        panel.transform.SetParent(_container);

        // Сбрасываем локальные трансформации, чтобы Layout Group подхватил объект
        panel.transform.localPosition = Vector3.zero;
        panel.transform.localRotation = Quaternion.identity;
        panel.transform.localScale = Vector3.one;

        if (!_snappedPanels.Contains(panel))
        {
            _snappedPanels.Add(panel);
            // Подписываемся на повторный захват, чтобы вытащить из сокета
            panel.selectEntered.AddListener(OnPanelGrabbedAgain);
        }

        // Принудительно обновляем Layout
        LayoutRebuilder.ForceRebuildLayoutImmediate(_container);
    }

    private void OnPanelGrabbedAgain(SelectEnterEventArgs args)
    {
        XRGrabInteractable panel = args.interactableObject as XRGrabInteractable;

        // Возвращаем в мировое пространство или в корень сцены, когда вытаскиваем
        panel.transform.SetParent(null);
        _snappedPanels.Remove(panel);
        panel.selectEntered.RemoveListener(OnPanelGrabbedAgain);

        LayoutRebuilder.ForceRebuildLayoutImmediate(_container);
    }
}
