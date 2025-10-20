using Hangfire.Annotations;
using Hangfire.Dashboard;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        // 🔓 Permitir acceso sin autenticación (solo para desarrollo)
        return true;
    }
}