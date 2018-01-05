using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Saketa_BOT_V2.Models
{
    [Serializable]
    public class User
    {
        public string Name { get; set; }
        public string EmailID { get; set; }

        public static IForm<User> BuildForm()
        {
            return new FormBuilder<User>()
                    .Message("I need just some information")
                    .Build();
        }
    }
}