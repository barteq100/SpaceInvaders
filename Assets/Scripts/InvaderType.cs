namespace DefaultNamespace
{
    public enum InvaderType
    {
        Small,
        Big
    }

    public static class InvaderTypeHelper
    {
        public static int GetInvaderScore(this InvaderType type)
        {
            switch (type)
            {
                case InvaderType.Small:
                    return 100;
                case InvaderType.Big:
                    return 200;
                default:
                    return 0;
            }
        }
    }
}