using SAPPortal.Models;

public class SessionService
{
    public event Func<Task> OnSessionChanged;

    public async Task NotifySessionChanged()
    {
        if (OnSessionChanged != null)
        {
            await OnSessionChanged.Invoke();
        }
    }

    public async Task Login(string userName, int userId)
    {
        CommonSessionClass.UName = userName;
        CommonSessionClass.UId = userId.ToString();
        CommonSessionClass.LogInTime = DateTime.Now.ToString();

        await NotifySessionChanged();
    }

    public async Task Logout()
    {
        CommonSessionClass.UName = string.Empty;
        CommonSessionClass.UId = string.Empty;
        CommonSessionClass.LogInTime = string.Empty;

        await NotifySessionChanged();
    }
}
