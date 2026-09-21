namespace LusiTrack.Models;

public class MenuCategoryModel
{
    public string Key { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string Icon { get; set; } = "bi-tag";
    public string Emoji { get; set; } = "🍽️";
    public string AnchorId { get; set; } = string.Empty;
    public string BadgeClass { get; set; } = "bg-warning text-dark";
    public string CoverImageUrl { get; set; } = string.Empty;
    public string PriceRange { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}
