using System;

namespace Signals
{
	/// <summary>
	/// The base Slot class used by all Slots. Contains all of the
	/// base values that can be extended by other Slot classes.
	/// </summary>
	public class SlotBase
	{
		public static implicit operator bool(SlotBase slot)
		{
			return slot != null;
		}

		//The Signal this Slot is associated/coupled with.
		private SignalBase signal;

		//The listener method of this Slot. Only 1 instance of this method/Slot may exist per Signal.
		private Delegate listener;

		//The priority of this Slot. Slots are ordered and invoked in their Signal by priority from lowest-to-hightest.
		private int priority = 0;

		//Determines whether this Slot/listener will be invoked only once or inidefinitely for
		//the lifetime of the Slot or Signal.
		private bool isOnce = false;

		internal SlotBase()
		{

		}

		/// <summary>
		/// Disposes of the Slot. Disposing removes the Slot from its Signal, and then
		/// nulls out and cleans up its values to be pooled and reused.
		/// </summary>
		public void Dispose()
		{
			if (!signal)
				return;
			if (signal.Get(listener))
			{
				signal.Remove(listener);
			}
			else
			{
				signal = null;
				listener = null;
				priority = 0;
				isOnce = false;
			}
		}

		/// <summary>
		/// The Signal this Slot is associated with. This should only be set
		/// from within a Signal.Add() call.
		/// </summary>
		public SignalBase Signal
		{
			get
			{
				return signal;
			}
			internal set
			{
				signal = value;
			}
		}

		/// <summary>
		/// The listening method of this Slot. Only one reference of a method may be
		/// added to a Signal at a time. This should only be set
		/// from within a Signal.Add() call.
		/// </summary>
		public Delegate Listener
		{
			get
			{
				return listener;
			}
			internal set
			{
				listener = value;
			}
		}

		/// <summary>
		/// If IsOnce is true, a Slot will only be invoked once and then removed from the Signal.
		/// If IsOnce is false, a Slot will be invoked indefinitely for every dispatch.
		/// </summary>
		public bool IsOnce
		{
			get
			{
				return isOnce;
			}
			set
			{
				isOnce = value;
			}
		}

		/// <summary>
		/// Slot priority determines in what order a Signal's Slots will be invoked in.
		/// Priority goes from the lowest number to the highest number.
		/// </summary>
		public int Priority
		{
			get
			{
				return priority;
			}
			set
			{
				if (priority == value)
					return;
				int previous = priority;
				priority = value;
				if (signal)
					signal.PriorityChanged(this, value, previous);
			}
		}
	}

	/// <summary>
	/// The base Slot class used by all Slots. Contains generics to cast the Signal and Delegate listener to their appropriate types.
	/// </summary>
	public class SlotBase<TSignal, TDelegate> : SlotBase
		where TSignal : SignalBase
		where TDelegate : class
	{
		/// <summary>
		/// The Signal this Slot is associated with. This should only be set
		/// from within a Signal.Add() call.
		/// </summary>
		public new TSignal Signal
		{
			get
			{
				return base.Signal as TSignal;
			}
			internal set
			{
				base.Signal = value as SignalBase;
			}
		}

		/// <summary>
		/// The listening method of this Slot. Only one reference of a method may be
		/// added to a Signal at a time.
		/// </summary>
		public new TDelegate Listener
		{
			get
			{
				return base.Listener as TDelegate;
			}
			internal set
			{
				base.Listener = value as Delegate;
			}
		}
	}
}