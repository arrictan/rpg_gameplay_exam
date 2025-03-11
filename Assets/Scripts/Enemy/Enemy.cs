using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Range( 1, 100 )]
    private float _health;

    public void SetDamage( float damage = 0f )
    {
        Debug.Log("Do damage! " + damage );
        
        _health -= damage;

        if ( _health > 0 )
        {
            return;
        }

        _health = 0;
        Destroy( gameObject );
    }
}
