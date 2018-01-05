using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Saketa_BOT_V2.Dialogs
{
    [Serializable]
    public class MeetingDialog: IDialog<object>
    {
        string name;
        string email;
        string phone;
        string plandetails;
        public MeetingDialog(string plan)
        {
            plandetails = plan;
        }
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Thanks for Select " + plandetails + " time slot , Can I Help for schedule this ? ");

            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var response = await activity;
            if (response.Text.ToLower().Contains("yes"))
            {
                PromptDialog.Text(
                    context: context,
                    resume: ResumeGetName,
                    prompt: "Please share your good name",
                    retry: "Sorry, I didn't understand that. Please try again."
                );
            }
            else
            {
                context.Done(this);
            }

        }

        public virtual async Task ResumeGetName(IDialogContext context, IAwaitable<string> Username)
        {
            string response = await Username;
            name = response; ;

            PromptDialog.Text(
                context: context,
                resume: ResumeGetEmail,
                prompt: "Please share your Email ID",
                retry: "Sorry, I didn't understand that. Please try again."
            );
        }

        public virtual async Task ResumeGetEmail(IDialogContext context, IAwaitable<string> UserEmail)
        {
            string response = await UserEmail;
            email = response; ;

            PromptDialog.Text(
                context: context,
                resume: ResumeGetPhone,
                prompt: "Please share your Mobile Number",
                retry: "Sorry, I didn't understand that. Please try again."
            );
        }
        public virtual async Task ResumeGetPhone(IDialogContext context, IAwaitable<string> mobile)
        {
            string response = await mobile;
            phone = response;

            await context.PostAsync(String.Format("Hello {0} ,Congratulation :) Your  demmo scheduled Successfullly completed with Name = {0} Email = {1} Mobile Number {2} . You will get Confirmation email and SMS", name, email, phone));


            SendEmail email1 = new SendEmail();
            email1.SendEmailtoAdmin(String.Format("Hello {0} ,Congratulation :) Your  demmo scheduled Successfullly completed with Name = {0} Email = {1} Mobile Number {2} . You will get Confirmation email and SMS", name, email, phone), email);

            context.Done(this);
        }
    }
}