using System.Collections;
using System.Collections.Generic;
using UndeadWarfare.Player;
using UnityEngine;

namespace UndeadWarfare.Interact
{
	public abstract class InteractableObject : MonoBehaviour
	{
		// Determine if this object can currently be interacted with 
		public bool interactable;

		// Reference to the player object interacting with this item
		protected GameObject player;

		// Start is called before the first frame update
		public virtual void Start()
		{
			// Initialize player reference using the singleton instance of the gamemanager 
			player = gamemanager.instance.player;
		}
		// Called with the interaction key is pressed 
		public virtual void KeyPressed()
		{
			// Set player's occupied state to true, preventing further interactions
			player.GetComponent<PlayerController>().IsOccupied = true;
		}
		// Called when the interaction key is released 
		public virtual void KeyReleased()
		{
			// Set player's occupied state to false, allowing other interactions
			player.GetComponent<PlayerController>().IsOccupied = false;
		}
		// Displays a prompt to the player with a specific interaction message
		public virtual void Prompt(string prompt)
		{
			// Set the prompt trxt in the game's UI
			gamemanager.instance.PromptText.SetText("[E] " +  prompt);
		}
		// Overload of the Prompt method for optional behavior
		public virtual void Prompt()
		{
			// Can be overriden in derived classes
		}
	}
}