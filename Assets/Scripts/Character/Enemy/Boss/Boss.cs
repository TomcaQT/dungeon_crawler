using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private void Start()
    {
        _hp.OnResourceChange += OnHpChange;
    }

    private void OnHpChange(object sender, EventArgs e)
    {
        var bossController = (BossController) _enemyController;
        if (bossController.CurrentPhasePercentages < _hp.Percentage01)
            bossController.NextPhase();
    }
    

    
}
