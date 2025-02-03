using Ynov.QuizApp.Controllers;
using Timer = System.Timers.Timer;
using System.Timers;


public class TimerService {
    private readonly Timer _timer;
    private readonly IServiceProvider _serviceProvider;

    public TimerService(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
        _timer = new Timer(30000); //30 seconds
        _timer.Elapsed += OnTimedEvent;
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e) {
        using (var scope = _serviceProvider.CreateScope()) {
            var userQuizProgressService = scope.ServiceProvider.GetRequiredService<IUserQuizProgressService>();
            userQuizProgressService.CloseUnfinishedQuizzes();
        }
    }
}