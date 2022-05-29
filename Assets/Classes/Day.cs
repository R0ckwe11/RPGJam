namespace Assets.Classes
{
    public class Day
    {
        public int dayID;
        public float intel;
        public float squad;
        public float numeric1;
        public float numeric2;
        public float numeric3;
        public float tolerance;
        public string intelInfo;
        public string squadFile;
        public string numericInfo1;
        public string numericInfo2;
        public string numericInfo3;
        public bool intelGathered = false;
        public bool squadFound = false;
        public float timeRemaining;
        public bool solved = false;
        public string[] requiredAnswers;
    }
}
