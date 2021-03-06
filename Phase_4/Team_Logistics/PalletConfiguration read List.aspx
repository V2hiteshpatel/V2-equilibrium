<%@ Page language="C#" MasterPageFile="~masterurl/default.master"    Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage,Microsoft.SharePoint,Version=15.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document" meta:webpartpageexpansion="full"  %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Import Namespace="Microsoft.SharePoint" %> <%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="ApplicationPages" Namespace="Microsoft.SharePoint.ApplicationPages.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
	<SharePoint:ListProperty Property="TitleOrFolder" runat="server"/> - 
	<SharePoint:ListProperty Property="CurrentViewTitle" runat="server"/></asp:Content>
<asp:content contentplaceholderid="PlaceHolderAdditionalPageHead" runat="server">
	<SharePoint:RssLink runat="server"/>
</asp:content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageImage" runat="server">
	<SharePoint:ViewIcon Width="145" Height="54" runat="server"/></asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderLeftActions" runat="server">
<SharePoint:RecentChangesMenu runat="server" id="RecentChanges"/>
<SharePoint:ModifySettingsLink runat="server"/>
</asp:Content>
<asp:Content ContentPlaceHolderId ="PlaceHolderBodyLeftBorder" runat="server">
	<div height="100%" class="ms-pagemargin"><img src="/_layouts/15/images/blank.gif?rev=23" width='6' height='1' alt="" /></div>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">
		<WebPartPages:WebPartZone runat="server" FrameType="None" ID="Main" Title="loc:Main"><ZoneTemplate>
<WebPartPages:ContentEditorWebPart runat="server" __MarkupType="xmlmarkup" WebPart="true" __WebPartId="{D0EF94B9-7C56-486E-ACE6-62E61327B061}" >
<WebPart xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns="http://schemas.microsoft.com/WebPart/v2">
  <Title>Content Editor</Title>
  <FrameType>Default</FrameType>
  <Description>Allows authors to enter rich text content.</Description>
  <IsIncluded>true</IsIncluded>
  <PartOrder>2</PartOrder>
  <FrameState>Normal</FrameState>
  <Height />
  <Width />
  <AllowRemove>true</AllowRemove>
  <AllowZoneChange>true</AllowZoneChange>
  <AllowMinimize>true</AllowMinimize>
  <AllowConnect>true</AllowConnect>
  <AllowEdit>true</AllowEdit>
  <AllowHide>true</AllowHide>
  <IsVisible>true</IsVisible>
  <DetailLink />
  <HelpLink />
  <HelpMode>Modeless</HelpMode>
  <Dir>Default</Dir>
  <PartImageSmall />
  <MissingAssembly>Cannot import this Web Part.</MissingAssembly>
  <PartImageLarge>/_layouts/15/images/mscontl.gif</PartImageLarge>
  <IsIncludedFilter />
  <ExportControlledProperties>true</ExportControlledProperties>
  <ConnectionID>00000000-0000-0000-0000-000000000000</ConnectionID>
  <ID>g_d0ef94b9_7c56_486e_ace6_62e61327b061</ID>
  <ContentLink xmlns="http://schemas.microsoft.com/WebPart/v2/ContentEditor" />
  <Content xmlns="http://schemas.microsoft.com/WebPart/v2/ContentEditor"><![CDATA[<p style="color: #4f2c1d;">
   <style>
   
#Ribbon\.ListItem\.New\.NewListItem-Large{
	display: none;
}
   #idHomePageNewItem
   {
   	display: none;
   }
   </style> 
   <script type="text/javascript"> </script>
   <span class="ms-rteFontSize-4">
      <strong> </strong></span></p> 
<table>
   <tbody>
      <tr>
         <td align="right">Stock Code :&nbsp; </td> 
         <td style="width: 179px;">​<input name="StockCode" id="txtStockCode" type="text"/></td>
      </tr> 
      <tr>
         <td align="right">Description :&nbsp;</td> 
         <td style="width: 179px;">
            <input name="Description" id="txtDescription" type="text"/>          
         </td>
      </tr> 
      <tr>
         <td align="right">Product Class :&nbsp; 
            <br/></td> 
         <td style="width: 179px;">
            <input name="ProductClass" id="txtProductClass" type="text"/>
         </td>
      </tr> 
      <tr>
         <td>
         </td> 
         <td align="right" style="width: 179px;">
            <input name="Button1" onclick="ApplySearchFilters();" type="button" value="Search"/>
         </td>
      </tr>
   </tbody>
</table> 
<p> &nbsp;</p>]]></Content>
  <PartStorage xmlns="http://schemas.microsoft.com/WebPart/v2/ContentEditor" />
