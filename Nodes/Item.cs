using Godot;
using System;

public class Item : Area2D
{
	private Sprite _sprite;
	private CollisionShape2D _shape;

	private static Item _current;
	
	public override void _Ready()
	{
		_sprite = GetNode<Sprite>(new NodePath("sprite"));
		_shape = GetNode<CollisionShape2D>(new NodePath("shape"));

	}

	private void HideSelf()
	{
		_shape.Disabled = true;
		_sprite.Visible = false;
	}
	
	private void ShowSelf()
	{
		_shape.Disabled = false;
		_sprite.Visible = true;
		
	}
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

private void _on_Item_body_entered(object body)
{
	if (!(body is Driver driver)) return;

	if (_current == null)
	{
		driver.ReceivedItem(this);
		HideSelf();
		_current = this;
	}
	else
	{
		
		if (_current != this)
		{
			driver.ReceivedItem(this);
			_current.ShowSelf();
			_current.GlobalPosition = driver.GlobalPosition;
			HideSelf();
			_current = this;
		}
		else
		{
			GD.Print("Unknown error: Current item pointer shouldn't be called here! ");
		}
		
	}
	

	
	


}

}


