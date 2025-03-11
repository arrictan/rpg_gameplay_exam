using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
	[SerializeField]
	private Rigidbody _rigidBody;
	
	[SerializeField]
	private float _speed = 5;
	
	[SerializeField]
	private float _turnSpeed = 360;
	
	private Vector3 _input;
	
	private void Update()
	{
		_input = new Vector3( Input.GetAxisRaw( "Horizontal" ), 0, Input.GetAxisRaw( "Vertical" ) );
		
		if ( _input == Vector3.zero )
		{
			_rigidBody.angularVelocity = Vector3.zero;
			_rigidBody.velocity = Vector3.zero;
			return;
		}
		
		Vector3 currentPos = transform.position;
		var matrix = Matrix4x4.Rotate( Quaternion.Euler( 0, 45, 0 ) );
		var skewedInput = matrix.MultiplyPoint3x4( _input );
		var relative = ( currentPos + skewedInput ) - currentPos;
		var rotation = Quaternion.LookRotation( relative, Vector3.up );
		transform.rotation = Quaternion.RotateTowards( transform.rotation, rotation, _turnSpeed * Time.deltaTime );
	}
	
	private void FixedUpdate()
	{
		Transform currentTransform = transform;
		_rigidBody.MovePosition( currentTransform.position + ( currentTransform.forward * _input.magnitude ) * _speed * Time.fixedDeltaTime );
	}
}
