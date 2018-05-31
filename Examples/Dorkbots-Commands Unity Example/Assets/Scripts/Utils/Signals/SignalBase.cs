using System;
using System.Collections.Generic;

namespace Signals
{
	/// <summary>
	/// The base Signal class used by all Signals. Contains all of the
	/// base values that can be extended by other Signal classes.
	/// </summary>
	public abstract class SignalBase
	{
		//Slots to be processed during a Dispatch() from subclasses.
		private List<SlotBase> slots = new List<SlotBase>();

		//Slots pooled when listeners are removed. Slots get reused in Add().
		private Stack<SlotBase> slotsPooled = new Stack<SlotBase>();

		//Slots removed during a Dispatch(). If a dispatch is already in progress,
		//then we can't remove and clean up a Slot until it's done.
		private Stack<SlotBase> slotsRemoved = new Stack<SlotBase>();

		//The number of concurrent Dispatch()'s happening to this Signal at once.
		//A Signal could be dispatched multiple times in one call stack.
		//Need to monitor when this reaches 0 to clean up removed Slots.
		private int dispatching = 0;

		//If this Signal is disposed, then we don't want to pool anymore Slots.
		//Becomes true when Dispose() is called. Becomes false when another Slot/listener gets added.
		private bool isDisposed = false;

		public static implicit operator bool(SignalBase signal)
		{
			return signal != null;
		}

		public SignalBase()
		{

		}

		/// <summary>
		/// Cleans up the Signal by removing and disposing all listeners,
		/// and unpooling allocated Slots.
		/// </summary>
		public void Dispose()
		{
			isDisposed = true;
			slotsPooled.Clear();
			RemoveAll();
		}

		/// <summary>
		/// A protected method that should only be called during Dispatch().
		/// If we have slots listening, then DispatchStart() deems it okay to proceed
		/// with a Dispatch() and subsequent DispatchStop() call.
		/// </summary>
		/// <returns></returns>
		protected bool DispatchStart()
		{
			if (slots.Count > 0)
			{
				++dispatching;
				return true;
			}
			return false;
		}

