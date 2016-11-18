using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace ArtWork_Event_Handler.ErrorEventReceiver
{
    /// <summary>
    /// List Workflow Events
    /// </summary>
    public class ErrorEventReceiver : SPWorkflowEventReceiver
    {
        /// <summary>
        /// A workflow was completed.
        /// </summary>
        public override void WorkflowCompleted(SPWorkflowEventProperties properties)
        {
            base.WorkflowCompleted(properties);
            if (!properties.AssociationName.Contains("Artwork Review Workflow"))
            {
                return;
            }
            else
            {
                if (properties.CompletionType == SPWorkflowEventCompletionType.Errored || properties.CompletionType == SPWorkflowEventCompletionType.FailedOnStart || properties.CompletionType == SPWorkflowEventCompletionType.ExternallyTerminated)
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite site = new SPSite(properties.WebUrl))
                        {
                           
                            
                            using (SPWeb web = site.OpenWeb())
                            {
                                try
                                {

                                    site.AllowUnsafeUpdates = true;
                                    web.AllowUnsafeUpdates = true;

                                    SPListItemCollection oList = web.Lists[properties.ListId].Items;

                                    SPListItem thisitem = oList.GetItemById(properties.ItemId);

                                    SPUtility.ValidateFormDigest();
                                    SPWorkflowManager manager = site.WorkflowManager;

                                    foreach (SPWorkflow workflow in manager.GetItemActiveWorkflows(thisitem))
                                    {
                                        foreach (SPWorkflowTask t in workflow.Tasks)
                                        {
                                            t["Status"] = "Canceled"; t.Update();
                                        }
                                        SPWorkflowManager.CancelWorkflow(workflow);
                                    }

                                    string lstItemId = thisitem["SelectedItemId"].ToString();
                                    string[] lstItemArr = lstItemId.Split(';');
                                    for (int index = 0; index < lstItemArr.Length; )
                                    {

                                        SPListItem lothisitem = oList.GetItemById(int.Parse(lstItemArr[index].ToString().Replace('#', ' ').Trim()));
                                        SPDocumentLibrary docs = (SPDocumentLibrary)web.Lists["Artwork Library"];
                                        //SPFile file = docs.GetItemById(int.Parse(lstItemArr[index].ToString().Replace('#', ' ').Trim())).File;
                                        SPFile file = lothisitem.File;
                                        //if (lothisitem.File.CheckOutType != SPFile.SPCheckOutType.None)
                                        //{
                                        if (file.Level == SPFileLevel.Checkout )
                                        {
                                            if (file.CheckOutType != SPFile.SPCheckOutType.None)
                                            {
                                                this.EventFiringEnabled = false;

                                                // lothisitem["CurrentWorkflowStatus"] = "Not Started";
                                                lothisitem.File.CheckIn("Automatisk uppdatering av metataggar", SPCheckinType.OverwriteCheckIn);
                                                // lothisitem.File.ReleaseLock(lothisitem.File.LockId);

                                                using (SPSite siteCK = new SPSite(properties.WebUrl))
                                                {


                                                    using (SPWeb webCK = siteCK.OpenWeb())
                                                    {
                                                        try
                                                        {
                                                            SPListItemCollection oListCK = webCK.Lists[properties.ListId].Items;
                                                            SPListItem lothisitemCK = oListCK.GetItemById(int.Parse(lstItemArr[index].ToString().Replace('#', ' ').Trim()));
                                                            lothisitemCK["CurrentWorkflowStatus"] = "Not Started";
                                                            //lothisitem.Update();
                                                            lothisitemCK.SystemUpdate(false);
                                                        }
                                                        catch (Exception ex1)
                                                        { }
                                                    }
                                                }

                                                this.EventFiringEnabled = true;
                                            }
                                        }
                                        else
                                        {
                                            lothisitem["CurrentWorkflowStatus"] = "Not Started";
                                            lothisitem.Update();
                                        }
                                        //}
                                        //else
                                        //{
                                        //if (file.Level == SPFileLevel.Checkout || file.Level == SPFileLevel.Draft)
                                        //    if (file.CheckOutType != SPFile.SPCheckOutType.None)
                                        //    {
                                        //        file.CheckIn("Checked In", SPCheckinType.MinorCheckIn);

                                        //    }
                                                //if (lothisitem.File.Level == SPFileLevel.Checkout)
                                                //{
                                                //    lothisitem.File.UndoCheckOut();
                                                //}
                                                //if (lothisitem.File.Level != SPFileLevel.Checkout)
                                                //{
                                                //    lothisitem.File.CheckOut();
                                                //}
                                      //  lothisitem["CurrentWorkflowStatus"] = "Not Started";
                                      //  lothisitem.Update();
                                   // }
                                        index = index + 2;
                                    }

                                    if (lstItemArr.Length == 0)
                                    {
                                        thisitem["CurrentWorkflowStatus"] = "Not Started";
                                        thisitem.Update();
                                    }

                                   // web.Update();
                                   

                                    site.AllowUnsafeUpdates = false;
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
        }

       
      
             
    }
}