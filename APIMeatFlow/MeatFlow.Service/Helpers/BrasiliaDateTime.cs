namespace MeatFlow.Service.Helpers;

/// <summary>
/// Fornece a data e hora atual no fuso horário de Brasília.
/// </summary>
internal static class BrasiliaDateTime
{
    // O identificador do fuso horário varia entre Windows e Linux.
    private static readonly TimeZoneInfo _tz = TimeZoneInfo.FindSystemTimeZoneById(
        OperatingSystem.IsWindows()
            ? "E. South America Standard Time"
            : "America/Sao_Paulo");

    /// <summary>Retorna a data e hora atual no horário de Brasília.</summary>
    public static DateTime Agora => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _tz);
}
