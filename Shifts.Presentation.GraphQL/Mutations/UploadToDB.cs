
using Shifts.Presentation.GraphQL.Payloads;

[ExtendObjectType("Mutation")]
public class UploadToDB
{
    public async Task<UploadToDBPayload> UploadToDBAsync(UploadToDbInput input, [Service] IExportToDB exportToDB)
    {
        UploadToDBPayload uploadPayload = new UploadToDBPayload
        {
            UploadedEmploee = await exportToDB.ExportWaterSamples(input.Name, input.ImgUrl, input.FritKlor, input.Bundklor, input.Differace, input.Ph, input.AutoFritKlor, input.AutoPh, input.WaterSampleTime)
        };
        return uploadPayload;
    }
}
