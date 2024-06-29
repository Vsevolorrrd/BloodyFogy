using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    public NPC npc;
    public bool Outlaw;
    public bool Repablic;
    public bool KaiserReich;
    public bool Commonwealth;
    public bool AliedToPlayer;


    void Start()
    {
        npc.WhatToSee = 0;
        npc.enemyLayers = 0;
        if (Outlaw)
        {
            // Add additional layers to the mask
            npc.WhatToSee |= LayerMask.GetMask("object", "Repablic", "KaiserRiech", "Commonwealth");
            npc.enemyLayers |= LayerMask.GetMask("Repablic", "KaiserRiech", "Commonwealth");
            if (!AliedToPlayer)
            {
                npc.WhatToSee |= LayerMask.GetMask("Player");
                npc.enemyLayers |= LayerMask.GetMask("Player");
            }
        }
        if (KaiserReich)
        {
            // Add additional layers to the mask
            npc.WhatToSee |= LayerMask.GetMask("object", "Repablic", "Outlaw", "Commonwealth");
            npc.enemyLayers |= LayerMask.GetMask("Repablic", "Outlaw", "Commonwealth");
            if (!AliedToPlayer)
            {
                npc.WhatToSee |= LayerMask.GetMask("Player");
                npc.enemyLayers |= LayerMask.GetMask("Player");
            }
        }
        if (Commonwealth)
        {
            // Add additional layers to the mask
            npc.WhatToSee |= LayerMask.GetMask("object", "Repablic", "KaiserRiech", "Outlaw");
            npc.enemyLayers |= LayerMask.GetMask("Repablic", "KaiserRiech", "Outlaw");
            if (!AliedToPlayer)
            {
                npc.WhatToSee |= LayerMask.GetMask("Player");
                npc.enemyLayers |= LayerMask.GetMask("Player");
            }
        }
        if (Repablic)
        {
            // Add additional layers to the mask
            npc.WhatToSee |= LayerMask.GetMask("object", "Outlaw", "KaiserRiech", "Commonwealth");
            npc.enemyLayers |= LayerMask.GetMask("Outlaw", "KaiserRiech", "Commonwealth");
            if (!AliedToPlayer)
            {
                npc.WhatToSee |= LayerMask.GetMask("Player");
                npc.enemyLayers |= LayerMask.GetMask("Player");
            }
        }
    }
}