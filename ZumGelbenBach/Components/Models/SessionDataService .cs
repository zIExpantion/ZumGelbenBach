namespace ZumGelbenBach.Components.Models
{
    public class SessionDataService
    {
        public Dictionary<string, MenuItemState> _menuItemStates;


        public SessionDataService()
        {
            _menuItemStates = new Dictionary<string, MenuItemState>();
        }

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
        public void AddOrUpdateMenuItem(string dbID, string itemId, int quantity, string product)
        {
            if (_menuItemStates.ContainsKey(itemId))
            {
                _menuItemStates[itemId].Quantity = quantity;
                _menuItemStates[itemId].sProdukt = product;
            }
            else
            {
                _menuItemStates[itemId] = new MenuItemState
                {
                    dbID = dbID,
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
