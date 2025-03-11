using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
    
    [SerializeField] 
    private Ability[] _abilities;
    
    private Ability _selectedAbility;
    private AbilityArgs _args;
    private LayerMask _enemyLayerMask; 

    protected void Awake()
    {
        _args = new AbilityArgs();
        _enemyLayerMask = LayerMask.GetMask("Enemy");
    }

    private void Update()
    {
        HandleInput();
        HandleAbility();
    }

    private void HandleInput()
    {
        if ( _selectedAbility != null )
        {
#if VERBOSE
            Debug.LogFormat( "PlayerAbilityController: cannot activate ability, ability {0} is active.", _selectedAbility.Id );
#endif
            return;
        }
        
        if ( _abilities == null || _abilities.Length == 0 )
        {
            return;
        }

        for ( int i = 0; i < _abilities.Length; ++i )
        {
            Ability ability = _abilities[i];

            if ( Input.GetKeyUp( ability.KeyCode ))
            {
                ActivateAbility( ability, _args );
            }
        }
    }

    private void ActivateAbility( Ability ability, AbilityArgs args )
    {
        if ( ability == null )
        {
            return;
        }
        
        args.Reset();
        args.Set( "pos", transform.position );
        args.Set( "scale", transform.localScale * 2 );
        args.Set( "radius", 2.0f );
        args.Set( "mask", _enemyLayerMask );
        ability.SetArgs( _args );
        _selectedAbility = ability;
    }

    private void HandleAbility()
    {
        if ( _selectedAbility == null )
        {
            return;
        }
        
        if ( _selectedAbility.IsDone )
        {
            _selectedAbility.Clear();
            _selectedAbility = null;
            return;
        }

        _selectedAbility.DoUpdate();
    }
    
    #if UNITY_EDITOR
    
    private void OnDrawGizmos()
    {
        Transform playerTransform = transform;
        Vector3 pos = playerTransform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos, playerTransform.localScale * 2);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere( pos, 2 );
    }
    
    #endif // UNITY_EDITOR
}
