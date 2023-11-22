// See https://aka.ms/new-console-template for more information

// Command Interface to be used in execution
public interface ICommand
{
    void Execute();
}

// actual Commands to be implemented
public class LightOn : ICommand
{
    private Light light;

    public LightOn(Light light)
    {
        this.light = light;
    }

    public void Execute()
    {
        light.TurnOn();
    }
}

public class LightOff : ICommand
{
    private Light light;

    public LightOff(Light light)
    {
        this.light = light;
    }

    public void Execute()
    {
        light.TurnOff();
    }
}

public class ThermostatIncrease : ICommand
{
    private Thermostat thermostat;

    public ThermostatIncrease(Thermostat thermostat)
    {
        this.thermostat = thermostat;
    }

    public void Execute()
    {
        thermostat.IncreaseTemperature();
    }
}

public class ThermostatDecrease : ICommand
{
    private Thermostat thermostat;

    public ThermostatDecrease(Thermostat thermostat)
    {
        this.thermostat = thermostat;
    }

    public void Execute()
    {
        thermostat.DecreaseTemperature();
    }
}

public class MusicPlayerOn : ICommand
{
    private MusicPlayer musicPlayer;

    public MusicPlayerOn(MusicPlayer musicPlayer)
    {
        this.musicPlayer = musicPlayer;
    }

    public void Execute()
    {
        musicPlayer.TurnOn();
    }
}

public class MusicPlayerOff : ICommand
{
    private MusicPlayer musicPlayer;

    public MusicPlayerOff(MusicPlayer musicPlayer)
    {
        this.musicPlayer = musicPlayer;
    }

    public void Execute()
    {
        musicPlayer.TurnOff();
    }
}

public class IncreaseVolume : ICommand
{
    private MusicPlayer musicPlayer;

    public IncreaseVolume(MusicPlayer musicPlayer)
    {
        this.musicPlayer = musicPlayer;
    }

    public void Execute()
    {
        musicPlayer.IncreaseVolume();
    }
}

public class DecreaseVolume : ICommand
{
    private MusicPlayer musicPlayer;

    public DecreaseVolume(MusicPlayer musicPlayer)
    {
        this.musicPlayer = musicPlayer;
    }

    public void Execute()
    {
        musicPlayer.DecreaseVolume();
    }
}

public class NextSong : ICommand
{
    private MusicPlayer musicPlayer;

    public NextSong(MusicPlayer musicPlayer)
    {
        this.musicPlayer = musicPlayer;
    }

    public void Execute()
    {
        musicPlayer.NextSong();
    }
}

public class PreviousSong : ICommand
{
    private MusicPlayer musicPlayer;

    public PreviousSong(MusicPlayer musicPlayer)
    {
        this.musicPlayer = musicPlayer;
    }

    public void Execute()
    {
        musicPlayer.PreviousSong();
    }
}

// IOT devices to be controlled
public class Light
{
    public bool IsOn { get; private set; }

    public void TurnOn()
    {
        IsOn = true;
        Console.WriteLine("light switched on");
    }

    public void TurnOff()
    {
        IsOn = false;
        Console.WriteLine("light switched off");
    }
}

public class Thermostat
{
    public int Temperature { get; private set; } = 20;

    public void IncreaseTemperature()
    {
        Temperature++;
    }

    public void DecreaseTemperature()
    {
        Temperature--;
    }
}

public class MusicPlayer
{
    public bool IsOn { get; private set; }
    public int Volume { get; private set; } = 30;

    public void TurnOn()
    {
        IsOn = true;
        Console.WriteLine("Music player now on");
    }

    public void TurnOff()
    {
        IsOn = false;
        Console.WriteLine("Music player turned off");
    }

    public void IncreaseVolume()
    {
        Volume += 2;
    }

    public void DecreaseVolume()
    {
        Volume -= 2;
    }

    public void NextSong()
    {
        Console.WriteLine("Playing Next Song");
    }

    public void PreviousSong()
    {
        Console.WriteLine("Playing Previous Song");
    }
}

// Invoker (RemoteController) to carry commands
public class RemoteController
{
    private Dictionary<int, ICommand> commands;

    public RemoteController()
    {
        commands = new Dictionary<int, ICommand>();
    }

    public void SetCommand(int key, ICommand command)
    {
        commands[key] = command;
    }

    public void PressButton(int key)
    {
        if (commands.TryGetValue(key, out ICommand ?command))
        {
            command.Execute();
        }
        else
        {
            Console.WriteLine("Invalid Key. Please try again.");
        }
    }
}

// Main Program
class Program
{
    static void Main()
    {
        // Create instances of IOT devices
        Light light = new Light();
        Thermostat thermostat = new Thermostat();
        MusicPlayer musicPlayer = new MusicPlayer();

        // Create instances of command objects
        ICommand lightOn = new LightOn(light);
        ICommand lightOff = new LightOff(light);
        ICommand thermostatIncrease = new ThermostatIncrease(thermostat);
        ICommand thermostatDecrease = new ThermostatDecrease(thermostat);
        ICommand musicPlayerOn = new MusicPlayerOn(musicPlayer);
        ICommand musicPlayerOff = new MusicPlayerOff(musicPlayer);
        ICommand musicPlayerIncreaseVolume = new IncreaseVolume(musicPlayer);
        ICommand musicPlayerDecreaseVolume = new DecreaseVolume(musicPlayer);
        ICommand musicPlayerNextSong = new NextSong(musicPlayer);
        ICommand musicPlayerPreviousSong = new PreviousSong(musicPlayer);

        // Create RemoteController to carry the command objects
        RemoteController remoteController = new RemoteController();

        // assign the commands as a key-value pair to the remote controller
        remoteController.SetCommand(1, lightOn);
        remoteController.SetCommand(2, lightOff);
        remoteController.SetCommand(3, thermostatIncrease);
        remoteController.SetCommand(4, thermostatDecrease);
        remoteController.SetCommand(5, musicPlayerOn);
        remoteController.SetCommand(6, musicPlayerOff);
        remoteController.SetCommand(7, musicPlayerIncreaseVolume);
        remoteController.SetCommand(8, musicPlayerDecreaseVolume);
        remoteController.SetCommand(9, musicPlayerNextSong);
        remoteController.SetCommand(0, musicPlayerPreviousSong);

        // Simulate user input
        Console.WriteLine("Press a key to perform an action (1-9, 0 to exit): \n" +
            "Press 1 to turn on the lights. \n" +
            "Press 2 to turn off the lights. \n" +
            "Press 3 to increase temperature. \n" +
            "Press 4 to reduce temperature. \n" +
            "Press 5 to turn on music player. \n" +
            "Press 6 to turn off music player. \n" +
            "Press 7 to increase music volume. \n" +
            "Press 8 to decrease music volume. \n" +
            "Press 9 to select next song. \n" +
            "Press 0 to replay previous song.");

        int userInput;
        do
        {
            if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out userInput))
            {
                remoteController.PressButton(userInput);
                // Display device status
                Console.WriteLine($"Light: {light.IsOn}, Temperature: {thermostat.Temperature}, MusicPlayer: {musicPlayer.IsOn}, Volume: {musicPlayer.Volume}");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number (1-9, 0 to exit).");
            }
        } while (userInput != 0);
    }
}