</WebPart>
</WebPartPages:ContentEditorWebPart>
<WebPartPages:XsltListViewWebPart runat="server" ViewFlag="" ViewSelectorFetchAsync="False" InplaceSearchEnabled="False" ServerRender="False" ClientRender="True" InitialAsyncDataFetch="False" WebId="00000000-0000-0000-0000-000000000000" IsClientRender="False" GhostedXslLink="main.xsl" NoDefaultStyle="" ViewGuid="{14CFC51D-286D-4F00-A098-300FD3A9F531}" EnableOriginalValue="False" DisplayName="Pallet Configurations Read List" ViewContentTypeId="0x" Default="TRUE" ListUrl="" ListDisplayName="" ListName="{70B458A2-724B-4727-B128-90568186053F}" ListId="70b458a2-724b-4727-b128-90568186053f" PageType="PAGE_DEFAULTVIEW" PageSize="-1" UseSQLDataSourcePaging="True" DataSourceID="" ShowWithSampleData="False" AsyncRefresh="False" ManualRefresh="False" AutoRefresh="False" AutoRefreshInterval="60" Title="Pallet Configurations" FrameType="None" SuppressWebPartChrome="False" Description="" IsIncluded="True" ZoneID="Main" PartOrder="4" FrameState="Normal" AllowRemove="True" AllowZoneChange="True" AllowMinimize="True" AllowConnect="True" AllowEdit="True" AllowHide="True" IsVisible="True" CatalogIconImageUrl="/_layouts/15/images/itebl.png?rev=23" TitleUrl="/Team/Logistics/Lists/Pallet Configurations" DetailLink="/Team/Logistics/Lists/Pallet Configurations" HelpLink="" HelpMode="Modeless" Dir="Default" PartImageSmall="" MissingAssembly="Cannot import this Web Part." PartImageLarge="/_layouts/15/images/itebl.png?rev=23" IsIncludedFilter="" ExportControlledProperties="False" ConnectionID="00000000-0000-0000-0000-000000000000" ID="g_14cfc51d_286d_4f00_a098_300fd3a9f531" ChromeType="None" ExportMode="NonSensitiveData" __MarkupType="vsattributemarkup" __WebPartId="{14CFC51D-286D-4F00-A098-300FD3A9F531}" __AllowXSLTEditing="true" __designer:CustomXsl="fldtypes_Ratings.xsl" WebPart="true" Height="" Width=""><ParameterBindings>




			<ParameterBinding Name="dvt_sortdir" Location="Postback;Connection"/>
			<ParameterBinding Name="dvt_sortfield" Location="Postback;Connection"/>
			<ParameterBinding Name="dvt_startposition" Location="Postback" DefaultValue=""/>
			<ParameterBinding Name="dvt_firstrow" Location="Postback;Connection"/>
			<ParameterBinding Name="OpenMenuKeyAccessible" Location="Resource(wss,OpenMenuKeyAccessible)"/>
			<ParameterBinding Name="open_menu" Location="Resource(wss,open_menu)"/>
			<ParameterBinding Name="select_deselect_all" Location="Resource(wss,select_deselect_all)"/>
			<ParameterBinding Name="idPresEnabled" Location="Resource(wss,idPresEnabled)"/>
			<ParameterBinding Name="NoAnnouncements" Location="Resource(wss,noXinviewofY_LIST)"/>
			<ParameterBinding Name="NoAnnouncementsHowTo" Location="Resource(wss,noXinviewofY_DEFAULT)"/>
			<ParameterBinding Name="FltStockCode" Location="QueryString(FltStockCode)" DefaultValue="**"/>
			<ParameterBinding Name="FltDesc" Location="QueryString(FltDesc)" DefaultValue="**"/>
			<ParameterBinding Name="FltProdClass" Location="QueryString(FltProdClass)" DefaultValue="**"/>
		</ParameterBindings>
<DataFields>
</DataFields>
<XmlDefinition>
<View Name="{14CFC51D-286D-4F00-A098-300FD3A9F531}" DefaultView="TRUE" MobileView="TRUE" MobileDefaultView="TRUE" Type="HTML" DisplayName="Pallet Configurations Read List" Url="/Team/Logistics/Lists/Pallet Configurations/Read List.aspx" Level="1" BaseViewID="1" ContentTypeID="0x" ImageUrl="/_layouts/15/images/generic.png?rev=23" ><Method Name="Read List"><Filter Name="StockCode" Value="*{FltStockCode}*"/><Filter Name="Description" Value="*{FltDesc}*"/><Filter Name="ProductClass" Value="*{FltProdClass}*"/></Method><Query><OrderBy><FieldRef Name="PalletConfigSK"/></OrderBy></Query><ViewFields><FieldRef Name="PalletConfigSK" ListItemMenu="TRUE" LinkToItem="TRUE"/><FieldRef Name="StockCode"/><FieldRef Name="Description"/><FieldRef Name="ProductClass"/><FieldRef Name="ProductClassDesc"/><FieldRef Name="PalletType"/><FieldRef Name="UOM"/><FieldRef Name="Factor"/><FieldRef Name="Block"/><FieldRef Name="Tiers"/><FieldRef Name="TotalCA"/><FieldRef Name="TotalEA"/><FieldRef Name="WeightEA"/><FieldRef Name="WeightPL"/><FieldRef Name="WeightUOM"/><FieldRef Name="HeightPL"/><FieldRef Name="LinearUOM"/></ViewFields><RowLimit Paged="TRUE">30</RowLimit><Aggregations Value="Off"/><JSLink>clienttemplates.js</JSLink><XslLink Default="TRUE">main.xsl</XslLink><Toolbar Type="Standard"/></View></XmlDefinition>
</WebPartPages:XsltListViewWebPart>

