namespace Pathfinder.Core.DataStructure
{
    public interface IOrganizer<T>
    {
        int Count { get; }
        bool IsEmpty { get; }
        void Put(T value);
        T Draw();
        T ViewNext();
        void Clear();
    }
}
