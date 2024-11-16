using System.Collections;
using System.Collections.Generic;
using UndeadWarfare.AI.State;
using UnityEngine;

namespace UndeadWarfare.AI
{
	public class EnemyManager : MonoBehaviour
	{
        #region QUEUE_PARAMETERS
        [Header(" ---- Queue Parameters ---- ")]
		// Queue of enemies waiting to engage with the player
		[SerializeField] private List<BaseUndead> enemyQueue = new List<BaseUndead>();

		// Maximnum number of enemies that can attack the player at any one time
		[SerializeField] private int MaxActiveEnemies = 2;

		private int activeEnemyCount = 0;		// Current count of enemies actively engaging the player
        #endregion

        #region	QUEUE_CONTROL
		// Adds enemy to the attack queue if it's not already in the queue
        public void AddToQueue(BaseUndead enemy)
		{
			if (!enemyQueue.Contains(enemy))
			{
				enemyQueue.Add(enemy);
				// Set the enemies state to indicate it is now waiting in the attack
				enemy.UpdateState(new QueueState(enemy));
			}
        }

		// Removes an enemy from the queue and sets its' state to engage
		public void RemoveFromQueue(BaseUndead enemy)
		{
			if (enemyQueue.Contains(enemy))
			{
				enemyQueue.Remove(enemy);
				// Set the enemies state to Idle when removed from the queue
				enemy.UpdateState(new IdleState(enemy));
			}
		}

		// Manages the queue by ativating queued enemies if there are available slots for attacking
        public void UpdateQueue()
        {
            for (int i = 0;  i < enemyQueue.Count; i++)
			{
                // Check to ensure that the maximum active enemies has not been reached, and if the enemy is in a queue state
                if (activeEnemyCount < MaxActiveEnemies && enemyQueue[i].CurrentState.Name == EnemyState.Queue)
				{
					// Transition the enemy to the engaging state to approach the player.
					enemyQueue[i].UpdateState(new EngageState(enemyQueue[i]));
					activeEnemyCount++;		// Increment to the count of active attackers
				}
			}
        }
        #endregion

        #region HANDLERS
		// Handles the event when an enemy finishes it's attack
        public void OnCompletedEnemyAttack(BaseUndead enemy)
		{
			activeEnemyCount--;     // Decrease the count of active enemy
			// Transition the enemy to the retreating state to move it to the retreat point
            enemy.UpdateState(new RetreatingState(enemy));		
			UpdateQueue();		// Update the queue to activate another enemy, if there are any waiting
		}

		// Handles completion of the retreating behavior, and readds the enemy to the attack queue
		public void OnCompletedEnemyRetreat(BaseUndead enemy)
		{
			AddToQueue(enemy);			// Add the enemy back to the queue
		}
		#endregion
	}
}
