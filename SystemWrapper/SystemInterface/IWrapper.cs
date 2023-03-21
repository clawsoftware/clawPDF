using System;

namespace SystemInterface
{
    /// <summary>
    /// Provides access to the underlying instance of the wrapper object.
    /// </summary>
    /// <typeparam name="T">Type that is wrapper by SystemInterface API.</typeparam>
    public interface IWrapper<out T>
    {
        T Instance { get; }
    }
}
