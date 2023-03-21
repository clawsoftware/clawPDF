using System;

namespace SystemInterface.Threading
{
    /// <summary>
    /// Wrapper for <see cref="System.Threading.Thread"/> class.
    /// </summary>
    public interface IThread
    {
        // Methods

        /// <summary>
        /// Suspends the current thread for a specified time.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds for which the thread is blocked. Specify zero (0) to indicate that this thread should be suspended to allow other waiting threads to execute. Specify Infinite  to block the thread indefinitely.</param>
        void Sleep(int millisecondsTimeout);

        /// <summary>
        /// Blocks the current thread for a specified time.
        /// </summary>
        /// <param name="timeout">A TimeSpan  set to the amount of time for which the thread is blocked. Specify zero to indicate that this thread should be suspended to allow other waiting threads to execute. Specify Timeout.Infinite to block the thread indefinitely.</param>
        void Sleep(TimeSpan timeout);

        /*

                // Methods
            public Thread(ParameterizedThreadStart start);
            public Thread(ThreadStart start);
            public Thread(ParameterizedThreadStart start, int maxStackSize);
            public Thread(ThreadStart start, int maxStackSize);
            [SecurityPermission(SecurityAction.Demand, ControlThread=true)]
            public void Abort();
            [SecurityPermission(SecurityAction.Demand, ControlThread=true)]
            public void Abort(object stateInfo);
            [HostProtection(SecurityAction.LinkDemand, SharedState=true, ExternalThreading=true)]
            public static LocalDataStoreSlot AllocateDataSlot();
            [HostProtection(SecurityAction.LinkDemand, SharedState=true, ExternalThreading=true)]
            public static LocalDataStoreSlot AllocateNamedDataSlot(string name);
            [MethodImpl(MethodImplOptions.InternalCall), ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail), HostProtection(SecurityAction.LinkDemand, Synchronization=true, ExternalThreading=true)]
            public static extern void BeginCriticalRegion();
            [MethodImpl(MethodImplOptions.InternalCall), ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail), SecurityPermission(SecurityAction.LinkDemand, ControlThread=true)]
            public static extern void BeginThreadAffinity();
            [MethodImpl(MethodImplOptions.InternalCall), ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), HostProtection(SecurityAction.LinkDemand, Synchronization=true, ExternalThreading=true)]
            public static extern void EndCriticalRegion();
            [MethodImpl(MethodImplOptions.InternalCall), ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail), SecurityPermission(SecurityAction.LinkDemand, ControlThread=true)]
            public static extern void EndThreadAffinity();
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            protected override void Finalize();
            [HostProtection(SecurityAction.LinkDemand, SharedState=true, ExternalThreading=true)]
            public static void FreeNamedDataSlot(string name);
            public ApartmentState GetApartmentState();
            [HostProtection(SecurityAction.LinkDemand, SharedState=true, ExternalThreading=true)]
            public static object GetData(LocalDataStoreSlot slot);
            public static AppDomain GetDomain();
            public static int GetDomainID();
            [MethodImpl(MethodImplOptions.InternalCall), ComVisible(false)]
            public override extern int GetHashCode();
            [HostProtection(SecurityAction.LinkDemand, SharedState=true, ExternalThreading=true)]
            public static LocalDataStoreSlot GetNamedDataSlot(string name);
            [SecurityPermission(SecurityAction.Demand, ControlThread=true)]
            public void Interrupt();
            [HostProtection(SecurityAction.LinkDemand, Synchronization=true, ExternalThreading=true)]
            public void Join();
            [HostProtection(SecurityAction.LinkDemand, Synchronization=true, ExternalThreading=true)]
            public bool Join(int millisecondsTimeout);
            [HostProtection(SecurityAction.LinkDemand, Synchronization=true, ExternalThreading=true)]
            public bool Join(TimeSpan timeout);
            [MethodImpl(MethodImplOptions.InternalCall)]
            public static extern void MemoryBarrier();
            [SecurityPermission(SecurityAction.Demand, ControlThread=true)]
            public static void ResetAbort();
            [HostProtection(SecurityAction.LinkDemand, Synchronization=true, SelfAffectingThreading=true)]
            public void SetApartmentState(ApartmentState state);
            [HostProtection(SecurityAction.LinkDemand, SharedState=true, ExternalThreading=true)]
            public static void SetData(LocalDataStoreSlot slot, object data);
            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), HostProtection(SecurityAction.LinkDemand, Synchronization=true, ExternalThreading=true)]
            public static void SpinWait(int iterations);
            [MethodImpl(MethodImplOptions.NoInlining), HostProtection(SecurityAction.LinkDemand, Synchronization=true, ExternalThreading=true)]
            public void Start();
            [HostProtection(SecurityAction.LinkDemand, Synchronization=true, ExternalThreading=true)]
            public void Start(object parameter);
            [HostProtection(SecurityAction.LinkDemand, Synchronization=true, SelfAffectingThreading=true)]
            public bool TrySetApartmentState(ApartmentState state);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static byte VolatileRead(ref byte address);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static double VolatileRead(ref double address);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static short VolatileRead(ref short address);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static int VolatileRead(ref int address);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static long VolatileRead(ref long address);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static IntPtr VolatileRead(ref IntPtr address);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static object VolatileRead(ref object address);
            [MethodImpl(MethodImplOptions.NoInlining), CLSCompliant(false)]
            public static sbyte VolatileRead(ref sbyte address);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static float VolatileRead(ref float address);
            [MethodImpl(MethodImplOptions.NoInlining), CLSCompliant(false)]
            public static ushort VolatileRead(ref ushort address);
            [MethodImpl(MethodImplOptions.NoInlining), CLSCompliant(false)]
            public static uint VolatileRead(ref uint address);
            [MethodImpl(MethodImplOptions.NoInlining), CLSCompliant(false)]
            public static ulong VolatileRead(ref ulong address);
            [MethodImpl(MethodImplOptions.NoInlining), CLSCompliant(false)]
            public static UIntPtr VolatileRead(ref UIntPtr address);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void VolatileWrite(ref byte address, byte value);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void VolatileWrite(ref double address, double value);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void VolatileWrite(ref short address, short value);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void VolatileWrite(ref int address, int value);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void VolatileWrite(ref long address, long value);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void VolatileWrite(ref IntPtr address, IntPtr value);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void VolatileWrite(ref object address, object value);
            [MethodImpl(MethodImplOptions.NoInlining), CLSCompliant(false)]
            public static void VolatileWrite(ref sbyte address, sbyte value);
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static void VolatileWrite(ref float address, float value);
            [MethodImpl(MethodImplOptions.NoInlining), CLSCompliant(false)]
            public static void VolatileWrite(ref ushort address, ushort value);
            [MethodImpl(MethodImplOptions.NoInlining), CLSCompliant(false)]
            public static void VolatileWrite(ref uint address, uint value);
            [MethodImpl(MethodImplOptions.NoInlining), CLSCompliant(false)]
            public static void VolatileWrite(ref ulong address, ulong value);
            [MethodImpl(MethodImplOptions.NoInlining), CLSCompliant(false)]
            public static void VolatileWrite(ref UIntPtr address, UIntPtr value);

            // Properties
            public ApartmentState ApartmentState { get; [HostProtection(SecurityAction.LinkDemand, Synchronization=true, SelfAffectingThreading=true)] set; }
            public static Context CurrentContext { [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.Infrastructure)] get; }
            public CultureInfo CurrentCulture { get; [SecurityPermission(SecurityAction.Demand, ControlThread=true)] set; }
            public static IPrincipal CurrentPrincipal { get; [SecurityPermission(SecurityAction.Demand, Flags=SecurityPermissionFlag.ControlPrincipal)] set; }
            public static Thread CurrentThread { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)] get; }
            public CultureInfo CurrentUICulture { get; [HostProtection(SecurityAction.LinkDemand, ExternalThreading=true)] set; }
            public ExecutionContext ExecutionContext { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)] get; }
            public bool IsAlive { get; }
            public bool IsBackground { get; [HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading=true)] set; }
            public bool IsThreadPoolThread { get; }
            public int ManagedThreadId { [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get; }
            public string Name { get; [HostProtection(SecurityAction.LinkDemand, ExternalThreading=true)] set; }
            public ThreadPriority Priority { get; [HostProtection(SecurityAction.LinkDemand, SelfAffectingThreading=true)] set; }
            public ThreadState ThreadState { get; }
        */
    }
}
