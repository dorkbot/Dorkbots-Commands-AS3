using System;
using UnityEngine;

namespace Signals
{
    // 
    // Ex:
    // Instantiate - mySignal = new Signal<MyType>();
    // Subscribe - mySignal.Add(MySigHandler);
    // Unsubscribe - mySignal.Remove(MySigHandler);
    // Dispatch - mySignal.Dispatch(myTypeRef);
    // Handler - void MySigHandler(MyType type)
    // 
    // What's is this? -> https://en.wikipedia.org/wiki/Signals_and_slots

	/// <summary>
	/// A Signal that dispatches 0 generic parameters to listening methods.
	/// </summary>
	public class Signal : SignalBase<Slot, Action>
	{
		/// <summary>
		/// Dispatches a 0 parameter event to any methods listening to this Signal.
		/// </summary>
		public void Dispatch()
		{
			if (DispatchStart())
			{
				foreach (Slot slot in Slots)
				{
					if (slot.IsOnce)
					{
						Remove(slot.Listener);
					}

					try
					{
						slot.Listener.Invoke();
					}
					catch (Exception e)
					{
						//We remove the Slot so the Error doesn't inevitably happen again.
						Remove(slot.Listener);
						Debug.LogException(e);
					}
				}
				DispatchStop();
			}
		}
	}

	/// <summary>
	/// A Signal that dispatches 1 generic parameter to listening methods.
	/// </summary>
	public class Signal<T1> : SignalBase<Slot<T1>, Action<T1>>
	{
		/// <summary>
		/// Dispatches a 1 parameter event to any methods listening to this Signal.
		/// </summary>
		public void Dispatch(T1 item1)
		{
			if (DispatchStart())
			{
				foreach (Slot<T1> slot in Slots)
				{
					if (slot.IsOnce)
					{
						Remove(slot.Listener);
					}

					try
					{
						slot.Listener.Invoke(item1);
					}
					catch (Exception e)
					{
						//We remove the Slot so the Error doesn't inevitably happen again.
						Remove(slot.Listener);
						Debug.LogException(e);
					}
				}
				DispatchStop();
			}
		}
	}

	/// <summary>
	/// A Signal that dispatches 2 generic parameters to listening methods.
	/// </summary>
	public class Signal<T1, T2> : SignalBase<Slot<T1, T2>, Action<T1, T2>>
	{
		/// <summary>
		/// Dispatches a 2 parameter event to any methods listening to this Signal.
		/// </summary>
		public void Dispatch(T1 item1, T2 item2)
		{
			if (DispatchStart())
			{
				foreach (Slot<T1, T2> slot in Slots)
				{
					if (slot.IsOnce)
					{
						Remove(slot.Listener);
					}

					try
					{
						slot.Listener.Invoke(item1, item2);
					}
					catch (Exception e)
					{
						//We remove the Slot so the Error doesn't inevitably happen again.
						Remove(slot.Listener);
						Debug.LogException(e);
					}
				}
				DispatchStop();
			}
		}
	}

	/// <summary>
	/// A Signal that dispatches 3 generic parameters to listening methods.
	/// </summary>
	public class Signal<T1, T2, T3> : SignalBase<Slot<T1, T2, T3>, Action<T1, T2, T3>>
	{
		/// <summary>
		/// Dispatches a 3 parameter event to any methods listening to this Signal.
		/// </summary>
		public void Dispatch(T1 item1, T2 item2, T3 item3)
		{
			if (DispatchStart())
			{
				foreach (Slot<T1, T2, T3> slot in Slots)
				{
					if (slot.IsOnce)
					{
						Remove(slot.Listener);
					}

					try
					{
						slot.Listener.Invoke(item1, item2, item3);
					}
					catch (Exception e)
					{
						//We remove the Slot so the Error doesn't inevitably happen again.
						Remove(slot.Listener);
						Debug.LogException(e);
					}
				}
				DispatchStop();
			}
		}
	}

	/// <summary>
	/// A Signal that dispatches 4 generic parameters to listening methods.
	/// </summary>
	public class Signal<T1, T2, T3, T4> : SignalBase<Slot<T1, T2, T3, T4>, Action<T1, T2, T3, T4>>
	{
		/// <summary>
		/// Dispatches a 4 parameter event to any methods listening to this Signal.
		/// </summary>
		public void Dispatch(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			if (DispatchStart())
			{
				foreach (Slot<T1, T2, T3, T4> slot in Slots)
				{
					if (slot.IsOnce)
					{
						Remove(slot.Listener);
					}

					try
					{
						slot.Listener.Invoke(item1, item2, item3, item4);
					}
					catch (Exception e)
					{
						//We remove the Slot so the Error doesn't inevitably happen again.
						Remove(slot.Listener);
						Debug.LogException(e);
					}
				}
				DispatchStop();
			}
		}
	}
}