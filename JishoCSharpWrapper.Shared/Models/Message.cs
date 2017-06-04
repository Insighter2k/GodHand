namespace JishoCSharpWrapper.Shared.Models
{
    public class Message<T>
    {
        private T _result;
        public T Result => _result;

        internal void SetResult(T result)
        {
            _result = result;
        }
    }
}