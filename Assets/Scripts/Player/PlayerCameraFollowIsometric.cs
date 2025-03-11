using UnityEngine;

public class PlayerCameraFollowIsometric : MonoBehaviour
{
	[SerializeField]
	private Transform _target;
	
	private Vector3 _defaultPosition;
	private Vector3 _offset;
	
	#region [ Unity Overrideable Methods ]
	
	private void Awake()
	{
		if ( _target == null )
		{
			Debug.LogWarningFormat( "PlayerCameraFollowIsometric: Target player is not set, camera won't follow." );
			return;
		}
		
		Vector3 currentPos = transform.position;
		_offset = currentPos - _target.position;
		_defaultPosition = currentPos;
	}
	
	private void Update()
	{
		if ( _target == null )
		{
			return;
		}
		
		transform.position = _target.position + _offset;
	}
	
	#endregion // Unity Overrideable Methods
	
	#region [ Debug ]
	
	[ContextMenu( "Reset Position" )]
	public void ResetPosition()
	{
		transform.position = _defaultPosition;
	}
	
	#endregion // Debug
}
