using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Saketa_BOT_V2.Models;

namespace Saketa_BOT_V2.Dialogs
{
    public class RegistrationDialog
    {
        public static IDialog<User> Register()
        {
            return Chain.From(() => FormDialog.FromForm(User.BuildForm));
        }
    }
}