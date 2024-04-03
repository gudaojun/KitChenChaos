using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private CuttingCounter cuttingCounter;

    private const string CUT = "Cut";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCut+=CuttingCounter_OnProgressChanged;
    }

    private void CuttingCounter_OnProgressChanged(object sender, EventArgs e)
    {
        _animator.SetTrigger(CUT);
    }
}
