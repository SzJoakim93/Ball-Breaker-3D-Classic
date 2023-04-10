public class Settings
{
    public enum InputType
    {
        Swap = 0,
        Slider = 1
    }

    public static bool MusicEnabled = true;
    public static float MusicVolume = 0.5f;
    public static bool SoundEnabled = true;
    public static float SoundVolume = 0.5f;
    public static string Language = "HUN";
    public static InputType Input = InputType.Swap;
    public static string [] languageIds = new string[]
    {
        "END", "GER", "HUN"
    };

}