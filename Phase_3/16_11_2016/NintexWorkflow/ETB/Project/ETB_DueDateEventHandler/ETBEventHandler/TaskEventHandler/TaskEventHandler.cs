using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Linq;

namespace ETBEventHandler.TaskEventHandler
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class TaskEventHandler : SPItemEventReceiver
    {
        /// <summary>
        /// An item is being added.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);
           
        }

        /// <summary>
        /// An item is being updated.
        /// </summary>
        public override void ItemUpdating(SPItemEventProperties properties)
        {
            base.ItemUpdating(properties);
        }

        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            if (properties.ListTitle != "Workflow Tasks")
            {
                return;
            }
            else
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
                                /* //Start Get Artwork Workflow Data */

                                SPList list = web.Lists["Workflow Tasks"];
                                SPListItemCollection itemCol = list.GetItems();
                                int currentItem = Convert.ToInt32(itemCol.GetItemById(properties.ListItemId)["Workflow Item ID"]);
                               // currentItem = 243;
                                SPList listArtwork = web.Lists["Artwork Library"];
                                SPListItem currentArtItem = listArtwork.GetItemById(currentItem);
                                string[] workflowItemId = currentArtItem["SelectedItemId"].ToString().Split(';');
                                string stBuyer = currentArtItem["Buyer"].ToString();
                                string stArtboard = currentArtItem["Artboard Number"].ToString();
                                string stFileName = "";
                                for (int index = 0; index < (workflowItemId.Length - 1); )
                                {
                                    int subItemId = Convert.ToInt32(workflowItemId[index].ToString().Replace('#', ' ').Trim());
                                    stFileName = stFileName +","+ workflowItemId[index + 1].ToString().Replace('#', ' ').Trim();
                                    if (subItemId != Convert.ToInt32(currentArtItem["ID"].ToString()))
                                    {
                                        SPListItem subCurrentArtItem = listArtwork.GetItemById(subItemId);
                                        stBuyer = stBuyer + "," + subCurrentArtItem["Buyer"].ToString();
                                        stArtboard = stArtboard + "," + subCurrentArtItem["Artboard Number"].ToString();

                                    }
                                    index = index + 2;
                                }
                                properties.ListItem["Base Stock Code"] = currentArtItem["Base Stock Code"].ToString();
                                properties.ListItem["Name"] = stFileName.Length > 2? stFileName.Substring(1):stFileName;
                                properties.ListItem["Artboard Number"] = stArtboard;
                                properties.ListItem["Stock Code"] = currentArtItem["Stock Code"].ToString();
                                properties.ListItem["Artwork Description"] = currentArtItem["Description"].ToString();
                                properties.ListItem["Brand"] = currentArtItem["Brand"].ToString();
                                properties.ListItem["Segment"] = currentArtItem["Segment"].ToString();
                                properties.ListItem["Buyer"] = stBuyer;
                                properties.ListItem["Change Description History"] = currentArtItem["Change Description History"].ToString();

                                /* //END Get Artwork Workflow Data*/


                                SPList loHolidayLst = web.Lists["Holiday"];
                                DateTime loStartDate = DateTime.Parse(properties.ListItem["Start Date"].ToString());
                                
                            //    DateTime loToday = DateTime.Parse("06/22/2016");
                                string stStartDate = (SPUtility.CreateISO8601DateTimeFromSystemDateTime(Convert.ToDateTime(loStartDate)));
                                SPQuery query = new SPQuery();

                                query.Query = @"<OrderBy><FieldRef Name='HolidayDate' Ascending='True' /></OrderBy>";
                                SPListItemCollection itemcollection = loHolidayLst.GetItems(query);

                                DateTime dtDueDate = loStartDate;
                                for (int iCount = 0; iCount < 1; )
                                {
                                    dtDueDate = dtDueDate.AddDays(1);
                                    if (!IsWeekend(dtDueDate, itemcollection))
                                    {
                                        iCount++;
                                    }

                                }
                               string dtDu = dtDueDate.ToString();
                               if (properties.ListItem["Due Date"] == null)
                               {
                                   properties.ListItem["Due Date"] = dtDueDate;
                               }
                               properties.ListItem.Update();
                                //if (itemcollection != null)
                                //{
                                //    if (itemcollection.Count > 0)
                                //    {
                                //        SPListItem item = itemcollection[0];
                                //        string strNavigationTag = Convert.ToString(item["Year"]);
                                //        // taxonomyFieldValueCollection.PopulateFromLabelGuidPairs(strNavigationTag);
                                //    }
                                //}

                            }
                            catch (Exception ex)
                            {
                                throw new SPException("Error: " + ex.Message);

                            }
                        }
                        site.AllowUnsafeUpdates = false;
                    }
                });
            }
        }



        /// <summary>
        /// An item was updated.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            base.ItemUpdated(properties);
        }

        //private DateTime GetDueDate(SPListItemCollection foItemColl,DateTime foDtStartDate)
        //{
        //    DateTime dtDueDate = foDtStartDate;
        //    for (int iCount = 0; iCount < 2; )
        //    {
        //        dtDueDate = dtDueDate.AddDays(1);
        //        if (!IsWeekend(dtDueDate))
        //        {
        //            iCount++;
        //        }

        //    }
        //    Label1.Text = dtDueDate.ToString();

        //    //foreach (SPListItem item in itemcollection)
        //    //{
        //    //    string title = item["Title"].ToString();
        //    //}
        //}

        public bool IsWeekend(DateTime date, SPListItemCollection foItemColl)
        {
            bool IsWeekOff;
            IsWeekOff = new[] { DayOfWeek.Sunday, DayOfWeek.Saturday }.Contains(date.DayOfWeek);
            if (!IsWeekOff)
            {
                //IsWeekOff = foItemColl.Exists(a => a.HolidayDate == date);
                foreach (SPListItem item in foItemColl)
                {
                    if (DateTime.Parse(item["HolidayDate"].ToString()).ToShortDateString() == date.ToShortDateString())
                    {
                        if (!IsWeekOff)
                        {
                            IsWeekOff = true;
                            break;
                        }
                    }
                }
                return IsWeekOff;
            }
            return IsWeekOff;
        }
    }
}