using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;


namespace ArtWork_Event_Handler.EventReceiver1
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiver1 : SPItemEventReceiver
    {
        /// <summary>
        /// An item is being added.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);
            if (properties.ListTitle != "Documents")
            {
                return;
            }
            else
            {
                //if (properties.BeforeProperties["Product"] == null)
                //{
                //    properties.Cancel = true;
                //    properties.ErrorMessage = "Document cannot be added.";
                //}
                //--InProgress
                if ((!properties.AfterUrl.EndsWith("pdf")) && (!properties.AfterUrl.EndsWith("Ai")))
                {
                    properties.ErrorMessage = "You are allowed to upload only PDF and AI Files!";
                    properties.Status = SPEventReceiverStatus.CancelWithError;
                    properties.Cancel = true;
                }
                //else
                //{
                //    if (properties.AfterProperties["Current_Status"] != null && properties.AfterProperties["Current_Status"].ToString() == "In Progress")
                //    {
                //        properties.ErrorMessage = "Workflow is In Progress, Document cannot be modified.";
                //        properties.Status = SPEventReceiverStatus.CancelWithError;
                //        properties.Cancel = true;
                //    }
                //}
            }

        }

        /// <summary>
        /// An item is being updated.
        /// </summary>
        public override void ItemUpdating(SPItemEventProperties properties)
        {
            base.ItemUpdating(properties);

            if (properties.ListTitle != "Documents")
            {
                return;
            }

//            if (properties.BeforeProperties["Current status"] != null && properties.BeforeProperties["Current status"].ToString() == "In Progress")
            if (properties.BeforeProperties["Current_Status"] != null &&
                (properties.BeforeProperties["Current_Status"].ToString() == "In Progress") || (properties.BeforeProperties["Current_Status"].ToString() == "Legal Approved & RnD Approved") ||
                (properties.BeforeProperties["Current_Status"].ToString() == "PM Approved") || (properties.BeforeProperties["Current_Status"].ToString() == "RnD Approved") || (properties.BeforeProperties["Current_Status"].ToString() == "Legal Approved"))
            {
                if (properties.ListItem["Current_Status"] != null && properties.AfterProperties["Current_Status"] != null && (properties.ListItem["Current_Status"].ToString() == properties.AfterProperties["Current_Status"].ToString()))

                {
                    properties.ErrorMessage = "Workflow is In Progress, Document cannot be modified.";
                    properties.Status = SPEventReceiverStatus.CancelWithError;
                    properties.Cancel = true;
                }
            }
        }

        /// <summary>
        /// An item is being deleted.
        /// </summary>
        public override void ItemDeleting(SPItemEventProperties properties)
        {
            base.ItemDeleting(properties);
            if (properties.ListTitle != "Documents")
            {
                return;
            }
            //if (properties.AfterProperties["Current_Status"] != null && ((properties.AfterProperties["Current_Status"].ToString() == "In Progress") || (properties.AfterProperties["Current_Status"].ToString() == "Approved")))
            if (properties.ListItem["Current_Status"] != null && properties.ListItem["Current_Status"].ToString() == "In Progress")
            {
                properties.ErrorMessage = "Workflow is In Progress, Document cannot be deleted.";
                properties.Status = SPEventReceiverStatus.CancelWithError;
                properties.Cancel = true;
            }
        }

        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            if (properties.ListTitle != "Documents")
            {
                return;
            }
            //else

            //{
            //    base.ItemAdding(properties);
            //    properties.Cancel = true;
            //    properties.ErrorMessage= "Document cannot be added";
            //    base.ItemDeleting(properties);
            //}

        }

        /// <summary>
        /// An item was updated.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
            if (properties.ListTitle != "Documents")
            {
                return;
            }
            else
            {
                if (properties.AfterProperties["Current_Status"] != null && properties.AfterProperties["Current_Status"].ToString() == "Approved" 
                    && properties.ListItem["Approval Status"].ToString() != "16" && properties.ListItem["Approval Status"].ToString() != "0") 
                {

                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite site = new SPSite(properties.WebUrl))
                        {
                            site.AllowUnsafeUpdates = true;
                            using (SPWeb web = site.OpenWeb())
                            {
                                try
                                {
                                    web.AllowUnsafeUpdates = true;

                                    SPListItemCollection oList = web.Lists[properties.ListTitle].Items;

                                    SPListItem thisitem = oList.GetItemById(properties.ListItemId);

                                    SPUtility.ValidateFormDigest();

                                    //thisitem.File.Versions.DeleteAllMinorVersions();
                                    web.Update();

                                    SPModerationInformation ApprovalStatus = thisitem.ModerationInformation;
                                    ApprovalStatus.Status = SPModerationStatusType.Approved;
                                    thisitem.Update();

                                   web.AllowUnsafeUpdates = false;
                                }

                                catch (Exception ex)
                                {
                                    throw new SPException("Error: " + ex.Message);

                                }
                            }
                        }
                    });
                }
            }
            //else
            //{
            //    try
            //    {
            //        SPSite site = new SPSite(properties.WebUrl);
            //        SPWeb web = site.OpenWeb();
            //        SPDocumentLibrary mylib = (SPDocumentLibrary)web.Lists["Documents"];
            //        //foreach(SPListItem folder in mylib.Folders)
            //        //    {
            //        //        deleteVersions(SPFolder folder);
            //        //    }
            //        SPListItemCollection oList = web.Lists[properties.ListTitle].Items;

            //        SPListItem thisitem = oList.GetItemById(properties.ListItemId);
            //        foreach (SPListItem doc in oList.Items)
            //             {
            //                 SPListItemVersionCollection coll = doc.Versions;
            //                 foreach (SPListItemVersion version in coll)
            //                 {
            //                     Console.Writeline('VersionLabel: ' + version.VersionLabel + ' IsCurrentVersion: ' + version.IsCurrentVersion )
            //                 }
            //             };
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new SPException(ex.Message);
            //    }

            //}


        }

        //protected void deleteVersions (SPFolder folder)
        //    {
        //        for (int i = 0; i < folder.Files.Count; i++)
        //        {
        //            SPFile file = folder.Files[i];
        //            int counter = file.Versions.Count;
        //            for (int j = 0; j < counter – 1; j++);
        //            {
        //                if (file.Versions[0] != null)
        //                {
        //                    file.Versions[0].Delete();
        //                }
        //            }
        //        }
        //    }
        /// <summary>
        /// An item was deleted.
        /// </summary>
        public override void ItemDeleted(SPItemEventProperties properties)
        {
            base.ItemDeleted(properties);
            if (properties.ListTitle != "Documents")
            {
                return;
            }
        }
    }
}
