
using BCrypt;

namespace Common.BCrypt
{
    public static class BCrypt
    {
        private const int Rounds = 13;

        public static string GeneratePasswordHash(string password, int overrideRounds = Rounds)
        {
            var tmpSalt = BCryptHelper.GenerateSalt(overrideRounds);
            var hash = BCryptHelper.HashPassword(password + ServerSideKey.Value, tmpSalt);
            return hash;
        }

        public static bool Validate(string password, string hash)
        {
            return BCryptHelper.CheckPassword(password + ServerSideKey.Value, hash);
        }
    }
}