public class UploadToDbInput
{
    public string Name { get; set; } = default!;
    public string ImgUrl { get; set; } = default!;
    public decimal? FritKlor { get; set; }
    public decimal? Bundklor { get; set; }
    public decimal? Differace { get; set; }
    public decimal? Ph { get; set; }
    public decimal? AutoFritKlor { get; set; }
    public decimal? AutoPh { get; set; }
    public DateTime? WaterSampleTime { get; set; }
}
