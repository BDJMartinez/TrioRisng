using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadWarfare.AI
{
	public class EnemyManager : MonoBehaviour
	{
        #region QUEUE_PARAMETERS
        [Header(" ---- Queue Parameters ---- ")]
		// Queue of enemies waiting to engage with the player
		[SerializeField] private List<BaseAI> enemyQueue = new List<BaseAI>();

		// Maximnum number of enemies that can attack the player at any one time
		[SerializeField] private int MaxActiveEnemies = 2;

		// Current count of enemies actively engaging the player
		private int activeEnemyCount = 0;
		#endregion

		public void AddToQueue(BaseUndead enemy)
		{
			if (!enemyQueue.Contains(enemy))
			{
				enemyQueue.Add(enemy);
				//enemy.UpdateState();
			}
		}
    }
}