		/// <summary>
		/// A protected method that should only be called during Dispatch().
		/// When a Dispatch() has been deemed okay to start by DispatchStart(),
		/// a DispatchStop() should be called when the Dispatch() is over
		/// to clean up removed slots.
		/// </summary>
		/// <returns></returns>
		protected bool DispatchStop()
		{
			if (dispatching == 0)
				return false;
			if (--dispatching == 0)
			{
				while (slotsRemoved.Count > 0)
				{
					DisposeSlot(slotsRemoved.Pop());
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Is this Signal currently dispatching? Returns true if dispatching > 0.
		/// </summary>
		public bool IsDispatching
		{
			get
			{
				return dispatching > 0;
			}
		}

		/// <summary>
		/// The number of concurrent dispatches this Signal is processing. During a dispatch, it's
		/// possible that code could require another dispatch on the same Signal during a currently
		/// running dispatch.
		/// </summary>
		public int Dispatching
		{
			get
			{
				return dispatching;
			}
		}

		/// <summary>
		/// The number of Slots/listeners attached to this Signal.
		/// </summary>
		public int NumSlots
		{
			get
			{
				return slots.Count;
			}
		}

		/// <summary>
		/// Returns a copy of the Slots being processed by this Signal
		/// in the order of how they're prioritized.
		/// </summary>
		public List<SlotBase> Slots
		{
			get
			{
				return new List<SlotBase>(slots);
			}
		}

		/// <summary>
		/// Calls Dispose() on a Slot and pools it for reuse. This is done at the end of
		/// a Dispatch() or when a Slot is removed and no dispatches are occurring.
		/// </summary>
		/// <param name="slot"></param>
		private void DisposeSlot(SlotBase slot)
		{
			slot.Dispose();
			if (!isDisposed)
				slotsPooled.Push(slot);
		}

		/// <summary>
		/// Returns the Slot with the given listener.
		/// </summary>
		/// <param name="listener"></param>
		/// <returns></returns>
		public SlotBase Get(Delegate listener)
		{
			if (listener == null)
				return null;
			foreach (SlotBase slot in slots)
			{
				if (slot.Listener == listener)
					return slot;
			}
			return null;
		}

		/// <summary>
		/// Returns the Slot at the given index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public SlotBase GetAt(int index)
		{
			if (index < 0)
				return null;
			if (index > slots.Count - 1)
				return null;
			return slots[index];
		}

		/// <summary>
		/// Returns the index of the Slot with the given listener.
		/// </summary>
		/// <param name="listener">The Delegate to call when this Signal is dispatched.</param>
		/// <returns></returns>
		public int GetIndex(Delegate listener)
		{
			if (listener == null)
				return -1;
			for (int index = slots.Count - 1; index > -1; --index)
			{
				if (slots[index].Listener == listener)
					return index;
			}
			return -1;
		}

		/// <summary>
		/// Adds a listener to this Signal. The priority defaults to 0 and the method is invoked on Dispatch() indefinitely.
		/// </summary>
		/// <param name="listener">The Delegate to call when this Signal is dispatched.</param>
		/// <returns></returns>
		protected SlotBase Add(Delegate listener)
		{
			return Add(listener, 0, false);
		}

		/// <summary>
		/// Adds a listener to this Signal. The method is invoked on Dispatch() indefinitely.
		/// </summary>
		/// <param name="listener">The Delegate to call when this Signal is dispatched.</param>
		/// <param name="priority">The priority of this Slot. Slots are called lowest-to-highest priority.</param>
		/// <returns></returns>
		protected SlotBase Add(Delegate listener, int priority)
		{
			return Add(listener, priority, false);
		}

		/// <summary>
		/// Adds a listener to this Signal. The priority defaults to 0 and the method is invoked on Dispatch() only once.
		/// </summary>
		/// <param name="listener">The Delegate to call when this Signal is dispatched.</param>
		/// <returns></returns>
		protected SlotBase AddOnce(Delegate listener)
		{
			return Add(listener, 0, true);
		}

		/// <summary>
		/// Adds a listener to this Signal. The method is invoked on Dispatch() only once.
		/// </summary>
		/// <param name="listener">The Delegate to call when this Signal is dispatched.</param>
		/// <param name="priority">The priority of this Slot. Slots are called lowest-to-highest priority.</param>
		/// <returns></returns>
		protected SlotBase AddOnce(Delegate listener, int priority)
		{
			return Add(listener, priority, true);
		}

		/// <summary>
		/// Adds a listener to this Signal.
		/// </summary>
		/// <param name="listener">The Delegate to call when this Signal is dispatched.</param>
		/// <param name="priority">The priority of this Slot. Slots are called lowest-to-highest priority.</param>
		/// <param name="isOnce">Determines if this listener is called only once or indefinitely on Dispatch().</param>
		/// <returns></returns>
		protected SlotBase Add(Delegate listener, int priority, bool isOnce)
		{
			if (listener == null)
				return null;
			SlotBase slot = Get(listener);
			if (!slot)
			{
				if (slotsPooled.Count > 0)
				{
					slot = slotsPooled.Pop();
				}
				else
				{
					slot = CreateSlot();
				}
			}
			slot.Listener = listener;
			slot.Priority = priority;
			slot.IsOnce = isOnce;
			slot.Signal = this;
			PriorityChanged(slot, slot.Priority, 0);
			isDisposed = false;
			return slot;
		}

		/// <summary>
		/// Creates a Slot to be used/coupled with a listener Delegate. Using the SignalBase with generics will
		/// automatically generate the correct Slot for your Signal.
		/// </summary>
		/// <returns></returns>
		virtual protected SlotBase CreateSlot()
		{
			return new SlotBase();
		}

		/// <summary>
		/// Internal method that is called from Slots when their priority has changed.
		/// Keeps the Signal's Slot order up to date. Should NOT be called from anywhere else!
		/// </summary>
		/// <param name="slot"></param>
		/// <param name="current"></param>
		/// <param name="previous"></param>
		internal void PriorityChanged(SlotBase slot, int current, int previous)
		{
			slots.Remove(slot);

			for (int index = slots.Count; index > 0; --index)
			{
				if (slots[index - 1].Priority <= slot.Priority)
				{
					slots.Insert(index, slot);
					return;
				}
			}

			slots.Insert(0, slot);
		}

		/// <summary>
		/// Removes a listener from this Signal.
		/// </summary>
		/// <param name="listener"></param>
		/// <returns></returns>
		public bool Remove(Delegate listener)
		{
			if (listener == null)
				return false;
			for (int index = slots.Count - 1; index > -1; --index)
			{
				if (slots[index].Listener == listener)
					return RemoveAt(index);
			}
			return false;
		}

		/// <summary>
		/// Removes the listener from this Signal at the given index.
		/// </summary>
		/// <param name="index">The index of the Slot to remove. Slots are ordered by priority.</param>
		/// <returns></returns>
		public bool RemoveAt(int index)
		{
			if (index < 0)
				return false;
			if (index >= slots.Count)
				return false;
			SlotBase slot = slots[index];
			slots.RemoveAt(index);
			if (dispatching > 0)
			{
				slotsRemoved.Push(slot);
			}
			else
			{
				DisposeSlot(slot);
			}
			return true;
		}

		/// <summary>
		/// Removes all listeners.
		/// </summary>
		public bool RemoveAll()
		{
			if (slots.Count <= 0)
				return false;
			while (slots.Count > 0)
				RemoveAt(slots.Count - 1);
			return true;
		}
	}

	/// <summary>
	/// The base Signal class used by all Signals. Contains generics to cast the Slot and Delegate listener to their appropriate types.
	/// </summary>
	public abstract class SignalBase<TSlot, TDelegate> : SignalBase
		where TSlot : SlotBase, new()
		where TDelegate : class
	{
		/// <summary>
		/// Adds a listener to this Signal. The priority defaults to 0 and the method is invoked on Dispatch() indefinitely.
		/// </summary>
		/// <param name="listener">The Delegate to call when this Signal is dispatched.</param>
		/// <returns></returns>
		public TSlot Add(TDelegate listener)
		{
			return Add(listener as Delegate) as TSlot;
		}

		/// <summary>
		/// Adds a listener to this Signal. The method is invoked on Dispatch() indefinitely.
		/// </summary>
		/// <param name="listener">The Delegate to call when this Signal is dispatched.</param>
		/// <param name="priority">The priority of this Slot. Slots are called lowest-to-highest priority.</param>
		/// <returns></returns>
		public TSlot Add(TDelegate listener, int priority)
		{
			return Add(listener as Delegate, priority, false) as TSlot;
		}

		/// <summary>
		/// Adds a listener to this Signal. The priority defaults to 0 and the method is invoked on Dispatch() only once.
		/// </summary>
		/// <param name="listener">The Delegate to call when this Signal is dispatched.</param>
		/// <returns></returns>
		public TSlot AddOnce(TDelegate listener)
		{
			return Add(listener as Delegate, 0, true) as TSlot;
		}

		/// <summary>
		/// Adds a listener to this Signal. The method is invoked on Dispatch() only once.
		/// </summary>
		/// <param name="listener">The Delegate to call when this Signal is dispatched.</param>
		/// <param name="priority">The priority of this Slot. Slots are called lowest-to-highest priority.</param>
		/// <returns></returns>
		public TSlot AddOnce(TDelegate listener, int priority)
		{
			return Add(listener as Delegate, priority, true) as TSlot;
		}

		/// <summary>
		/// Adds a listener to this Signal.
		/// </summary>
		/// <param name="listener">The Delegate to call when this Signal is dispatched.</param>
		/// <param name="priority">The priority of this Slot. Slots are called lowest-to-highest priority.</param>
		/// <param name="isOnce">Determines if this listener is called only once or indefinitely on Dispatch().</param>
		/// <returns></returns>
		public TSlot Add(TDelegate listener, int priority, bool isOnce)
		{
			return Add(listener as Delegate, priority, isOnce) as TSlot;
		}

		/// <summary>
		/// Returns the Slot with the given listener.
		/// </summary>
		/// <param name="listener"></param>
		/// <returns></returns>
		public TSlot Get(TDelegate listener)
		{
			return Get(listener as Delegate) as TSlot;
		}

		/// <summary>
		/// Returns the index of the Slot with the given listener.
		/// </summary>
		/// <param name="listener">The Delegate to call when this Signal is dispatched.</param>
		/// <returns></returns>
		public int GetIndex(TDelegate listener)
		{
			return GetIndex(listener as Delegate);
		}

		/// <summary>
		/// Removes a listener from this Signal.
		/// </summary>
		/// <param name="listener"></param>
		/// <returns></returns>
		public bool Remove(TDelegate listener)
		{
			return Remove(listener as Delegate);
		}

		/// <summary>
		/// Returns the Slot at the given index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public new TSlot GetAt(int index)
		{
			return base.GetAt(index) as TSlot;
		}

		/// <summary>
		/// Creates a Slot to be used/coupled with a listener Delegate. Automatically generates the correct Slot for your Signal
		/// from the given TSlot generic.
		/// </summary>
		/// <returns></returns>
		sealed protected override SlotBase CreateSlot()
		{
			return new TSlot();
		}
	}
}