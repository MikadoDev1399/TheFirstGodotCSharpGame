using Godot;


public class Driver : KinematicBody2D
{
	[Export()] private float _movespeed = 600f;
	[Export()] private float _turnspeed = 2f;
	
	private float _horizontalInput = 0f;
	private float _verticalInput = 0f;

	private Item _heldItem;

	private Vector2 _motion = Vector2.Zero;

	

	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		
		_horizontalInput = Input.GetAxis("MoveLeft", "MoveRight");
		_verticalInput = Input.GetAxis("MoveUp", "MoveDown");
		Rotate(_horizontalInput * delta * _turnspeed);
		_motion =  new Vector2(0,_verticalInput).Rotated(Rotation) * delta * _movespeed;
		var col = MoveAndCollide(_motion);
		
		
	}

	public void ReceivedItem(Item item)
	{
		//I'm trying to find way to let the driver hold only one item by hiding the current one before setting the other
		//Perhaps a better approach to throw current away and then hide it, and get the new one...
		if (_heldItem != null)
		{
			_heldItem.HideSelf();
			
		}
		_heldItem = item;
		


	}

	public bool GiveItem(Item orderedItem) => _heldItem == orderedItem;
}

