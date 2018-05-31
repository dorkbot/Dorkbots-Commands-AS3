namespace Signals.Example
{
    public class SignalerExample
    {
        public Signal<int, SignalerExample> mySignal { get; private set; }

        public SignalerExample()
        {
            mySignal = new Signal<int, SignalerExample>();
        }

        public void DoSomething()
        {
            mySignal.Dispatch(1, this);
        }
    }
}