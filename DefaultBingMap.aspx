<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DefaultBingMap.aspx.cs" Inherits="BingMapTest_DefaultBingMap" %>

<%@ Register src="../UserControls/MyAjaxMapControl.ascx" tagname="MyAjaxMapControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Table runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="Button1" runat="server" Text="Button" onClick="Button1_click"/>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <uc1:MyAjaxMapControl ID="MyAjaxMapControl1" runat="server" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

