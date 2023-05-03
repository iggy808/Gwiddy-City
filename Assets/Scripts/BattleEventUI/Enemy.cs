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
		LameDancer,
		SickDancer
	}

	public class Enemy
	{
		public SpecialEnemies Name;
		public EnemyType Type;
		public DanceEvent.Pose[] DanceMoves;
		public int MaxStamina;

		public Enemy(SpecialEnemies enemy)
		{
			switch (enemy)
			{
				case SpecialEnemies.LameDancer:
					Name = enemy;
					Type = EnemyType.Special;
					DanceMoves = new DanceEvent.Pose[]
					{
						Pose.Splits	
					};
					MaxStamina = 1;
					break;
				case SpecialEnemies.SickDancer:
					Name = enemy;
					Type = EnemyType.Special;
					DanceMoves = new DanceEvent.Pose[]
					{
						Pose.Splits
					};
					MaxStamina = 5;
					break;
				default:
					break;
			}
		}
	}	
}
