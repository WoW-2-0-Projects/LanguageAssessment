using Refit;

namespace Temporary;

public interface ITextToSpeechService
{
    Task<byte[]> TextToSpeechAsync(string text, string voice, string model = "tts-1", CancellationToken cancellationToken = default);
}

public class OpenAITTSService(IOpenAITTSApi openAittsApi) : ITextToSpeechService
{
    public async Task<byte[]> TextToSpeechAsync(string text, string voice, string model = "tts-1", CancellationToken cancellationToken = default)
    {
        var request = new TTSRequest
        {
            Model = model,
            Input = text,
            Voice = voice
        };

        var response = await openAittsApi.TextToSpeechAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"OpenAI TTS API request failed with status code: {response.StatusCode}");
        }

        using var memoryStream = new MemoryStream();
        await response.Content.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}

public interface IOpenAITTSApi
{
    [Post("/v1/audio/speech")]
    Task<ApiResponse<Stream>> TextToSpeechAsync([Body] TTSRequest request, CancellationToken cancellationToken = default);
}

public class TTSRequest
{
    public string Model { get; set; }
    public string Input { get; set; }
    public string Voice { get; set; }
}