<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DefaultMap.aspx.cs" Inherits="BingMapTest_DefaultMap" %>


<%@ Register src="../UserControls/MyAjaxMapControl.ascx" tagname="MyAjaxMapControl" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="700px" Width="600px">
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:TextBox ID="textInput" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID="Geocode" runat="server" Text="Geocode" OnClick="Geocode_Click" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="ReverseGeocode" runat="server" Text="Reverse Geocode" OnClick="ReverseGeocode_Click" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="Search" runat="server" Text="Search" OnClick="Search_Click" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="RequestImage" runat="server" Text="Request Image" OnClick="RequestImage_Click" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="RequestMetadata" runat="server" Text="Request Metadata" OnClick="RequestMetadata_Click" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="Route" runat="server" Text="Route" OnClick="Route_Click" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID="MajorRoutes" runat="server" Text="Major Routes" OnClick="MajorRoutes_Click" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="labelResults" runat="server" Text="Label"></asp:Label>
                    <asp:Image ID="imageResults" runat="server" Visible="false" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
</asp:Content>

