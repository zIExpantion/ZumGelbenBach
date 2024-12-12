namespace ZumGelbenBach.Components.Models
{
    using System;
    using System.Collections.Generic;

    public class NutritionResponse
    {
        public List<FoodItem>? Foods { get; set; } // Liste der Lebensmittel, die von der API zurückgegeben wird

        public class FoodItem
        {
            public string? FoodName { get; set; } // Name des Lebensmittels
            public int ServingQty { get; set; } // Portionsgröße (Menge)
            public string? ServingUnit { get; set; } // Maßeinheit (z.B. "medium", "cup")
            public double ServingWeightGrams { get; set; } // Portionsgewicht in Gramm
            public double NfCalories { get; set; } // Kalorien pro Portion
            public double NfTotalFat { get; set; } // Gesamtfett in Gramm
            public double NfSaturatedFat { get; set; } // Gesättigte Fette in Gramm
            public double NfCholesterol { get; set; } // Cholesterin in Milligramm
            public double NfSodium { get; set; } // Natrium in Milligramm
            public double NfTotalCarbohydrate { get; set; } // Gesamt Kohlenhydrate in Gramm
            public double NfDietaryFiber { get; set; } // Ballaststoffe in Gramm
            public double NfSugars { get; set; } // Zucker in Gramm
            public double NfProtein { get; set; } // Proteine in Gramm
            public double NfVitaminD { get; set; } // Vitamin D in Prozent
            public double NfCalcium { get; set; } // Kalzium in Milligramm
            public double NfIron { get; set; } // Eisen in Milligramm
            public double NfPotassium { get; set; } // Kalium in Milligramm
        }
    }

}
