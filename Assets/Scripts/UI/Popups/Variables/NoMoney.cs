using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoMoney : BasicPopup
{
    [SerializeField] private Button close;
    [SerializeField] private Button okay;

    private void Start()
    {
        close.onClick.AddListener(Close);
        okay.onClick.AddListener(Close);
    }

    private void OnDestroy()
    {
        close.onClick.RemoveListener(Close);
        okay.onClick.RemoveListener(Close);
    }
    public override void ResetPopup()
    {
    }

    public override void SetPopup()
    {
    }

    private void Close()
    {
        Hide();
    }
}
