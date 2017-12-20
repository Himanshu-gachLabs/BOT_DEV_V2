using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Saketa_BOT_V2.Dialogs
{
    public class CommonResponsesDialog : IDialog<object>
    {
        private readonly string _messageToSend;
        private Activity _activity;

        public CommonResponsesDialog(string message)
        {
            _messageToSend = message;
        }

        // overload constructor to handle activities (card attachments)
        public CommonResponsesDialog(Activity activity)
        {
            var heroCard = new HeroCard
            {
                Title = "Help",
                Text = "Need assisstance?",
                Buttons = new List<CardAction> {
                    new CardAction(ActionTypes.OpenUrl, "Contact Us", value: "https://saketa.com/contact-us/"),
                    new CardAction(ActionTypes.OpenUrl, "FAQ", value: "https://saketa.com/faqs/")
                }
            };
            var reply = activity.CreateReply();

            reply.Attachments.Add(heroCard.ToAttachment());

            _activity = reply;
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (string.IsNullOrEmpty(_messageToSend))
            {
                await context.PostAsync(_activity);
            }
            else
            {
                await context.PostAsync(_messageToSend);
            }
            context.Done<object>(null);
        }
    }
}