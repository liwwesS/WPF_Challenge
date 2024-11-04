namespace WPF_Challenge.Services;

public class MessageService
{
    public event Action UserAuthenticated;
    public event Action UserLoggedOut;

    public void NotifyUserAuthenticated()
    {
        UserAuthenticated?.Invoke();
    }

    public void NotifyUserLoggedOut()
    {
        UserLoggedOut?.Invoke();
    }
}