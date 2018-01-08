using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Saketa_BOT_V2.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Saketa_BOT_V2.Dialogs
{ 
    [Serializable]
    [LuisModel("9c526256-7256-4309-8131-01bdfcaa1921", "91f3b2a0904146de8ac2d8bc07f0633a")]
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


        [LuisIntent("AppPrice")]
        public async Task AppPrice(IDialogContext context, LuisResult result)
        {
            string message = $"You have selected App Price, Acton yet to be written.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }


        [LuisIntent("AppInfo")]
        public async Task AppInfo(IDialogContext context, LuisResult result)
        {
            string message = $"You have selected App Info, Acton yet to be written.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        private ResumeAfter<object> after()
        {
            return null;
        }

        [LuisIntent("Demo|FreeTrial")]
        public async Task Demo(IDialogContext context, LuisResult result)
        {
            //var dia = new ScheduleDemoDialog();
            //context.Wait(dia.ShowAnnuvalConferenceTicket);


            string message = $"You have selected Demo|Free Trail, Acton yet to be written.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);

            // await AskForaDemo(context, null);
            //string message = $"I need some information to schedule a demo. Have you set aside a few minutes to get yourself registered?";
            //await context.PostAsync(message);
            //context.Wait(this.YessOrNo);

            //  await context.PostAsync(message);

            //  context.Wait(this.MessageReceived);
        }

        public virtual async Task YessOrNo(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            var isRegistered = false;
            context.UserData.TryGetValue<bool>("isRegistered", out isRegistered);
            //context.UserData.TryGetValue<string>("Name");
            if (message.Text == "yes")
            {
                var dia = new ScheduleDemoDialog();
                if (!isRegistered)
                    context.Wait(dia.ShowAnnuvalConferenceTicket);
                // await context.Forward(RegistrationDialog.Register(), this.ResumeAfterRegistrationDialog, message, CancellationToken.None);
                else
                {
                    context.Wait(dia.ShowAnnuvalConferenceTicket);
                    //  await context.Forward(new ScheduleDemoDialog(), this.ResumeAfterScheduleDemoDialog, message, CancellationToken.None);
                }
            }
            else
            {
                context.Done(message);
            }
        }

        private async Task ResumeAfterScheduleDemoDialog(IDialogContext context, IAwaitable<object> result)
        {
            User user;
            // TravelDetails travelDetails;
            // int noOfPartners;
            // DemoSlots demoSlots;
            context.UserData.TryGetValue<User>("User", out user);
            // context.UserData.TryGetValue<TravelDetails>("TravelDetails", out travelDetails);
            //  context.UserData.TryGetValue<int>("noOfPartners", out noOfPartners);
            //  context.UserData.TryGetValue<DemoSlots>("demoSlots", out demoSlots);
        }

        private async Task ResumeAfterRegistrationDialog(IDialogContext context, IAwaitable<User> result)
        {
            User user = await result;
            context.UserData.SetValue<bool>("isRegistered", true);
            context.UserData.SetValue<User>("User", user);
            await AskForaDemo(context, null);
        }

        public virtual async Task AskForaDemo(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            User user = new User();
            context.UserData.TryGetValue<User>("User", out user);
            await context.PostAsync($"Hi {user.Name}, want to schedule a demo?");
            context.Wait(YessOrNo);
            //context.Done("");
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