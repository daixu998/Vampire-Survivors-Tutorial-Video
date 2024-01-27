using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.currentMoveSpeed *= 1.0f + passiveItemData.Multipler/100f;
    }
}
