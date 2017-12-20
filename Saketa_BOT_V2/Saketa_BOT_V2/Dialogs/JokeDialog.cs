using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace Saketa_BOT_V2.Dialogs
{
    [Serializable]
    public class JokeDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            // Confirmation that we're in the JokeDialog, forwarded from the LUIS dialog
            string response = "Who stole the Marker?  Marker Chor!!";

            context.PostAsync(response);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
        }
    }
}