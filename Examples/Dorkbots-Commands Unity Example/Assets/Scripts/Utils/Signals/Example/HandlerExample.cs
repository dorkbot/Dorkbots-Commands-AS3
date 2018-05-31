namespace Signals.Example
{
    public class HandlerExample
    {
        public HandlerExample()
        {
            SignalerExample signaler = new SignalerExample();
            signaler.mySignal.Add(MyHandler);
            signaler.DoSomething();
        }

        private void MyHandler(int myInt, SignalerExample signaler)
        {
            // do stuff
        }
    }
}