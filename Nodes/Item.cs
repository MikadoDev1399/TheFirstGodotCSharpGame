using Godot;
using System;

public class Item : Area2D
{
	private Sprite _sprite;
	private CollisionShape2D _shape;
	public override void _Ready()
	{
		_sprite = GetNode<Sprite>(new NodePath("sprite"));
		_shape = GetNode<CollisionShape2D>(new NodePath("shape"));

	}

	public void HideSelf()
	{
		_shape.SetDeferred("disabled", true);
		_sprite.SetDeferred("visible", false);
	}
	
	public void ShowSelf()
	{
		_shape.SetDeferred("disabled", false);
		_sprite.SetDeferred("visible", true);
		
	}
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

private void _on_Item_body_entered(object body)
{
	if (!(body is Driver driver)) return;

	driver.ReceivedItem(this);
	
}

}


