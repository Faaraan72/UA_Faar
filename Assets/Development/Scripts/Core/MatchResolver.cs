using UAFaar.Cards;

namespace UAFaar.Gameplay
{
    public static class MatchResolver
    {
        public static bool IsMatch(CardView a, CardView b)
        {
            return a.Data.Id == b.Data.Id;
        }
    }
}
