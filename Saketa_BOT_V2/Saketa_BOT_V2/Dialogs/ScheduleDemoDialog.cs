using AdaptiveCards;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Saketa_BOT_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Saketa_BOT_V2.Dialogs
{
    public class ScheduleDemoDialog:IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            //var userName = String.Empty;
            await ShowAnnuvalConferenceTicket(context, null);
           // await AskforTimeSlot(context, null);
        }

        public virtual async Task ShowAnnuvalConferenceTicket(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var message = await activity;

            PromptDialog.Choice(
                context: context,
                resume: ChoiceReceivedAsync,
                options: (IEnumerable<DemoSlots>)Enum.GetValues(typeof(DemoSlots)),
                prompt: "Hi. Please Select your preferrd time slot :",
                retry: "Selected slot not avilabel . Please try again.",
                promptStyle: PromptStyle.Auto
                );
        }

        public virtual async Task ChoiceReceivedAsync(IDialogContext context, IAwaitable<DemoSlots> activity)
        {
            DemoSlots response = await activity;
            context.Call<object>(new MeetingDialog(response.ToString()), ChildDialogComplete);

        }

        public virtual async Task ChildDialogComplete(IDialogContext context, IAwaitable<object> response)
        {
            await context.PostAsync("Thanks for select Saketa Apps.");
            context.Done(this);
        }

        public virtual async Task AskforTimeSlot(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            Activity message = (Activity)context.MakeMessage();
            message.Attachments = new List<Attachment>();

            AdaptiveCard card = new AdaptiveCard();

            // Add text to the card.
            card.Body.Add(new TextBlock()
            {
                Text = "Please select available time slot?",
                Size = TextSize.Large,
                Weight = TextWeight.Bolder
            });

            // Add list of choices to the card.
            card.Body.Add(new ChoiceSet()
            {
                Id = "Time Slot",
                Style = ChoiceInputStyle.Compact,
                Choices = new List<AdaptiveCards.Choice>()
            {
                new AdaptiveCards.Choice() { Title = "10.00 IST", Value = DemoSlots.Morning.ToString()},
                new AdaptiveCards.Choice() { Title = "13.00 IST", Value = DemoSlots.Afternoon.ToString() },
                new AdaptiveCards.Choice() { Title = "17.00 IST", Value = DemoSlots.Evening.ToString() }
            }
            });

            card.Actions.Add(new SubmitAction()
            {
                Title = "submit"
            });

            // Create the attachment.
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };

            message.Attachments.Add(attachment);

            await context.PostAsync(message, CancellationToken.None);
           // context.Wait(this.abc);
        }
    }
}