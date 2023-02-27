using WebSocketSharp;
using WebSocketSharp.Server;

namespace toolboxmamaneg
{
    public class SessionId : WebSocketBehavior
    {
        
        protected override void OnMessage(MessageEventArgs e)
        {
            var pssessionID = ProcessExtensions.WTSGetActiveConsoleSessionId();
            Send(pssessionID.ToString());
        }
    }
}