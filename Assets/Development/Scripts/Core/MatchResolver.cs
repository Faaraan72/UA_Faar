using UAFaar.Cards;

namespace UAFaar.Core
{
    public static class MatchResolver
    {
        public static bool IsMatch(CardView a, CardView b)
        {
            return a.Data.Id == b.Data.Id;
        }
    }
}
