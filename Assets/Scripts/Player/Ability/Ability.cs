using UnityEngine;
using System.Collections.Generic;

public class AbilityArgs
{
    private Dictionary<string, object> _params;

    public long Size
    {
        get
        {
            return _params != null ? _params.Count : 0;
        }
    }

    public void Set( string key, object value )
    {
        _params[ key ] = value;
    }

    public T Get<T>( string key )
    {
        object temp = null;

        if ( !_params.TryGetValue( key, out temp ))
        {
            return default(T);
        }

        T ret = ( T ) temp;

        return ret;
    }

    public AbilityArgs()
    {
        _params = new Dictionary<string, object>();
    }

    public void Reset()
    {
        _params.Clear();
    }
}


[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability/Create Ability", order = 1)]
public class Ability : ScriptableObject
{
    public int Id;
    public float Cooldown;
    public KeyCode KeyCode;
    public AbilityBehaviour AbilityBehaviour;

    public bool IsDone
    {
        get
        {
            return AbilityBehaviour != null && AbilityBehaviour.IsDone;
        }
    }

    public void DoUpdate()
    {
        if ( AbilityBehaviour == null )
        {
#if VERBOSE
            Debug.LogWarningFormat( "Ability {0} cannot be activated, ability behaviour is not set.", Id );
#endif
            return;
        }

        AbilityBehaviour.DoUpdate();
    }

    public void Clear()
    {
        if ( AbilityBehaviour == null )
        {
#if VERBOSE
            Debug.LogWarningFormat( "Ability {0} cannot be activated, ability behaviour is not set.", Id );
#endif
            return;
        }

        AbilityBehaviour.Clear();
    }

    public void SetArgs( AbilityArgs args )
    {
        if ( AbilityBehaviour == null )
        {
#if VERBOSE
            Debug.LogWarningFormat( "Ability {0} cannot be activated, ability behaviour is not set.", Id );
#endif
            return;
        }

        if ( args == null )
        {
            args = new AbilityArgs();
        }

        args.Set( "id", Id );
        args.Set( "duration", Cooldown );

        AbilityBehaviour.SetArgs( args );
    }
}

