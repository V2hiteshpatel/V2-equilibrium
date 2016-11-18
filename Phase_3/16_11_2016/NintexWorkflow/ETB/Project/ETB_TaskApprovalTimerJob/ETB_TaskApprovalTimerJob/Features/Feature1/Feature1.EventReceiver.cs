using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace ETB_TaskApprovalTimerJob.Features.Feature1
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("d7a13421-2868-4f4d-813b-fa631cded04c")]
    public class Feature1EventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWebApplication webApp = properties.Feature.Parent as SPWebApplication;
            DeleteJob(webApp.JobDefinitions);

            ApprovalTask simplejob = new ApprovalTask(webApp);


            SPMinuteSchedule schedule = new SPMinuteSchedule();
            schedule.BeginSecond = 0;
            schedule.EndSecond = 59;
            schedule.Interval = 1;

            simplejob.Schedule = schedule;
            simplejob.Update();
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPWebApplication webApp = properties.Feature.Parent as SPWebApplication;
            DeleteJob(webApp.JobDefinitions);
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}

        private void DeleteJob(SPJobDefinitionCollection jobs)
        {
            foreach (SPJobDefinition job in jobs)
            {
                if (job.Name.Equals(ApprovalTask.JobName,
                StringComparison.OrdinalIgnoreCase))
                {
                    job.Delete();
                }
            }
        }
    }
}
