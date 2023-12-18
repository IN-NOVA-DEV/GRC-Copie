using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using grc_copie.Data;
using grc_copie.Models;

namespace grc_copie.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    public class ApiSchedulesController : Controller
    {
        private GRC_Context _context;

        public ApiSchedulesController(GRC_Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var schedules = _context.Schedules.Select(i => new
            {
                i.Id,
                i.EmailOwner,
                i.CreatedDate,
                i.StartAt,
                i.EndAt,
                i.TaskTitle
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(schedules, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values)
        {
            var model = new Schedule();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Schedules.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.Id });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {
            var model = await _context.Schedules.FirstOrDefaultAsync(item => item.Id == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key)
        {
            var model = await _context.Schedules.FirstOrDefaultAsync(item => item.Id == key);

            _context.Schedules.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Schedule model, IDictionary values)
        {
            string ID = nameof(Schedule.Id);
            string EMAIL_OWNER = nameof(Schedule.EmailOwner);
            string CREATED_DATE = nameof(Schedule.CreatedDate);
            string START_AT = nameof(Schedule.StartAt);
            string END_AT = nameof(Schedule.EndAt);
            string TASK_TITLE = nameof(Schedule.TaskTitle);

            if (values.Contains(ID))
            {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if (values.Contains(EMAIL_OWNER))
            {
                model.EmailOwner = Convert.ToString(values[EMAIL_OWNER]);
            }

            if (values.Contains(CREATED_DATE))
            {
                model.CreatedDate = Convert.ToDateTime(values[CREATED_DATE]);
            }

            if (values.Contains(START_AT))
            {
                model.StartAt = Convert.ToDateTime(values[START_AT]);
            }

            if (values.Contains(END_AT))
            {
                model.EndAt = Convert.ToDateTime(values[END_AT]);
            }

            if (values.Contains(TASK_TITLE))
            {
                model.TaskTitle = Convert.ToString(values[TASK_TITLE]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return string.Join(" ", messages);
        }
    }
}