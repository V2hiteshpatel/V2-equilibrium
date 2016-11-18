<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Migrate.aspx.cs" Inherits="FileMigration.Layouts.FileMigration.Migrate" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
 <asp:Label ID="PageTitleLabel" runat="server" Font-Bold="true"></asp:Label>
    <br />
    <br />
    <table>
        <tr>
            <td colspan="3"> Kindly Upload the document in temporary "Artwork Document" library Using "Open With Explorer" before starting the process.</td>
        </tr>
        <tr>
            <td colspan="3"> <a href="https://stg-sp-04.etbrowne.com/Artwork%20Document/Forms/AllItems.aspx" target="_blank">Click hear </a> to go to temporary "Artwork Document" library. <br /><br /></td>
        </tr>
         <tr>
            <td colspan="3"> <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label> <br /><br /></td>
        </tr>
        <tr>
            <td>File</td>
            <td> : </td>
            <td>
                <asp:FileUpload ID="MetaFile" runat="server"/>
                    </td>
        </tr>

        <%--<tr>
            <td>File Location</td>
            <td> : </td>
            <td>
                <asp:TextBox ID="LocationTextBox" runat="server"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
             <td ></td>
            <td colspan="2" align="left">
                <br />
                <asp:Button ID="MetaFileUpload" runat="server" Text="Meta File Upload" OnClick="MetaFileUploadButton_Click" Visible="true" />
            </td>
           
        </tr>
        <tr>
            <td colspan="3"> Click on the view for the reports. <br /><br /></td>
        </tr>
         <tr>
             <td colspan="3"> <table><tr>
            <td style="width:50%;" ><a href="https://stg-sp-04.etbrowne.com/Lists/Artwork%20Excel/Not%20Upload.aspx" target="_blank">Not found files name </a> <br /><br /></td>
             <td align="right"><a href="https://stg-sp-04.etbrowne.com/Artwork%20Document/Forms/AllItems.aspx" target="_blank">No Metadata </a> <br /><br /></td>
                 </tr> </table> </td>
        </tr>
       <%-- <tr>
            <td colspan="3" align="right">
                <asp:Button ID="UploadLocalFile" runat="server" Text="Upload Local Disk File" OnClick="UploadLocalFile_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="right">
                <asp:Button ID="UpdateButton" runat="server" Text="Update Contacts" OnClick="UpdateButton_Click" />
            </td>
        </tr>--%>
    </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Application Page
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >

</asp:Content>
