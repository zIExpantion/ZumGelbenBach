namespace ZumGelbenBach.Components.Models
{
    public class SessionDataServiceX
    {
        private Dictionary<string, MenuItemState> _menuItemStates = new();

        // Gibt die Menü-Item-Zustände zurück
        public Dictionary<string, MenuItemState> GetMenuItemStates()
        {
            return _menuItemStates;
        }

        // Aktualisiert die Zustände
        public void UpdateMenuItemStates(Dictionary<string, MenuItemState> states)
        {
            _menuItemStates = states;
        }

        // Fügt ein Element hinzu oder aktualisiert es
        public void AddOrUpdateMenuItem(string itemId, int quantity, string product)
        {
            if (_menuItemStates.ContainsKey(itemId))
            {
                _menuItemStates[itemId].Quantity += quantity;
                _menuItemStates[itemId].sProdukt = product;
            }
            else
            {
                _menuItemStates[itemId] = new MenuItemState
                {
                    Quantity = quantity,
                    sProdukt = product,
                    IsSelected = true
                };
            }
        }

        // Löscht alle Elemente
        public void ClearItems()
        {
            _menuItemStates.Clear();
        }
        // Entfernt ein spezifisches Item aus der Session
        public void RemoveItem(string itemId)
        {
            if (_menuItemStates.ContainsKey(itemId))
            {
                _menuItemStates.Remove(itemId);
            }
        }
    }
}
