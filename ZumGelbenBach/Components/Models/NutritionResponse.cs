using System.Text.Json.Serialization;

public class NutritionResponse
{
    public List<CommonFood> Common { get; set; }
    public List<BrandedFood> Branded { get; set; }
}

public class CommonFood
{
    [JsonPropertyName("tag_id")]  // Falls die JSON das Attribut anders nennt
    public string TagId { get; set; }

    [JsonPropertyName("tag_name")]
    public string TagName { get; set; }

    [JsonPropertyName("serving_qty")]
    public double ServingQty { get; set; }

    [JsonPropertyName("serving_unit")]
    public string ServingUnit { get; set; }

    [JsonPropertyName("common_type")]
    public string CommonType { get; set; }

    [JsonPropertyName("food_name")]
    public string FoodName { get; set; }

    public Photo Photo { get; set; }

    public string Locale { get; set; }
}

public class BrandedFood
{
    [JsonPropertyName("serving_qty")]
    public double ServingQty { get; set; }

    public int Region { get; set; }

    [JsonPropertyName("nf_calories")]
    public double? NfCalories { get; set; }

    [JsonPropertyName("nix_brand_id")]
    public string NixBrandId { get; set; }

    public Photo Photo { get; set; }

    [JsonPropertyName("nix_item_id")]
    public string NixItemId { get; set; }

    [JsonPropertyName("food_name")]
    public string FoodName { get; set; }

    public int BrandType { get; set; }

    [JsonPropertyName("brand_name_item_name")]
    public string BrandNameItemName { get; set; }

    [JsonPropertyName("serving_unit")]
    public string ServingUnit { get; set; }

    [JsonPropertyName("brand_name")]
    public string BrandName { get; set; }

    public string Locale { get; set; }
}

public class Photo
{
    public string Thumb { get; set; }
    public string Highres { get; set; }
    public bool IsUserUploaded { get; set; }
}
