using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Workflow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETB_TaskApprovalTimerJob
{
    public class ApprovalTask : SPJobDefinition
    {
        public const string JobName = "Approval Of Task";

        public ApprovalTask()
            : base()
        {

        }
        public ApprovalTask(SPWebApplication webApp) :
            base(JobName, webApp, null, SPJobLockType.Job)
        {
            Title = "Workflow Task Approval";
        }

        public override void Execute(Guid targetInstanceId)
        {

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite oSPsite = new SPSite("https://stg-sp-04.etbrowne.com/"))
                {
                    using (SPWeb oSPWeb = oSPsite.OpenWeb())
                    {
                        try
                        {
                            oSPWeb.AllowUnsafeUpdates = true;
                            var taskList = oSPWeb.Lists["Workflow Tasks"];
                            //SPList taskList = webApp.Sites[0].RootWeb.Lists["MtTestTask"];
                            SPQuery query = new SPQuery();

                            // LoginUserName = properties.UserDisplayName;

                            //query.Query = "<Where><Eq><FieldRef Name='Title' /></Eq></Where>";
                            query.Query = "<OrderBy><FieldRef Name='Title' Ascending='False' /></OrderBy>";

                            query.ViewAttributes = "Scope='RecursiveAll'";

                            SPListItemCollection items = taskList.GetItems(query);

                            DateTime dt = DateTime.Now;

                            foreach (SPListItem item in items)
                            {
                                if (item["Due Date"] != null && item["Outcome"] != null && item["Outcome"].ToString() == "Pending" && DateTime.Parse(item["Due Date"].ToString()) <= DateTime.Now.Date)
                                {
                                    //   SPListItem newTask = taskList.Items.Add();
                                    //   newTask["Title"] = DateTime.Now.ToString();
                                    //   newTask.Update();
                                    item["Outcome"] = "Approved";
                                    item["PercentComplete"] = "1";
                                    item["Status"] = "Completed";
                                    item["Modified"] = DateTime.Now;
                                    item.Update();
                                    return;
                                }
                            }
                            //End of for loop
                        }
                        catch (Exception ex)
                        {
                            throw new SPException("Error:" + ex.Message);
                        }
                    }
                }
            });




            //Commented by Bhavna from here
            //SPSecurity.RunWithElevatedPrivileges(delegate()
            //  {
            //      SPSite mysitecoll = new SPSite("https://stg-sp-04.etbrowne.com/");

            //      SPWeb web = mysitecoll.OpenWeb();
            //      try
            //      {
            //          SPList timesheets = web.Lists["MyTestTask"];
            //          // string listID = "";
            //          SPListItem item = timesheets.Items[0];
            //          SPWorkflowTask taskedit = null;
            //          SPWorkflowTask task = item.Tasks[0];

            //          taskedit = task;
            //          // alter the task

            //          Hashtable ht = new Hashtable();

            //          ht["Status"] = "Complete";

            //          ht["PercentComplete"] = 1.0f;

            //          ht["TaskStatus"] = "#";
            //          SPWorkflowTask.AlterTask((taskedit as SPListItem), ht, true);
            //      }


            //      catch (Exception ex)
            //      {
            //          //  MessageBox.Show(ex.InnerException.ToString());
            //      }

            //  });

            //Commented by Bhavna upto here

            //SPSecurity.RunWithElevatedPrivileges(delegate()
            //  {
            //      using (SPSite oSPsite = new SPSite("https://stg-sp-04.etbrowne.com/"))
            //      {
            //          using (SPWeb oSPWeb = oSPsite.OpenWeb())
            //          {
            //              // oSPWeb.AllowUnsafeUpdates = true;
            //              //EventFiringEnabled = false;
            //              var docLib = oSPWeb.Lists["MyTestTask"];
            //              SPQuery query = new SPQuery();
            //              query.Query = @"<OrderBy><FieldRef Name='Title' Ascending='True' /></OrderBy>";
            //              SPListItemCollection itemcollection = docLib.GetItems(query);
            //              //foreach (SPListItem item in itemcollection)
            //              //{
            //              //    SPWorkflowCollection workflows = item.Workflows;
            //              //    foreach (SPWorkflow workflow in workflows)
            //              //    {
            //              //        SPWorkflowTaskCollection tasks = workflow.Tasks;
            //              //        foreach (SPWorkflowTask task in tasks)
            //              //        {
            //              //            if (task[SPBuiltInFieldId.WorkflowVersion].ToString() != "1")
            //              //            {
            //              //                task[SPBuiltInFieldId.WorkflowVersion] = 1;
            //              //                task.SystemUpdate();
            //              //            }
            //              //        }
            //              //    }
            //              //}
            //              //   _site.AllowUnsafeUpdates = true;
            //              //   SPList _taskList = _web.Lists[spTaskListName];
            //              SPListItem _taskItem = docLib.GetItemById(21);
            //              if (_taskItem != null)
            //              {
            //                  oSPWeb.AllowUnsafeUpdates = true;
            //                  _taskItem["TaskOutcome"] = "Approved";
            //                  _taskItem["PercentComplete"] = "1";
            //                  _taskItem["Status"] = "Completed";
            //                  _taskItem.Update();
            //                  oSPWeb.AllowUnsafeUpdates = false;

            //              }

            //              //foreach (SPListItem item in itemcollection)
            //              //{
            //              //    if (int.Parse(item["ID"].ToString()) == 21)
            //              //    {
            //              //        item["Outcome"] = "Approved";
            //              //        item.Update();
            //              //    }
            //              //}
            //          }
            //      }
            //  });
        }
    }
}
