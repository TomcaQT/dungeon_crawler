/// <summary>
/// Container which contains probability of choosing determined value
/// </summary>
/// <typeparam name="T">Type of value</typeparam>
[System.Serializable]
public struct ProbabilityValue<T>
{
    public ProbabilityValue(T value, float probability)
    {
        this.probability = probability;
        this.value = value;
    }

    public T value;
    public float probability;
}