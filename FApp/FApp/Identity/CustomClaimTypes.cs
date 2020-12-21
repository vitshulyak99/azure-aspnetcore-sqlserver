using System.Security.Claims;

namespace FApp
{
    public static class CustomClaimTypes
    {
        public static class UserBlockStatus
        {
            public const string Type = "BlockStatus";

            public const string DefaultValue = "Blocked";

            public static Claim CreateClaim() => new Claim(Type,DefaultValue);
        }
    }
}