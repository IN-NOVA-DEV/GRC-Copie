using grc_copie.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace grc_copie.Controllers.Tools
{
    public class JobNameActionFilter : IActionFilter
    {
        private IHttpContextAccessor inside;
        public readonly GRC_Context _db;
        public static List<SimpleJob>? listeJob;

        public JobNameActionFilter(GRC_Context db, IHttpContextAccessor httpContextAccessor)
        {
            inside = httpContextAccessor;
            _db = db;
        }

        public List<SimpleJob> AllJob()
        {
            return _db.Jobs.Select(i => new SimpleJob { JobId = i.JobId, JobName = i.JobName }).ToList();
        }
        public class SimpleJob
        {
            public string JobName { get; set; }
            public int JobId { get; set; }
        }
        public static string? GetJobName(IHttpContextAccessor other)
        {

            return GetJobName(other.HttpContext);
        }
        public static void SetJobName(HttpContext other, string? jobName)
        {
            other.Session.SetString("JobName", (jobName == null) ? "Pas selectionner" : jobName);
        }
        public static string? GetJobName(HttpContext other)
        {

            return other.Session.GetString("JobName");
        }
        public static void SetJobID(HttpContext other, string newjobId, GRC_Context db)
        {
            other.Session.SetString("jobid", newjobId);
            string jobid = GetJobId(other);
            if (jobid != null)
            {
                int JobId = int.Parse(jobid);
               string? jobName = db.Jobs.Where(x => x.JobId == JobId).Select(x => x.JobName).FirstOrDefault();
                SetJobName(other, jobName);

            }
        }
        public static string GetJobId(IHttpContextAccessor other)
        {

            return GetJobId(other?.HttpContext);
        }
        public static string GetJobId(HttpContext? other)
        {
            return other?.Session.GetString("jobid");
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the "jobName" is set in the session
            var jobName = GetJobId(inside);
            listeJob = AllJob();
            if (string.IsNullOrEmpty(jobName))
            {

                SetJobName(inside.HttpContext, "Pas Selectionner");
                // If "jobName" is not set, set a default value

            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // No action needed after the action is executed
        }
    }
}
