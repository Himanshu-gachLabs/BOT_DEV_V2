using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Configuration;
using System.Threading.Tasks;

namespace Saketa_BOT_V2.Dialogs
{
    public class QnaDialog : QnAMakerDialog
    {
        public QnaDialog() : base(
            new QnAMakerService(new QnAMakerAttribute(ConfigurationManager.AppSettings["QnaSubscriptionKey"],
            ConfigurationManager.AppSettings["QnaKnowledgebaseId"], "Hmm, I wasn't able to find any relevant content. Can you try asking in a different way? or try with typing help.", 0.5)))
        {


        }

        protected override async Task DefaultWaitNextMessageAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {
            if (result.Answers.Count == 0)
            {
                SendEmail email = new SendEmail();
                email.SendEmailtoAdmin(message.Text, "himanshu.k@technovert.com");
            }

            context.Done(true);
        }

    }
}