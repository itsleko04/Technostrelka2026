using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SocketsChecker : MonoBehaviour
{
    [SerializeField] private XRSocket _rightSocket, _wrongSocket, _allSocket;

    public void Check()
    {
        if (_allSocket.SnapPanels.Count > 0)
            return;

        bool allRight = true;
        foreach (XRGrabInteractable obj in _rightSocket.SnapPanels)
        {
            if(!obj.GetComponent<SocketItem>().IsRight)
            {
                allRight = false;
            }
        }
        foreach (XRGrabInteractable obj in _wrongSocket.SnapPanels)
        {
            if (obj.GetComponent<SocketItem>().IsRight)
            {
                allRight = false;
            }
        }

        if (allRight)
            GlobalFinishController.Instance.DoGoodFinish();
        else
            GlobalFinishController.Instance.DoBadFinish();
    }
}