</ZoneTemplate></WebPartPages:WebPartZone>
<script type="text/javascript">
  

function ApplySearchFilters()
{
     try
     {
       var accessNumber = document.getElementById("txtStockCode").value;
       var Desc = document.getElementById("txtDescription").value;
       var ProdClass= document.getElementById("txtProductClass").value;
       var url = window.location.href;
      // debugger;
         if(url.indexOf("?") > 0)
         {       url = url.split("?")[0];
         }
 
      /*   if(accessNumber.length > 0)
         {      // url = url + "?FltStockCode=" + accessNumber +"#InplviewHash14cfc51d-286d-4f00-a098-300fd3a9f531=FltStockCode%3D"+accessNumber.replace('-','--') ;
          url = url+"?FltStockCode=" + accessNumber +"#InplviewHash14cfc51d-286d-4f00-a098-300fd3a9f531=" +encodeURIComponent( "FltStockCode=" + accessNumber.replace('-','--') );
         }
         if(Desc.length > 0)
         {
         if(accessNumber.length == 0)
         {
         url = url+"?FltDesc=" + Desc+"#InplviewHash14cfc51d-286d-4f00-a098-300fd3a9f531=" +encodeURIComponent( "FltDesc=" + Desc);
         }
         else
         {
         url = url+"?FltStockCode=" + accessNumber +"&FltDesc="+Desc+"#InplviewHash14cfc51d-286d-4f00-a098-300fd3a9f531=" +encodeURIComponent( "FltStockCode=" + accessNumber.replace('-','--') +"-FltDesc="+Desc);
         }         
         }*/
 
        // alert(url);
        var NorUrl="";
        var encUrl="";
        
       if(accessNumber.length > 0)
       {
       NorUrl="?FltStockCode="+ accessNumber;
       encUrl="FltStockCode="+accessNumber.replace('-','--');
       }
       if(Desc.length > 0)
       {
       NorUrl=NorUrl+((NorUrl.length>0)?"&":"?");
       NorUrl=NorUrl+"FltDesc="+ Desc;
       encUrl=encUrl+((encUrl.length>0)?"-":"");
       encUrl=encUrl+"FltDesc="+Desc;
       }
       if(ProdClass.length > 0)
       {
        NorUrl=NorUrl+((NorUrl.length>0)?"&":"?");
        NorUrl=NorUrl+"FltProdClass="+ ProdClass;
        encUrl=encUrl+((encUrl.length>0)?"-":"");
        encUrl=encUrl+"FltProdClass="+ProdClass;
       }
        
        if(NorUrl.length>0)
        {
        url=url+NorUrl+"#InplviewHash14cfc51d-286d-4f00-a098-300fd3a9f531="+encodeURIComponent(encUrl);
        }
       // alert(url);
         window.location.href = url;

 
     }
     catch(ex)
     {}
 }
 
 function GetQueryStringValue(variable)
 {      var qs = location.search.substring(1, location.search.length);
 
     var args = qs.split("&");
     var vals = new Object();
 
     for (var i=0; i < args.length; i++)      {      var nameVal = args[i].split("=");
         var temp = unescape(nameVal[1]).split('+');
         nameVal[1] = temp.join(' ');
         vals[nameVal[0]] = nameVal[1];
     }
 
     return vals[variable];
  }
 
  function SetQueryStringValues()
  {
      try
      {
          var url = window.location.href;
          if(url.indexOf("FltStockCode") > 0)
          {
             var accessNumber = GetQueryStringValue("FltStockCode");
             //alert(accessNumber);
             document.getElementById("txtStockCode").value = accessNumber;
          }
          if(url.indexOf("FltDesc") > 0)
          {
             var accessNumber = GetQueryStringValue("FltDesc");
             //alert(accessNumber);
             document.getElementById("txtDescription").value = accessNumber;
          }
           if(url.indexOf("FltProdClass") > 0)
          {
             var accessNumber = GetQueryStringValue("FltProdClass");
             //alert(accessNumber);
             document.getElementById("txtProductClass").value = accessNumber;
          }


      }
      catch(ex)
      {}
  }
 
  setTimeout("SetQueryStringValues();",100);
   
  </script>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderPageDescription" runat="server">
<SharePoint:ListProperty CssClass="ms-listdescription" Property="Description" runat="server"/>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderCalendarNavigator" runat="server">
	<SharePoint:SPCalendarNavigator id="CalendarNavigatorId" runat="server"/>
  <ApplicationPages:CalendarAggregationPanel id="AggregationPanel" runat="server"/>
</asp:Content>
