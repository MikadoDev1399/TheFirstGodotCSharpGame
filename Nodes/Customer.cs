using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstGame
{
    public class Customer : Area2D
    {
        private static List<Customer> Customers = new List<Customer>();

        public static event Action<bool> OnReceivedItem;

        [Export()] private NodePath _itemPath;
        private Item _itemOrdered;

        private bool _hasReceivedItem = false;

        private Sprite _sprite;

        public override void _EnterTree()
        {
            base._EnterTree();
            Customers.Add(this);
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            Customers.Remove(this);
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _sprite = GetNode<Sprite>(new NodePath("Sprite"));
            _itemOrdered = GetNode<Item>(_itemPath);

        }

        public static Customer FindCustomerByItem(Item item)
        {
            return Customers.FirstOrDefault(x => x._itemOrdered == item);
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

            if (driver.CurrentHeldItem == _itemOrdered)
            {
                _hasReceivedItem = true;
                GD.Print("Got item!");
                _sprite.SelfModulate = Colors.Green;
                OnReceivedItem?.Invoke(true);
            }
            else
            {
                _hasReceivedItem = false;
                GD.Print("No item");
                OnReceivedItem?.Invoke(false);
            }
        }


    }
}
