using System;

namespace Signals
{
	/// <summary>
	/// A Slot with a 0 parameter listener method.
	/// </summary>
	public class Slot : SlotBase<Signal, Action>
	{

	}

	/// <summary>
	/// A Slot with a 1 parameter listener method.
	/// </summary>
	public class Slot<T1> : SlotBase<Signal<T1>, Action<T1>>
	{

	}

	/// <summary>
	/// A Slot with a 2 parameter listener method.
	/// </summary>
	public class Slot<T1, T2> : SlotBase<Signal<T1, T2>, Action<T1, T2>>
	{

	}

	/// <summary>
	/// A Slot with a 3 parameter listener method.
	/// </summary>
	public class Slot<T1, T2, T3> : SlotBase<Signal<T1, T2, T3>, Action<T1, T2, T3>>
	{

	}

	/// <summary>
	/// A Slot with a 4 parameter listener method.
	/// </summary>
	public class Slot<T1, T2, T3, T4> : SlotBase<Signal<T1, T2, T3, T4>, Action<T1, T2, T3, T4>>
	{

	}
}
