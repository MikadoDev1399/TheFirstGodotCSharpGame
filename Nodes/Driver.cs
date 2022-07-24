using Godot;


public class Driver : KinematicBody2D
{
	[Export()] private float _movespeed = 600f;
	[Export()] private float _turnspeed = 2f;

	private float _motivation = 100f;
	
	private float _horizontalInput = 0f;
	private float _verticalInput = 0f;

	private Item _currentHeldItem;
	private Customer _currentTarget;

	private Vector2 _motion = Vector2.Zero;
	private Node2D _arrowHolder;

	public Item CurrentHeldItem => _currentHeldItem;

	public override void _EnterTree()
	{
		base._EnterTree();
		Customer.OnReceivedItem += HandleOnReceivedItem;
	}

	public override void _ExitTree()
	{
		base._ExitTree();
		Customer.OnReceivedItem -= HandleOnReceivedItem;
	}

	public override void _Ready()
	{
		_arrowHolder = GetNode<Node2D>(new NodePath("ArrowHolder"));
	}

	private void HandleOnReceivedItem(bool isCorrectItem)
	{
		if (isCorrectItem)
		{
			_currentHeldItem.Destroy();
			_currentTarget = null;
		}
		else
			EmotionDamage();

	}

	private void EmotionDamage()
	{
		GD.Print("Hurt my feelings!!!");
		_motivation = Mathf.Clamp(_motivation-20f, 0, 100);
		if (_motivation <= 0)
		{
			GD.Print("Stop working");
			
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);
		if (_currentHeldItem != null && _currentTarget != null)
		{
			 
			_arrowHolder.LookAt(_currentTarget.GlobalPosition);
			_arrowHolder.Rotation += Mathf.Pi/2;

		}
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
		_currentHeldItem = item;
		_currentTarget = Customer.FindCustomerByItem(item);
		
	}
}


