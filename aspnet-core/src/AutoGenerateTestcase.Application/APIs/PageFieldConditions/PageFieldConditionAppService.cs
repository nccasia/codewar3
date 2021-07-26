using AutoGenerateTestcase.APIs.PageFieldConditions.Dto;
using AutoGenerateTestcase.APIs.PageFields;
using AutoGenerateTestcase.APIs.PageFields.Dto;
using AutoGenerateTestcase.Entities;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NccCore.Extension;
using NccCore.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGenerateTestcase.APIs.PageFieldConditions
{
    public class PageFieldConditionAppService : AutoGenerateTestcaseAppServiceBase
    {
        private readonly PageFieldAppService _pageFieldAppService;
        public PageFieldConditionAppService(PageFieldAppService pageFieldAppService)
        {
            _pageFieldAppService = pageFieldAppService;
        }
        public async Task<List<GetPageFieldConditionDto>> GetAllNonLogicByPageId(long pageId)
        {
            var listPageField = WorkScope.GetAll<PageField>().Where(x => x.RequestPageId == pageId);
            var rs = WorkScope.GetAll<PageFieldCondition>()
                .Where(x => x.Type != Constants.Enum.PageFieldConditionType.Logic)
                .Where(x => listPageField.Select(l => l.Id).Contains(x.PageFieldId))
                .Select(x => new GetPageFieldConditionDto
                {
                    Id = x.Id,
                    DependPageFieldId = x.DependPageFieldId,
                    DependPageFieldName = !x.DependPageFieldId.HasValue ? "" : x.DependPageField.Name,
                    Description = x.Description,
                    PageFieldId = x.PageFieldId,
                    PageFieldName = x.PageField.Name,
                    Type = x.Type.ToString()
                });

            return await rs.ToListAsync();
        }
        public async Task<List<GetPageFieldConditionDto>> GetAllLogicByPageId(long pageId)
        {
            var listPageField = WorkScope.GetAll<PageField>().Where(x => x.RequestPageId == pageId);
            var rs = WorkScope.GetAll<PageFieldCondition>()
                .Where(x => x.Type == Constants.Enum.PageFieldConditionType.Logic)
                .Where(x => listPageField.Select(l => l.Id).Contains(x.PageFieldId))
                .Select(x => new GetPageFieldConditionDto
                {
                    Id = x.Id,
                    DependPageFieldId = x.DependPageFieldId,
                    DependPageFieldName = !x.DependPageFieldId.HasValue ? "" : x.DependPageField.Name,
                    Description = x.Description,
                    PageFieldId = x.PageFieldId,
                    PageFieldName = x.PageField.Name,
                    Type = x.Type.ToString()
                });

            return await rs.ToListAsync();
        }

        public async Task<PageFieldConditionDto> Create(PageFieldConditionDto input)
        {
            input.Id = await WorkScope.InsertAndGetIdAsync(ObjectMapper.Map<PageFieldCondition>(input));
            return input;
        }

        public async Task Delete(long pageFieldConditionId)
        {
            await WorkScope.DeleteAsync<PageFieldCondition>(pageFieldConditionId);
        }

        public async Task<PageFieldConditionDto> Update(PageFieldConditionDto input)
        {
            var pageFieldCondition = await WorkScope.GetAsync<PageFieldCondition>(input.Id);
            ObjectMapper.Map(input, pageFieldCondition);
            await WorkScope.UpdateAsync(pageFieldCondition);
            return input;
        }

        public async Task<FileInfoDto> ExportExcel(long pageId)
        {
            var getAllByPageId = await _pageFieldAppService.GetAllByPageId(pageId);
            var getAllNonLogicByPageId = await GetAllNonLogicByPageId(pageId);
            var getAllLogicByPageId = await GetAllLogicByPageId(pageId);
            var currentRow = 0;

            using (var workbook = new XLWorkbook())
            {
                currentRow++;
                var worksheet = workbook.Worksheets.Add("Input");
                worksheet.Cell(currentRow, 1).Value = "PART A";
                worksheet.Cell(currentRow, 2).Value = "Form input Design";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Field";
                worksheet.Cell(currentRow, 2).Value = "Type";
                worksheet.Cell(currentRow, 3).Value = "Min value";
                worksheet.Cell(currentRow, 4).Value = "Max value";
                worksheet.Cell(currentRow, 5).Value = "Allow null";
                
                foreach (var i in getAllByPageId)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = i.Name;
                    worksheet.Cell(currentRow, 2).Value = i.Type.ToString();
                    worksheet.Cell(currentRow, 3).Value = i.MinValue;
                    worksheet.Cell(currentRow, 4).Value = i.MaxValue;
                    worksheet.Cell(currentRow, 5).Value = i.Nullable ? "Yes" : "False";
                }
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Forminput.END";
                worksheet.Cell(currentRow, 2).Value = " (Don't touch this row). You still can insert row above this row.";

                // bảng 2
                currentRow++;
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "PART B";
                worksheet.Cell(currentRow, 2).Value = "Relationship between input";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Field 1";
                worksheet.Cell(currentRow, 2).Value = "Relation";
                worksheet.Cell(currentRow, 3).Value = "Field 2";
                
                foreach(var i in getAllNonLogicByPageId)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = i.PageFieldName;
                    worksheet.Cell(currentRow, 2).Value = i.Type.ToString();
                    worksheet.Cell(currentRow, 3).Value = i.DependPageFieldName;
                }
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Relationship.END";
                worksheet.Cell(currentRow, 2).Value = " (Don't touch this row). You still can insert row above this row.";

                // bảng 3
                currentRow++;
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "PART A";
                worksheet.Cell(currentRow, 2).Value = "Form input Design";
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Field";
                worksheet.Cell(currentRow, 2).Value = "Type";
                worksheet.Cell(currentRow, 3).Value = "Min value";
                worksheet.Cell(currentRow, 4).Value = "Max value";
                worksheet.Cell(currentRow, 5).Value = "Allow null";

                foreach(var i in getAllLogicByPageId)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = i.PageFieldName;
                    worksheet.Cell(currentRow, 2).Value = i.Description;
                }

                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Logic.END";
                worksheet.Cell(currentRow, 2).Value = " (Don't touch this row). You still can insert row above this row.";

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    var now = DateTime.Now;
                    var name = now.Year.ToString() + "_" + now.Month.ToString() + "_" + "PageDetail";
                    return new FileInfoDto
                    {
                        Name = name,
                        Data = content
                    };
                }
            }
        }
    }
}
