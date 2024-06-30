using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    public NPC npc;
    public bool Outlaw;
    public bool Republic;
    public bool KaiserReich;
    public bool Commonwealth;
    public bool AliedToPlayer;


    void Start()
    {
        gameObject.layer = 0;
        npc.WhatToSee = 0;
        npc.enemyLayers = 0;
        if (Outlaw)
        {
            gameObject.layer = LayerMask.NameToLayer("Outlaw");
            // Add additional layers to the mask
            npc.WhatToSee |= LayerMask.GetMask("object", "Republic", "KaiserReich", "Commonwealth");
            npc.enemyLayers |= LayerMask.GetMask("Republic", "KaiserReich", "Commonwealth");
            if (!AliedToPlayer)
            {
                npc.WhatToSee |= LayerMask.GetMask("Player");
                npc.enemyLayers |= LayerMask.GetMask("Player");
            }
        }
        if (KaiserReich)
        {
            gameObject.layer = LayerMask.NameToLayer("KaiserReich");
            // Add additional layers to the mask
            npc.WhatToSee |= LayerMask.GetMask("object", "Republic", "Outlaw", "Commonwealth");
            npc.enemyLayers |= LayerMask.GetMask("Republic", "Outlaw", "Commonwealth");
            if (!AliedToPlayer)
            {
                npc.WhatToSee |= LayerMask.GetMask("Player");
                npc.enemyLayers |= LayerMask.GetMask("Player");
            }
        }
        if (Commonwealth)
        {
            gameObject.layer = LayerMask.NameToLayer("Commonwealth");
            // Add additional layers to the mask
            npc.WhatToSee |= LayerMask.GetMask("object", "Republic", "KaiserReichh", "Outlaw");
            npc.enemyLayers |= LayerMask.GetMask("Republic", "KaiserReich", "Outlaw");
            if (!AliedToPlayer)
            {
                npc.WhatToSee |= LayerMask.GetMask("Player");
                npc.enemyLayers |= LayerMask.GetMask("Player");
            }
        }
        if (Republic)
        {
            gameObject.layer = LayerMask.NameToLayer("Republic");
            // Add additional layers to the mask
            npc.WhatToSee |= LayerMask.GetMask("object", "Outlaw", "KaiserReich", "Commonwealth");
            npc.enemyLayers |= LayerMask.GetMask("Outlaw", "KaiserReich", "Commonwealth");
            if (!AliedToPlayer)
            {
                npc.WhatToSee |= LayerMask.GetMask("Player");
                npc.enemyLayers |= LayerMask.GetMask("Player");
            }
        }
    }
}