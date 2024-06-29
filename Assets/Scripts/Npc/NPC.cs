using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool Hostile, Neutral, Allied;
    public float opinion;
    public LayerMask WhatToSee;
    public LayerMask enemyLayers;

    public OpinionState currentOpinionState = OpinionState.Neutral;

    #region Opinion States <---!!!!!
    public enum OpinionState
    {
        Allied,
        Neutral,
        Hostile
    }
    public virtual void ChangeOpinionState()
    {
        switch (currentOpinionState)
        {
            case OpinionState.Allied:
                HandleAlliedState();
                break;
            case OpinionState.Neutral:
                HandleNeutralState();
                break;
            case OpinionState.Hostile:
                HandleHostileState();
                break;
            default:
                // Handle default behavior or error state
                break;
        }
    }

    void HandleAlliedState()
    {
        Allied = true;
        Neutral = false; 
        Hostile = false;
        // Logic for allied behavior
        // Example: Follow player, assist in battles, give quests/rewards
        // Check for transitions to other states based on player actions
        if (PlayerAttacks())
        {
            currentOpinionState = OpinionState.Neutral;
        }
    }

    void HandleNeutralState()
    {
        Allied = false;
        Neutral = true;
        Hostile = false;
        // Logic for neutral behavior
        // Example: Neutral dialogue options, potential quests
        // Check for transitions to other states based on player actions
        if (PlayerHelps())
        {
            currentOpinionState = OpinionState.Allied;
        }
        else if (PlayerAttacks())
        {
            currentOpinionState = OpinionState.Hostile;
        }
    }
    void HandleHostileState()
    {
        Allied = false;
        Neutral = false;
        Hostile = true;
        // Logic for hostile behavior
        // Example: Attack the player on sight
        // Check for transitions to other states based on player actions
        if (PlayerHelps())
        {
            currentOpinionState = OpinionState.Neutral; // Hostility may decrease over time
        }
    }

    #endregion

    bool PlayerAttacks()
    {
        // Example logic: Check if the player attacks this NPC
        // Replace with actual implementation based on your game mechanics
        return false;
    }

    bool PlayerHelps()
    {
        // Example logic: Check if the player helps this NPC
        // Replace with actual implementation based on your game mechanics
        return false;
    }

    bool PlayerDoesNothing()
    {
        // Example logic: Check if the player does nothing hostile
        // Replace with actual implementation based on your game mechanics
        return false;
    }
}