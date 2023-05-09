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
		public int CoolnessThreshhold;

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
					CoolnessThreshhold = 5;
					break;
				case SpecialEnemies.Smoothness:
					Name = enemy;
					Type = EnemyType.Special;
					DanceMoves = new DanceEvent.Pose[]
					{
						Pose.Splits
					};
					MaxStamina = 3;
					CurrentStamina = MaxStamina;
					CoolnessThreshhold = 10;
					break;
				default:
					break;
			}
		}
	}	
}
