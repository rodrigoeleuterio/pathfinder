namespace Genesis.Organizer
{
    public struct PriorityObject<T> : IPriority
    {
        public T Value { get; set; }
        public int PriorityValue { get; set; }
    }
}
