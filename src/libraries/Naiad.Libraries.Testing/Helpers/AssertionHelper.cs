namespace Naiad.Libraries.Testing.Helpers
{
    public static class AssertionHelper
    {
        public static void ApproximatelyEqual(
            DateTimeOffset expected,
            DateTimeOffset actual)
        {
            TimeSpan diff = expected - actual;

            if (diff.Milliseconds > 1)
            {
                throw new Exception("Values are more than 1 millisecond");
            }
        }
    }
}
