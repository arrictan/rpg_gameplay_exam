//#define VERBOSE
using UnityEngine;

public enum ConsequenceBehaviourType
{
    Unknown,
    OverlapBox,
    OverlapSphere,
    DealDamage,
    Max
}

[CreateAssetMenu(fileName = "Consequence", menuName = "ScriptableObjects/Ability/Create Consequence", order = 4)]
public class Consequence : ScriptableObject
{
    public float Stat;
    public float NormalizedTime;
    public ConsequenceBehaviourType BehaviourType = ConsequenceBehaviourType.Unknown;


    public void TriggerEffect( AbilityArgs args )
    {
        Vector3 pos = args.Get<Vector3>( "pos" );
        Vector3 scale = args.Get<Vector3>( "scale" );
        LayerMask layerMask = args.Get<LayerMask>( "mask" );
        float radius = args.Get<float>( "radius" );
        
        Collider[] colliders;
        
        switch ( BehaviourType )
        {
            case ConsequenceBehaviourType.OverlapBox:
#if VERBOSE
                Debug.LogFormat( "Ability {0} is done applying effect with {1} on {2}", args.Get<int>( "id"), "OverlapBox", args.Get<float>("deltaTime") );
#endif
                colliders = Physics.OverlapBox( pos, scale, Quaternion.identity, layerMask );
                args.Set( "targets", colliders );
                break;
            
            case ConsequenceBehaviourType.OverlapSphere:
#if VERBOSE
                Debug.LogFormat( "Ability {0} is done applying effect with {1} on {2}", args.Get<int>( "id"), "OverlapSphere", args.Get<float>("deltaTime") );
#endif
                colliders = Physics.OverlapSphere( pos, radius, layerMask );
                args.Set( "targets", colliders );
                break;
            
            case ConsequenceBehaviourType.DealDamage:
                colliders = args.Get<Collider[]>( "targets" );
#if VERBOSE
                Debug.LogFormat( "Ability {0} is done applying effect with {1} and targets are {2} on {3}", args.Get<int>( "id"), "Damage", 
                    ( colliders != null ? colliders.Length : 0 ), args.Get<float>("deltaTime") );
#endif

                break;
        }
        
        // then deal damage?
    }
}