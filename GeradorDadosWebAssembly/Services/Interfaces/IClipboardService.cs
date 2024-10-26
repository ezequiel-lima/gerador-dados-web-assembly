namespace GeradorDadosWebAssembly.Services.Interfaces
{
    public interface IClipboardService
    {
        ValueTask<string> ReadTextAsync();
        ValueTask WriteTextAsync(string text);
    }
}
