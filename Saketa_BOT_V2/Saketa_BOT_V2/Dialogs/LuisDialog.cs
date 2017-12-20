using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Saketa_BOT_V2.Dialogs
{
    [Serializable]
    [LuisModel("eaef2269-57b3-4bf6-abce-6331aa16b2fd", "91f3b2a0904146de8ac2d8bc07f0633a")]
    public class LuisDialog : LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            //Récupération du message
            var messageToForward = await activity;
            var cts = new CancellationTokenSource();
            var faq = new QnaDialog();
            //Transfert du message une fois réponse retournée => AfterFAQDialog
            await context.Forward(faq, AfterFAQDialog, messageToForward, CancellationToken.None);
        }

        private async Task AfterFAQDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            string message = $"Hello there";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        private ResumeAfter<object> after()
        {
            return null;
        }

        [LuisIntent("weather")]
        public async Task Middle(IDialogContext context, LuisResult result)
        {
            // confirm we hit weather intent
            string message = $"Weather forecast is...";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("joke")]
        public async Task Joke(IDialogContext context, LuisResult result)
        {
            // confirm we hit joke intent
            string message = $"Let's see...I know a good joke...";

            await context.PostAsync(message);

            await context.Forward(new JokeDialog(), ResumeAfterJokeDialog, context.Activity, CancellationToken.None);
        }

        [LuisIntent("question")]
        public async Task QnA(IDialogContext context, LuisResult result)
        {
            // confirm we hit QnA
            string message = $"Routing to QnA... ";
            await context.PostAsync(message);

            var userQuestion = (context.Activity as Activity).Text;
            await context.Forward(new QnaDialog(), ResumeAfterQnA, context.Activity, CancellationToken.None);
        }

        private async Task ResumeAfterQnA(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }

        private async Task ResumeAfterJokeDialog(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(null);
        }

    }
}