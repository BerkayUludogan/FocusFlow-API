namespace FocusFlow.Api.Shared.Errors;

public static class PomodoroSessionErrors
{
    public const string ClientIdAlreadyExists = "PomodoroSession.ClientIdAlreadyExists";
    public const string RunningSessionAlreadyExists = "PomodoroSession.RunningSessionAlreadyExists";
    public const string TaskItemNotFound = "PomodoroSession.TaskItemNotFound";
    
    public const string PomodoroSessionNotFound = "PomodoroSession.NotFound";
    public const string PomodoroSessionAlreadyCompleted = "PomodoroSession.AlreadyCompleted";
}
