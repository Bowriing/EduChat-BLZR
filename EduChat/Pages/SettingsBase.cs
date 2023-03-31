using Microsoft.AspNetCore.Components;

namespace EduChat.Pages
{
    public class SettingsBase : ComponentBase
    {
        protected bool Enabled = false;
        protected void DarkModeButtonClick()
        {
            Enabled = !Enabled;
            Console.WriteLine(Enabled ? "enabled" : "disabled");
        }     
    }
}
