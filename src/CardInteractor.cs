using Godot;
using System;
using System.Collections;

public partial class CardInteractor : Node2D
{

	Player player;
	Gameboard gameboard;
	Sprite2D stackPosition;
	Card selectedCard = null;

	public override void _Ready()
	{
		player = GetParent<Player>();
		if (player == null)
		{
			GD.PrintErr("CardInteractor could not find parent Player node.");
			QueueFree();
		}

		gameboard = player.GetParent<Gameboard>();
		if (gameboard == null)
		{
			GD.PrintErr("CardInteractor could not find Gameboard node.");
			QueueFree();
		}

		stackPosition = gameboard.GetNode<Sprite2D>("CardStackPosition");
		if(stackPosition == null)
		{
			GD.PrintErr("CardInteractor could not find CardStackPosition node.");
			QueueFree();
		}

	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("SelectCard"))
		{
			selectedCard = GetTopSelectableCard();
		}

		if (Input.IsActionJustReleased("SelectCard"))
		{
			if (selectedCard != null)
			{
				PlaceCardOnGameboard(selectedCard);
				selectedCard = null;
			}
		}


		if (selectedCard != null)
		{
			selectedCard.GlobalPosition = GetGlobalMousePosition();
		}
	}
	
	private void PlaceCardOnGameboard(Card card)
	{
		Vector2 placementDimensions = GetViewport().GetVisibleRect().Size/4;
		Rect2 placeArea = new Rect2(gameboard.Position + GetViewport().GetVisibleRect().Size / 2 - .5f * placementDimensions,
		 placementDimensions);

		if (placeArea.HasPoint(GetGlobalMousePosition()))
		{
			card.ResetPosition();

			if (!gameboard.AddCardToStack(card))
			{
				return;
			}
			//so they are on different layers and appear on top of each other
			card.ZIndex = gameboard.GetCardCount();
			

			card.DeactivateCard();
			card.Scale = new Vector2(1, 1);

			//reparent the node to the stack location
			card.GetParent<Deck>().RemoveCardFromDeck(card);
			card.GetParent().RemoveChild(card);
			stackPosition.AddChild(card);

			card.CardStaticPosition = new Vector2(0, -gameboard.GetCardCount() * 5);


		}
		
		card.ResetPosition();
		
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
