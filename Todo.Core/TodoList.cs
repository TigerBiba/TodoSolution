using System.Text.Json;

namespace Todo.Core
{
    public class TodoList
    {
        private readonly List<TodoItem> _items = new();
        public IReadOnlyList<TodoItem> Items => _items.AsReadOnly();
        public TodoItem Add(string title)
        {
            var item = new TodoItem(title);
            _items.Add(item);
            return item;
        }
        public bool Remove(Guid id) => _items.RemoveAll(i => i.Id == id) > 0;
        public IEnumerable<TodoItem> Find(string substring) =>
            _items.Where(i => i.Title.Contains(substring ?? string.Empty, StringComparison.OrdinalIgnoreCase));
        public int Count => _items.Count;

        public string Serrialize(IReadOnlyList<TodoItem> todoItems)
        {
            string jsonString = JsonSerializer.Serialize(todoItems);
            return jsonString;
        }
        public void Save(string path) => Console.WriteLine(path);//типо сохранение

        public IReadOnlyList<TodoItem> Load(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;
            return JsonSerializer.Deserialize<IReadOnlyList<TodoItem>>(path);
        }
    }
}
