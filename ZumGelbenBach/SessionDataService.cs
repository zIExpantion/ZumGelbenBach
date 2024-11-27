internal class SessionDataService
{
    public List<string> Items { get; set; } = new List<string>();

    // You can add more methods to manipulate or retrieve session-specific data
    public void AddItem(string item)
    {
        Items.Add(item);
    }

    public void ClearItems()
    {
        Items.Clear();
    }




}