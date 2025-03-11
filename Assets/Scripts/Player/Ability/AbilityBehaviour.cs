using System;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "AbilityBehaviour", menuName = "ScriptableObjects/Ability/Create Ability Behaviour", order = 2)]
public class AbilityBehaviour : ScriptableObject
{
    public AbilityPhase[] AbilityPhases;

    private bool _isDone = false;
    public bool IsDone
    {
        get
        {
            return _isDone;
        }
    }
    
    private float _currentTime;
    private int _currentIndex = 0;
    private AbilityArgs _args;

    public void SetArgs( AbilityArgs args )
    {
        Clear();
        _args = args;
    }

    public void DoUpdate()
    {
        if ( IsDone )
        {
            return;
        }
        
        if ( _currentIndex < AbilityPhases.Length )
        {
            AbilityPhase currentAbility = AbilityPhases[_currentIndex];

            if ( currentAbility.TryApplyEffects( _args, _currentTime ))
            {
                ++_currentIndex;
            }
        }
        
        _currentTime += Time.fixedDeltaTime;

        if ( _currentIndex == AbilityPhases.Length )
        {
            _isDone = true;
        }
    }

    public void Clear()
    {
        _currentIndex = 0;
        _isDone = false;
        _currentTime = 0;

        if ( AbilityPhases == null || AbilityPhases.Length == 0 )
        {
            return;
        }
        
        for ( int i = 0; i < AbilityPhases.Length; ++i )
        {
            AbilityPhase currentAbility = AbilityPhases[i];
            currentAbility.Clear();
        }
    }
}
