using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    protected bool isChangedData = false;
    protected bool bInitialized = false;

    virtual public void Init()
    {
        if (bInitialized)
            return;

        bInitialized = true;
    }

    virtual public void Setup()
    {
        if (!bInitialized)
            Init();
    }

    virtual public void ShowPanel()
    {
        isChangedData = false;
        this.gameObject.SetActive(true);
        Setup();
    }

    virtual public void ClosePanel()
    {
        isChangedData = false;
        this.gameObject.SetActive(false);
    }

    virtual public void Refresh()
    {
        Setup();
    }
}
