namespace ZumGelbenBach.Components.Models
{
    public class MenuItemState
    {
        // warum speichern wir nicht alle ids die angeklickt wurden und lesen im Warenkorb nur aus 
        // im sds mehrere ds zu speichern und nach und nach auszulesen und in eine abfrage bauen geht bestimmt
        public int Quantity { get; set; }
        public string sProdukt { get; set; } // was ist selektiert fehlt noch !!!!!!!!!!!
        public bool IsSelected { get; set; }
    }
}
