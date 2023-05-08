using DanceEvent;

namespace BattleEvent
{
	public enum EnemyType
	{
		Special,
		Environmental
	}

	public enum SpecialEnemies
	{
		FunkMaster,
		Smoothness	
	}

	public class Enemy
	{
		public SpecialEnemies Name;
		public EnemyType Type;
		public DanceEvent.Pose[] DanceMoves;
		public int MaxStamina;
		public int CurrentStamina;

		public Enemy(SpecialEnemies enemy)
		{
			switch (enemy)
			{
				case SpecialEnemies.FunkMaster:
					Name = enemy;
					Type = EnemyType.Special;
					DanceMoves = new DanceEvent.Pose[]
					{
						Pose.Splits	
					};
					MaxStamina = 1;
					CurrentStamina = MaxStamina;
					break;
				case SpecialEnemies.Smoothness:
					Name = enemy;
					Type = EnemyType.Special;
					DanceMoves = new DanceEvent.Pose[]
					{
						Pose.Splits
					};
					MaxStamina = 5;
					CurrentStamina = MaxStamina;
					break;
				default:
					break;
			}
		}
	}	
}
