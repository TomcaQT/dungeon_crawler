using UnityEngine;

namespace Helpers
{
    /// <summary>
    /// Container as nearly same as Tuple<T1,T2> (C++ pair-like) but can be serialized in Inspector
    /// </summary>
    /// <typeparam name="T1">Type of first value</typeparam>
    /// <typeparam name="T2">Type of second value</typeparam>
    [System.Serializable]
    public struct Pair<T1,T2>
    {
        [SerializeField] private T1 _first;
        [SerializeField] private T2 _second;
        
        public Pair(T1 first, T2 second)
        {
            _first = first;
            _second = second;
        }

        //Properties
        public T1 First => _first;
        public T2 Second => _second;

    }
}
