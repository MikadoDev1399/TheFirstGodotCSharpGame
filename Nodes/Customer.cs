using Godot;
using System;

public class Customer : Area2D
{
	[Export()] private NodePath _itemPath;
	private Item _itemOrdered;
	
	private bool _hasReceivedItem = false;

	private Sprite _sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite = GetNode<Sprite>(new NodePath("Sprite"));
		_itemOrdered = GetNode<Item>(_itemPath);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	
	private void _on_Customer_body_entered(object body)
	{
		if (_hasReceivedItem) return;

		if (!(body is Driver driver)) return;

		if (driver.GiveItem(_itemOrdered))
		{
			_hasReceivedItem = true;
			GD.Print("Got item!");
			_sprite.SelfModulate = Colors.Green;
		}
		else
		{
			_hasReceivedItem = false;
			GD.Print("No item");
		}
	}
}
