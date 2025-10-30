namespace task1135.Extensions
{
    public static class AuthorizationExtensions
    {
        public static void AddAuthorizationWithPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("OlderThan18", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        if (!context.User.HasClaim(c => c.Type == "DateOfBirth")) return false;

                        var dobClaim = context.User.FindFirst("DateOfBirth").Value;
                        if (!DateTime.TryParse(dobClaim, out var dob))
                            return false;

                        return (DateTime.UtcNow - dob).TotalDays / 365 >= 18;
                    });
                });
            });
        }
    }
}
