using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AbilityPhase", menuName = "ScriptableObjects/Ability/Create Ability Phase", order = 3)]
public class AbilityPhase : ScriptableObject
{
    public List<Consequence> Consequences;
    public float Duration;
    private int _currentIndex = 0;
    
    public bool TryApplyEffects( AbilityArgs args, float deltaTime )
    {
        if ( args == null || args.Size == 0 )
        {
            return false;
        }

        Consequence effect = Consequences[ _currentIndex ];
        Duration = args.Get<float>("duration" );
        
        // TODO: remove this one, used for debugging only
        args.Set( "deltaTime", deltaTime );
        
        float targetTime = Duration * effect.NormalizedTime;

        if ( deltaTime >= targetTime )
        {
            effect.TriggerEffect( args );
            ++_currentIndex;
        }

        if ( _currentIndex >= Consequences.Count )
        {
            return true;
        }

        return false;
    }

    public void Clear()
    {
        _currentIndex = 0;
    }
}
