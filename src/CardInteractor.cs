using Godot;
using System;
using System.Collections;

public partial class CardInteractor : Node2D
{
	Player player;
	Card selectedCard = null;

	public override void _Ready()
	{
		player = GetParent<Player>();
		if (player == null)
		{
			GD.PrintErr("PlayerCardDragger could not find parent Player node.");
			QueueFree();
		}

	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("SelectCard"))
		{
			selectedCard = GetTopSelectableCard();
		}

		if(Input.IsActionJustReleased("SelectCard"))
		{
			if (selectedCard != null)
			{
				selectedCard.ResetPosition();
				selectedCard = null;
			}
		}


		if (selectedCard != null)
		{
			selectedCard.GlobalPosition = GetGlobalMousePosition();
		}
	}
	
	private Card GetTopSelectableCard()
	{
		Card topCard = null;
		float highestSelectability = 0.1f;

		foreach (Node child in player.GetNode<Deck>("Deck").GetChildren())
		{
			if (child is Card card)
			{
				if (card.Selectability > highestSelectability)
				{
					highestSelectability = card.Selectability;
					topCard = card;
				}
			}
		}

		return topCard;
	}
}
