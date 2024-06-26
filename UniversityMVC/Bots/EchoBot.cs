using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace UniversityMVC.Bots
{
    public class EchoBot: ActivityHandler
    {
        private readonly BotState _conversationState;
        private readonly DialogSet _dialogs;

        public EchoBot(ConversationState conversationState)
        {
            _conversationState = conversationState;
            _dialogs = new DialogSet(_conversationState.CreateProperty<DialogState>("DialogState"));
            _dialogs.Add(new TextPrompt("textPrompt"));
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var dialogContext = await _dialogs.CreateContextAsync(turnContext, cancellationToken);
            var results = await dialogContext.ContinueDialogAsync(cancellationToken);

            if (results.Status == DialogTurnStatus.Empty)
            {
                await dialogContext.PromptAsync("textPrompt", new PromptOptions { Prompt = MessageFactory.Text("Hello! How can I assist you today?") }, cancellationToken);
            }
            else if (results.Status == DialogTurnStatus.Complete)
            {
                var response = results.Result.ToString();
                await turnContext.SendActivityAsync(MessageFactory.Text($"You said: {response}"), cancellationToken);
            }
        }
    }
}
