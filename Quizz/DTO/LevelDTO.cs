namespace Quizz.DTO
{
    public class LevelDTO
    {
        public int Id { get; set; }
        public int LevelNumber { get; set; }
        public int PointsToUnlock { get; set; }
        public int Score { get; set; }
        public bool IsBlock { get; set; }
    }
}
