using CodeCapital.Bullhorn.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCapital.Bullhorn.Api
{
    public class FileAttachmentApi
    {
        private readonly BullhornApi _bullhornApi;

        public FileAttachmentApi(BullhornApi bullhornApi) => _bullhornApi = bullhornApi;

        public async Task<List<FileAttachmentDto>> GetAsync(string query)
        {
            var data = await _bullhornApi.QueryAsync<FileAttachmentDto>(query);

            return data;

            //var result = await _bullhornApi.ApiQueryAsync(query, BullhornApi.QueryCount);
            //var data = result.Data;

            //for (var i = result.Count; i < result.Total; i += result.Count)
            //{
            //    result = await _bullhornApi.ApiQueryAsync(query, BullhornApi.QueryCount, i);
            //    data.AddRange(result.Data);
            //}

            //return _bullhornApi.MapResults<FileAttachmentDto>(data);
        }

        //private async Task SynchroniseCandidatesAsync()
        //{
        //    var dateFrom = DateTime.Now.AddDays(-5800);
        //    var dateTo = DateTime.Now.AddDays(-3800);

        //    var query = $"Candidate?fields=id,firstName,lastName,email,email2,email3,mobile,phone,phone2,phone3,workPhone,status,dateAdded,owner,address&query=dateAdded:[{dateFrom:yyyyMMdd} TO {dateTo:yyyyMMdd}]";

        //    var candidates = await _bullhornApi.GetCandidatesAsync(query);

        //    var service = new CandidateService(_unitOfWork);

        //    await service.AddCandidatesAsync(candidates);
        //}

        //private async Task SynchroniseFileAttachmentsAsync()
        //{
        //    var dateFrom = DateTime.Now.AddDays(-100);
        //    var dateTo = DateTime.Now.AddDays(0);

        //    var query = $"CandidateFileAttachment?fields=id,contentSubType,contentType,dateAdded,fileExtension,fileSize,fileType,name,type,candidate(id)&where=id>0 AND isDeleted=false AND dateAdded> '{dateFrom.ToShortDateString()} 00:00:00' AND dateAdded <'{dateTo.ToShortDateString()} 23:59:59'&orderBy=dateAdded";

        //    var fileAttachment = new FileAttachmentApi(_bullhornApi);

        //    var entities = await fileAttachment.GetAsync(query);

        //    var service = new FileAttachmentService(_unitOfWork);

        //    await service.AddFileAttachmentsAsync(entities);
        //}
    }
}
